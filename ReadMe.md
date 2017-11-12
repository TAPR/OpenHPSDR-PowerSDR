# OpenHPSDR-PowerSDR

Latest Release v3.4.3 November 11, 2017

Read the RealeaseNotes.md for more information.

# 3.4.3 (2017-11-11)

# 3.4.2 (2017-7-5)
- CTUN has had several modifications and bug fixes.
- Band Stacks have been increased to 5 deep for each band.
- CW Filters are now being saved correctly.

# 3.4.1 (2017-6-1)
- Swapped places with XIT and RIT controls on the console.
- Added 4 CAT Commands
 - ZZAP Audio Peak Filter On/Off
 - ZZAT APF Tune
 - ZZAB APB Bandwidth
 - ZZAA APF Gain
- Corrected compatibility issue with N1MM+ and FocusMaster.
- relabeled the 'CW Break-In' feature from 'Enabled' to 'Semi Break-In' to better describe the actual behavior. Toggles between Full Break-In and Semi Break-In.
- Single side-band Full Carrier (SSBFC)
- Continuous Frequency Compressor (CFC) audio tools
- Database enhancements allowing importing old databases

# 3.3.17 (2017-5-16)
- Corrects a compatibility issue with DDUtil
- Corrects the 10x watt meter reading for the Anan-10/10E transceivers
 
# 3.3.16 (2017-5-14)
- Corrects sporadic HIGH SWR message found in v3.3.14 & v3.3.15
- Chris, W2PA added support for the Behringer CMD Micro and CMD PL-1 MIDI controllers in the Midi2Cat interface. More information can be found in the BehringerMods_Midi2Cat_v2.pdf located in the PowerSDR working directory.
- Chris, W2PA added a RIT/XIT sync feature to the console that increments RIT and XIT the same amount when adjusting either of the values.
- Corrected an issue with the 'Limit Stitched Receivers' feature not updating when using the 8000DLE.
- Added 200W Meter Trim range for the 8000DLE.


