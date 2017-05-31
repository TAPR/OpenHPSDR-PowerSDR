/*A
 * JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
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


// PowerSDR-Interface.c - this file contains routines called from PowerSDR to
// access the FPGA based USB connected sound device
//

//#define KD5TFDVK6APHAUDIO_EXPORTS
#include <stdio.h>
#include "KD5TFD-VK6APH-Audio.h"
#include "private.h"
#include "Ozyutils.h"

const int numInputBuffs = 12;

//
// StartAudio -- called when we need to start reading audio and passing it to PowerSDR via the callback.
//
// This rountine allocates needed buffers, resamplers, and opens the device to read from.
// If it fails it cleans up and return non zero, returns 0 on success
// Things allocated/created/opened here need to be freed/destroyed/closed in StopAudio
//
// error returns:
//      2 - could not open Xylo
//  3 - bad state - already started
//  4 - failed creating IOThread
//  5 - failed to alloc mem for callback input buffer
//  6 - failed to alloc mem for input buffers
//  7 - failed creating fifo for io -> callback
//  8 - failed creating callback -> io fifo
//  9 - failed creating callback thread
// 10 - failed alloc of callback return buffer
// 13 - failed creating resampler
// 14 - failed alloc of low level USB buffers
//
// this is the 8 in 8 out (actually 5 in 4 out) version of start audio
//

KD5TFDVK6APHAUDIO_API int StartAudioNative(int sample_rate, int samples_per_block,
                      int (__stdcall *callbackp)(void *inp, void *outp, int framcount, void *timeinfop, int flags, void *userdata),
                                           int sample_bits, int no_send)
{
        int rc;
        int myrc = 0;
        int *in_sample_bufp = NULL;
       // float *bufp = NULL;
	    //float *INbufp = NULL;
	    //float *OUTbufp = NULL;

        //
        // DttSP runs at a single sampling rate - the IQ in sampling rate.  The Janus hardware supports selection of (192,96,48)
        // khz on IQ in, 48 khz on mic in and IQ and LR output.
        //
        // what's our sampling rate?  The code is setup to read IQ and 384, 192, 96 or 48 khz.  Samples on the mic input match
        // the IQ sample rate, but only the 1st of (4/2/1) sample is valid others are duplicated because the mic is always sampled
        // at 48 khz.  If the IQ rate does not equal 48 khz the mic data is resampled to match the IQ rate.
        //
        // Output (xmit IQ, and LR audio) is always at 48 khz - downsampled by dropping samples
        //
		int nchannels = 2 * nreceivers + 2;
		int i;

        HaveSync = 0;
        SampleRate = sample_rate;
        FPGAWriteBufSize = 512; // 512;
        FPGAReadBufp = NULL;
        FPGAWriteBufp = NULL;
        SampleBits = sample_bits;
        ForceNoSend = no_send;
        IQConversionDivisor = (float)8388607.0;  // (2**23)-1
        if ( SampleBits == 16 ) {
                IQConversionDivisor = (float)32767.0;
        }


        /* right size our buffers -- if the dsp is running small bufs, we need to
         * do the same.
         */

        if ( samples_per_block < 1024 ) {
                FPGAWriteBufSize = 1024;
        }
		else if ( samples_per_block >= 2048 ) {
                FPGAWriteBufSize = 1024;
        }

        else {
                FPGAWriteBufSize = 2048;
        }

		if ( isMetis ) {  // force write buf size on Metis -- we can only send 2x512 usb frames in an enet frame for Metis so lock this down 
						  // 
			FPGAWriteBufSize = 1024; 
		} 

        // setup sampling rate, buffer sizes  create resampler if needed
        switch ( SampleRate ) {
                case 48000:
                        SampleRateIn2Bits = 0;
                        FPGAReadBufSize = FPGAWriteBufSize; // was 512;
                        MicResamplerP = NULL;
                        break;
                case 96000:
                        SampleRateIn2Bits = 1;
                        FPGAReadBufSize = 2 * FPGAWriteBufSize;  // was 1024;
                        //MicResamplerP = NewResamplerF(48000, 96000);
						MicResamplerP = create_resampleFV(48000, 96000);
                        if ( MicResamplerP == NULL ) {
                                fprintf(stderr, "Warning NewResamplerF failed in PowerSDR-Interface.c\n");
                        }
                        break;
                case 192000:
                        SampleRateIn2Bits = 2;
                        FPGAReadBufSize = 4 * FPGAWriteBufSize;  // was 2048;
                        //MicResamplerP = NewResamplerF(48000, 192000);
						MicResamplerP = create_resampleFV(48000, 192000);
                        if ( MicResamplerP == NULL ) {
                                fprintf(stderr, "Warning NewResamplerF failed in PowerSDR-Interface.c\n");
                        }
                        break;
                 case 384000:
                        SampleRateIn2Bits = 3;
                        FPGAReadBufSize = 8 * FPGAWriteBufSize;  // was 2048;
                        //MicResamplerP = NewResamplerF(48000, 384000);
						MicResamplerP = create_resampleFV(48000, 384000);
                        if ( MicResamplerP == NULL ) {
                                fprintf(stderr, "Warning NewResamplerF failed in PowerSDR-Interface.c\n");
                        }
                        break;
                        default:
                        SampleRateIn2Bits = 2;
                        break;
        }
        BlockSize = samples_per_block;
        Callback = callbackp;

       // printf("sa: samples_per_block: %d\n", samples_per_block); fflush(stdout);

        // make sure we're not already opened
        //
        if ( OzyH != NULL || IOThreadRunning  ) {
                return 3;
        }

        do { // once
                // allocate buffers for callback buffers
			INbufp = (float *)calloc(1, sizeof(float) * BlockSize * numInputBuffs);
			for (i = 0; i < numInputBuffs; i++)
				INpointer[i] = INbufp + (i * BlockSize);

  		    OUTbufp = (float *)calloc(1, sizeof(float) * BlockSize * 8);
			for (i = 0; i < 8; i++)
				OUTpointer[i] = OUTbufp + (i * BlockSize);

			// allocate buffers for inbound and outbound samples
                in_sample_bufp = (int *)calloc(1, sizeof(int) * BlockSize * nchannels);  //(6) 6 channels in 
                if ( in_sample_bufp == NULL ) {
                        myrc = 6;
                        break;
                }
                IOSampleInBufp = in_sample_bufp;
                // printf("IOSample buffer at: 0x%08x, len=%d\n", (unsigned long)in_sample_bufp, 4*BlockSize*sizeof(int));

                CBSampleOutBufp = (short *)calloc(1, sizeof(short) * BlockSize * 8); // 4 out
                if ( CBSampleOutBufp == NULL ) {
                        myrc = 10;
                        break;
                }
                // printf("CBSampleOut buffer at: 0x%08x, len=%d\n", (unsigned long)CBSampleOutBufp, 4*BlockSize*sizeof(short));  fflush(stdout);

                if ( MicResamplerP != NULL ) {  // we're going to resample mic data - need a buffer for it
                        MicResampleBufp = (float *)calloc(1, samples_per_block * sizeof(float));
                        if ( MicResampleBufp == NULL ) {
                                myrc = 13;
                                break;
                        }
                }

                // allocate buffer for low level USB I/O
                FPGAReadBufp = (char *)calloc(1, sizeof(char) * (FPGAReadBufSize + FPGAWriteBufSize));
                if ( FPGAReadBufp == NULL ) {
                        myrc = 14;
                        break;
                }
                /*else*/
                FPGAWriteBufp = FPGAReadBufp + FPGAReadBufSize;

				if ( !isMetis ) { 
					// go open the ozy
					OzyH = OzyOpen();
					if ( OzyH == NULL ) {
						    myrc =  2;
							break;
					}
					// printf("Ozy openend!\n");											
				}
				else { // is Metis 					
					MetisStartReadThread(); 
				} 

                // create FIFO for inbound samples
                InSampleFIFOp = createFIFO();
                if ( InSampleFIFOp == NULL ) {
                        myrc = 7;
                        break;
                }

                OutSampleFIFOp = createFIFO();
                if ( OutSampleFIFOp == NULL ) {
                        myrc = 8;
                        break;
                }

                // start the io_thread
                //
                sem_init(&IOThreadInitSem, 0, 0);
                rc = pthread_create(&IOThreadID, NULL,  IOThreadMain, NULL);
                if ( rc != 0 ) {  // failed
                        fprintf(stderr, "pthread_created failed on IOThread w/ rc=%d\n", rc);
                        myrc =  4;
                        break;
                }
                sem_wait(&IOThreadInitSem);  // wait for the thread to get going

#ifdef EP4DRAIN_ENABLED 
				// 
				// start the ep4 drain thread 
				// 
				sem_init(&EP4DrainThreadInitSem, 0, 0);
                rc = pthread_create(&EP4DrainThreadID, NULL,  EP4DrainThreadMain, NULL);
                if ( rc != 0 ) {  // failed
                        fprintf(stderr, "pthread_created failed on EP4DrainThread w/ rc=%d\n", rc);
                        /* myrc =  4; */ 
                        break;
                }
                sem_wait(&EP4DrainThreadInitSem);  // wait for the thread to get going
#endif 

        } while ( 0 );

        if ( myrc != 0 ) {  // we failed -- clean up
    
                if ( FPGAReadBufp != NULL ) {
                        free(FPGAReadBufp);
                }

                if ( in_sample_bufp != NULL ) {
                        IOSampleInBufp = NULL;
                        CBSampleOutBufp = NULL;
                        free(in_sample_bufp);
                }
                if ( CBSampleOutBufp != NULL ) {
                        short *tmp = CBSampleOutBufp;
                        CBSampleOutBufp = NULL;
                        free(tmp);
                }
                if ( InSampleFIFOp != NULL ) {
                        destroyFIFO(InSampleFIFOp);
                        InSampleFIFOp = NULL;
                }
                if ( OutSampleFIFOp != NULL ) {
                        destroyFIFO(OutSampleFIFOp);
                        OutSampleFIFOp = NULL;
                }

				if (INbufp)   free(INbufp);
				if (OUTbufp)  free(OUTbufp);
#ifdef XYLO
                if ( XyloH != NULL ) {
                        XyloClose(XyloH);
                        XyloH = NULL;
                }
#endif
#ifdef OZY
                if ( OzyH != NULL ) {
                        OzyClose(OzyH);
                        OzyH = NULL;
                }
#endif
				if ( isMetis ) { 
					MetisStopReadThread(); /* is a no op if not running */ 
				}
        }
        DotDashBits = 0;
        // printf("StartAudioNative - myrc: %d\n", myrc);
        return myrc;

}

//
// StopAudio -- undo what start audio did.  Close/Free/Destroy that which StartAudio Opened/Alloc'd/Created.
//
KD5TFDVK6APHAUDIO_API void StopAudio() {
        int rc;
        printf("stop audio called\n");  fflush(stdout);
        rc = IOThreadStop();
        if ( rc != 0 ) {
                fprintf(stderr, "Warning: IOThreadStop failed with rc=%d\n", rc);
        }
        printf("iothread stopped\n");   fflush(stdout);

		if ( isMetis ) { 
			MetisStopReadThread(); 
		} 
        if ( InSampleFIFOp != NULL ) {
                destroyFIFO(InSampleFIFOp);
                InSampleFIFOp = NULL;
        }
        printf("fifo destroyted\n");   fflush(stdout);


        if ( IOSampleInBufp != NULL ) {
                free(IOSampleInBufp);
                IOSampleInBufp = NULL;
        }

		if (INbufp)   free(INbufp);
		if (OUTbufp)  free(OUTbufp);

        if ( OzyH != NULL ) {
                OzyClose(OzyH);
                OzyH = NULL;
        }
        printf("Ozy closed\n");   fflush(stdout);


        if ( MicResamplerP != NULL ) {
                //DelPolyPhaseFIRF(MicResamplerP);
				destroy_resampleFV(MicResamplerP);
                MicResamplerP = NULL;
        }

        if ( FPGAReadBufp != NULL ) {
                free(FPGAReadBufp);
                FPGAReadBufp = NULL;
                FPGAWriteBufp = NULL;
        }

		DotDashBits = 0;

        return;
}

/*
C0
0 0 0 0 0 0 0 0
          | | |
          | | + --------- PTT  (1 = active, 0 = inactive), GPIO[23]= Ozy J8-8, Hermes J16-1 
          | +------------ DASH (1 = active, 0 = inactive), GPIO[21]= Ozy J8-6, Hermes J6-2
          +-------------- DOT  (1 = active, 0 = inactive), GPIO[22]= Ozy J8-7, Hermes J6-3 
*/

int getDDPTTcount = 0; 
int last_DDP = 0; 

KD5TFDVK6APHAUDIO_API int nativeGetDotDashPTT() {
		
	if ( last_DDP != (DotDashBits & 0x7) ) { 
		last_DDP = DotDashBits & 0x7; 
	//	printf("ddp: 0x%04x\n", last_DDP);  fflush(stdout); 
	}		
    return DotDashBits & 0x7;
}

/*
C1 
0 0 0 0 0 0 0 0
  | | | | | | |
  | | | | | | +---------- LT2208 Overflow (1 = active, 0 = inactive)
  | | | | | +------------ Hermes I01 (0 = active, 1 = inactive)
  | | | | +-------------- Hermes I02 (0 = active, 1 = inactive)
  | | | +---------------- Hermes I03 (0 = active, 1 = inactive)
  | | +------------------ Hermes I04 (0 = active, 1 = inactive)
  | +-------------------- Cyclops PLL locked (0 = unlocked, 1 = locked)
  +---------------------- Cyclops - Mercury frequency changed, bit toggles   
 */

KD5TFDVK6APHAUDIO_API int getAndResetADC_Overload() { 
	int n; 
	if ( !isMetis ) { 
	getI2CBytes(I2C_MERC1_ADC_OFS);
	getI2CBytes(I2C_MERC2_ADC_OFS);
	//getI2CBytes(I2C_MERC3_ADC_OFS);
	//getI2CBytes(I2C_MERC4_ADC_OFS);
	}

	//n = ADC_Overloads; 
	//ADC_Overloads = 0; 
	n = ADC_Overload | ADC1_Overload | ADC2_Overload | ADC3_Overload;// | ADC4_Overload;
	return n; 
} 

KD5TFDVK6APHAUDIO_API int getUserI01() { 

	return User_I01; // IO4
} 

KD5TFDVK6APHAUDIO_API int getUserI02() { 

	return User_I02; // IO5
}

KD5TFDVK6APHAUDIO_API int getUserI03() { 

	return User_I03;  // IO6
}

KD5TFDVK6APHAUDIO_API int getUserI04() { 

	return User_I04; // IO8
}

//C2 – 	Mercury software serial number  (0 to 255) - set to 0 when Hermes

KD5TFDVK6APHAUDIO_API int getMercuryFWVersion() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_MERC1_FW);
	}
	return MercuryFWVersion; 
} 

//C3 -  Penelope software serial number (0 to 255) – set to 0 when Hermes

KD5TFDVK6APHAUDIO_API int getPenelopeFWVersion() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_PENNY_FW);
	}
	return PenelopeFWVersion; 
} 

//C4 -  Ozy/Magister or Metis or Hermes software serial number (0 to 255) 

KD5TFDVK6APHAUDIO_API int getOzyFWVersion() { 
	return OzyFWVersion; 
} 

/*
C0
0 0 0 0 1 x x x    
C1 – Bits 15-8 of Forward Power from Penelope or Hermes* (AIN5)
C2 - Bits 7-0  of Forward Power from Penelope or Hermes* (AIN5)
C3 – Bits 15-8 of Forward Power from Alex or Apollo*(AIN1)
C4 – Bits 7-0  of Forward Power from Alex or Apollo*(AIN1)
*/

KD5TFDVK6APHAUDIO_API int getFwdPower() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_PENNY_ALC);
	}
	return FwdPower; 
} 

KD5TFDVK6APHAUDIO_API int getAlexFwdPower() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_PENNY_FWD);
	}
	return AlexFwdPower; 
} 

/*
C0
0 0 0 1 0 x x x    
C1 – Bits 15-8 of Reverse Power from Alex or Apollo*(AIN2)
C2 - Bits 7-0  of Reverse Power from Alex or Apollo*(AIN2)
C3 – Bits 15-8 of AIN3 from Penny or Hermes*
C4 – Bits 7-0  of AIN3 from Penny or Hermes*
*/

KD5TFDVK6APHAUDIO_API int getRefPower() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_PENNY_REV);
	}
	return RefPower; 
} 

KD5TFDVK6APHAUDIO_API int getAIN3() {
	return AIN3;
}

/*
C0
0 0 0 1 1 x x x    
C1 – Bits 15-8 of AIN4 from Penny or Hermes*
C2 - Bits 7-0  of AIN4 from Penny or Hermes*
C3 – Bits 15-8 of AIN6,13.8v supply on Hermes*
C4 – Bits 7-0  of AIN6,13.8v supply on Hermes*
*/

KD5TFDVK6APHAUDIO_API int getAIN4() {
	return AIN4;
}

KD5TFDVK6APHAUDIO_API int getHermesDCVoltage() { 
	return HermesDCV;
} 

/*
C0
0 0 1 0 0 x x x    
C1 
0 0 0 0 0 0 0 0
|           | |
|           | +---------- Mercury 1 LT2208 Overflow (1 = active, 0 = inactive)
+-----------+------------ Mercury 1 software version (0 to 127)
(NOTE: This is a duplicate of C0 = 00000xxx to maintain software
 compatibility)
 */


/*
C2 
0 0 0 0 0 0 0 0
|           | |
|           | +---------- Mercury 2 LT2208 Overflow (1 = active, 0 = inactive)
+-----------+------------ Mercury 2 software version (0 to 127)
*/

KD5TFDVK6APHAUDIO_API int getMercury2FWVersion() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_MERC2_FW);
	}
	return Mercury2FWVersion; 
} 

/*
C3 
0 0 0 0 0 0 0 0
|           | |
|           | +---------- Mercury 3 LT2208 Overflow (1 = active, 0 = inactive)
+-----------+------------ Mercury 3 software version (0 to 127) 
*/

KD5TFDVK6APHAUDIO_API int getMercury3FWVersion() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_MERC3_FW);
	}
	return Mercury3FWVersion; 
} 

/*
C4 
0 0 0 0 0 0 0 0
|           | |
|           | +---------- Mercury 4 LT2208 Overflow (1 = active, 0 = inactive)
+-----------+------------ Mercury 4 software version (0 to 127) 
*/

KD5TFDVK6APHAUDIO_API int getMercury4FWVersion() { 
	if ( !isMetis ) { 
		getI2CBytes(I2C_MERC4_FW);
	}
	return Mercury4FWVersion; 
} 


// PC to HPSDR Commands
/*
C0
0 0 0 0 0 0 0 0
              | 
              +------------ MOX (1 = active, 0 = inactive)
*/

KD5TFDVK6APHAUDIO_API void SetXmitBit(int xmit) { 
        if ( xmit != 0 ) {
               XmitBit = 1;
				//reset_control_idx = 1;
        }
        else {
                XmitBit = 0;
        }
}

KD5TFDVK6APHAUDIO_API void SetDelayXmit(int bit, int loops) {

	if (loops != 0)
	{
		delay_Xmit_loop = loops;
		delay_Xmit = bit;
	}

}

/*
C1
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | + +------------ Speed (00 = 48kHz, 01 = 96kHz, 10 = 192kHz, 11 = 384kHz)
| | | | + +---------------- 10MHz Ref. (00 = Atlas/Excalibur, 01 = Penelope, 10 = Mercury)
| | | +-------------------- 122.88MHz source (0 = Penelope, 1 = Mercury)
| + +---------------------- Config (00 = nil, 01 = Penelope, 10 = Mercury, 11 = both)
+-------------------------- Mic source (0 = Janus, 1 = Penelope)
*/

KD5TFDVK6APHAUDIO_API void SetC1Bits(int bits) { 
	C1Mask = bits; 
	return;
}

/*
C2
0 0 0 0 0 0 0 0
|           | |
|           | +------------ Mode (1 = Class E, 0 = All other modes)
+---------- +-------------- Open Collector Outputs on Penelope or Hermes (bit 6…..bit 0)
*/

KD5TFDVK6APHAUDIO_API void EnableEClassModulation(int bit) { 
        if ( bit != 0 ) {
                EClass = 1;
        }
        else {
                EClass = 0;
        }
}

KD5TFDVK6APHAUDIO_API void SetPennyOCBits(int b) { 
	PennyOCBits = b << 1; 
	return;
}

/*
C3
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | + +------------ Alex Attenuator (00 = 0dB, 01 = 10dB, 10 = 20dB, 11 = 30dB)
| | | | | +---------------- Preamp On/Off (0 = Off, 1 = On)
| | | | +------------------ LT2208 Dither (0 = Off, 1 = On)
| | | + ------------------- LT2208 Random (0= Off, 1 = On)
| + + --------------------- Alex Rx Antenna (00 = none, 01 = Rx1, 10 = Rx2, 11 = XV)
+ ------------------------- Alex Rx out (0 = off, 1 = on). Set if Alex Rx Antenna > 0.
*/

KD5TFDVK6APHAUDIO_API void SetAlexAtten(int bits) { 
	AlexAtten = bits; 
	if ( AlexAtten > 3 ) AlexAtten = 0; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetMercDither(int bits) { 
	if ( bits != 0 ) { 
		MercDither = (1 << 3); 
	} 
	else { 
		MercDither = 0;
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void SetMercRandom(int bits) { 
	if ( bits != 0 ) { 
		MercRandom = (1 << 4); 
	} 
	else { 
		MercRandom = 0; 
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlexAntBits(int rx_only_ant, int trx_ant, int rx_out) {  
	

	trx_ant = (trx_ant - 1) & 0x3;
	if ( trx_ant >= 4 ) {
		AlexTxAnt = 0;  
	}
	else {
		AlexTxAnt = trx_ant;
	}

	rx_only_ant = ( rx_only_ant << 5); 
	AlexRxAnt = rx_only_ant & 0x60; 

	if ( rx_out ) { 
		AlexRxOut = 0x80; 
	} 
	else { 
		AlexRxOut = 0; 
	} 

	//delay_Xmit = 1;

	return;
}

/*
C4 
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | + + ----------- Alex Tx relay (00 = Tx1, 01= Tx2, 10 = Tx3)
| | | | | + --------------- Duplex (0 = off, 1 = on)
| | + + +------------------ Number of Receivers (000 = 1, 111 = 8)
| +------------------------ Time stamp – 1PPS on LSB of Mic data (0 = off, 1 = on)
+-------------------------- Common Mercury Frequency (0 = independent frequencies to Mercury 
			                Boards, 1 = same frequency to all Mercury boards)
*/


KD5TFDVK6APHAUDIO_API void SetDuplex(int dupx) {   // dupx == 0, half duplex, != 0, full duplex
        if ( dupx != 0 ) {
                Duplex = (1 << 2);
        }
        else {
                Duplex = 0;
        }
		return;
}

KD5TFDVK6APHAUDIO_API void SetNRx(int nrx) {  
	receivers = nrx * 2;
	nreceivers = nrx;
	nrx = (nrx-1) << 3; 
	NRx = nrx & 0x38; 
	return;
}

KD5TFDVK6APHAUDIO_API void EnableDiversity2(int g) { 
	if ( g == 0 ) diversitymode2 = 0; 
	else
	{
		diversitymode2 = 1;
		//reset_control_idx = 1;
	}
	return;
}

/*
C0
0 0 0 0 0 0 1 x     C1, C2, C3, C4 NCO Frequency in Hz for Transmitter, Apollo ATU
                   (32 bit binary representation - MSB in C1) 
C0
0 0 0 0 0 1 0 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver_1
C0
0 0 0 0 0 1 1 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver _2 
C0
0 0 0 0 1 0 0 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver _3 
C0
0 0 0 0 1 0 1 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver _4 
C0
0 0 0 0 1 1 0 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver _5 
C0
0 0 0 0 1 1 1 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver _6 
C0
0 0 0 1 0 0 0 x     C1, C2, C3, C4   NCO Frequency in Hz for Receiver _7 
*/

// ff in hz 
KD5TFDVK6APHAUDIO_API void SetTXVFOfreq(int tx) {
       VFOfreq_tx = tx;
	   //reset_control_idx = 1;
        return;
}

KD5TFDVK6APHAUDIO_API void SetRX1VFOfreq(int rx1) {
         VFOfreq_rx1 = rx1; 
       return;
}

KD5TFDVK6APHAUDIO_API void SetRX2VFOfreq(int rx2) {
       VFOfreq_rx2 = rx2;
       return;
}

KD5TFDVK6APHAUDIO_API void SetRX3VFOfreq(int rx3) {
       VFOfreq_rx3 = rx3;
       return;
}

KD5TFDVK6APHAUDIO_API void SetRX4VFOfreq(int rx4) {
       VFOfreq_rx4 = rx4;
       return;
}

KD5TFDVK6APHAUDIO_API void SetRX5VFOfreq(int rx5) {
       VFOfreq_rx5 = rx5;
       return;
}

KD5TFDVK6APHAUDIO_API void SetRX6VFOfreq(int rx6) {
       VFOfreq_rx6 = rx6;
       return;
}

KD5TFDVK6APHAUDIO_API void SetRX7VFOfreq(int rx7) {
       VFOfreq_rx7 = rx7;
       return;
}

/*
C0
0 0 0 1 0 0 1 x    
C1
0 0 0 0 0 0 0 0
|             |
+-------------+------------ Hermes/PennyLane Drive Level (0-255)
*/

KD5TFDVK6APHAUDIO_API void SetOutputPowerFactor(int u) {
	OutputPowerFactor = (unsigned int)(u & 0xff);
}

/*
C2
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | | +------------ Hermes/Metis Penelope Mic boost (0 = 0dB, 1 = 20dB)
| | | | | | +-------------- Metis/Penelope or PennyLane Mic/Line-in (0 = mic, 1 = Line-in)
| | | | | +---------------- Hermes – Enable/disable Apollo filter (0 = disable, 1 = enable)
| | | | +------------------ Hermes – Enable/disable Apollo tuner (0 = disable, 1 = enable)
| | | +-------------------- Hermes – Apollo auto tune (0 = end, 1 = start)
| | +---------------------- Hermes – select filter board (0 = Alex, 1 = Apollo)
| +------------------------ Alex   - manual HPF/LPF filter select (0 = disable, 1 = enable)
+-------------------------- VNA Mode (0 = off, 1 = on)
*/

KD5TFDVK6APHAUDIO_API void SetMicBoost(int bits) { 
        if ( bits != 0 ) {
                MicBoost = 1;
        }
        else {
                MicBoost = 0;
        }
		return;
}

KD5TFDVK6APHAUDIO_API void SetLineIn(int bits) { 
	if ( bits != 0 ) { 
		LineIn = 0x2; 
	} 
	else { 
		LineIn = 0;
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void EnableApolloFilter(int bits) { 
	if (bits != 0)
		ApolloFilt = 0x4; 
	else
		ApolloFilt = 0;
	return;
}

KD5TFDVK6APHAUDIO_API void EnableApolloTuner(int bits) { 
	if (bits != 0)
		ApolloTuner = 0x8; 
	else
		ApolloTuner = 0;
	return;
}

KD5TFDVK6APHAUDIO_API void EnableApolloAutoTune(int bits) { 
	if (bits != 0)
		ApolloATU = 0x10; 
	else
		ApolloATU = 0;
	return;
}

KD5TFDVK6APHAUDIO_API void SetHermesFilter(int bits) { 
	if (bits != 0)
		HermesFilt = 0x20; 
	else
		HermesFilt = 0;
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlexManEnable(int bit) { 
        if ( bit != 0 ) {
                AlexManEnable = 0x40;
        }
        else {
                AlexManEnable = 0;
        }
//printf("SetAlexEnable: %d\n", bit); fflush(stdout); 
}

/*
C3
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | | +------------ Alex   -	select 13MHz  HPF (0 = disable, 1 = enable)
| | | | | | +-------------- Alex   -	select 20MHz  HPF (0 = disable, 1 = enable)
| | | | | +---------------- Alex   -	select 9.5MHz HPF (0 = disable, 1 = enable)
| | | | +------------------ Alex   -	select 6.5MHz HPF (0 = disable, 1 = enable)
| | | +-------------------- Alex   -	select 1.5MHz HPF (0 = disable, 1 = enable)
| | +---------------------- Alex   -	Bypass all HPFs   (0 = disable, 1 = enable)
| +------------------------ Alex   -	6M low noise amplifier (0 = disable, 1 = enable)
+-------------------------- Disable Alex T/R relay (0 = enable, 1 = disable) 
*/

KD5TFDVK6APHAUDIO_API void SetAlexHPFBits(int bits) { 
	AlexHPFMask = bits;
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlexTRRelayBit(int bit) { 
	if (bit != 0)
		AlexTRRelay = 0x80; 
	else
		AlexTRRelay = 0;
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlex3HPFBits(int bits) { 
	Alex3HPFMask = bits; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlex4HPFBits(int bits) { 
	Alex4HPFMask = bits; 
	return;
}


/*
C4
0 0 0 0 0 0 0 0
  | | | | | | |
  | | | | | | +------------ Alex   - 	select 30/20m LPF (0 = disable, 1 = enable)
  | | | | | +-------------- Alex   - 	select 60/40m LPF (0 = disable, 1 = enable)
  | | | | +---------------- Alex   - 	select 80m    LPF (0 = disable, 1 = enable)
  | | | +------------------ Alex   - 	select 160m   LPF (0 = disable, 1 = enable)
  | | +-------------------- Alex   - 	select 6m     LPF (0 = disable, 1 = enable)
  | +---------------------- Alex   - 	select 12/10m LPF (0 = disable, 1 = enable)
  +------------------------ Alex   - 	select 17/15m LPF (0 = disable, 1 = enable)
 */

KD5TFDVK6APHAUDIO_API void SetAlexLPFBits(int bits) { 
	AlexLPFMask = bits; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlex2LPFBits(int bits) { 
	Alex2LPFMask = bits; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlex3LPFBits(int bits) { 
	Alex3LPFMask = bits; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlex4LPFBits(int bits) { 
	Alex4LPFMask = bits; 
	return;
}

/*
C0
0 0 0 1 0 1 0 x   '0x14'
C1
0 0 0 0 0 0 0 0
  | | | | | | |
  | | | | | | +------------ Rx1 pre-amp (0=OFF, 1= ON)
  | | | | | +-------------- Rx2 pre-amp (0=OFF, 1= ON)
  | | | | +---------------- Rx3 pre-amp (0=OFF, 1= ON)
  | | | +------------------ Rx4 pre-amp (0=OFF, 1= ON)
  | | +-------------------- 0=PTT to ring/mic audio & bias to tip 1=PTT to tip/mic audio & bias to ring (Orion)
  | +---------------------- Mic Bias (0=OFF, 1=ON) (Orion)
  +------------------------ Mic PTT (0=Enable, 1=Disable) (Orion)
  */

KD5TFDVK6APHAUDIO_API void SetRX1Preamp(int bits) { 
	if ( bits != 0 ) { 
		RX1Preamp = 1; 
		MercPreamp = (1 << 2);
	} 
	else { 
		RX1Preamp = 0; 
		MercPreamp = 0;
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void SetRX2Preamp(int bits) { 
	if ( bits != 0 ) { 
		RX2Preamp = (1 << 1); 
	} 
	else { 
		RX2Preamp = 0; 
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void SetMicTipRing(int bits) { 
	if ( bits != 0 ) { 
		MicTipRing = (1 << 4); 
	} 
	else { 
		MicTipRing = 0; 
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void SetMicBias(int bits) { 
	if ( bits != 0 ) { 
		MicBias = (1 << 5); 
	} 
	else { 
		MicBias = 0; 
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void SetMicPTT(int bits) { 
	if ( bits != 0 ) { 
		MicPTT = (1 << 6); 
	} 
	else { 
		MicPTT = 0; 
	}	
	return;
}

/*
C2
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | | +------------ TLV320 Line-in Gain bit 0 
| | | | | | +-------------- TLV320 Line-in Gain bit 1
| | | | | +---------------- TLV320 Line-in Gain bit 2
| | | | +------------------ TLV320 Line-in Gain bit 3
| | | +-------------------- TLV320 Line-in Gain bit 4
| | +---------------------- If set enable 20dB Attenuator on Mercury when on Tx*
| +------------------------ PureSignal (0 = disable, 1 = enable)
+-------------------------- Penelope board in use
*/

KD5TFDVK6APHAUDIO_API void SetLineBoost(int bits) {
		LineBoost = bits;
	return;
}

KD5TFDVK6APHAUDIO_API void SetMercTxAtten(int bit) {
        if ( bit != 0 ) {
                MercTxAtten = 0x20;
        }
        else {
			MercTxAtten = 0;
        }
        return;
}

KD5TFDVK6APHAUDIO_API void SetPureSignal(int bit) {
        if ( bit != 0 ) {
                PureSignal = 0x40;
        }
        else {
                PureSignal = 0;
        }
        return;
}

KD5TFDVK6APHAUDIO_API void SetPennyPresent(int bit) {
        if ( bit != 0 ) {
                PennyPresent = 0x80;
        }
        else {
                PennyPresent = 0;
        }
        return;
}

/*
C3
0 0 0 0 0 0 0 0
        | | | |
        | | | +------------ Metis DB9 pin 1 Open Drain Output (0=OFF, 1= ON)
        | | +-------------- Metis DB9 pin 2 Open Drain Output (0=OFF, 1= ON)
        | +---------------- Metis DB9 pin 3 3.3v TTL Output (0=OFF, 1= ON)
        +------------------ Metis DB9 pin 4 3.3v TTL Output (0=OFF, 1= ON)
*/

KD5TFDVK6APHAUDIO_API void SetUserOut0(int out) {
        if ( out == 0 ) {
                UserOut0 = 0;
        }
        else {
                UserOut0 = 1;
        }
       return;
}

KD5TFDVK6APHAUDIO_API void SetUserOut1(int out) {
        if ( out == 0 ) {
                UserOut1 = 0;
        }
        else {
                UserOut1 = 0x2;
        }
        return;
}

KD5TFDVK6APHAUDIO_API void SetUserOut2(int out) {
        if ( out == 0 ) {
                UserOut2 = 0;
        }
        else {
                UserOut2 = 0x4;
        }
        return;
}

KD5TFDVK6APHAUDIO_API void SetUserOut3(int out) {
        if ( out == 0 ) {
                UserOut3 = 0;
        }
        else {
                UserOut3 = 0x8;
        }
        return;
}

/*
C4
0 0 0 0 0 0 0 0
    | |       |
    | +-------+------------ Hermes/Angelia/Orion RX1 Input Attenuator (0 – 31dB) [4:0]
    +---------------------- Hermes/Angelia/Orion RX1 Attenuator enable (0 = disable, 1 = enable)
                             If disabled then Preamp On/Off bit is used.
*/

KD5TFDVK6APHAUDIO_API void SetADC1StepAttenData(int bits) { 
		adc1_step_att_data = bits & 0x1f; 
		if (diversitymode2) adc2_step_att_data = adc1_step_att_data;
    	return;
}

KD5TFDVK6APHAUDIO_API void EnableADC1StepAtten(int bits) { 
	if ( bits != 0 ) { 
		enable_ADC1_step_att = 0x20; 
	} 
	else { 
		enable_ADC1_step_att = 0; 
	}	
	return;
}

/*
C0
0 0 0 1 0 1 1 x  0x16
C1
0 0 0 0 0 0 0 0
    | |       |
    | +-------+------------ Angelia/Orion ADC2 Input Attenuator (0 – 31dB) [4:0]
    +---------------------- Angelia/Orion ADC2 Attenuator enable (0 = disable, 1 = enable)
*/

KD5TFDVK6APHAUDIO_API void SetADC2StepAttenData(int bits) { 
		adc2_step_att_data = bits & 0x1f; 
    	return;
}

KD5TFDVK6APHAUDIO_API void EnableADC2StepAtten(int bits) { 
	if ( bits != 0 ) { 
		enable_ADC2_step_att = 0x20; 
	} 
	else { 
		enable_ADC2_step_att = 0; 
	}	
	return;
}

/*
C2
0 0 0 0 0 0 0 0
  | | |       |
  | | +-------+------------ Orion ADC3 Input Attenuator (0 – 31dB) [4:0]
  | +---------------------- Orion ADC3 Attenuator enable (0 = disable, 1 = enable)
  +------------------------ Reverse Paddles (0 = disable, 1 = enable)
*/

KD5TFDVK6APHAUDIO_API void SetADC3StepAttenData(int bits) { 
		adc3_step_att_data = bits & 0x1f; 
    	return;
}

KD5TFDVK6APHAUDIO_API void EnableADC3StepAtten(int bits) { 
	if ( bits != 0 ) { 
		enable_ADC3_step_att = 0x20; 
	} 
	else { 
		enable_ADC3_step_att = 0; 
	}	
	return;
}

KD5TFDVK6APHAUDIO_API void ReversePaddles(int bits) { 
	if ( bits != 0 ) { 
		reverse_paddles = 0x40; 
	} 
	else { 
		reverse_paddles = 0; 
	}	
	return;
}

/*
C3
0 0 0 0 0 0 0 0
| | |         |
| | +---------+------------ Keyer speed [5:0] (1-60 WPM)
+-+------------------------ Keyer Mode [1:0] (00 = straight/bug, 01 = Mode A, 10 = Mode B)
*/

KD5TFDVK6APHAUDIO_API void SetCWKeyerSpeed(int speed) {
	cw_speed = speed & 0x3f;
}

KD5TFDVK6APHAUDIO_API void SetCWKeyerMode(int mode) {
	if (mode > 3) mode = 0;
	cw_mode = mode << 6;
}


/*
C4 
0 0 0 0 0 0 0 0
| |           |
| +-----------+------------ Keyer Weight [6:0] (0 – 100)
+-------------+------------ Keyer Spacing (0 = off, 1 = on)
*/

KD5TFDVK6APHAUDIO_API void SetCWKeyerWeight(int weight) {
	cw_weight = weight & 0x7f;
}

KD5TFDVK6APHAUDIO_API void EnableCWKeyerSpacing(int bits) { 
	if ( bits != 0 ) { 
		enable_cw_spacing = 0x80; 
	} 
	else { 
		enable_cw_spacing = 0x0; 
	}	
	return;
}

/*
C0
0 0 0 1 1 0 0 x   	    
*/

/*
C0
0 0 0 1 1 0 1 x   	
*/

/*
C0
0 0 0 1 1 1 0 x  
C1
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | +-+------------ ADC assignment for RX1, where 00 = ADC1, 01 = ADC2, 10 = ADC3
| | | | +-+---------------- ADC assignment for RX2, where 00 = ADC1, 01 = ADC2, 10 = ADC3
| | +-+-------------------- ADC assignment for RX3, where 00 = ADC1, 01 = ADC2, 10 = ADC3
+-+------------------------ ADC assignment for RX4, where 00 = ADC1, 01 = ADC2, 10 = ADC3	
*/

KD5TFDVK6APHAUDIO_API void SetADC_cntrl1(int g) { 
	ADC_cntrl1 = g; 
	return;
}

KD5TFDVK6APHAUDIO_API int GetADC_cntrl1() { 
	return ADC_cntrl1;
}

/*
C2
0 0 0 0 0 0 0 0
    | | | | | |
    | | | | +-+------------ ADC assignment for RX5, where 00 = ADC1, 01 = ADC2, 10 = ADC3*
    | | +-+---------------- ADC assignment for RX6, where 00 = ADC1, 01 = ADC2, 10 = ADC3
    +-+-------------------- ADC assignment for RX7, where 00 = ADC1, 01 = ADC2, 10 = ADC3
* Except on Tx where RX5 input is assigned to the Tx DAC
*/

KD5TFDVK6APHAUDIO_API void SetADC_cntrl2(int g) { 
	ADC_cntrl2 = g; 
	return;
}

KD5TFDVK6APHAUDIO_API int GetADC_cntrl2() { 
	return ADC_cntrl2;
}

//C3
//0 0 0 0 0 0 0 0
//      |       |
//      +-------+------------ ADC Input Attenuator Tx (0-31dB) [4:0]

KD5TFDVK6APHAUDIO_API void SetTxAttenData(int bits) { 
		tx_att_data = bits & 0x1f; 
    	return;
}

// C4 currently not used.
  
/*
C0
0 0 0 1 1 1 1 x  
C1
0 0 0 0 0 0 0 0
              |
              +------------ CW (0 = External, 1 = Internal)
*/

KD5TFDVK6APHAUDIO_API void EnableCWKeyer(int enable) { 
	if ( enable != 0 ) { 
		enable_cw_keyer = 1; 
	} 
	else { 
		enable_cw_keyer = 0; 
	}	
	return;
}

/*
C2
0 0 0 0 0 0 0 0
|             |
+-------------+------------ CW Sidetone Volume (0 to 255 [7:0])
*/

KD5TFDVK6APHAUDIO_API void SetCWSidetoneVolume(int vol) {
	cw_sidetone_volume = vol;
}

/*
C3
0 0 0 0 0 0 0 0
|             |
+-------------+------------ CW PTT delay mS (0 to 255 [7:0])
*/

KD5TFDVK6APHAUDIO_API void SetCWPTTDelay(int delay) {
	cw_ptt_delay = delay & 0xff;
}

//C4 currently not used – reserved for raised cosine profile time if required

/*
C0
0 0 1 0 0 0 0 x  
C1
0 0 0 0 0 0 0 0
|             |
+-------------+------------ CW Hang Time mS (bits [9:2])

C2
0 0 0 0 0 0 0 0
            | |
            +-+------------ CW Hang Time mS (bits [1:0])
*/

KD5TFDVK6APHAUDIO_API void SetCWHangTime(int time) {
         cw_hang_time = time; 
}

/*
C3
0 0 0 0 0 0 0 0
|             |
+-------------+------------ CW Sidetone Frequency Hz (bits [9:2])

C4
0 0 0 0 0 0 0 0
            | |
            +-+------------ CW Sidetone Frequency Hz (bits [1:0])
*/

KD5TFDVK6APHAUDIO_API void SetCWSidetoneFreq(int freq) {
         cw_sidetone_freq = freq; 
}

/*
C0
0 0 1 0 0 0 1 x  
C1
0 0 0 0 0 0 0 0
|             |
+-------------+------------ PWM Min pulse width (bits [9:2])

C2
0 0 0 0 0 0 0 0
            | |
            +-+------------ PWM Min pulse width (bits [1:0])
*/

KD5TFDVK6APHAUDIO_API void SetEERPWMmin(int min) {
         eer_pwm_min = min; 
}

/*
C3
0 0 0 0 0 0 0 0
|             |
+-------------+------------ PWM Max pulse width (bits [9:2])

C4
0 0 0 0 0 0 0 0
            | |
            +-+------------ PWM Max pulse width (bits [1:0])
*/

/*
C0
0 0 1 0 0 1 0 x
C1
0 0 0 0 0 0 0 0
| | | | | | | |
| | | | | | | +------------ 
| | | | | | +-------------- 
| | | | | +---------------- 
| | | | +------------------ 
| | | +-------------------- 
| | +---------------------- 
| +------------------------ 
+-------------------------- RX2 Ground

C2
0 0 0 0 0 0 0 0
              |
              +------------ XVTR Enable ( Enables transverter T/R relay on the ANAN-8000DLE )

*/

KD5TFDVK6APHAUDIO_API void SetAlex2HPFBits(int bits) {
	Alex2HPFMask = bits;
	return;
}

KD5TFDVK6APHAUDIO_API void SetGndRx2onTx(int e)
{
	gndrx2ontx = e << 7;
}

KD5TFDVK6APHAUDIO_API void SetXVTREnable(int e)
{
	xvtr_enable = e;
}

KD5TFDVK6APHAUDIO_API void SetEERPWMmax(int max) {
         eer_pwm_max = max; 
}

// *************************************************
// misc functions
// *************************************************
KD5TFDVK6APHAUDIO_API void SetCWDash(int bit) { 
	if ( bit != 0 ) {
		cw_dash = 2;
	}
	else {
		cw_dash = 0;
	}
}

KD5TFDVK6APHAUDIO_API void SetCWDot(int bit) { 
	if ( bit != 0 ) {
		cw_dot = 4;
	}
	else {
		cw_dot = 0;
	}
}

KD5TFDVK6APHAUDIO_API void SetCWX(int bit) { 
	if ( bit != 0 ) {
		cwx = 1;
	}
	else {
		cwx = 0;
	}
}

KD5TFDVK6APHAUDIO_API int GetC1Bits(void) { 
	return C1Mask; 
}

KD5TFDVK6APHAUDIO_API int getHaveSync() { 
	return HaveSync; 
} 

KD5TFDVK6APHAUDIO_API int getControlByteIn(int n) { 
	if ( n < 0 || n > 4 ) { 
		return -1; 
	} 
	return ControlBytesIn[n];  
} 

//
// bufp MUST point to a 4096 byte buffer 
// 
// returns 0 on success 
// -1 - receiver not on 
// -2 - timeout 
// -3 - short read 
KD5TFDVK6APHAUDIO_API int GetEP4Data(char *bufp) { 
	int numread; 
	if ( OzyH == NULL || !IOThreadRunning ) { 
		return -1; 
	}
	numread = OzyBulkRead(OzyH, 0x84, bufp, 4096); 
	if ( numread <= 0 )  {  /* read failed - bail out */ 
		return -2; 		
	} 
	if ( numread != 4096 ) { 
		return -3; 
	} 
	return 0; 
}

// diag data mapping
// 0-4 C0-C4 in
// 5-9 C0-C4 out
// 10 sync gain count
// 11 sync lost count
// 12 not ok to send count
// 13 have sync
KD5TFDVK6APHAUDIO_API int GetDiagData(int *a, int count) {
        int i;
        for ( i = 0; i < 5; i++ ) {  /* do the ControlBytesIn */
                if ( count == i ) return count; /* bail if we're out of space */
                a[i] = ControlBytesIn[i];
        }
        for ( i = 5; i < 10; i++ ) {
                if ( count == i ) return count;
                a[i] = ControlBytesOut[i-5];
        }
        if ( count == 10 ) return 10;
        a[10] = SyncGainedCount;
        if ( count == 11 ) return 11;
        a[11] = LostSyncCount;
        if ( count == 12 ) return 12;
        a[12] = NotOKtoSendCount;
        if ( count == 13 ) return 13;
        a[13] = HaveSync;
        return 14;
}

KD5TFDVK6APHAUDIO_API void SetSWRProtect(float g) { 
	swr_protect = g; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetAlexEnabled(int bit) { //unused
        if ( bit != 0 ) {
                AlexEnabled = 1;
        }
        else {
                AlexEnabled = 0;
        }
//printf("SetAlexEnable: %d\n", bit); fflush(stdout); 
}

KD5TFDVK6APHAUDIO_API void EnableHermesPower(int enabled) { 
	printf("HermesPowerEndabled: %d\n", enabled); fflush(stdout); 
	HermesPowerEnabled = enabled; 
} 

KD5TFDVK6APHAUDIO_API void SetFPGATestMode(int i) {
	FPGATestMode = i;
}

KD5TFDVK6APHAUDIO_API void SetLegacyDotDashPTT(int bit) { 
	if ( bit ) { 
		CandCAddrMask = 0xfc; 
		CandCFwdPowerBit = 0x4; 
		DotDashMask = 0x3;
	}
	else { 
		CandCAddrMask = 0xf8; 
		CandCFwdPowerBit = 0x8; 
		DotDashMask = 0x7;
	} 
}

KD5TFDVK6APHAUDIO_API void SetDiscoveryMode(int bit) { 
	if ( bit == 0 ) {
		full_discovery = 0;
	}
	else {
		full_discovery = 1;
	}
}

KD5TFDVK6APHAUDIO_API void SetMercSource(int g) { 
	MercSource = g; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetrefMerc(int g) { 
	refMerc = g; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetIQ_Rotate(double a, double b) { 
	I_Rotate = a; 
	Q_Rotate = b;
	return;
}

KD5TFDVK6APHAUDIO_API void SetTheta(double a) { 
	theta = a; 
	return;
}

KD5TFDVK6APHAUDIO_API void SetIQ_RotateA(double a, double b) { 
	I_RotateA = a; 
	Q_RotateA = b;
	return;
}

KD5TFDVK6APHAUDIO_API void SetIQ_RotateB(double a, double b) { 
	I_RotateB = a; 
	Q_RotateB = b;
	return;
}

KD5TFDVK6APHAUDIO_API int GetAndResetAmpProtect()
{
	int apw = amp_protect_warning;
	amp_protect_warning = 0;
	return apw;
}

KD5TFDVK6APHAUDIO_API void SetAmpProtectRun(int run)
{
	amp_protect_on = run;
}

KD5TFDVK6APHAUDIO_API void SetAIN4Voltage(int v)
{
	ain4_voltage = v;
}

KD5TFDVK6APHAUDIO_API void isOrionMKII(int v)
{
	is_orion_mkii = v;
}

