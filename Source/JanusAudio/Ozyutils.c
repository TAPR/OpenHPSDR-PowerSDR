/*
This file is part of a program that implements a Software-Defined Radio.

Copyright (C) 2007, 2008 Philip A Covington
Copyright (C) 2010-2013 Doug Wigley (W5WC)

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

The author can be reached by email at

p.covington@gmail.com

*/

#include "Ozyutils.h"
#include <time.h>
#include <windows.h>

//#define debug_str				// ***** JJA

int Reset_CPU(struct usb_dev_handle *hdev, int reset)
{
    char val;		 //  unsigned char	***** JJA
    
    val = reset;
    return Write_RAM(hdev, 0xE600, &val, 1); 
}

int Write_RAM(struct usb_dev_handle *hdev, int start_addr, char *buffer, size_t length)
{
    int pkt_size = MAX_EP0_PACKET_SIZE;
    int bytes_written = 0;
    int addr;
    int nsize;
    char *pbuffer;
    int count;
    
    for (addr = start_addr; addr < start_addr + length; addr += pkt_size)
    {
        nsize = length + start_addr - addr;
        if (nsize > pkt_size) nsize = pkt_size;
        
        count = usb_control_msg(hdev, 0x40, 0xA0, addr, 0, 
                    (char *)(buffer + (addr - start_addr)), nsize, 1000);
        if (count < 0) {
            printf("write_internal_ram_failed\n");
            return -1;
        }
    }
    return 0;
}

int Load_Firmware(struct usb_dev_handle *hdev, const char *filename)
{
    char s[1024];
    int length;
    int addr;
    int type;
    unsigned char data[256];			 // unsigned char ***** JJA
    unsigned char checksum, a;
    unsigned int b;
    int i;
	
    FILE *f = fopen(filename, "ra");
    if (f == 0) {
#ifdef debug_str
		printf("error, could not find file %s\n",filename);	 // ***** JJA
#else
		printf("error, could not find firmware file...");
#endif
        return -1;
    }
    else {
#ifdef debug_str
		printf("opened file %s...\n", filename);	// ***** JJA
#endif
	}
	    
    while (!feof(f)) {
        fgets(s, 1024, f);
        if (s[0] != ':') {
#ifdef debug_str
			printf("error, corrupted file %s\n", filename);		// ***** JJA
#else
			printf("error, corrupted file...");	 // ***** JJA
#endif			
						
            fclose(f);
            return -1;
        }
        sscanf(s+1, "%02x", &length);
        sscanf(s+3, "%04x", &addr);
        sscanf(s+7, "%02x", &type);
        
        if (type == 0) {
            a = length + (addr & 0xff) + (addr>>8) + type;
            for (i=0; i<length; i++) {
                sscanf(s+9+i*2, "%02x", &b);
                data[i] = b;
                a = a + data[i];
            }
            
            sscanf(s+9+length*2, "%02x", &b);
            checksum = b;
            if (((a + checksum) & 0xff) != 0x00) {
                printf("checksum failed\n");
                fclose(f);
                return -1;
            }
            if (Write_RAM(hdev, addr, (char *)data, length) == -1) {
                printf("write failed\n");
                fclose(f);
                return -1;
            }
        } else if (type == 0x01) {
            break;
        } else if (type == 0x02) {
            printf("extended address not supported\n");
            fclose(f);
            return -1;
        }                
    }
    
    if (Reset_CPU(hdev, 0) == -1) {
        fclose(f);
		printf("error starting CPU\n");
        return -1;
    }        
    
    fclose(f);
    return 0;
}

int Write_Command  (struct usb_dev_handle *hdev, 
                                          int request, 
                                          int value, 
                                          int index, 
                                          unsigned char *bytes, 
                                          int length)
{
    int requesttype = (request & 0x80) ? VRT_VENDOR_IN : VRT_VENDOR_OUT;
    
    int r = usb_control_msg(hdev, requesttype, request, value, index,
                            (char *) bytes, length, 1000);
    
    if (r < 0) {
        if (errno != EPIPE)
            printf("usb control message failed: %s\n", usb_strerror());
    }
    return r;
}

int Load_Fpga_Rbf(usb_dev_handle *hdev, const char *filename)
{
    unsigned char buffer[MAX_EP0_PACKET_SIZE];
    int n;

    FILE *f = fopen(filename, "rb");
    if (f == 0) {
#ifdef debug_str
		printf("error, coould not find file %s\n", filename);
#else
		printf("error, could not find fpga file...");
#endif
        return -1;
    }
#ifdef debug_str
	else {
		printf("opened file %s...\n", filename);	// ***** JJA
	}
#endif
	    
    if (Write_Command(hdev, VRQ_FPGA_LOAD, 0, FL_BEGIN, 0, 0) != 0) {
        fclose(f);
        return -1;
    } else {
        printf("FL_BEGIN\n");
    }
    
    while ((n = fread(buffer, 1, sizeof(buffer), f)) > 0) {
        if (Write_Command(hdev, VRQ_FPGA_LOAD, 0, FL_XFER, buffer, n) != n) {
            fclose(f);
            return -1;
        }
    }
    
    if (Write_Command(hdev, VRQ_FPGA_LOAD, 0, FL_END, 0, 0) != 0) {
        fclose(f);
        return -1;
    } else {
        printf("FL_END\n");
    }
    fclose(f);
    return 0;
}

int Read_I2c(struct usb_dev_handle *hdev, 
            int i2c_addr, 
            unsigned char *buffer, 
            int length)
{
    return (Write_Command(hdev,
                         VRQ_I2C_READ,
                         i2c_addr,
                         0,
                         buffer,
                         length));
}

int Write_I2c(struct usb_dev_handle *hdev, 
            int i2c_addr, 
            unsigned char *buffer, 
            int length)
{
    return (Write_Command(hdev,
                         VRQ_REQ_I2C_WRITE,
                         i2c_addr,
                         0,
                         buffer,
                         length));
}

int Read_EEPROM(struct usb_dev_handle *hdev, 
                 int i2c_addr,
                 unsigned short offset,
                 unsigned char *buffer,
                 int length) {
                 
    unsigned char cmd[2];
    
    if (hdev == NULL) return -1;
    if (length < 1) return -1;    
    if (buffer == NULL) return -1;
    
    cmd[0] = (char)((0xFF00 & offset) >> 8); // high byte address
    cmd[1] = (char)(0xFF & offset); // low byte address
    
    // set address pointer in EEPROM
    if (Write_I2c(hdev, i2c_addr, cmd, 2) == -1) {
        printf("Could not set EEPROM address\n");
        return -1;
    }
    
    // now read from the address
    if (Read_I2c(hdev, i2c_addr, buffer, length) == -1) {
        printf("Oops! Could not read I2c devicen");
        return -1;
    }
    return 0;        
}

int Write_EEPROM(struct usb_dev_handle *hdev, 
                 int i2c_addr,
                 unsigned short offset,
                 unsigned char *buffer,
                 int length) {
                 
    unsigned char cmd[3];
    int i;
    
    if (hdev == NULL) return -1;
    if (length < 1) return -1;    
    if (buffer == NULL) return -1;
    
    for (i=0; i < length; i++) {
        cmd[0] = (char)((0xFF00 & offset) >> 8); // high byte address
        cmd[1] = (char)(0xFF & offset); // low byte address
        cmd[2] = (char)buffer[i]; // value to write
        // set address pointer in EEPROM
        if (Write_I2c(hdev, i2c_addr, cmd, 3) == -1) {
            printf("Could not write EEPROM\n");
            return -1;            
        }
        offset++;
        uSleep(10);
    }    
    return 0;        
}

void uSleep(int waitTime) {
    __int64 time1 = 0, time2 = 0, freq = 0;

    QueryPerformanceCounter((LARGE_INTEGER *) &time1);
    QueryPerformanceFrequency((LARGE_INTEGER *)&freq);

    do {
        QueryPerformanceCounter((LARGE_INTEGER *) &time2);
    } while((time2-time1) < waitTime);
}
