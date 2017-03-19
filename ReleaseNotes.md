<<<<<<< HEAD
# PowerSDR_mRX_PSVersion: 3.2.21.0 January 23, 2015
=======
# PowerSDR_mRX_PSVersion: 3.2.21.0Date: January 23, 2015
>>>>>>> d1b40c8935e40835e810fd6fd087c10700a80c48




# 3.2.21 (2015-1-23)

-CESSB (Controlled Envelope Single SideBand):
Dave Hershberger, W9GR, published an interesting article on "CESSB" in the November/December 2014 issue of QEX.  The intent of his algorithm is to increase "talk-power" by bringing the average power/speech level closer to the peak level.  This is also the algorithm that Flex chose for incorporation in their recent 6000 series transceivers.  The algorithm can be viewed as comprising two series-connected blocks:  (1) the "baseband RF Speech Clipper," and (2) the filter overshoot control.  
The first block, the "baseband RF Speech Clipper" is what we've used for speech processor ("COMP") functionality for the last three years or so.  
(Phil used this in KISS even before that.)  This release adds the "filter overshoot control" block which can be optionally enabled in Setup on the DSP/Options tab.  Note that block (2) will NOT function unless "COMP" is enabled.  However, COMP will function as before whether or not (2) is enabled.  In our implementation, this functionality is available for ALL voice modes, not just SSB.  We are grateful to several members of our group who have tested and provided feedback on this addition!  Note that not everyone has gotten the same results by enabling item (2); this may depend upon voice characteristics and other settings.

-RX2 DSP Buffer Size bug:
Jack, K1VT, reported what we believe to be a long-standing bug that resulted in the DSP Buffer Size for the second receiver, RX2, to be set to the default value rather than the database-saved value when PowerSDR was opened.  This resulted in noticeably different delays through RX1 and RX2 when listening to the same station with both receivers.  This has been fixed.

-Squelch Tail Length Control for NON-FM (SSB/AM/CW/...) modes:
This new control has been added in Setup on the DSP -> AM/SAM tab.

- Diversity 'Enable' button text changed to 'Enabled' with green background when ON and 'Disabled' with red background when OFF.
- Bar meter (Original) implemented in the 'Collapse' display.
- 'Disable PA' control now takes effect immediately.
- Added shortcut keys that will allow the CWX memories to be called without the CWX form being open. To use press <CNTRL> + F1-F10. This changed was submitted by Roberto, IK4JPN

# 3.2.22 (2015-1-24) 
- Fix for bug introduced in 3.2.21 causing XVTR Form to crash. 
- RIT will control RX1 and RX2 when VFOSunc is enabled. 

# 3.2.23 (2015-1-30)
- Corrects the sideband reversal when using the SAM sideband select feature.
- Corrects the NaN error in the SWR display.
- Corrects error in reporting negative 6m reverse power values.
- Corrects problem when tuning in the 10GHz frequency range using a DJ Console.
- Added workaround for initializing Hermes PA relays.
- Added "Stereo Diversity" capability for Beta testing.

# 3.2.28 (2015-10-3)
- added Spectral Noise Blanker (SNB)
- Radio Astronomy (RA) enhanced for use with Rx2
- corrected a undesired behavior preventing discovery from working when using a static IP address
- added the ability to split the open collector outputs between VFOA (1-4) and VFOB (5-7) "4x3 Split"
- added multilingual for ToolTip information
- fixed 6m panadapter display reading for the Anan-10E
- fixed crashing on startup when the Disable PA for HF/VHF is enabled

# 3.2.29 (2015-10-4)
- fixed open collectors not functioning during transmit
- fixed the problem with crashing using Split mode
- removed the German console language translation, ToolTip translation remains 
- added a colon to the LED fonts

# 3.3.6 (2015-11-16)
- Add Multi Notch Filter
- Added CAT command for Spectral Noise Blanker ZZNN RX1 & ZZNO RX2.
- Added 25Hz Step Tune.
- Extended CAT command ZZPB to set & get 10dB, 20dB, and 30dB settings.
- Removed the subnet mask requirement for static ip addresses.
- Extended out-of-band transmit between 29.7-61.44MHz.
- Improved Russian language translation for the ToolTips by Michael, R2AGG.
- Corrected a problem with the VFO not updating when dragging the panadapter in CW mode

# 3.3.7 (2016-4-3)
* Added a completely new MIDI mapping interface from Andrew, M0YGG. This new interface is called Midi2Cat and replaces the DJ Console midi controller interface. It has the ability to map any midi device. We want to give Andrew a huge thanks for sharing this very nice project.
The User Guide for MidiCat is located in the PowerSDR program folder or can be downloaded from the Yahoo Apache Labs group files folder. Look for 'Midi2Cat_Instructions_V3.pdf'. One change to note that has changed since the user guide was written is the Midi Interface is located on the 'CAT' page within the Setup Form.

* Our Spectral Noise Reduction, NR2 has some significant audio improvements.  If you haven't tried it in a while, please take a moment to do so.  Just as a reminder, Noise Reduction is for Random Noise, which we all have.  Also, be sure to set your AGC Gain properly when using noise reduction --- AGC is the "enemy" of noise reduction if the gain is set too high, i.e., if the green "G" line is set too low.

* MNF, the Multi-Notch Filter, is now functional on transverter frequencies.

* There are some enhancements and changes to the Display controls.  
- First, you'll find that you can now set averaging and choice of detector separately for the panadapter and the waterfall. 
 
- There is now a choice of "detectors."  For a digital "spectrum analyzer" like your SDR display, a "detector" can be defined as the algorithm used to convert FFT bins into the values displayed for the pixels across the screen.  The "Peak" detector (default) is best for signals; however, it consistently overstates noise.  If you're interested in noise measurements (band noise, phase noise, etc.), the "Average" detector would be the optimum choice and I recommend one of the larger FFT sizes.  A "Sample" detector and "Rosenfell" detector are also provided.  To learn more about detectors, see Agilent Application Note 150, "Spectrum Analyzer Basics," which you can find with Google's help.
- Multiple averaging modes are provided.  For general signal display and operation, the "Log Recursive" mode (default) may be your favorite choice as it has a visual appearance of being very responsive.  For noise measurements, you should choose one of the other modes for accuracy, probably the "Recursive" mode is optimum.  The "Log Recursive" mode will NOT be accurate for noise measurement.
- For convenience, when either the Average or Sample detector is selected for doing noise measurements, checking the "1 Hz BW:  Av / Sa" box will automatically normalize the displayed result to that of a 1 Hz bandwidth, i.e., this yields the "dBm/Hz" level.  This check box has no effect if either the Peak or Rosenfell detector is selected.

Several changes to the RAForm were made to this release.
- fixed signal level discrepancies between PowerSDR S-meter readings and RA utility readings.
- corrected the signal averaging code to take the logarithm of the averaged power values instead of the incorrect average of the logarithms of the power values
- added global region support to make file read/write functions operate correctly in all country regions
- increased the maximum number of data points limit to 2,000,000 for each of the time, RX1, and RX2 data arrays
- increased the maximum selectable value for x-axis display range to 100,000 seconds
- the above two new limits permit more than 27 hours of continuous data recording                         
- added control value updates on file read/write for the "numericUpDown_mSec_between_measurements" control and the "numericUpDown_measurements_per_point” control
- added display of Date/Time and Comment information on file READ

Other additions and fix that were added are:
- While PowerSDR is running, the Windows system timer will be set to a 1mS precision. On exiting PowerSDR the system timer will revert back to its previous setting.
- The 60m band frequency for the Netherlands region was changed to allow transmit between 5.35MHz and 5.45MHz.
- Removed the DJ Console project.
- Removed the transmit low filter cutoff restriction.
- Corrected a problem when using CTUN when Stereo Diversity (SD) is enabled.
- Added a Keyboard mapping for Transmit and Receive.
- Corrected several issues with the Waterfall display.







