/*
 * JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
 * Copyright (C) 2006,2007  Bill Tracey (bill@ejwt.com) (KD5TFD)
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


#ifndef LINUX
#ifdef KD5TFDVK6APHAUDIO_EXPORTS
#define KD5TFDVK6APHAUDIO_API __declspec(dllexport)
#else
#define KD5TFDVK6APHAUDIO_API __declspec(dllimport)
#endif
#else 
#define KD5TFDVK6APHAUDIO_API
#endif 



#if 0 
// This class is exported from the KD5TFD-VK6APH-Audio.dll
class KD5TFDVK6APHAUDIO_API CKD5TFDVK6APHAudio {
public:
	CKD5TFDVK6APHAudio(void);
	// TODO: add your methods here.
};

extern KD5TFDVK6APHAUDIO_API int nKD5TFDVK6APHAudio;

KD5TFDVK6APHAUDIO_API int fnKD5TFDVK6APHAudio(void);
#endif 


extern KD5TFDVK6APHAUDIO_API int I2C_Cmd(unsigned char addr, unsigned char cmdbuf[], unsigned char cmdlen); 
extern KD5TFDVK6APHAUDIO_API char * I2C_RCtoString(int rc);


extern KD5TFDVK6APHAUDIO_API struct OzyHandle *OzyOpen(void); 
extern KD5TFDVK6APHAUDIO_API void OzyClose(struct OzyHandle *h); 
extern KD5TFDVK6APHAUDIO_API void *OzyHandleToRealHandle(struct OzyHandle *ozh);
extern KD5TFDVK6APHAUDIO_API int IsOzyAttached(void);
