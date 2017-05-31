
#ifndef _OZYUTILS_H
#define	_OZYUTILS_H

#ifdef	__cplusplus
extern "C" {
#endif

#include <stdio.h>
#include <errno.h>
#include <usb.h>
#include "Ozydefs.h"    
#include "Ozycommands.h"

int Write_RAM(struct usb_dev_handle *hdev, int start_addr, 
                char *buffer, size_t length);
int Reset_CPU(struct usb_dev_handle *hdev, int reset);
int Load_Firmware(struct usb_dev_handle *hdev, const char *filename);
int Load_Fpga_Rbf(struct usb_dev_handle *hdev, const char *filename);
int Write_Command(struct usb_dev_handle *hdev, int request, 
                    int value, int index, unsigned char *bytes, int length);
int Write_I2c(struct usb_dev_handle *hdev, int i2c_addr, 
                unsigned char *buffer, int length);
int Read_I2c(struct usb_dev_handle *hdev, int i2c_addr, 
                unsigned char *buffer, int length);
int Read_EEPROM(struct usb_dev_handle *hdev, 
                 int i2c_addr,
                 unsigned short offset,
                 unsigned char *buffer,
                 int length);
int Write_EEPROM(struct usb_dev_handle *hdev, 
                 int i2c_addr,
                 unsigned short offset,
                 unsigned char *buffer,
                 int length);
void uSleep(int waitTime);
#ifdef	__cplusplus
}
#endif

#endif	/* _OZYUTILS_H */

