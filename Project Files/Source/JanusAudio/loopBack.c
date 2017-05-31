/*
 * 
 * JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
 * Copyright (C) 2007  Bill Tracey (bill@ejwt.com) (KD5TFD)
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

/* loopBack.c - test Janus support on Linux by looping back input to output */ 


#include <stdio.h> 

#include "..\JanusAudio\private.h"

//
// this is called with audio from the Janus IO thread -- for now we just send the input back to be played on 
// the output 
//
//   on input, inp points to 4 pointers to arrays of floats for input (inL, inR, micL, micR) 
//             outp points to 4 pointers to arrsys of floats for output (outL, outR, monL, monR) 
// 
int __stdcall AudioCallback(void *inp, void *outp, int framecount, void *timep, int flags, void *userp) { 
  float **in_floats; 
  float **out_floats; 
  float *inlp; 
  float *inrp; 
  float *miclp; 
  float *micrp; 
  float *outlp; 
  float *outrp; 
  float *monlp; 
  float *monrp; 
  int i; 

  in_floats = (float * *)inp; 
  out_floats = (float * *)outp; 

  // printf("callback frameCount: %d\n", framecount); 
  inlp = in_floats[0]; 
  inrp = in_floats[1]; 
  miclp = in_floats[2]; 
  micrp = in_floats[3]; 

  outlp = out_floats[0]; 
  outrp = out_floats[1]; 
  monlp = out_floats[2]; 
  monrp = out_floats[3]; 

  // copy inl and inr to outl outr.  Also swap and invert into monl and monr 

  for ( i = 0; i < framecount; i++ ) { 
    outlp[i] = inlp[i]; 
    monrp[i] = -inlp[i]; 
    outrp[i] = inrp[i]; 
    monlp[i] = -inrp[i]; 
  } 
  return 0; 
} 


int main(int argc, char *argv[]) { 
  char ibuf[100]; 
  int rc; 

  printf("Hello"); 

  while ( 1 ) { 
    printf("Press enter to start..."); 
    fgets(ibuf, sizeof(ibuf), stdin); 
    rc = StartAudioNative(48000, 2048, AudioCallback, 24, 0);  
    printf("Started rc=%d.\nPress enter to stop...", rc); 
    fgets(ibuf, sizeof(ibuf), stdin); 
    StopAudio();
    printf("Stopped.\n");  
  } 
} 



