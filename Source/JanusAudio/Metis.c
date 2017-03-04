/*
 * Metis.c - Support code for OpenHPSDR.org's Metis board
 * Copyright (C) 2010, 2011 Bill Tracey, KD5TFD (bill@ewjt.com)
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
#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#include "private.h" 
#include <windows.h> 
#include <winsock2.h>
#include <ws2tcpip.h>
#include <wspiapi.h>
#include <Iphlpapi.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "IPHLPAPI.lib")

#define OBSOLETE (0) 

/* globals */
int MetisKeepRunning;
int MetisReadThreadRunning = 0;
sem_t MetisReadThreadInitSem;
#define NUM_RINGBUF_FRAMES (200)
void *MetisEP6RingBuf = NULL;
pthread_t MetisReadThreadID;
int MetisLastRecvSeq = 0;

extern void Dump(FILE *ofile,                /* file handle to dump to - assumed to be      */
	/* open.                                       */
	unsigned char *buf,            /* pointer to data to dump                     */
	unsigned int count,            /* number of bytes to dump                      */
	unsigned char *comment);      /* comment put out at top of dump, may be NULL */

/* returns 0 on success, non zero on failure */
int initWSA(void) {
	int rc;
	/* int addrs[10];  */
	if (WSAinitialized) {
		return 0;
	}
	WSAinitialized = 1;
	rc = WSAStartup(MAKEWORD(2, 2), &WSAdata);
	if (rc != 0) {
		printf("WSAStartup failed with rc=%d\n", rc);
	}
	/* getNetworkAddrs(addrs, 10);    */
	return rc;
}

int recvfrom_withtimeout(SOCKET s, char *buf, int buflen, int flags, struct sockaddr *fromsockp, int *fromlenp, long secs, long usecs) {
	struct timeval timeout;
	struct fd_set rdsocks;
	int rc;
	rdsocks.fd_count = 1;
	rdsocks.fd_array[0] = s;
	timeout.tv_sec = secs;
	timeout.tv_usec = usecs;

#if 0		
	rc = select(0, &rdsocks, NULL, NULL, &timeout); 
	if ( rc == 0 ) { 
		printf("select timed out\n"); 
		return 0; 
	} 
	if ( rc == SOCKET_ERROR ) { 
		printf("select failed.  WSAGetLAstError=%d\n", WSAGetLastError()); 
		return SOCKET_ERROR; 
	}
#endif 
	rc = recvfrom(s, buf, buflen, flags, (SOCKADDR *)fromsockp, fromlenp);
	return rc;
}

struct sockaddr_in MetisSockAddr;
int WSA_inited = 0;

KD5TFDVK6APHAUDIO_API void DeInitMetisSockets(void) {
	if (listenSock != (SOCKET)0) {
		shutdown(listenSock, SD_BOTH);
		closesocket(listenSock);
		listenSock = (SOCKET)0;
	}
}

/* returns 0 on success, != 0 otherwise */
KD5TFDVK6APHAUDIO_API int nativeInitMetis(char *netaddr) {
	DWORD dwRetVal;
	IPAddr DestIp = 0;
	IPAddr SrcIp = 0;       /* default for src ip */
	ULONG MacAddr[2];       /* for 6-byte hardware addresses */
	ULONG PhysAddrLen = 6;  /* default to length of six bytes */
	int rcv_timeo;
	int rc;
	int sndbufsize;
	//u_long addrs[10];
	isMetis = 1;

	if (!WSA_inited) {
		rc = initWSA();
		if (rc != 0) {
			return rc;
		}
		WSA_inited = 1;
		printf("initWSA ok!");
	}

	listenSock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

	if (listenSock == INVALID_SOCKET) {
		printf("createSocket Error: socket failed %ld\n", WSAGetLastError());
		WSACleanup();
		return INVALID_SOCKET;
	}

	MetisSockAddr.sin_family = AF_INET;
	MetisSockAddr.sin_port = htons(1024);
	MetisSockAddr.sin_addr.s_addr = inet_addr(netaddr);

	rcv_timeo = 500;
	rc = setsockopt(listenSock, SOL_SOCKET, SO_RCVTIMEO, (const char *)&rcv_timeo, sizeof(int));
	if (rc == SOCKET_ERROR) {
		printf("CreateSockets Warning: setsockopt SO_RCVTIMEO failed!\n");
	}

	fflush(stdout);

	sndbufsize = 0xffff;
	rc = setsockopt(listenSock, SOL_SOCKET, SO_SNDBUF, (const char *)&sndbufsize, sizeof(int));
	if (rc == SOCKET_ERROR) {
		printf("CreateSockets Warning: setsockopt SO_SNDBUF failed!\n");
	}

	sndbufsize = 0xffff;
	rc = setsockopt(listenSock, SOL_SOCKET, SO_RCVBUF, (const char *)&sndbufsize, sizeof(int));
	if (rc == SOCKET_ERROR) {
		printf("CreateSockets Warning: setsockopt SO_RCVBUF failed!\n");
	}

	DestIp = inet_addr(netaddr);

	if (DestIp != 0) {
		printf("metis_addr: 0x%08x\n", DestIp);
		fflush(stdout);

		//add to ARP table
		memset(&MacAddr, 0xff, sizeof(MacAddr));
		dwRetVal = SendARP(DestIp, SrcIp, &MacAddr, &PhysAddrLen);
		return 0;
	}
	
	/* if we get here no metis was found */
	return -4;
}

int SendStartToMetis(void) 	 {

	int starting_seq;
	struct outdgram {
		unsigned char packetbuf[64];
	} outpacket;

	struct indgram {
		unsigned char fbuf[2000];
	} inpacket;

	int i;

	memset(outpacket.packetbuf, 0, sizeof(outpacket));

	outpacket.packetbuf[0] = 0xef;
	outpacket.packetbuf[1] = 0xfe;
	outpacket.packetbuf[2] = 0x04;
	outpacket.packetbuf[3] = 0x01;

	starting_seq = MetisLastRecvSeq;
	for (i = 0; i < 5; i++) {
		/* printf("start sent\n"); */
		ForceCandCFrame(1);
		sendto(listenSock, (char *)&outpacket, sizeof(outpacket), 0, (SOCKADDR *)&MetisSockAddr, sizeof(MetisSockAddr));
		MetisReadDirect((char *)&inpacket, sizeof(inpacket));
		if (MetisLastRecvSeq != starting_seq) {
			break;
		}
		/* printf("c&c forced\n");  */
		//ForceCandCFrame(); 
		Sleep(10);
		//		if ( MetisLastRecvSeq != starting_seq ) { 
		//			break; 
		//		} 
	}
	/* fflush(stdout);  */
	if (MetisLastRecvSeq == starting_seq) {
		return -1;
	}
	return 0;
}


/* returns 0 if metis appears to have stopped, !0 otherwise */
int SendStopToMetis(void) 	 {

	int starting_seq;
	// unsigned char packetbuf[64];
	struct outdgram {
		unsigned char packetbuf[64];
	} outpacket;

	int i;

	memset(outpacket.packetbuf, 0, sizeof(outpacket));

	outpacket.packetbuf[0] = 0xef;
	outpacket.packetbuf[1] = 0xfe;
	outpacket.packetbuf[2] = 0x04;
	outpacket.packetbuf[3] = 0x00;

	starting_seq = MetisLastRecvSeq;
	for (i = 0; i < 5; i++) {
		sendto(listenSock, (char *)&outpacket, sizeof(outpacket), 0, (SOCKADDR *)&MetisSockAddr, sizeof(MetisSockAddr));
		Sleep(10);
		if (MetisLastRecvSeq == starting_seq) {
			break;
		}
	}
	if (MetisLastRecvSeq != starting_seq) {
		return -1;
	}
	return 0;
}

int MetisReadDirect(char *bufp, int buflen) {
	struct indgram {
		unsigned char readbuf[1074];
	} inpacket;

	struct sockaddr_in fromaddr;
	int fromlen;
	int rc;
	unsigned int endpoint;
	unsigned int seqnum;
	unsigned char *seqbytep = (unsigned char *)&seqnum;

	fromlen = sizeof(fromaddr);

	rc = recvfrom_withtimeout(listenSock, (char *)&inpacket, sizeof(inpacket), 0, (struct sockaddr *)&fromaddr, &fromlen, 0, 500000);
	/* rc = recvfrom(listenSock, readbuf, sizeof(readbuf), 0, (struct sockaddr *)&fromaddr, &fromlen);  */
	if (rc < 0) {  /* failed */
		printf("MRD: recvfrom on listSock failed w/ rc=%d!\n", rc);  fflush(stdout);
		return rc;
	}
	/* check frame is from who we expect */
	if (rc == 1032) {   /* looks like a data frame */
		if ((inpacket.readbuf[0] == 0xef) && (inpacket.readbuf[1] == 0xfe) && (inpacket.readbuf[2] == 0x01)) {
			endpoint = (unsigned int)inpacket.readbuf[3];
			seqbytep[3] = inpacket.readbuf[4];
			seqbytep[2] = inpacket.readbuf[5];
			seqbytep[1] = inpacket.readbuf[6];
			seqbytep[0] = inpacket.readbuf[7];
			if (seqnum != (1 + MetisLastRecvSeq))  {
				printf("MRD: seq error this: %d last: %d\n", seqnum, MetisLastRecvSeq);
			}
			MetisLastRecvSeq = seqnum;

			if (endpoint == 6) {
				if ((inpacket.readbuf[8] == 0x7f) && (inpacket.readbuf[9] == 0x7f) && (inpacket.readbuf[10] == 0x7f)) {
					HaveSync = 1;
				}
				else {
					HaveSync = 0;
					printf("MRD: sync error on frame %d\n", seqnum);
					// return 0;
				}
				memcpy(bufp, inpacket.readbuf + 8, 1024);
				return 1024;
			}
			else {
				printf("MRD: ignoring data for ep %d\n", endpoint);
			}
		}
		else {
			printf("MRD: ignoring right sized frame bad header! %d\n", rc);
		}
	}
	else {
		printf("MRD: ignoring short frame size=%d\n", rc);
	}
	return 0;
}


void MetisReadThreadMainLoop() {
	while (MetisKeepRunning) {
		Sleep(1000);
	}
	return;
}


// this is the main thread that reads data from Metis
void *MetisReadThreadMain(void *argp) {
	MetisKeepRunning = 1;
	MetisReadThreadRunning = 1;
#ifndef LINUX
	// SetThreadPriority(GetCurrentThread(), /* THREAD_PRIORITY_ABOVE_NORMAL */  THREAD_PRIORITY_TIME_CRITICAL /* THREAD_PRIORITY_HIGHEST  */ ); 
#else
	#warning message("info - LINUX code missing ... set priority!")
#endif

	sem_post(&MetisReadThreadInitSem); // tell launching thread we're rockin and rollin
	printf("MetisReadThread runs...\n"); fflush(stdout); 
	MetisReadThreadMainLoop();
	MetisReadThreadRunning = 0;
	printf("MetisReadThread dies...\n"); fflush(stdout); 

	return NULL;
}


/* returns 0 on sucess, !0 on failure */
int MetisStartReadThread(void) {
	int myrc = 0; 
	int rc; 

	do { 
		/* allocate ring buffer */ 
		MetisEP6RingBuf = createRingBuffer(512 * NUM_RINGBUF_FRAMES); 
		if ( MetisEP6RingBuf == NULL ) { 
			return -1; 
		} 
		sem_init(&MetisReadThreadInitSem, 0, 0);
		rc = pthread_create(&MetisReadThreadID, NULL, MetisReadThreadMain, NULL);
		if ( rc != 0 ) {  // failed
			fprintf(stderr, "pthread_created failed on MetisReadThread w/ rc=%d\n", rc);
			myrc =  -2;   
			break; 
		}
		sem_wait(&MetisReadThreadInitSem);  // wait for the thread to get going

		rc = SendStartToMetis(); 
		if ( rc != 0 ) { 
			printf("SendStartTo Metis failed ...\n"); fflush(stdout); 
			MetisStopReadThread(); 
			myrc = -3; 
			break; 
		} 


	} while ( 0 ); 

	if ( myrc != 0 ) {  /* something died - cleanup */ 
		if ( MetisEP6RingBuf != NULL ) { 
			destroyRingBuffer(MetisEP6RingBuf); 
			MetisEP6RingBuf = NULL; 
		} 		
	} 		
	return myrc; 
} 

void MetisStopReadThread(void) {
	SendStopToMetis();
	MetisKeepRunning = 0;
	DeInitMetisSockets();
	return;
}

/* read data from the ringbuffer */
int MetisBulkRead(int endpoint, char *bufp, int buflen) {
	int result;
	int sleepcount = 10;
	if (endpoint != 6) {
		printf("ep != 6!!\n");  fflush(stdout); 
		return 0; 
	}
	while (1) {
		result = getRingBuffer(MetisEP6RingBuf, bufp, buflen, 0);
		if (result != 0) {
			break;
		}
		--sleepcount;
		if (sleepcount <= 0) break;
		Sleep(13);
	}

	return result;
}


/* write data to Metis board */
unsigned int MetisOutBoundSeqNum;

int MetisWriteFrame(int endpoint, char *bufp, int buflen) {
	int result;
	//unsigned char framebuf[1032]; 
	struct outdgram {
		unsigned char framebuf[1032];
	} outpacket;
	unsigned char *p = (unsigned char *)&MetisOutBoundSeqNum;

	if (buflen > 1024) {
		return -1;
	}
	outpacket.framebuf[0] = 0xef;
	outpacket.framebuf[1] = 0xfe;
	outpacket.framebuf[2] = 01;
	outpacket.framebuf[3] = endpoint;
	outpacket.framebuf[4] = p[3];
	outpacket.framebuf[5] = p[2];
	outpacket.framebuf[6] = p[1];
	outpacket.framebuf[7] = p[0];
	++MetisOutBoundSeqNum;
	memcpy(outpacket.framebuf + 8, bufp, buflen);

	result = sendto(listenSock, (char *)&outpacket, 8 + buflen, 0, (SOCKADDR *)&MetisSockAddr, sizeof(MetisSockAddr));
	result -= 8;
	return result;
}


int MetisBulkWrite(int endpoint, char *bufp, int buflen) {
	int framecount;
	int leftover;
	int i;
	int result = 0;
	int rc;

	framecount = buflen / 1024;
	leftover = buflen - (framecount * 1024);
	if (leftover != 0) {
		printf("mayday  - MetisBulkWrite - buflen not even multiple of 1024!!\n");  fflush(stdout);
	}
	for (i = 0; i < framecount; i++) {
		rc = MetisWriteFrame(endpoint, bufp + (i * 1024), 1024);
		if (rc != 1024) {
			printf("warning MetisFrameWrite rc: %d\n", rc); fflush(stdout);
		}
		result += rc;
	}
	return result;
}

