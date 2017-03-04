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

// KD5TFD-VK6APH-Audio.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "KD5TFD-VK6APH-Audio.h"
BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}

// This is an example of an exported variable
KD5TFDVK6APHAUDIO_API int nKD5TFDVK6APHAudio=0;

// This is an example of an exported function.
KD5TFDVK6APHAUDIO_API int fnKD5TFDVK6APHAudio(void)
{
	return 42;
}


#if 0 
// This is the constructor of a class that has been exported.
// see KD5TFD-VK6APH-Audio.h for the class definition
CKD5TFDVK6APHAudio::CKD5TFDVK6APHAudio()
{ 
	return; 
}
#endif