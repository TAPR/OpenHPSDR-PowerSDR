/*
 * JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
 * Copyright (C) 2006,2007,2009  Bill Tracey (bill@ejwt.com) (KD5TFD)
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

// ep4drain.c - this is code to drain ep4 continually and write it to the file ep4.dat 
//


// this is updated for 4 channels
#include <stdio.h>


#ifdef LINUX
#include <unistd.h>
#endif


#include "KD5TFD-VK6APH-Audio.h"
#include "private.h"

#ifdef EP4DRAIN_ENABLED
int ep4_keep_running = 0; 

// FILE *ofile = NULL;
char ep4buf[1024]; 

void EP4DrainThreadMainLoop(void) { 
	int numread; 
	fprintf(stderr, "EP4DrainThread main loop starts\n");  fflush(stderr); 
	while ( ep4_keep_running ) { 
		numread = OzyBulkRead(OzyH, 0x84, ep4buf, sizeof(ep4buf)); 
		if ( numread <= 0 )  {  /* read failed - bail out */ 
			fprintf(stderr, "OzyBulkRead on ep4 failed numread=%d\n", numread);  fflush(stderr); 
			ep4_keep_running = 0;
		} 
		if ( (numread > 0) && (EP4_DumpBufBytesUsed < EP4FRAMEDUMPLIMIT) ) { 
			int copy_count = min(numread, EP4FRAMEDUMPLIMIT - EP4_DumpBufBytesUsed);
			memcpy(EP4_DumpBuf + EP4_DumpBufBytesUsed, ep4buf, copy_count); 
			EP4_DumpBufBytesUsed += copy_count; 
		} 

		if ( EP4_DumpBufBytesUsed >= EP4FRAMEDUMPLIMIT ) { 
			ep4_keep_running = 0; 
		} 
#if 0 
		/* else */ 
		fwrite(ep4buf, 1, numread, ofile); 
#endif 
	} 
	fprintf(stderr, "EP4DrainThread main loop ends\n");  fflush(stderr); 
	return; 
} 


// this is the main thread that reads/writes data to/from the Xylo
// when this is called the Xylo device is open
// this routine does setup, calls IOThreadMainLoop to do the real work and cleans up on termination
void *EP4DrainThreadMain(void *argp) {
				
#ifndef LINUX
#if 0 
               SetThreadPriority(GetCurrentThread(),  /* THREAD_PRIORITY_ABOVE_NORMAL */   THREAD_PRIORITY_TIME_CRITICAL /* THREAD_PRIORITY_HIGHEST  */ );
#endif 
#else
#warning message("info - LINUX code missing ... set priority!")
#endif


				sem_post(&EP4DrainThreadInitSem); // tell launching thread we're rockin and rollin

                ep4_keep_running = 1;
                EP4DrainThreadRunning = 1;	            
				if ( !isMetis ) {         
					EP4DrainThreadMainLoop();
				}

                EP4DrainThreadRunning = 0;
				fprintf(stderr, "EP4 main thread dies\n");  fflush(stderr); 
                return NULL;

}



int EP4DrainThreadStop() {
        int rc;
        void *junk;

        if ( ep4_keep_running == 0 ) {  // not running
                return 1;
        }
        ep4_keep_running = 0;  // flag to stop
        rc = pthread_join(EP4DrainThreadID, &junk);
        if ( rc != 0 ) {
                fprintf(stderr, "Warning: ep4drain.c, pthread_join failed with rc=%d\n", rc);
                return 2;
        }
        return 0;
}
#endif