/* 
 * Copyright (C) 2009  Bill Tracey (bill@ejwt.com) (KD5TFD)
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 *
 */ 
#include <stdio.h> 
#include "..\JanusAudio\private.h"


struct OzyHandle *OzyH; 

 int main(int argc, char *argv[]) { 
  char ibuf[100]; 
  int rc; 

  printf("Hello\n"); 

  OzyH = OzyOpen();
  if ( OzyH == NULL ) {
	  fprintf(stderr, "Failed to open Ozy!\n"); 
	  exit(99); 
  }

#if 0 
  while ( 1 ) { 
    printf("Press enter to start..."); 
    fgets(ibuf, sizeof(ibuf), stdin); 
    rc = StartAudioNative(48000, 2048, AudioCallback, 24, 0);  
    printf("Started rc=%d.\nPress enter to stop...", rc); 
    fgets(ibuf, sizeof(ibuf), stdin); 
    StopAudio();
    printf("Stopped.\n");  
  } 
#endif 
} 

