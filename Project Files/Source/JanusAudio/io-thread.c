/* JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
 * Copyright (C) 2006,2007  Bill Tracey (bill@ejwt.com) (KD5TFD)
 * Copyright (C) 2010-2013 Doug Wigley (W5WC)
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

// io-thread.c - this file contains routines reading and writing from the xylo
//


// this is updated for 8 channels
#include <stdio.h>


#ifdef LINUX
#include <unistd.h>
#endif


#include "KD5TFD-VK6APH-Audio.h"
// #define PERF_DEBUG 1
#include "private.h"

#ifdef PERF_DEBUG
#include "nanotimer.h"
HLA_COUNTER ReadHLA;
HLA_COUNTER WriteHLA;
#endif

// #define INBUF_SIZE (512)  // size of fpga buffers - incoming
// #define OUTBUF_SIZE (512) // size of fpga buffers - outgoing
// char read_buf[INBUF_SIZE];
// char write_buf[OUTBUF_SIZE];
int io_keep_running;

#define MAX_INBUF_SIZE (8192)  // max size of fpga buffers - incoming  - only used for debugging
#define MAX_OUTBUF_SIZE (2048) // max size of fpga buffers - outgoing  - only used for debugging


// #define PRINTF_C_AND_C (1) 

// obsoloete - fpga tells us free space in fifo now #define FPGA_INPUT_FIFO_SIZE (2048)

// save writes to the fpga
//#define SAVE_WRITES_TO_BUFFER  (1)
#ifdef SAVE_WRITES_TO_BUFFER
#define NUM_WRITE_BUFS_TO_SAVE (300)
char WriteSaveBuf[NUM_WRITE_BUFS_TO_SAVE][MAX_OUTBUF_SIZE];
int SaveWritesIdx = 0;  // idx of next buffer to be written
#endif

// save buffers read from the fpga
// #define SAVE_READS_TO_BUFFER (1)
#ifdef SAVE_READS_TO_BUFFER
#define NUM_READ_BUFS_TO_SAVE (1000)
char ReadSaveBuf[NUM_READ_BUFS_TO_SAVE][MAX_INBUF_SIZE];
int SaveReadsIdx = 0; // idx of next buf to write to
#endif


unsigned char fpga_test_buf[512] = { 
    0x7f, 0x7f,
    0x7f, 0x00, 
	0x00, 0x00,
	0x00, 0x00, 
	0x00, 0x01,
    0x00, 0x02,
    0x00, 0x03,
    0x00, 0x04,
    0x00, 0x05,
    0x00, 0x06,
    0x00, 0x07,
    0x00, 0x08,
    0x00, 0x09,
    0x00, 0x0a,
    0x00, 0x0b,
    0x00, 0x0c,
    0x00, 0x0d,
    0x00, 0x0e,
    0x00, 0x0f,
    0x00, 0x10,
    0x00, 0x11,
    0x00, 0x12,
    0x00, 0x13,
    0x00, 0x14,
    0x00, 0x15,
    0x00, 0x16,
    0x00, 0x17,
    0x00, 0x18,
    0x00, 0x19,
    0x00, 0x1a,
    0x00, 0x1b,
    0x00, 0x1c,
    0x00, 0x1d,
    0x00, 0x1e,
    0x00, 0x1f,
    0x00, 0x20,
    0x00, 0x21,
    0x00, 0x22,
    0x00, 0x23,
    0x00, 0x24,
    0x00, 0x25,
    0x00, 0x26,
    0x00, 0x27,
    0x00, 0x28,
    0x00, 0x29,
    0x00, 0x2a,
    0x00, 0x2b,
    0x00, 0x2c,
    0x00, 0x2d,
    0x00, 0x2e,
    0x00, 0x2f,
    0x00, 0x30,
    0x00, 0x31,
    0x00, 0x32,
    0x00, 0x33,
    0x00, 0x34,
    0x00, 0x35,
    0x00, 0x36,
    0x00, 0x37,
    0x00, 0x38,
    0x00, 0x39,
    0x00, 0x3a,
    0x00, 0x3b,
    0x00, 0x3c,
    0x00, 0x3d,
    0x00, 0x3e,
    0x00, 0x3f,
    0x00, 0x40,
    0x00, 0x41,
    0x00, 0x42,
    0x00, 0x43,
    0x00, 0x44,
    0x00, 0x45,
    0x00, 0x46,
    0x00, 0x47,
    0x00, 0x48,
    0x00, 0x49,
    0x00, 0x4a,
    0x00, 0x4b,
    0x00, 0x4c,
    0x00, 0x4d,
    0x00, 0x4e,
    0x00, 0x4f,
    0x00, 0x50,
    0x00, 0x51,
    0x00, 0x52,
    0x00, 0x53,
    0x00, 0x54,
    0x00, 0x55,
    0x00, 0x56,
    0x00, 0x57,
    0x00, 0x58,
    0x00, 0x59,
    0x00, 0x5a,
    0x00, 0x5b,
    0x00, 0x5c,
    0x00, 0x5d,
    0x00, 0x5e,
    0x00, 0x5f,
    0x00, 0x60,
    0x00, 0x61,
    0x00, 0x62,
    0x00, 0x63,
    0x00, 0x64,
    0x00, 0x65,
    0x00, 0x66,
    0x00, 0x67,
    0x00, 0x68,
    0x00, 0x69,
    0x00, 0x6a,
    0x00, 0x6b,
    0x00, 0x6c,
    0x00, 0x6d,
    0x00, 0x6e,
    0x00, 0x6f,
    0x00, 0x70,
    0x00, 0x71,
    0x00, 0x72,
    0x00, 0x73,
    0x00, 0x74,
    0x00, 0x75,
    0x00, 0x76,
    0x00, 0x77,
    0x00, 0x78,
    0x00, 0x79,
    0x00, 0x7a,
    0x00, 0x7b,
    0x00, 0x7c,
    0x00, 0x7d,
    0x00, 0x7e,
    0x00, 0x7f,
    0x00, 0x80,
    0x00, 0x81,
    0x00, 0x82,
    0x00, 0x83,
    0x00, 0x84,
    0x00, 0x85,
    0x00, 0x86,
    0x00, 0x87,
    0x00, 0x88,
    0x00, 0x89,
    0x00, 0x8a,
    0x00, 0x8b,
    0x00, 0x8c,
    0x00, 0x8d,
    0x00, 0x8e,
    0x00, 0x8f,
    0x00, 0x90,
    0x00, 0x91,
    0x00, 0x92,
    0x00, 0x93,
    0x00, 0x94,
    0x00, 0x95,
    0x00, 0x96,
    0x00, 0x97,
    0x00, 0x98,
    0x00, 0x99,
    0x00, 0x9a,
    0x00, 0x9b,
    0x00, 0x9c,
    0x00, 0x9d,
    0x00, 0x9e,
    0x00, 0x9f,
    0x00, 0xa0,
    0x00, 0xa1,
    0x00, 0xa2,
    0x00, 0xa3,
    0x00, 0xa4,
    0x00, 0xa5,
    0x00, 0xa6,
    0x00, 0xa7,
    0x00, 0xa8,
    0x00, 0xa9,
    0x00, 0xaa,
    0x00, 0xab,
    0x00, 0xac,
    0x00, 0xad,
    0x00, 0xae,
    0x00, 0xaf,
    0x00, 0xb0,
    0x00, 0xb1,
    0x00, 0xb2,
    0x00, 0xb3,
    0x00, 0xb4,
    0x00, 0xb5,
    0x00, 0xb6,
    0x00, 0xb7,
    0x00, 0xb8,
    0x00, 0xb9,
    0x00, 0xba,
    0x00, 0xbb,
    0x00, 0xbc,
    0x00, 0xbd,
    0x00, 0xbe,
    0x00, 0xbf,
    0x00, 0xc0,
    0x00, 0xc1,
    0x00, 0xc2,
    0x00, 0xc3,
    0x00, 0xc4,
    0x00, 0xc5,
    0x00, 0xc6,
    0x00, 0xc7,
    0x00, 0xc8,
    0x00, 0xc9,
    0x00, 0xca,
    0x00, 0xcb,
    0x00, 0xcc,
    0x00, 0xcd,
    0x00, 0xce,
    0x00, 0xcf,
    0x00, 0xd0,
    0x00, 0xd1,
    0x00, 0xd2,
    0x00, 0xd3,
    0x00, 0xd4,
    0x00, 0xd5,
    0x00, 0xd6,
    0x00, 0xd7,
    0x00, 0xd8,
    0x00, 0xd9,
    0x00, 0xda,
    0x00, 0xdb,
    0x00, 0xdc,
    0x00, 0xdd,
    0x00, 0xde,
    0x00, 0xdf,
    0x00, 0xe0,
    0x00, 0xe1,
    0x00, 0xe2,
    0x00, 0xe3,
    0x00, 0xe4,
    0x00, 0xe5,
    0x00, 0xe6,
    0x00, 0xe7,
    0x00, 0xe8,
    0x00, 0xe9,
    0x00, 0xea,
    0x00, 0xeb,
    0x00, 0xec,
    0x00, 0xed,
    0x00, 0xee,
    0x00, 0xef,
    0x00, 0xf0,
    0x00, 0xf1,
    0x00, 0xf2,
    0x00, 0xf3,
    0x00, 0xf4,
    0x00, 0xf5,
    0x00, 0xf6,
    0x00, 0xf7,
    0x00, 0xf8,
    0x00, 0xf9,
    0x00, 0xfa,
    0x00, 0xfb,
    0x00, 0xfc,    
};

#define STATE_NOSYNC (1)
#define STATE_CONTROL0 (2)
#define STATE_CONTROL1 (3)
#define STATE_CONTROL2 (4)
#define STATE_CONTROL3 (5)
#define STATE_CONTROL4 (6)
#define STATE_SYNC_HI (7)
#define STATE_SYNC_MID (8)
#define STATE_SYNC_LO (9)
#define STATE_SAMPLE_I1_HI (10)
#define STATE_SAMPLE_I1_MID (11)
#define STATE_SAMPLE_I1_LO (12)
#define STATE_SAMPLE_Q1_HI (13)
#define STATE_SAMPLE_Q1_MID (14)
#define STATE_SAMPLE_Q1_LO (15)
#define STATE_SAMPLE_I2_HI (16)
#define STATE_SAMPLE_I2_MID (17)
#define STATE_SAMPLE_I2_LO (18)
#define STATE_SAMPLE_Q2_HI (19)
#define STATE_SAMPLE_Q2_MID (20)
#define STATE_SAMPLE_Q2_LO (21)
#define STATE_SAMPLE_MIC_HI (22)
#define STATE_SAMPLE_MIC_LO (23)
#define STATE_PADDING_LOOP  (24)
#define STATE_SAMPLE_HI (25)
#define STATE_SAMPLE_MID (26)
#define STATE_SAMPLE_LO (27)
#define OUT_STATE_SYNC_HI_NEEDED (1)
#define OUT_STATE_LEFT_HI_NEEDED (2)
#define OUT_STATE_RIGHT_HI_NEEDED (3)
#define OUT_STATE_SYNC_LO_NEEDED (4)
#define OUT_STATE_LEFT_LO_NEEDED (5)
#define OUT_STATE_RIGHT_LO_NEEDED (6)
#define OUT_STATE_SYNC_ZERO_LO_NEEDED (7)
#define OUT_STATE_SYNC_ZERO_HI_NEEDED (8)
#define OUT_STATE_SYNC_MID_NEEDED (9)
#define OUT_STATE_CONTROL0 (10)
#define OUT_STATE_CONTROL1 (11)
#define OUT_STATE_CONTROL2 (12)
#define OUT_STATE_CONTROL3 (13)
#define OUT_STATE_CONTROL4 (14)
#define OUT_STATE_MON_LEFT_HI_NEEDED (15)
#define OUT_STATE_MON_LEFT_LO_NEEDED (16)
#define OUT_STATE_MON_RIGHT_HI_NEEDED (17)
#define OUT_STATE_MON_RIGHT_LO_NEEDED (18)


// #define TEST_READ (1)
// #define TEST_WRITE (1)

int complete_blocks;
// int LostSyncCount;
// int sync_gain_count;


#define INCL_DUMP (1)
#ifdef INCL_DUMP
void Dump(FILE *ofile,                /* file handle to dump to - assumed to be      */
                                         /* open.                                       */
    unsigned char *buf,            /* pointer to data to dump                     */
    unsigned int count,            /* number of bytes to dump                      */
    unsigned char *comment) {      /* comment put out at top of dump, may be NULL */
    static unsigned char HexTbl[] = "0123456789ABCDEF"; /* Hex Xlat tbl */
    static unsigned char FmtSpec[] = "%05u %04X  %s  [%s]\n";

    unsigned char obuf1[40];  /* buffer for the hex part of the output */
    unsigned char obuf2[20];  /* buffer for the text part of output    */
    unsigned char *optr1;     /* points to current position in obuf1   */
    unsigned char *optr2;     /* points to current position in obuf2   */
    unsigned int i,j;         /* counters                              */

    if ( comment ) fprintf(ofile,"%s\n",comment);
    if ( !buf ) {
        fprintf(ofile,"(null)\n");
        return;
    }
    /*
     * For each full block of 16 bytes
     */
    for ( i = 0; i < (count >> 4); i++ ) {
        optr1 = obuf1;
        optr2 = obuf2;
        for ( j = 0; j < 16; j++ ) {
            if ( isprint(buf[(i << 4) + j])) *optr2++ = buf[(i<<4)+j];
            else *optr2++ = '.';
            *optr1++ = HexTbl[buf[(i<<4) + j] >> 4];
            *optr1++ = HexTbl[buf[(i<<4) + j] & 0x0f];
            if ( (j == 3) || (j == 7) || (j == 11) ) *optr1++ = ' ';
        }
        *optr1 = 0; /* make them null */
        *optr2 = 0;
        fprintf(ofile,(char *)FmtSpec,i<<4,i<<4,(char *)obuf1,
                (char *)obuf2);
    }
    /*
     * Now do anything remaining (after all the even 16 byte blocks)
     */
    if ( count & 0x0f ) { /* there is a partial block at the end */
        optr1 = obuf1;
        optr2 = obuf2;
        for ( i = 0; i < (count & 0x0f); i++ ) {
            if ( isprint(buf[(count & 0xfff0) + i]) ) *optr2++ = buf[(count & 0xfff0) + i];
            else *optr2++ = '.';
            *optr1++ = HexTbl[buf[(count & 0xfff0) + i] >> 4];
            *optr1++ = HexTbl[buf[(count & 0xfff0) + i] & 0x0f];
            if ((i == 3) || (i == 7) || (i == 11) ) *optr1++ = ' ';
        }
        *optr2 = 0;
        for ( i = 0; i < 16 - (count & 0x0f); i++ ) {
            *optr1++ = ' ';
            *optr1++ = ' ';
        }
        if ( (count &0x0f ) < 4 ) *optr1++ = ' ';
        if ( (count &0x0f ) < 8 ) *optr1++ = ' ';
        if ( (count &0x0f ) < 12 ) *optr1++ = ' ';
        *optr1 = 0;
        fprintf(ofile,(char *)FmtSpec,count & 0xfff0,count & 0xfff0,
                (char *)obuf1,(char *)obuf2);
    }
    return;
}
#endif

#ifdef INCL_DUMP
int read_dump_count = 0;
#endif

#ifdef TEST_READ
unsigned char test_data[4*32] = {
0x00, 0x00, 0x7f, 0xff,  /* 00000 0x0000 32767 0x7fff */
0x18, 0xf8, 0x7d, 0x89,  /* 06392 0x18f8 32137 0x7d89 */
0x30, 0xfb, 0x76, 0x40,  /* 12539 0x30fb 30272 0x7640 */
0x47, 0x1c, 0x6a, 0x6c,  /* 18204 0x471c 27244 0x6a6c */
0x5a, 0x81, 0x5a, 0x81,  /* 23169 0x5a81 23169 0x5a81 */
0x6a, 0x6c, 0x47, 0x1c,  /* 27244 0x6a6c 18204 0x471c */
0x76, 0x40, 0x30, 0xfb,  /* 30272 0x7640 12539 0x30fb */
0x7d, 0x89, 0x18, 0xf8,  /* 32137 0x7d89 06392 0x18f8 */
0x7f, 0xff, 0x00, 0x00,  /* 32767 0x7fff 00000 0x0000 */
0x7d, 0x89, 0xe7, 0x08,  /* 32137 0x7d89 -6392 0xe708 */
0x76, 0x40, 0xcf, 0x05,  /* 30272 0x7640 -12539 0xcf05 */
0x6a, 0x6c, 0xb8, 0xe4,  /* 27244 0x6a6c -18204 0xb8e4 */
0x5a, 0x81, 0xa5, 0x7f,  /* 23169 0x5a81 -23169 0xa57f */
0x47, 0x1c, 0x95, 0x94,  /* 18204 0x471c -27244 0x9594 */
0x30, 0xfb, 0x89, 0xc0,  /* 12539 0x30fb -30272 0x89c0 */
0x18, 0xf8, 0x82, 0x77,  /* 06392 0x18f8 -32137 0x8277 */
0x00, 0x00, 0x80, 0x01,  /* 00000 0x0000 -32767 0x8001 */
0xe7, 0x08, 0x82, 0x77,  /* -6392 0xe708 -32137 0x8277 */
0xcf, 0x05, 0x89, 0xc0,  /* -12539 0xcf05 -30272 0x89c0 */
0xb8, 0xe4, 0x95, 0x94,  /* -18204 0xb8e4 -27244 0x9594 */
0xa5, 0x7f, 0xa5, 0x7f,  /* -23169 0xa57f -23169 0xa57f */
0x95, 0x94, 0xb8, 0xe4,  /* -27244 0x9594 -18204 0xb8e4 */
0x89, 0xc0, 0xcf, 0x05,  /* -30272 0x89c0 -12539 0xcf05 */
0x82, 0x77, 0xe7, 0x08,  /* -32137 0x8277 -6392 0xe708 */
0x80, 0x01, 0x00, 0x00,  /* -32767 0x8001 00000 0x0000 */
0x82, 0x77, 0x18, 0xf8,  /* -32137 0x8277 06392 0x18f8 */
0x89, 0xc0, 0x30, 0xfb,  /* -30272 0x89c0 12539 0x30fb */
0x95, 0x94, 0x47, 0x1c,  /* -27244 0x9594 18204 0x471c */
0xa5, 0x7f, 0x5a, 0x81,  /* -23169 0xa57f 23169 0x5a81 */
0xb8, 0xe4, 0x6a, 0x6c,  /* -18204 0xb8e4 27244 0x6a6c */
0xcf, 0x05, 0x76, 0x40,  /* -12539 0xcf05 30272 0x7640 */
0xe7, 0x08, 0x7d, 0x89   /* -6392 0xe708 32137 0x7d89 */
};


int test_data_idx = 0;
int oframe_count = 0; // keep track of where we are in output so we can insert sync words

int testRead(unsigned char *bufp, int len) {
        int j;

        for ( j = 0; j < len; j++ ) {
                if ( oframe_count == 0 ) {  // need first byte of sync word
                        bufp[j] = 0x80;
                }
                else if ( oframe_count == 1 || oframe_count == 2 || oframe_count == 3 ) {  // 2nd byte of sync word
                        bufp[j] = 0x00;
                }
                else {  // data words
                        bufp[j] = test_data[test_data_idx];
                        ++test_data_idx;
                        if ( test_data_idx == sizeof(test_data) ) {
                        test_data_idx = 0;
                        }
                }
                ++oframe_count;
                if ( oframe_count >= 512 ) {
                        oframe_count = 0;
                }
        }

#if 0
        if ( read_dump_count < 4 ) {
                ++read_dump_count;
                Dump(stdout, bufp, len, "testRead: Buffer returned");
                fflush(stdout);
        }
#endif

#ifndef LINUX
        Sleep(12);  // assumes a 2048 byte read buffer
#else
        usleep(12000);
#endif
        return len;
 }
#endif

#ifdef TEST_WRITE
FILE *testOutF = NULL;

int testWrite(unsigned char *bufp, int len) {
        if ( testOutF == NULL ) return len;  // silently fail
        return (int)fwrite(bufp, 1, len, testOutF);
}
#endif

int bytes_processed = 0;

// swap bytes of count shorts pointed to by sp
void swapBytesShort(short *insp, int count) {

#ifndef LINUX
        if ( count <= 0 ) return;

        __asm {
                mov esi, insp
                mov ecx, count
top1:
                mov ax, [esi]
                xchg al, ah
                mov [esi], ax
                add esi, 2
                dec ecx
                jnz top1
        }


#else
        unsigned char *p;
        unsigned char tmp;
        int i;
        for ( i = 0; i < count; i++ ) {
                p = (unsigned char *)insp;
                tmp = *p;
                *p = *(p+1);
                *(p+1) = tmp;
                ++insp;
        }
        return;
#endif
}

//  get an output buffer from the fifo, don't wait, and make the buffer big endian
unsigned char *getNewOutBufFromFIFO() {
        int outbuflen;
        unsigned char *outbufp;
        int out_buf_len_needed;

        outbufp = (unsigned char *)getFIFO(OutSampleFIFOp, &outbuflen, 0); // don't wait
        if ( outbufp == NULL ) {
                return NULL;
        }
        /* else */
        out_buf_len_needed = BlockSize * 4 * sizeof(short);
        if  ( SampleRate == 96000 ) {
                out_buf_len_needed /= 2;
        }
        else if ( SampleRate == 192000 )  {
                out_buf_len_needed /= 4;
        }
       else if ( SampleRate == 384000 )  {
                out_buf_len_needed /= 8;
        }
       if ( outbuflen != out_buf_len_needed  ) {
                printf("Warning: IOThread short block from getFIFO on output, frame dropped obl=%d obln=%d\n", outbuflen, out_buf_len_needed);
                freeFIFOdata(outbufp);
                return NULL;
        }
        swapBytesShort((short *)outbufp, outbuflen/sizeof(short));
        return outbufp;
}

#ifdef PERF_DEBUG
typedef struct zperf_record {
                // int RxFifoAvail;
                // int RxFramePlaying;
                int wrote_frame;
                int ok_to_write;
                __int64 rt;
                __int64 wt;
                __int64 drt;
                __int64 dwt;
                __int64 ticks;
                int cb;
                int sync_lost;
                unsigned char RxOverrun;
                unsigned char TxOverrun;
                unsigned char frame_sending;
} PERF_RECORD;

#define PERF_DATA_COUNT (50000)

PERF_RECORD PerfData[PERF_DATA_COUNT];

int PerfDataIdx = 0;
#endif

// unsigned short RxFifoAvail = 4096;  // make it large to start
// unsigned char RxFramePlaying = 0;

// unsigned int NotOKtoSendCount = 0;
unsigned int SendOpprotunity = 0;

int IsOkToSendDataToFPGA(void) {
	return 1; 

#if 0 
        if ( RxFifoAvail >= 512 ) {
                return 1;
        }
        else {
                ++NotOKtoSendCount;
                return 1;
        }
#endif 
}


/* force a command and control frame - used to poke ozy when a read 
   fails */ 


unsigned int lastOutPower = 0; 

void ForceCandCFrames(int count, int c0, int vfofreq) { 
	int	numwritten;	
	unsigned char buf[1024];
	int	i; 
	memset(buf,	0, sizeof(buf)); 

	
	buf[0] = 0x7f; 
	buf[1] = 0x7f; 
	buf[2] = 0x7f; 
	buf[3] = XmitBit;									  /* c0	*/ 
	buf[4] = (SampleRateIn2Bits	& 3) | ( C1Mask	& 0xfc ); /* c1	*/
	buf[5] = PennyOCBits <<	1;							  /* c2	*/
	buf[6] = (AlexAtten | MercDither | MercPreamp | MercRandom | AlexRxAnt | AlexRxOut) & 0xff; /* c3 */
//	buf[7] = (AlexTxAnt & 0x3) | 0x08; //dual Mercs
	buf[7] =  0x08 | 0x80; //0x08 dual Mercs, 0x80 common Merc freq

	buf[512] = 0x7f; 
	buf[513] = 0x7f; 
	buf[514] = 0x7f; 
	buf[515] = c0; 								  /* c0	*/ 
	buf[516] = (vfofreq >> 24) & 0xff;	/* byte	0 of freq -	c1	*/
	buf[517] = (vfofreq >> 16) & 0xff;	/* byte	1 of freq -	c2	*/
	buf[518] = (vfofreq >> 8) &	0xff;	/* byte	2 of freq -	c3	*/ 
	buf[519] = vfofreq & 0xff;			/* byte	3 of freq -	c4	*/	

	if (  FPGATestMode ) { 
		memcpy(buf+8, fpga_test_buf+8, 512-8); 
		memcpy(buf+8+512, fpga_test_buf+8, 512-8); 
	} 
	for	( i	= 0; i < count;	i++	) {	
		if ( isMetis ) { 
			numwritten = MetisBulkWrite(2, (char *)buf, sizeof(buf)); 
		} 
		else { 
			numwritten = OzyBulkWrite(OzyH,	0x02, buf, sizeof(buf));
		}
	}
} 

void ForceCandCFrame(int count) { 
	ForceCandCFrames(count, 2, VFOfreq_tx); 
	Sleep(10);
	ForceCandCFrames(count, 4, VFOfreq_rx1);
	Sleep(10);
  
   /*  if(nreceivers >= 2) {
	ForceCandCFrames(count, 6, VFOfreq_rx2);
	Sleep(10);
	}
	else {
    ForceCandCFrames(count, 6, 0);
	Sleep(10);
	} 

	if(nreceivers >= 3) {
	ForceCandCFrames(count, 8, VFOfreq_rx3);
	Sleep(10);
	}
	else {
	ForceCandCFrames(count, 8, 0);
	Sleep(10);
	}

	if(nreceivers >= 4) {
	ForceCandCFrames(count, 0x0a, VFOfreq_rx4);
	Sleep(10);
	}
	else {
	ForceCandCFrames(count, 0x0a, 0);
	Sleep(10);
	}

	if (nreceivers == 5) {
    ForceCandCFrames(count, 0x0c, VFOfreq_rx5);
	} 
	else {
	ForceCandCFrames(count, 0x0c, 0);
	} */


//	Sleep(10);
//	ForceCandCFrames(1, 0x0e, VFOfreq_rx6);
//	Sleep(10);
//	ForceCandCFrames(1, 0x10, VFOfreq_rx7);
} 

void processOverloadBit(int bit, int bit2) { 
	if (bit == 1){
		ADC_Overloads = ADC_Overloads | 1; // Merc1
	}
	if (bit2 == 1){
		ADC_Overloads = ADC_Overloads | 2; // Merc2
	}
	return; 
} 
 
#if 0
int last_xmit_bit = 0;
#endif

#define MDECAY  0.9992f
void PeakAlexFwdPower(float alex_fwd)
{
	if (alex_fwd > AlexFwdPower)
		AlexFwdPower = alex_fwd;
	else
		AlexFwdPower *= MDECAY;
}

void PeakRefPower(float alex_ref)
{
	if (alex_ref > RefPower)
		RefPower = alex_ref;
	else
		RefPower *= MDECAY;
}


// main loop of the iothread -- real work happens here
// read data out of the xylo, block it, and put it into a fifo  to go to the callback thread
//
void IOThreadMainLoop(void) {

	int fwd_power_stage; 
	int ref_power_stage; 
	int alex_fwd_power; 
	int ain3, ain4;
	short *sample_bufp = NULL;
	int sample_count = 0;
	int i;
	int numread;
	int numwritten;
	int have_out_buf = 0; // do we have an output buffer to work on
	int state = STATE_NOSYNC;
	int out_state = OUT_STATE_SYNC_HI_NEEDED;
	unsigned char last_byte = 12;  // init to anything other than 0x80 or 0x00
	unsigned char second_last_byte = 12; // init to something other than 0x80 or 0x00
	int this_num = 0;
	int byte_num = 0;
	// short left_sample;
	// short right_sample;
	// short sample;
	// int in_stream_sync_count = 0; // count where we are in input stream to know when to look for sync
	// int out_stream_sync_count = 0; // where are we relative to sync in output stream
	int total_written = 0;
	unsigned char out_frame_idx = 0;
	int hermes_dcv;
	int loop = 0;

	int samples_this_sync = 0; // how many samples we've read on the current sync word - l+r = 2 samples
	int out_sample_pairs_this_sync = 0; // how many sample pairs we've written -- l+r = 1
	int frames_written_this_read = 0; // how many frames have we written on this read
	int sync_samples = 0;
	int padding_loops = 0;
	double R_I = 0;				//I component of phase-shift rotate vector R
	double R_Q = 0;				//Q component of phase-shift rotate vector R
	double R_IA = 0;
	double R_QA = 0;
	int r_Merc;					//Mercury board to use as reference stream
	double I_shift = 0;			//for vector phase shift calc
	double Q_shift = 0;			//for vector phase shift calc
	int Merc1_I = 0;			//I input from first Merc board
	int Merc1_Q = 0;			//Q input from first Merc board
	int Merc2_I = 0;		    //I input from second Merc board
	int Merc2_Q = 0;			//Q input from second Merc board
	int div_toggle = 0;			//toggle for updating freq for Merc11, Merc2, and Tx (same freq for all)
	int IQ_stream;				//sets the IQ stream processing (1: Merc1 only, 2: Merc2 only, 
	//3: both Mercs in diversity/directional nulling mode)

	int wrote_frame = 0;
	int ok_to_write = 0;

	int out_control_idx = 0;

	unsigned char *outbufp = NULL;
	int outbufpos; // position of next sample to be processed in outbufp
	int outbuflen; // size of *outbufp in bytes
	int writebufpos = 0;
	int sample_bufp_size;
	//        int sample_is_left;
	int rx_sample_loops = 1;
	int buf_num = 0;
	unsigned char RxOverrun = 0;
	unsigned char TxOverrun = 0;
	int zero_read_count = 0; 		
	int nc = 2 * nreceivers + 1;
	sample_bufp_size =  nc * sizeof(int) * BlockSize;
	//reset_control_idx = 0;
	delay_Xmit = 0;
	reset_delay_xmit = 0;
	delay_Xmit_loop = 0;

	// how big is outbuf ... account for us being fixed at 48 khz on output side
	outbuflen = 4 * sizeof(short) * BlockSize;
	if ( SampleRate == 96000 ) {
		outbuflen /= 2;
	}
	else if ( SampleRate == 192000 ) {
		outbuflen /= 4;
	}
	else if ( SampleRate == 384000 ) {
		outbuflen /= 8;
	}

	switch (nreceivers) {
	case 1:
		sync_samples = 189;
		padding_loops = 0;
		break;
	case 2:
		sync_samples = 180;
		padding_loops = 0;
		break;
	case 3:
		sync_samples = 175;
		padding_loops = 3;
		break;
	case 4:
		sync_samples = 171;
		padding_loops = 9;
		break;
	case 5:
		sync_samples = 165;
		padding_loops = 23;
	case 6:
		sync_samples = 165;
		padding_loops = 9;
	case 7:
		sync_samples = 165;
		padding_loops = 19;
		break;
	case 8:
		sync_samples = 165;
		padding_loops = 3;
		break;
	}

	if (MercSource == 0){
		MercSource = 3; //set default to diversity Rx mode
	}
	if (refMerc == 0) {
		refMerc = 1;  //set default to Mercury 1 for reference stream
	}
	ForceCandCFrame(3); // send 3 C&C frames to make sure ozy knows the clock settings 
	// printf("iot: main loop starting\n"); fflush(stdout);
	// main loop - read a buffer, processe it and then write a buffer if we have one to write
	while ( io_keep_running != 0 ) {
		// should we read - or just skip to writing
		// if we wrote a frame last time (saying we may still have data waiting to be written) around
		// and there is space in the fifo, skip the read and write another frame
		//
		//first get the current MercSource value to determine how to process the IQ data streams from Metis/Ozy/Magister
		// MercSource = 1, process Mercury 1 IQ stream only
		// MercSource = 2, process Mercury 2 IQ stream only
		// MercSource = 3, process both Merc IQ streams in directional steering mode

		IQ_stream = MercSource; //get the current value

		if ( !isMetis || !wrote_frame ) { /* if not metis write, or if we did not write frame last time, time to read */ 				

			frames_written_this_read = 0;
			// first read data and process it
#ifndef TEST_READ
			// numread = XyloBulkRead(XyloH, 4, read_buf, sizeof(read_buf));
#ifdef OZY
			if ( isMetis ) { 
				//numread = MetisBulkRead(6, FPGAReadBufp, FPGAReadBufSize);
				numread = MetisReadDirect(FPGAReadBufp, FPGAReadBufSize); 
				if ( numread > 0 ) { 
					zero_read_count = 0; 
				} 
				else { 
					++zero_read_count; 
					// printf("MetisRead: nr: %d zc: %d\n", numread, zero_read_count); fflush(stdout); 
				} 													
			}
			else { 
				numread = OzyBulkRead(OzyH, 0x86, FPGAReadBufp, FPGAReadBufSize);
			}
			if ( (!isMetis && numread <= 0 ) ||  (isMetis && zero_read_count >= 1000 )  ) {											
				// if ( numread <= 0 ) {
#ifndef LINUX
				Sleep(10); /* this sleep is here to keep us from sucking all the CPU
						   on the machine when the USB cable is pullled out of the
						   computer while we're running -- without pausing here we get into
						   a tight loop reading from a device that is gone -- the sleep makes it
						   a less tight loop and leaves some cycles for the user to turn off PowerSDR
						   An ugly hack, but does work more or less

						   To fix this for real we need to be able to send some sort of error indication
						   thru the callback to tell the C# side that the sound card MIA
						   */
#else
				usleep(10000);
#endif

				fprintf(stdout, "OzyBulkRead failed rc=%d\n", numread);  fflush(stdout);
				ForceCandCFrame(1);  /* force a C and C frame in case Ozy is looking at wrong clocks */ 
			}

#ifdef EP6FILEDUMP 
			if ( (numread >  0) && (EP6_DumpBufBytesUsed < EP6FRAMEDUMPLIMIT) )  {
				int avail_space = EP6FRAMEDUMPLIMIT - EP6_DumpBufBytesUsed; 
				int copy_count = min(avail_space, numread); 
				memcpy(EP6_DumpBuf + EP6_DumpBufBytesUsed, FPGAReadBufp, copy_count); 
				EP6_DumpBufBytesUsed += copy_count; 
			} 
#endif 


#endif
			// printf("iot: read %d bytes\n", numread); fflush(stdout);
			++buf_num;

#ifdef SAVE_READS_TO_BUFFER
			memcpy(ReadSaveBuf[SaveReadsIdx], FPGAReadBufp, FPGAReadBufSize);
			++SaveReadsIdx;
			if ( SaveReadsIdx >= NUM_READ_BUFS_TO_SAVE ) {
				SaveReadsIdx = 0;
			}
#endif


#if 0
			if ( read_dump_count < 5 ) {
				++read_dump_count;
				Dump(stdout, FPGAReadBufp, FPGAReadBufSize, "Input Buffer from Xylo");  fflush(stdout);
			}
#endif
			// XyloBulkWrite(XyloH, 2, read_buf, numread);
#else
			numread = testRead(FPGAReadBuf, FPGAReadBufSize);
#endif
#ifdef PERF_DEBUG
			read_stop_t = getPerfTicks();
			read_t = read_stop_t - read_start_t;
			// printf("d1: %I64d\n", delta_t);
			// delta_t = perfTicksToNanos(delta_t);
			// printf("d2: %I64d\n", delta_t); fflush(stdout);
			updateHLA(&ReadHLA, read_t);
#endif
			for ( i = 0; i < numread; i++ ) {
				++bytes_processed;

				switch ( state ) {
				case STATE_NOSYNC:
					if ( second_last_byte == 0x7f && last_byte == 0x7f && FPGAReadBufp[i] == 0x7f ) {  // found sync
						// in_stream_sync_count = 2; // we've consumed 3 bytes already, it will bump at bottom of loop to make 3
						state = STATE_CONTROL0;
						samples_this_sync = 0;
						++SyncGainedCount;
						HaveSync = 1;
						//printf("sync gained: bufnum: %d i: %d\n", buf_num, i);
						//printf("\nSG");
					}
					break;

				case STATE_CONTROL0:
					// printf(" C0: 0x%08x\n", FPGAReadBufp[i]); fflush(stdout);
					ControlBytesIn[0] = FPGAReadBufp[i];
					DotDashBits = FPGAReadBufp[i] & DotDashMask;
					state = STATE_CONTROL1; // look for 2nd control byte
					break;

				case STATE_CONTROL1:
					// printf(" C1");
					ControlBytesIn[1] = FPGAReadBufp[i];

					switch (ControlBytesIn[0] & CandCAddrMask) {
					case 0:
						ADC_Overload = ControlBytesIn[1] & 1;
						if (HermesPowerEnabled) {
						//processOverloadBit((ControlBytesIn[1] & 1), (ControlBytesIn[1] & 1));
						User_I01 = ControlBytesIn[1] & 0x02;
						User_I02 = ControlBytesIn[1] & 0x04;
						User_I03 = ControlBytesIn[1] & 0x08;
						User_I04 = ControlBytesIn[1] & 0x10;
						}
						break;
					case 0x8:
						fwd_power_stage = ((ControlBytesIn[1] << 8) & 0xff00); // bits 15-8 (AIN5) Penny/Hermes
						break;
					case 0x10:
						ref_power_stage = ((ControlBytesIn[1] << 8) & 0xff00); // bits 15-8 (AIN2) Alex reverse power
						break;
					case 0x18:
						ain4 = ((ControlBytesIn[1] << 8) & 0xff00);	// bits 15-8 of AIN4
						break;
					case 0x20:
						MercuryFWVersion =  (int)(ControlBytesIn[1] >> 1);
						//processOverloadBit(ControlBytesIn[1] & 1);
						ADC1_Overload = ControlBytesIn[1] & 1;
						break;
					}
					// TxOverrun = FPGAReadBufp[i];
					state = STATE_CONTROL2; // look for 3rd control byte
					break;

				case STATE_CONTROL2:
					// printf(" C2");
					ControlBytesIn[2] = FPGAReadBufp[i];

					switch (ControlBytesIn[0] & CandCAddrMask) {
					case 0:
						MercuryFWVersion =  (int)(ControlBytesIn[2]);
						break;
					case 0x8:
						fwd_power_stage |=  (((int)(ControlBytesIn[2])) & 0xff); // bits 7-0 (AIN5) Penny/Hermes
						FwdPower = fwd_power_stage; 
						break;
					case 0x10:
						ref_power_stage |=  (((int)(ControlBytesIn[2])) & 0xff); // bits 7-0 (AIN2) Alex reverse power
						// RefPower = ref_power_stage;
						PeakRefPower ((float)ref_power_stage);
						break;
					case 0x18:
						ain4 |=  (((int)(ControlBytesIn[2])) & 0xff); // bits 7-0 of AIN4
						if (amp_protect_on && !is_orion_mkii)
						{
							if ((AIN4 = ain4 - 20) < 0) AIN4 = 0;							
						}
						else AIN4 = ain4;
						break;
					case 0x20:
						Mercury2FWVersion =  (int)(ControlBytesIn[2] >> 1);										                 
						//if (!HermesPowerEnabled) 
							//processOverloadBit((ControlBytesIn[1] & 1), (ControlBytesIn[2] & 1));
						ADC2_Overload = (ControlBytesIn[2] & 1) << 1;
						break;
					}

					// RxOverrun = FPGAReadBufp[i];
					state = STATE_CONTROL3; // look for 4th control byte
					break;

				case STATE_CONTROL3:  //
					// printf(" C3");
					ControlBytesIn[3] = FPGAReadBufp[i];
					switch (ControlBytesIn[0] & CandCAddrMask) {
					case 0:
						PenelopeFWVersion = (int)(ControlBytesIn[3]);
						break;
					case 0x8:
						alex_fwd_power = ((ControlBytesIn[3] << 8) & 0xff00); //bits 15-8 (AIN1) Alex
						break;
					case 0x10:
						ain3 = ((ControlBytesIn[3] << 8) & 0xff00); // AIN3
						break;
					case 0x18://bits 15-8 (AIN6) Hermes
						if (HermesPowerEnabled)
							hermes_dcv = ((ControlBytesIn[3] << 8) & 0xff00); //Hermes 13.8v (22.2v scale)
						break;
					case 0x20:
						//  Mercury3FWVersion =  (int)(ControlBytesIn[3] >> 1);	
						ADC3_Overload = (ControlBytesIn[3] & 1) << 2;
						break;
					}

					state = STATE_CONTROL4;  // look for 5th and last control byte
					break;

				case STATE_CONTROL4:
					// printf(" C4");
					ControlBytesIn[4] = FPGAReadBufp[i];

					switch (ControlBytesIn[0] & CandCAddrMask) {
					case 0:
						OzyFWVersion =  (int)(ControlBytesIn[4]);
						break;
					case 0x8:
						alex_fwd_power |=  (((int)(ControlBytesIn[4])) & 0xff); // bits 7-0 (AIN1) Alex
						// AlexFwdPower = alex_fwd_power;
						PeakAlexFwdPower ((float)alex_fwd_power);
						break;
					case 0x10:	
						ain3 |=  (((int)(ControlBytesIn[4])) & 0xff);
						AIN3 = ain3;
						break;
					case 0x18:
						if (HermesPowerEnabled) {
							hermes_dcv |=  (((int)(ControlBytesIn[4])) & 0xff);// bits 7-0 (AIN6) Hermes
							HermesDCV = hermes_dcv; //Hermes 13.8v (22.2v scale)
						}
						break;
					case 0x20:
						// Mercury4FWVersion = (int)(ControlBytesIn[4] >> 1);	
						//ADC4_Overload = (ControlBytesIn[4] & 1) << 3;
						break;
					}

					//if (0)//(diversitymode2)	//if diversity
					//	state = STATE_SAMPLE_I1_HI;
					//else
						state = STATE_SAMPLE_HI;

					// printf("fa: %d fp: %d\n", RxFifoAvail, RxFramePlaying);
					break;
				case STATE_SAMPLE_HI:
					//printf(" HI");
					this_num = ((int)FPGAReadBufp[i]) << 16; // start new sample, preserve the sign bit!
					//this_num = 0xFF << 16; // start new sample, preserve the sign bit!
					state = STATE_SAMPLE_MID;
					break;

				case STATE_SAMPLE_MID:
					//printf(" MI");
					this_num = (((unsigned char)FPGAReadBufp[i]) <<8) | this_num;  // add in middle part of sample -- unsigned!
					state = STATE_SAMPLE_LO;
					break;

				case STATE_SAMPLE_LO:
					// printf(" LO");
					//printf("sil: %d rx2: %d sc: %d\n", sample_is_left, rx2_samples, sample_count);
					this_num = ((unsigned char)FPGAReadBufp[i]) | this_num; // add in last part of sample
					IOSampleInBufp[sample_count] = this_num;
					++sample_count;
					//printf("sil: %d rx2: %d sc: %d\n", sample_is_left, rx2_samples, sample_count);

					++samples_this_sync;

					//printf(" Rx: %d loops: %d", receivers, rx_sample_loops);
					if(rx_sample_loops < (nc - 1)) // #receivers*2
					{
						state = STATE_SAMPLE_HI;
						++rx_sample_loops;
					}
					else 
					{
						state = STATE_SAMPLE_MIC_HI;
						rx_sample_loops = 1;
					}

					break;

				case STATE_SAMPLE_MIC_HI:
					//printf(" Mic HI ");
					this_num = ((int)FPGAReadBufp[i]) << 8;  // read hi word of sample - preserve sign moron!
					state = STATE_SAMPLE_MIC_LO;
					break;

				case STATE_SAMPLE_MIC_LO:
					//printf(" Mic LO ");
					this_num = ((unsigned char)FPGAReadBufp[i]) | this_num; // add in last part of sample
					IOSampleInBufp[sample_count] = this_num;

					++sample_count;
					if ( sample_count >= nc * BlockSize ) {   // only 5 channels on input (i1,q1,i2,q2,mic), although buffer is sized for 6
						sample_count = 0;
						//printf(" IOS: %d ", IOSampleInBufp); fflush(stdout);
						// printf("iot: calling proc buf\n"); fflush(stdout);
						Callback_ProcessBuffer(IOSampleInBufp, sample_bufp_size);
						//printf("iot: proc buf returned\n"); fflush(stdout);
						++complete_blocks;
					}

					++samples_this_sync;  // 1-rx = 189, 2-rx = 180, 3-rx = 175  4-rx = 171 7-rx = 165
					//state = samples_this_sync == sync_samples ? STATE_PADDING_LOOP : STATE_SAMPLE_I1_HI;//STATE_SAMPLE_HI; 
					if (samples_this_sync == sync_samples) {
						state = STATE_PADDING_LOOP;
					}
					else {										
						//if (diversitymode2) {
						if (0) {
							state = STATE_SAMPLE_I1_HI;
						}
						else {
							state = STATE_SAMPLE_HI;
						}
					}					

					if (state == STATE_PADDING_LOOP && padding_loops == 0) state = STATE_SYNC_HI;

					//printf("s_t_s: %d\n", samples_this_sync);
					//printf(" i: %d\n",i);
					break;

				case STATE_PADDING_LOOP:
					if(loop < padding_loops){
						if ( (unsigned char)FPGAReadBufp[i] != 0) {
							state = STATE_SYNC_HI;
							loop = 0;
						}
						else {
							loop++;
							state = STATE_PADDING_LOOP;
						}
					}
					else {
						state = STATE_SYNC_HI;
						loop = 0;
					} 
					break;

				case STATE_SYNC_HI:
					//printf(" SH");
					samples_this_sync = 0;
					if ( (unsigned char)FPGAReadBufp[i] != 0x7f ) { // argh did not find sync
						++LostSyncCount;
						state = STATE_NOSYNC;
						HaveSync = 0;
					}
					else {
						state = STATE_SYNC_MID;
					}
					//printf(" Sync: %d\n", FPGAReadBufp[i]);
					break;

				case STATE_SYNC_MID:
					//printf(" SM");
					if ( FPGAReadBufp[i] != 0x7f ) { // argh did not find sync
						++LostSyncCount;
						state = STATE_NOSYNC;
						HaveSync = 0;
					}
					else {
						state = STATE_SYNC_LO;
					}
					//printf(" Sync: %d\n", FPGAReadBufp[i]);
					break;

				case STATE_SYNC_LO:
					// printf(" SL");
					if ( FPGAReadBufp[i] != 0x7f ) { // argh did not find sync
						++LostSyncCount;
						state = STATE_NOSYNC;
						HaveSync = 0;
					}
					else {
						state = STATE_CONTROL0;
					}
					// printf(" Sync: %d\n", FPGAReadBufp[i]);
					break;

				default:
					// printf("io-thead: internal error, bad state in read loop\n");
					state = STATE_NOSYNC;
					break;

				}
				second_last_byte = last_byte;
				last_byte = FPGAReadBufp[i];

			} /* for numread = ... */
		}	/* metis or write frame */

		// ok now that we've handled the inbound block send outbound data if we have any
		//
		// printf("out top: out_sync: %d out_state: %d total_written: %d\n", out_stream_sync_count, out_state, total_written); fflush(stdout);
		wrote_frame = 0;

		if ( !have_out_buf ) {
			outbufp = getNewOutBufFromFIFO();
			if ( outbufp != NULL ) {
				have_out_buf = 1;
			}
			outbufpos = 0;
		}

		++SendOpprotunity;
		ok_to_write = IsOkToSendDataToFPGA();
		// printf("write top: okw: %d\n", ok_to_write); fflush(stdout);
		if ( have_out_buf && ok_to_write  ) {
			// printf("enter out loop /w buf: out_sync: %d out_state: %d total_written: %d\n", out_stream_sync_count, out_state, total_written); fflush(stdout);
			// while we have space in the output buffer put data in it
			while ( writebufpos < FPGAWriteBufSize /*OUTBUF_SIZE*/ ) {
				switch ( out_state ) {
				case OUT_STATE_SYNC_HI_NEEDED:
					out_sample_pairs_this_sync = 0;
					out_state = OUT_STATE_SYNC_MID_NEEDED;
					FPGAWriteBufp[writebufpos] = 0x7f;
					break;

				case OUT_STATE_SYNC_MID_NEEDED:
					out_state = OUT_STATE_SYNC_LO_NEEDED;
					FPGAWriteBufp[writebufpos] = 0x7f;
					break;

				case OUT_STATE_SYNC_LO_NEEDED:
					out_state = OUT_STATE_CONTROL0;
					FPGAWriteBufp[writebufpos] = 0x7f;
					break;

				case OUT_STATE_CONTROL0: //C0
					out_state = OUT_STATE_CONTROL1;
					if (delay_Xmit)
					{
						FPGAWriteBufp[writebufpos] = 0;
						reset_delay_xmit = 1;
					}
					else FPGAWriteBufp[writebufpos] = (unsigned char)XmitBit;                                                    
					//FPGAWriteBufp[writebufpos] &= 1; // not needed ?
					switch (out_control_idx) {
					case 0:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 25:
					case 27:
					case 29:
					case 31:
						FPGAWriteBufp[writebufpos] |= 0;

						if (reset_delay_xmit)
						{
							--delay_Xmit_loop;
							if (delay_Xmit_loop <= 0)
							{
								reset_delay_xmit = 0;
								delay_Xmit = 0;		
					     		out_control_idx = 0;
							}
						}
						break;
					case 1: //TX VFO
						FPGAWriteBufp[writebufpos] |= 2;
						break;
					case 2: //RX1 VFO
						FPGAWriteBufp[writebufpos] |= 4;
						break;
					case 3: //RX2 VFO
						FPGAWriteBufp[writebufpos] |= 6;
						if (diversitymode2) VFOfreq_rx2 = VFOfreq_rx1;
						break;
					case 4: //RX3 VFO
						FPGAWriteBufp[writebufpos] |= 8;
						break;
					case 5: //RX4 VFO
						FPGAWriteBufp[writebufpos] |= 0x0a;
						break;
					case 6: 
						FPGAWriteBufp[writebufpos] |= 0x12; //C0 0001 001x
						break;
					case 7: //Preamp control
						FPGAWriteBufp[writebufpos] |= 0x14; //C0 0001 010x
						break;
					case 8: // Step ATT control
						FPGAWriteBufp[writebufpos] |= 0x16; //C0 0001 011x
						break;
					case 9: // Alex 3 control
						FPGAWriteBufp[writebufpos] |= 0x18; //C0 0001 100x
						break;
					case 10: // Alex 4 control
						FPGAWriteBufp[writebufpos] |= 0x1a; //C0 0001 101x
						break;
					case 11: //RX5 VFO
						FPGAWriteBufp[writebufpos] |= 0x0c;
						break;
					case 12: //RX6 VFO
						FPGAWriteBufp[writebufpos] |= 0x0e;
						break;
					case 13: //RX7 VFO
						FPGAWriteBufp[writebufpos] |= 0x10;
						break;
					case 24: // ADC assignments & ADC Tx ATT
						FPGAWriteBufp[writebufpos] |= 0x1c; //C0 0001 110x
						break;
				    case 26: // CW
						FPGAWriteBufp[writebufpos] |= 0x1e; //C0 0001 111x
						break;
				    case 28: // CW
						FPGAWriteBufp[writebufpos] |= 0x20; //C0 0010 000x
						break;
					case 30: // EER PWM
						FPGAWriteBufp[writebufpos] |= 0x22; //C0 0010 001x
						break;
					case 32: // BPF2
						FPGAWriteBufp[writebufpos] |= 0x24; //C0 0010 010x
						break;

					}

					ControlBytesOut[0] = FPGAWriteBufp[writebufpos];
					break;

				case OUT_STATE_CONTROL1: //C1
					out_state = OUT_STATE_CONTROL2;
					// send sample rate in C1 low 2 bits
					switch (out_control_idx) {
					case 0:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 25:
					case 27:
					case 29:
					case 31:
						FPGAWriteBufp[writebufpos] = (SampleRateIn2Bits & 3) | (C1Mask & 0xfc);
						// printf(" C1Mask: %d\n", C1Mask);
						break;
					case 1:
						FPGAWriteBufp[writebufpos] = (VFOfreq_tx >> 24) & 0xff; // byte 0 of tx freq 
						break;
					case 2:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx1 >> 24) & 0xff; // RX1
						break;
					case 3:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx2 >> 24) & 0xff; // RX2
						break;
					case 4:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx3 >> 24) & 0xff; // RX3
						break;
					case 5:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx4 >> 24) & 0xff; // RX4
						break;
					case 6:
						//	if (HermesPowerEnabled || enable_cw_keyer) { 															     
						if (swr_protect == 0.0f) swr_protect = 0.3f;
						pf = (unsigned char)(OutputPowerFactor * swr_protect);
						FPGAWriteBufp[writebufpos] = (unsigned char)(pf & 0xff); //(OutputPowerFactor & 0xff);
						//printf("outPower: %u\n", OutputPowerFactor);  fflush(stdout);
						/*					}
											else
											{
											FPGAWriteBufp[writebufpos] = 0;
											} */
						break;
					case 7:
						FPGAWriteBufp[writebufpos] = (RX2Preamp | RX1Preamp | MicTipRing | MicBias | MicPTT) & 0x7f;
						// FPGAWriteBufp[writebufpos] = ( MicTR | MicBias ) & 0x30; 
						// printf(" pamp1: %d pamp2: %d\n", RX1Preamp, RX2Preamp);
						break;
					case 8:
						if (diversitymode2)
							FPGAWriteBufp[writebufpos] = (enable_ADC1_step_att | adc1_step_att_data) & 0x3f;
						else
							FPGAWriteBufp[writebufpos] = (enable_ADC2_step_att | adc2_step_att_data) & 0x3f;
						break;
					case 9:
						FPGAWriteBufp[writebufpos] = 0;
						break;
					case 10:
						FPGAWriteBufp[writebufpos] = 0;
						break;
					case 11:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx5 >> 24) & 0xff; // RX5
						break;
					case 12:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx6 >> 24) & 0xff; // RX6
						break;
					case 13:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx7 >> 24) & 0xff; // RX7
						break;
					case 24: // ADC
						FPGAWriteBufp[writebufpos] = ADC_cntrl1 & 0xff;   //RX1, RX2, RX3, and RX4 ADC assignments;
						break;
					case 26:
						FPGAWriteBufp[writebufpos] = enable_cw_keyer & 0x01;
						break;
					case 28:
						FPGAWriteBufp[writebufpos] = (cw_hang_time >> 2) & 0xff;
						break;
					case 30:
						FPGAWriteBufp[writebufpos] = (eer_pwm_min >> 2) & 0xff;
						break;
					case 32: // BPF2
						FPGAWriteBufp[writebufpos] = (XmitBit ? gndrx2ontx : 0 | Alex2HPFMask) & 0xff;
						break;
					}

					ControlBytesOut[1] = FPGAWriteBufp[writebufpos];
					break;

				case OUT_STATE_CONTROL2: //C2
					out_state = OUT_STATE_CONTROL3;

					switch (out_control_idx) {
					case 0:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 25:
					case 27:
					case 29:
					case 31:
						FPGAWriteBufp[writebufpos] = (PennyOCBits | (EClass & XmitBit)) & 0xff;
						break;
					case 1:
						FPGAWriteBufp[writebufpos] = (VFOfreq_tx >> 16) & 0xff; // TX freq
						break;
					case 2:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx1 >> 16) & 0xff; // RX1 freq
						break;
					case 3:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx2 >> 16) & 0xff; // RX2 freq
						break;
					case 4:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx3 >> 16) & 0xff; // RX3 freq
						break;
					case 5:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx4 >> 16) & 0xff; // RX4 freq
						break;
					case 6:
						FPGAWriteBufp[writebufpos] = (MicBoost | LineIn | ApolloFilt | 							
							ApolloTuner | ApolloATU | HermesFilt | AlexManEnable) & 0x7f;
						break;																 
					case 7:
						FPGAWriteBufp[writebufpos] = (LineBoost | MercTxAtten | PureSignal | PennyPresent) & 0xff; 
						break;
					case 8:
						FPGAWriteBufp[writebufpos] = (enable_ADC3_step_att | adc3_step_att_data | reverse_paddles) & 0x7f;
						break;
					case 9:
						FPGAWriteBufp[writebufpos] = Alex3HPFMask & 0x7f;
						break;
					case 10:
						FPGAWriteBufp[writebufpos] = Alex4HPFMask & 0x7f;
						break;
					case 11:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx5 >> 16) & 0xff; // RX5 freq
						break;
					case 12:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx6 >> 16) & 0xff; // RX6 freq
						break;
					case 13:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx7 >> 16) & 0xff; // RX7 freq
						break;
					case 24: // ADC
						FPGAWriteBufp[writebufpos] = ADC_cntrl2 & 0x3f;  //RX5, RX6, RX7 ADC assignments;
						break;
				    case 26:
						FPGAWriteBufp[writebufpos] =  cw_sidetone_volume & 0xff;
						break;
				    case 28:
						FPGAWriteBufp[writebufpos] =  cw_hang_time & 0x03;
						break;
					case 30:
						FPGAWriteBufp[writebufpos] =  eer_pwm_min & 0x03;
						break;
					case 32: // xvtr
						FPGAWriteBufp[writebufpos] = (xvtr_enable << 1) & 0x02;
						break;
					}

					ControlBytesOut[2] = FPGAWriteBufp[writebufpos];
					break;

				case OUT_STATE_CONTROL3: //C3
					out_state = OUT_STATE_CONTROL4;
					switch (out_control_idx) {
					case 0:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
				    case 23:
					case 25:
					case 27:
					case 29:
					case 31:
						FPGAWriteBufp[writebufpos] = (AlexAtten | MercDither | MercPreamp |
							MercRandom | AlexRxAnt | AlexRxOut) & 0xff; 
						break;
					case 1:
						FPGAWriteBufp[writebufpos] = (VFOfreq_tx >> 8) & 0xff;
						break;
					case 2:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx1 >> 8) & 0xff;
						break;
					case 3:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx2 >> 8) & 0xff;
						break;
					case 4:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx3 >> 8) & 0xff;
						break;
					case 5:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx4 >> 8) & 0xff;
						break;
					case 6:
						FPGAWriteBufp[writebufpos] = (AlexTRRelay | AlexHPFMask) & 0xff;
						break;
					case 7: // (0, 1 = open drain pins 1,2) (2, 3 = TTL pins 3,4)
						FPGAWriteBufp[writebufpos] = (UserOut0 | UserOut1 | UserOut2 | UserOut3) & 0x0f;
						break;
					case 8:
						FPGAWriteBufp[writebufpos] = (cw_speed | cw_mode) & 0xff;
						break;
					case 9:
						FPGAWriteBufp[writebufpos] = Alex3LPFMask & 0x7f;
						break;
					case 10:
						FPGAWriteBufp[writebufpos] = Alex4LPFMask & 0x7f;
						break;
					case 11:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx5 >> 8) & 0xff;
						break;
					case 12:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx6 >> 8) & 0xff;
						break;
					case 13:
						FPGAWriteBufp[writebufpos] = (VFOfreq_rx7 >> 8) & 0xff;
						break;
					case 24: // ADC
						FPGAWriteBufp[writebufpos] = tx_att_data & 0x1f;
						break;
				    case 26:
						FPGAWriteBufp[writebufpos] =  cw_ptt_delay & 0xff;
						break;
				    case 28:
						FPGAWriteBufp[writebufpos] =  (cw_sidetone_freq >> 4) & 0xff;
						break;
					case 30:
						FPGAWriteBufp[writebufpos] =  (eer_pwm_max >> 2) & 0xff;
						break;
					case 32: // BPF2
						FPGAWriteBufp[writebufpos] = 0;
						break;
					}

					ControlBytesOut[3] = FPGAWriteBufp[writebufpos];
					break;

				case OUT_STATE_CONTROL4: //C4
					out_state = OUT_STATE_MON_LEFT_HI_NEEDED;
					// write_buf[writebufpos] = ControlBytesIn[4];
					switch (out_control_idx) {
					case 0:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 25:
					case 27:
					case 29:
					case 31:
						FPGAWriteBufp[writebufpos] = AlexTxAnt | NRx | Duplex | diversitymode2 << 7;
						break;

					case 1:
						FPGAWriteBufp[writebufpos] = VFOfreq_tx & 0xff;
						break;
					case 2:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx1 & 0xff;
						break;
					case 3:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx2 & 0xff;
						break;
					case 4:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx3 & 0xff;
						break;
					case 5:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx4 & 0xff;
						break;
					case 6:
						FPGAWriteBufp[writebufpos] = AlexLPFMask & 0x7f;
						break;
					case 7:
						FPGAWriteBufp[writebufpos] = (enable_ADC1_step_att | adc1_step_att_data) & 0x3f;
						break;
					case 8:
						FPGAWriteBufp[writebufpos] = (cw_weight | enable_cw_spacing) & 0xff;
						break;
					case 9:
						FPGAWriteBufp[writebufpos] = 0;
						break;
					case 10:
						FPGAWriteBufp[writebufpos] = 0;
						break;
					case 11:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx5 & 0xff;
						break;
					case 12:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx6 & 0xff;
						break;
					case 13:
						FPGAWriteBufp[writebufpos] = VFOfreq_rx7 & 0xff;
						break;
					case 24: // ADC
						FPGAWriteBufp[writebufpos] = 0;
						break;
				    case 26:
						FPGAWriteBufp[writebufpos] = 0;
						break;
				    case 28:
						FPGAWriteBufp[writebufpos] =  cw_sidetone_freq & 0x0f;
						break;
					case 30:
						FPGAWriteBufp[writebufpos] =  eer_pwm_max & 0x03;
						break;
					case 32: // BPF2
						FPGAWriteBufp[writebufpos] = 0;
						break;
					}

					ControlBytesOut[4] = FPGAWriteBufp[writebufpos];
					++out_frame_idx;

					switch(out_control_idx) {
					case 0:
						out_control_idx = 1;
						break;
					case 1: // tx vfo
						//out_control_idx = 2;
						out_control_idx = 14;
						break;
					case 2: //rx1 vfo
						// out_control_idx = 3;
						out_control_idx = 15;
						break;
					case 3: //rx2 vfo
						//out_control_idx = 4;
						out_control_idx = 16;
						break;
					case 4: //rx3 vfo
						//out_control_idx = 5;
						out_control_idx = 17;
						break; 
					case 5: //rx4 vfo
						//out_control_idx = 6; //11;
						out_control_idx = 18;
						break;
					case 6: //Alex filters
						//out_control_idx = 7;
						out_control_idx = 19;
						break;
					case 7: //Preamp controls
						//out_control_idx = 8; //  Goto Alex 2 Controls
						out_control_idx = 20;
						break;
					case 8: // Step ATT control
						// out_control_idx = 0;
						out_control_idx = 21;
						break;

					case 9: // Alex 3 control
						out_control_idx = 10;
						break;
					case 10: // Alex 4 control
						out_control_idx = 0; 
						break;

					case 11: // rx5 vfo
						out_control_idx = 22;
						break;

					case 12: //rx6 vfo
						out_control_idx = 13;
						break;
					case 13: //rx7 vfo
						out_control_idx = 6;
						break; 

					case 14:
						out_control_idx = 2;
						break;
					case 15: 
						out_control_idx = 3;
						break;
					case 16: 
						out_control_idx = 4;
						break;
					case 17: 
						out_control_idx = 5;
						break;
					case 18: 
						out_control_idx = 6;
						break;
					case 19: 
						out_control_idx = 7;
						break;
					case 20: 
						out_control_idx = 8; 
						break;
					case 21: 
						out_control_idx = 11; 
						break;
					case 22: 
						out_control_idx = 24;//1; 
						break;		

					case 23: // C0 0
						out_control_idx = 26; 
						break;
					case 24:  // ADC
						out_control_idx = 23; 
						break;
					case 25: // C0 0
						out_control_idx = 28; 
						break;		
				    case 26: 
						out_control_idx = 25; 
						break;
					case 27: // C0 0
						out_control_idx = 30; 
						break;
					case 28: 
						out_control_idx = 27;
						break;		
					//case 29:
					//	out_control_idx = 30; 
					//	break;
					case 30: 
						out_control_idx = 31; // 0;
						break;		
					case 31:
						out_control_idx = is_orion_mkii ? 32 : 0;
						break;
					case 32: // BPF2
						out_control_idx = 0;
						break;
					}
	/*				if (reset_control_idx)
					{
						out_control_idx = 0;
						reset_control_idx = 0;
					}*/
					//if (reset_delay_xmit)
					//{
					//	reset_delay_xmit = 0;
					//	delay_Xmit = 0;
					//}
					break; 

				case OUT_STATE_MON_LEFT_HI_NEEDED:
					out_state = OUT_STATE_MON_LEFT_LO_NEEDED;
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					break;

				case OUT_STATE_MON_LEFT_LO_NEEDED:
					out_state = OUT_STATE_MON_RIGHT_HI_NEEDED;
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					break;

				case OUT_STATE_MON_RIGHT_HI_NEEDED:
					out_state = OUT_STATE_MON_RIGHT_LO_NEEDED;
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					break;

				case OUT_STATE_MON_RIGHT_LO_NEEDED:
					out_state = OUT_STATE_LEFT_HI_NEEDED;
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					break;

				case OUT_STATE_LEFT_HI_NEEDED:
					out_state = OUT_STATE_LEFT_LO_NEEDED;
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					break;

				case OUT_STATE_LEFT_LO_NEEDED:
					out_state = OUT_STATE_RIGHT_HI_NEEDED;
					if (enable_cw_keyer) {
                    FPGAWriteBufp[writebufpos] = (cw_dash | cw_dot | cwx) & 0x07;
					}
					else {
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					}
					++outbufpos;
					break;

				case OUT_STATE_RIGHT_HI_NEEDED:
					out_state = OUT_STATE_RIGHT_LO_NEEDED;
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					break;

				case OUT_STATE_RIGHT_LO_NEEDED:
					FPGAWriteBufp[writebufpos] = outbufp[outbufpos];
					++outbufpos;
					++out_sample_pairs_this_sync;
					if ( out_sample_pairs_this_sync == 63 ) {
						out_state = OUT_STATE_SYNC_HI_NEEDED;
					}
					else {
						out_state = OUT_STATE_MON_LEFT_HI_NEEDED;
					}
					break;

				default:
					printf("internal error - bad out_state\n");
					out_state = OUT_STATE_SYNC_HI_NEEDED;
					break;
				}       /* switch (out_state) */

				++writebufpos;
				//++out_stream_sync_count;
				//if ( out_stream_sync_count >=  512  ) {
				//      out_stream_sync_count = 0;
				//}
				if ( outbufpos >= outbuflen ) {  // we've consumed output buf from powrsdr dsp -- get a new one if one avail
					freeFIFOdata(outbufp);
					outbufp = getNewOutBufFromFIFO();
					if ( outbufp == NULL ) {  // no data bail out
						have_out_buf = 0;
						// printf("no new buffer - bail out: out_sync: %d out_state: %d total_written: %d\n", out_stream_sync_count, out_state, total_written);  fflush(stdout);
						break;
					}
					else {
						have_out_buf = 1;
						outbufpos = 0;
						// printf("new buffer: out_sync: %d out_state: %d total_written: %d\n", out_stream_sync_count, out_state, total_written); fflush(stdout);
					}
				}
			}  /* while writebufpos < FPGAWriteBufSize */

			if ( writebufpos >= FPGAWriteBufSize ) {  // write the buffer if we've filled it.
				wrote_frame = 1;
				if ( !ForceNoSend ) {
#ifdef XYLO
					numwritten = XyloBulkWrite(XyloH, 2, FPGAWriteBufp, FPGAWriteBufSize);
#endif
#ifdef OZY	
					if ( FPGATestMode ) { 
						int loop_count = FPGAWriteBufSize / 512; 
						int j; 
						for ( j = 0; j < loop_count; j++ ) { 
							memcpy(FPGAWriteBufp + 8 + (512*j), fpga_test_buf+8, 512-8); 
						} 
					} 
					if ( isMetis ) { 
						numwritten = MetisBulkWrite(0x02, FPGAWriteBufp, FPGAWriteBufSize);
					}
					else { 
						numwritten = OzyBulkWrite(OzyH, 0x02, FPGAWriteBufp, FPGAWriteBufSize);
					}
#endif
				}
				else {
					numwritten = FPGAWriteBufSize;
				}
				// numwritten = OUTBUF_SIZE;

#ifdef SAVE_WRITES_TO_BUFFER
				memcpy(WriteSaveBuf[SaveWritesIdx], FPGAWriteBufp, FPGAWriteBufSize);
				++SaveWritesIdx;
				if ( SaveWritesIdx >= NUM_WRITE_BUFS_TO_SAVE ) {
					SaveWritesIdx = 0;
				}

#endif


#ifdef PERF_DEBUG
				now_t = getPerfTicks();
				if ( last_write_t != 0 ) {
					write_interval_t = now_t - last_write_t;
					updateHLA(&WriteHLA, write_interval_t);
				}
				last_write_t = now_t;
#endif
#ifdef TEST_WRITE
				/* numwritten = */  testWrite(write_buf, OUTBUF_SIZE);
#endif
				total_written += numwritten;
#ifdef WRITE_MULTI_PER_READ
				++frames_written_this_read;
#endif
				++frames_written_this_read;
				//                              printf("buffer write: out_sync: %d out_state: %d total_written: %d\n", out_stream_sync_count, out_state, total_written); fflush(stdout);

				if ( numwritten != FPGAWriteBufSize ) {
					printf("warning: iothread short write! numwritten=%d\n", numwritten);
				}
				writebufpos = 0;
			}
		} /* if have_out_buf */
#ifdef PERF_DEBUG
		write_bottom_t = getPerfTicks();


		if ( PerfDataIdx < PERF_DATA_COUNT ) {
			PERF_RECORD *pdp = &(PerfData[PerfDataIdx]);
			++PerfDataIdx;
			// pdp->RxFifoAvail = RxFifoAvail;
			// pdp->RxFramePlaying = RxFramePlaying;
			pdp->wrote_frame = wrote_frame;
			pdp->ok_to_write = ok_to_write;
			pdp->rt = read_t;
			pdp->wt = write_bottom_t - write_top_t;
			pdp->drt = read_stop_t - last_read_t;
			pdp->dwt = write_bottom_t - last_write_t;
			pdp->ticks = now_t;
			pdp->cb = complete_blocks;
			pdp->RxOverrun = RxOverrun;
			pdp->TxOverrun = TxOverrun;
			pdp->frame_sending = out_frame_idx;
			pdp->sync_lost = LostSyncCount;
		}


		//printf("fa: %d fp: %d w: %d okw: %d rt: %I64d wt: %I64d drt: %I64d dwt: %I64d cb: %d\n",
		//          RxFifoAvail, RxFramePlaying, wrote_frame, ok_to_write, perfTicksToNanos(read_t), perfTicksToNanos(write_bottom_t - write_top_t),
		//              perfTicksToNanos(read_stop_t - last_read_t), perfTicksToNanos(write_bottom_t - last_write_t), complete_blocks);
		last_read_t = read_stop_t;
		last_write_t = read_stop_t;
#endif
	}  /* while keep_running */
}




// stops and kill's the IOThread
// returns 0 on success, !0 on failure
// error returns
// 1 - thread not running
// 2 - pthread_join failed
int IOThreadStop() {
	int rc;
	void *junk;

	if ( io_keep_running == 0 ) {  // not running
		return 1;
	}
	io_keep_running = 0;  // flag to stop
	rc = pthread_join(IOThreadID, &junk);
	if ( rc != 0 ) {
		fprintf(stderr, "Warning: io-thread.c, pthread_join failed with rc=%d\n", rc);
		return 2;
	}
	return 0;
}


// this is the main thread that reads/writes data to/from the Xylo
// when this is called the Xylo device is open
// this routine does setup, calls IOThreadMainLoop to do the real work and cleans up on termination
void *IOThreadMain(void *argp) {
                io_keep_running = 1;
                IOThreadRunning = 1;
#ifndef LINUX
                SetThreadPriority(GetCurrentThread(), /* THREAD_PRIORITY_ABOVE_NORMAL */  THREAD_PRIORITY_TIME_CRITICAL /* THREAD_PRIORITY_HIGHEST  */ );
#else
#warning message("info - LINUX code missing ... set priority!")
#endif
            sem_post(&IOThreadInitSem); // tell launching thread we're rockin and rollin
                complete_blocks = 0;
                LostSyncCount = 0;
                SyncGainedCount = 0;
                bytes_processed = 0;
                SendOpprotunity = 0;
                NotOKtoSendCount = 0;


#ifdef TEST_WRITE
                testOutF = fopen("testout.dat", "wb");
#endif
#ifdef PERF_DEBUG
                initHLA(&ReadHLA);
                initHLA(&InConvertHLA);
        initHLA(&OutConvertHLA);
        initHLA(&CallbackHLA);
                initHLA(&WriteHLA);
#endif

                IOThreadMainLoop();
                IOThreadRunning = 0;
#ifdef TEST_WRITE
                if ( testOutF != NULL )  {
                        fclose(testOutF);
                        testOutF = NULL;
                }
#endif
                printf("iothread: complete_blocks: %d\niothread: LostSyncCount: %d\nbytes_processed: %d\nSyncGainedCount: %d\nsend opprotunities: %d\nNotOKTosendCount: %d\n",
                                      complete_blocks, LostSyncCount, bytes_processed, SyncGainedCount, SendOpprotunity, NotOKtoSendCount);
                fflush(stdout);

#ifdef SAVE_WRITES_TO_BUFFER
                {
                        //
                        // dump out the last set of buffers we wrote to the fpga
                        // save_writes_idx points to next buffer to fill, so we start with the buffer immediately following it and
                        // dump buffs until we get
                        FILE *buf_file;
                        int curridx;

                        buf_file = fopen("fpga-writes.dat", "wb");
                        if ( buf_file != NULL ) {
                                curridx = SaveWritesIdx + 1;
                                if ( curridx >= NUM_WRITE_BUFS_TO_SAVE ) {
                                        curridx = 0;
                                }
                                while ( curridx != SaveWritesIdx ) {
                                        fwrite(WriteSaveBuf[curridx], 1, FPGAWriteBufSize /*OUTBUF_SIZE*/, buf_file);
                                        ++curridx;
                                        if ( curridx >= NUM_WRITE_BUFS_TO_SAVE ) {
                                                curridx = 0;
                                        }
                                }
                                fclose(buf_file);
                        }
                }
#endif

#ifdef SAVE_READS_TO_BUFFER
                {
                        //
                        // dump out the last set of buffers we wrote to the fpga
                        // save_writes_idx points to next buffer to fill, so we start with the buffer immediately following it and
                        // dump buffs until we get
                        FILE *buf_file;
                        int curridx;

                        buf_file = fopen("fpga-reads.dat", "wb");
                        if ( buf_file != NULL ) {
                                curridx = SaveReadsIdx + 1;
                                if ( curridx >= NUM_READ_BUFS_TO_SAVE ) {
                                        curridx = 0;
                                }
                                while ( curridx != SaveReadsIdx ) {
                                        fwrite(ReadSaveBuf[curridx], 1, FPGAReadBufSize /*INBUF_SIZE*/ , buf_file);
                                        ++curridx;
                                        if ( curridx >= NUM_READ_BUFS_TO_SAVE ) {
                                                curridx = 0;
                                        }
                                }
                                fclose(buf_file);
                        }
                }
#endif


#ifdef PERF_DEBUG
                // printHLA(&ReadHLA, /* stdout, */ "ReadHLA (perfticks): ");
                // ReadHLA.sum = perfTicksToNanos(ReadHLA.sum);
                // ReadHLA.hi = perfTicksToNanos(ReadHLA.hi);
                // ReadHLA.lo = perfTicksToNanos(ReadHLA.lo);

                printHLANano(&ReadHLA, /* stdout, */ "ReadHLA (ns): ");

                printHLANano(&InConvertHLA, "InConvert (ns): ");
                // printHLA(&InConvertHLA, "InConvert: ");

                printHLANano(&OutConvertHLA, "OutConvert (ns): ");
                // printHLA(&OutConvertHLA, "OutConvert: ");

                printHLANano(&CallbackHLA, "Callback (ns): ");
                printHLANano(&WriteHLA, "Write Interval (ns): ");
                // printHLA(&CallbackHLA, "Callback: ");

                // dump out the detailed data

                if ( PerfDataIdx > 0 ) {
                        FILE *f;
                        int i;
                        __int64 last_nanos = 0;
                        __int64 now_nanos;
                        PERF_RECORD *pdp;
                        f = fopen("perfdata.txt", "w");
                        if ( f != NULL ) {
                                fprintf(f, "iothread: complete_blocks: %d\niothread: LostSyncCount: %d\nbytes_processed: %d\nSyncGainedCount: %d\nsend opprotunities: %d\nNotOKTosendCount: %d\n",
                                     complete_blocks, LostSyncCount, bytes_processed, SyncGainedCount, SendOpprotunity, NotOKtoSendCount);
                                for ( i = 0; i < PerfDataIdx; i++ ) {
                                        pdp = &(PerfData[i]);
                                        now_nanos = perfTicksToNanos(pdp->ticks);
                                        fprintf(f,"i: %d   fs: %3d sl: %d txo: %3d rxo: %3d w: %d okw: %d rt: %8I64u wt: %8I64d drt: %8I64u dwt: %8I64u cb: %d t: %I64u dt: %8I64u\n",  i,
                                         pdp->frame_sending, pdp->sync_lost, pdp->TxOverrun, pdp->RxOverrun, pdp->wrote_frame, pdp->ok_to_write, perfTicksToNanos(pdp->rt), perfTicksToNanos(pdp->wt),
                                        perfTicksToNanos(pdp->drt), perfTicksToNanos(pdp->dwt), pdp->cb, now_nanos, now_nanos-last_nanos );
                                        last_nanos = now_nanos;
                                }
                                fclose(f);
                        }
                }
#endif
                return NULL;
}

