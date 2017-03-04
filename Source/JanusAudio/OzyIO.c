/*
 * JanusAudio.dll - Support library for HPSDR.org's Janus/Ozy Audio card
 * Copyright (C) 2006,2007  Bill Tracey (bill@ejwt.com) (KD5TFD)
 * Copyright (C) 20010-2013 Doug Wigley (W5WC)
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

#include <stdio.h> 
#include "private.h"
#include "Ozyutils.h"
//#include <usb.h> 
//#include <time.h>

//#define OZY_PID (0x0007) 
//#define OZY_VID (0xfffe) 

/* IO Timeout - in msecs */ 
//#define OZY_IO_TIMEOUT (300) 

// returns non zero if devp match vid and pid passed 
int doesDevMatchVIDPID(struct usb_device *devp, int vid, int pid) { 
	if ( devp->descriptor.bDescriptorType == USB_DT_DEVICE  && devp->descriptor.idVendor == vid &&  devp->descriptor.idProduct == pid )  {	
		return 1; 
	}
	return 0; 
} 

struct usb_device *findOzyFromDevice(struct usb_device *devp) { 
	int i; 
	struct usb_device *dp;
	if ( doesDevMatchVIDPID(devp, OZY_VID, OZY_PID) ) { 
		return devp; 
	}
	if ( devp->num_children != 0 ) { 
		for ( i = 0; i < devp->num_children; i++ ) { 
			dp = findOzyFromDevice(devp->children[i]); 
			if ( dp != NULL ) { 
				return dp; 
			}
		} 
	} 
	return NULL; 
} 

struct usb_device *findOzyFromDevices(struct usb_device *devp) { 
	struct usb_device *dp; 
	while ( devp != NULL ) { 
		dp = findOzyFromDevice(devp); 
		if ( dp != NULL ) { 
			return dp; 
		} 
		devp = devp->next; 
	} 
	return NULL; 
}

struct usb_device *findOzy(struct usb_bus *busp) { 
	struct usb_bus *bp; 
	struct usb_device *devp; 
	bp = busp; 
	while ( bp != NULL )  {
		if ( bp->root_dev != NULL ) { 
			if ( (devp = findOzyFromDevices(bp->root_dev)) != NULL ) { 
				return devp; 
			} 			
		}
		bp = bp->next; 
	} 
	return NULL; 
} 

int OzyUSBinitialized = 0;

struct OzyHandle *SavedOzyh = NULL;
int SavedOzyHandleUseCount = 0;  

/* 
 * Returns !0 if Ozy found attached to bus, 0 if not attached 
 */ 
KD5TFDVK6APHAUDIO_API int IsOzyAttached(void) { 
	struct usb_bus *busp; 
	struct usb_device *ozydevp; 

	isMetis = 0; 

	if ( !OzyUSBinitialized ) { 
		usb_init(); 
		OzyUSBinitialized = 1; 
	}
	usb_find_busses(); 
	usb_find_devices(); 
	busp = usb_get_busses(); 

	ozydevp = findOzy(busp); 	
	if ( ozydevp == NULL ) { 
		return 0; 
	}
	/* else */ 
	return 1; 
}

struct OzyHandle *internalOzyOpen(void)
{
	struct OzyHandle *ozyh = NULL; 
	struct usb_bus *busp; 
	struct usb_device *ozydevp; 
	struct usb_dev_handle *ozyusbh; 
	int rc; 
	
	ozyh = (struct OzyHandle *) malloc(sizeof(struct OzyHandle)); 
	if ( ozyh == NULL ) { 
		return NULL; 
	} 
	ozyh->devp = NULL; 
	ozyh->h = NULL; 


	if ( !OzyUSBinitialized ) { 
		usb_init(); 
		OzyUSBinitialized = 1; 
	}
	usb_find_busses(); 
	usb_find_devices(); 
	busp = usb_get_busses(); 

	ozydevp = findOzy(busp); 
	if ( ozydevp == NULL ) { 
		fprintf(stdout, "findOzy didn't!\n"); fflush(stdout); 
		free(ozyh); 
		return NULL; 
	} 
	
	/* let's see if we can open the device */ 
	ozyusbh = usb_open(ozydevp); 
	if ( ozyusbh == NULL ) { 
		fprintf(stdout, "open on ozy failed\n"); fflush(stdout); 
		free(ozyh); 
		return NULL; 
	} 

	/* set the configuration */ 
	rc = usb_set_configuration(ozyusbh, 1); 
	if ( rc != 0 ) { 
		fprintf(stdout, "ozy set config failed rc=%d\n", rc); fflush(stdout); 
		free(ozyh); 
		return NULL; 
	} 

	/* claim the  interface */ 
	rc = usb_claim_interface(ozyusbh, 0); 
	if ( rc != 0 ) { 
		fprintf(stdout, "usb_claim_interface failed: rc=%d\n", rc); fflush(stdout); 
		free(ozyh); 
		return NULL; 		
	} 

	rc = usb_set_altinterface(ozyusbh, 0); 
	if ( rc != 0 ) { 
		fprintf(stdout, "warning: usb_set_altinterface failed: rc=%d\n", rc); fflush(stdout); 
	} 
	 
	/* clear halts */ 
	rc = usb_clear_halt(ozyusbh, 0x86); 
	if ( rc != 0 ) { 
		fprintf(stdout, "warning: usb_clear_halt on ep 0x86 failed: rc=%d\n", rc); fflush(stdout); 
	} 

	rc = usb_clear_halt(ozyusbh, 0x84); 
	if ( rc != 0 ) { 
		fprintf(stdout, "warning: usb_clear_halt on ep 0x84 failed: rc=%d\n", rc); fflush(stdout); 
	} 

	rc = usb_clear_halt(ozyusbh, 0x2); 
	if ( rc != 0 ) { 
		fprintf(stdout, "warning: usb_clear_halt on ep 0x2 failed: rc=%d\n", rc); fflush(stdout); 
	} 

	/* if we get here all is well - device is open and claimed */ 
	ozyh->devp = ozydevp; 
	ozyh->h = ozyusbh; 
	return ozyh; 
}

KD5TFDVK6APHAUDIO_API void *OzyHandleToRealHandle(struct OzyHandle *ozh) {
	return (void *)ozh->h; 
}

KD5TFDVK6APHAUDIO_API struct OzyHandle *OzyOpen(void) {
	//!!fixme -- need mutex
	if ( SavedOzyh == NULL ) { /* not opened yet - open it */  
		SavedOzyh = internalOzyOpen(); 
		if ( SavedOzyh != NULL ) {
			  SavedOzyHandleUseCount = 1; 
		} 
		return SavedOzyh; 
	} 
	/* else */ 
	/* handle already exisits - just bump use count and retrn it */ 
	++SavedOzyHandleUseCount; 
	return SavedOzyh; 	 
}  

void internalOzyClose(struct OzyHandle *h)
{
	usb_release_interface(h->h, 0x0); 
	usb_close(h->h); 
	free(h); 
	return; 
}

KD5TFDVK6APHAUDIO_API void OzyClose(struct OzyHandle *h) {
	//!!fixme -- need mutex 
	if ( h != SavedOzyh ) {
		printf("unkmown ozyh closed!"); 
		return; 
	}
    if ( SavedOzyh == NULL ) {
    	printf("close already closed ozyh");  
    } 
    --SavedOzyHandleUseCount; 
    if ( SavedOzyHandleUseCount == 0 ) {
    	internalOzyClose(SavedOzyh); 
    	SavedOzyh = NULL;  
    } 
    return; 
}

///////////////////////////////////////////////////
// USB functions to send and receive bulk packets
KD5TFDVK6APHAUDIO_API int OzyBulkWrite(struct OzyHandle *h, int ep, void* buffer, int buffersize)
{
	int rc; 
	rc = usb_bulk_write(h->h, ep, (char *)buffer, buffersize, OZY_IO_TIMEOUT); 
	return rc; 	
}

KD5TFDVK6APHAUDIO_API int OzyBulkRead(struct OzyHandle *h, int ep, void* buffer, int buffersize)
{ 
	int rc; 
	rc = usb_bulk_read(h->h, ep, (char *)buffer,  buffersize, OZY_IO_TIMEOUT);
	return rc; 
}

KD5TFDVK6APHAUDIO_API int LoadFirmware(int VID, int PID, char *filename) {
    int myvid = VID;
    int mypid = PID;
	  
    usb_dev_handle *hdev = NULL;
    struct usb_bus *bus = NULL;
    struct usb_device *dev = NULL;
    
    usb_init();
    usb_find_busses();
    usb_find_devices();
    dev = NULL;
    bus = NULL;
	
    for (bus = usb_get_busses(); bus; bus = bus->next)
    {
        for (dev = bus->devices; dev; dev = dev->next)
        {
			//sleep(1);
            if (dev->descriptor.idVendor == myvid 
                    && dev->descriptor.idProduct == mypid)
            {
				hdev = usb_open(dev);
				break;
            }
        }
    }
    
    if(!hdev) {
        printf("error: did not find usb device 0x%X 0x%X\n", myvid, mypid);	
        return -1;
    } 
	else {
		printf("found and opened usb device\n");		
    }
    
    // RESET CPU
    if (Reset_CPU(hdev, 1) == -1) {
        printf("error reseting CPU\n");
        usb_close(hdev);
        return -1;
    } 
	else {
        printf("cpu in reset...\n");
    }
    
    // LOAD FIRMWARE
	Sleep(1);
	printf("Loading firmware...  ");        
    if (Load_Firmware(hdev, filename) == -1) {
        printf("error loading firmware\n");
        usb_close(hdev);
        return -1;
    } 
	else {
        printf("firmware loaded\n");
    }
    
    // TAKE CPU OUT OF RESET
    if (Reset_CPU(hdev, 0) == -1) {
        printf("error starting CPU\n");
        usb_close(hdev);
        return -1;
    } 
	else {
        printf("cpu was restarted...\n");
    }
    
    usb_close(hdev);
    hdev = NULL;
    
    return 0;
}
        
KD5TFDVK6APHAUDIO_API int LoadFPGA(int VID, int PID, char *filename) {
    int myvid = VID; //OZY_VID;		
    int mypid = PID; //OZY_PID;		
           
    usb_dev_handle *hdev = NULL;
    struct usb_bus *bus = NULL;
    struct usb_device *dev = NULL;
    
    usb_init();
    usb_find_busses();
    usb_find_devices();
    dev = NULL;
    bus = NULL;
    
    for (bus = usb_get_busses(); bus; bus = bus->next)
    {
        for (dev = bus->devices; dev; dev = dev->next)
        {
            if (dev->descriptor.idVendor == myvid 
                    && dev->descriptor.idProduct == mypid)
            {
                hdev = usb_open(dev);
            }
        }
    }
    
    if(!hdev) {
        printf("error: did not find usb device 0x%X 0x%X\n", myvid, mypid);		
        return -1;
    } 
	else {
		printf("found and opened usb device\n");
    }
	printf("Loading FPGA...\n");        

    if (Load_Fpga_Rbf(hdev, filename) == -1) {
        printf("failed to load FPGA\n"); 
		return -1;							        
    }     
    usb_close(hdev);
    
    hdev = NULL;
    
    return 0;    
}

void getI2CByte(int i2c_value) {
    unsigned char *bufp = NULL;
    int count;    
    usb_dev_handle *usb_h = NULL;

    bufp = (unsigned char *)malloc(sizeof(char));

    OzyH = OzyOpen();
    usb_h = (usb_dev_handle *)OzyHandleToRealHandle(OzyH);

    count = usb_control_msg(usb_h, 
                            VRT_VENDOR_IN, 
                            VRQ_I2C_READ, 
                            i2c_value, 
                            0, // index
                            (char *)bufp, 
                            sizeof(bufp), 
                            1000);
    OzyClose(OzyH);

}

void getI2CBytes(int i2c_value) {
    unsigned char *bufp = NULL;
    int count;    
    usb_dev_handle *usb_h = NULL;

    bufp = (unsigned char *)malloc(sizeof(char)*2);

    OzyH = OzyOpen();
    usb_h = (usb_dev_handle *)OzyHandleToRealHandle(OzyH);

    count = usb_control_msg(usb_h, 
                            VRT_VENDOR_IN, 
                            VRQ_I2C_READ, 
                            i2c_value, 
                            0, // index
                            (char *)bufp, 
                            sizeof(bufp),
                            1000);
    OzyClose(OzyH);

}

KD5TFDVK6APHAUDIO_API int GetOzyID(struct usb_dev_handle *hdev, 
								   unsigned char *buffer, 
								   int length)
{
    int count;
    int nsize = 8;      
    
   // if (qswasinit == _FALSE) return -1;
    if (length != nsize) return -1;
    
    count = usb_control_msg(hdev, 
                            VRT_VENDOR_IN, 
                            VRQ_SDR1K_CTL, 
                            SDR1KCTRL_READ_VERSION, 
                            0, // index
                            (char *)buffer, 
                            nsize, 
                            1000);
    if (count != nsize) 
        return -1;
    else
        return count;
}
#if 0
KD5TFDVK6APHAUDIO_API int Write_I2C(struct usb_dev_handle *hdev, 
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
#endif

KD5TFDVK6APHAUDIO_API int WriteControlMsg(struct usb_dev_handle *hdev,
                                          int requesttype,
                                          int request, 
                                          int value, 
                                          int index, 
                                          unsigned char *bytes, 
                                          int length,
										  int timeout)
{
    int r = usb_control_msg(hdev, requesttype, request, value, index,
                            (char *) bytes, length, timeout);
    
    if (r < 0) {
        if (errno != EPIPE)
            printf("usb control message failed: %s\n", usb_strerror());
    }
    return r;

}

KD5TFDVK6APHAUDIO_API int WriteI2C(struct usb_dev_handle *hdev, 
								   int i2c_addr, 
								   unsigned char *buffer, int length)
{
    return (Write_I2c(hdev, i2c_addr, 
             buffer, length));    
}

KD5TFDVK6APHAUDIO_API int ReadI2C(struct usb_dev_handle *hdev, 
								   int i2c_addr, 
								   unsigned char *buffer, int length)
{
    return (Read_I2c(hdev, i2c_addr, 
             buffer, length));    
}

KD5TFDVK6APHAUDIO_API int Set_I2C_Speed(struct usb_dev_handle *hdev, int speed)
{
            int ret = usb_control_msg(hdev,
                VRT_VENDOR_OUT,
                VRQ_I2C_SPEED_SET,
                speed,
                0x00,
                0,
                0,
                1000
                );

            if (ret < 0)
                return FALSE;
            else
                return TRUE;
}