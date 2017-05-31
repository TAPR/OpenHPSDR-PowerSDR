/*
 * JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
 * Copyright (C) 2010 Bill Tracey (bill@ejwt.com) (KD5TFD)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 *
 */

//
// routines to manage a ring buffer 

#include <stdio.h> 
#include <stddef.h>
#include "private.h" 


typedef struct ringbuf { 
	sem_t wait_sem;         // semaphore for people waiting for item to be readable  
	pthread_mutex_t mutex;  // mutex protecting the data struct 	
	char *readp; 
	char *writep; 
	int size; 
	char data[1]; 
} RINGBUF; 


void *createRingBuffer(int size) { 
	RINGBUF *rb;
	
	rb = (RINGBUF *)malloc(sizeof(RINGBUF) + size); 
	if ( rb == NULL ) { 
		return NULL; 
	} 


	sem_init(&(rb->wait_sem),0,0); 
	pthread_mutex_init(&(rb->mutex), NULL); 
	rb->readp = rb->data;
	rb->writep = rb->data; 
	rb->size = size; 
	return rb; 
}



void destroyRingBuffer(void *p) { 	
	RINGBUF *rb = (RINGBUF *)p; 
	pthread_mutex_lock(&(rb->mutex));

	
	rb->readp = &(rb->data[0]); 
	rb->writep = rb->readp; 

	// suspect this term code is not quite correct! 
#ifndef LINUX 
	sem_post_multiple(&(rb->wait_sem), 0x7fff); // unblock anyone waiting 
#else 
	// hack -- not sure this term code is correct 
	sem_post(&(rb->wait_sem)); // unblock anyone waiting 
#endif 
	sched_yield(); // let other threads run 
	sem_destroy(&(rb->wait_sem)); 
	pthread_mutex_unlock(&(rb->mutex)); 
	pthread_mutex_destroy(&(rb->mutex)); 
	free(rb); 
	return;
} 

// returns < 0 for error, 0 for ok 
int resetRingBuffer(void *rbh) { 
	int rc; 
	RINGBUF *rb = (RINGBUF *)rbh; 

	rc = pthread_mutex_lock(&(rb->mutex));
	if ( rc  != 0 )  { 
		return -1; 
	}	
	rb->readp = rb->data; 
	rb->writep = rb->data; 
	pthread_mutex_unlock(&(rb->mutex)); 
	return 0; 
}


// returns <0 for error, number of bytes put otherwise 
// 
int putRingBuffer(void *rbh, char *datap, int dlen) { 
	int avail_no_wrap; 
	int rc; 
	int myrc; 
	int copycount; 
	int sem_val = 0; 
	RINGBUF *rb = (RINGBUF *)rbh; 

	rc = pthread_mutex_lock(&(rb->mutex));
	if ( rc  != 0 )  { 
		return -1; 
	}
	do { /* once */ 
		if ( dlen > rb->size ) {  /* too much data */ 
			myrc = -2; 
			break;
		}
		avail_no_wrap = rb->size - (int)( rb->writep - rb->data ); 
		copycount = min(dlen, avail_no_wrap); 
		memcpy(rb->writep, datap, copycount); 
		if ( copycount < dlen ) { /* more to copy */ 
			memcpy(rb->data, datap+copycount, dlen-copycount); 
			rb->writep = rb->data + (dlen-copycount); 
		} 
		else {
			rb->writep += copycount; 
		}
		myrc = dlen; 
		sem_getvalue(&(rb->wait_sem), &sem_val);
		if ( sem_val <= 0 ) { /* sem is not posted, post it */ 
			sem_post(&(rb->wait_sem));
		} 
	} while ( 0 ); 
	pthread_mutex_unlock(&(rb->mutex)); 

	return myrc; 
} 


// returns number of bytes returned, 0 if none avail, -1 for error 
int getRingBuffer(void *rbh, char *bufp, int dlen, int wait) { 	
	int rc; 
	int myrc = 0; 
	int copycount; 
	RINGBUF *rb = (RINGBUF *)rbh; 
	rc = pthread_mutex_lock(&(rb->mutex));
	if ( rc  != 0 )  { 
		return -1; 
	}
	do { /* once */ 
		if ( rb->readp == rb->writep ) {  /* no data */ 
			if ( wait ) { 
				pthread_mutex_unlock(&(rb->mutex));
				while ( 1  ) { 
					rc = sem_wait(&(rb->wait_sem)); 
					if ( rc != 0 ) { 
						myrc = -1; 
						break; 
					} 
					rc = pthread_mutex_lock(&(rb->mutex));
					if ( rc != 0 ) { 
						return -2; /* bail out hard - mutex hosed */ 
					} 
					if ( rb->readp != rb->writep ) {  /* we've got data - get out of wait loop */ 
						break;
					} 
					pthread_mutex_unlock(&(rb->mutex));
				} 
				if ( myrc < 0 ) { 
					break; 
				} 
			} 
			else {
				myrc = 0;
				break; 
			}
		} 
		/* once we get here we're inside the mutex and have data  */ 
		if ( rb->readp < rb->writep ) { /* easy case, read pointer trailing write pointer */ 
			myrc = min((int)(rb->writep - rb->readp), dlen); 
			memcpy(bufp, rb->readp, myrc); 
			rb->readp += myrc; 
		} 
		else { /* readp in front of writep, data is wrapped into end and start of buffer */ 
			myrc = min(dlen, rb->size - ((int)(rb->readp - rb->data)) + ((int)( rb->writep - rb->data )));
			copycount = min(dlen, rb->size - (int)( rb->readp - rb->data )); 
			memcpy(bufp, rb->readp, copycount); 
			if ( copycount < dlen ) { /* more data to copy */ 
				memcpy(bufp+copycount, rb->data, dlen-copycount); 
				rb->readp = rb->data + (dlen-copycount); 
			} 
			else { 
				rb->readp += copycount;
				if ( rb->readp >= ( rb->data + rb->size ) ) { 
					rb->readp = rb->data; 
				}
			} 
		} 
	}
	while ( 0 ); 
	pthread_mutex_unlock(&(rb->mutex)); 

	return myrc; 

}

