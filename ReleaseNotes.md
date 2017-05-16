# PowerSDR_mRX_PS 3.3.17.0 May 16, 2017


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

# 3.3.6 (2015-11-16)
This release includes another tool for your Noise/Interference Toolbox.  
This particular tool, Multi Notch Filter (MNF), allows you to specify up to 1024 notches.  The notches are specified by RF frequency and width and will be invoked, as needed, when they overlap the passband.  This feature is useful for those who have interference that consistently appears on specific frequencies.

To avoid phase distortion, the notches are implemented with linear phase.  Also, they introduce no additional processing delay nor do they consume any additional CPU cycles once you're on frequency and the notches are set up.  This is all accomplished by simply "cutting" the notches into the existing bandpass filters rather than adding additional filters.

Sorry, there's currently no fancy on-panadapter UI for this.  If you have programming talents and would like to write one, please let us know.

To enter notches, in Setup, go to the new DSP / MNF tab.  To Add a notch, click Add then enter the Center Frequency and Notch Width and click Enter.  Once multiple notches are in the database, you can scroll back and forth through them using the up/down arrows on the "Notch #" 
control.  When you add a new notch, it will be added immediately after the notch being viewed.  You can Edit notches by clicking Edit, making your changes, then clicking Enter.  You can Delete a notch by scrolling to the notch and clicking Delete.  If you were to enter very narrow notch widths (less than 200Hz with default filter settings), you might not achieve the full attenuation of >100dB.  By leaving "Auto-Increase width" checked, if your specified width is not enough to achieve >100dB notch depth, it will be automatically increased for you when the filter is cut in the passband.  When you are entering a new notch, rather than typing in the Center Frequency, you also have the option to tune VFO-A to the desired frequency and then click the VFOA button. 

Other changes include:
- Added CAT command for Spectral Noise Blanker ZZNN RX1 & ZZNO RX2.
- Added 25Hz Step Tune.
- Extended CAT command ZZPB to set & get 10dB, 20dB, and 30dB settings.
- Removed the subnet mask requirement for static ip addresses.
- Extended out-of-band transmit between 29.7-61.44MHz.
- Improved Russian language translation for the ToolTips by Michael, R2AGG.
- Corrected a problem with the VFO not updating when dragging the panadapter in CW mode.

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

# 3.3.8 (2016-6-9)
CRASH BUG FIX

Some experienced a crash in 3.3.7, especially when changing DSP Buffer Sizes or during RX/TX transitions.  We believe this has been totally resolved.

SIGNIFICANTLY LOWER LATENCY

Receive latency is the time between when RF reaches your antenna and the corresponding audio is produced in your speaker or headphones.  
Similarly, transmit latency is, for example, the time between audio reaching your microphone and RF being on its way to your antenna.  For many SDRs, especially those with sharp "brick wall" filters, the latency can be much larger than you might expect.  Depending upon the radio design and various settings, SDR latencies can significantly exceed 100mS.  Long latencies can create problems for the operator in contest operation, high-speed break-in CW, and even SSB rapid-turnaround VOX operation.

This release incorporates some technologies that allow us to achieve low latencies in the same category as leading conventional radios.  
Furthermore, we can do this with extremely sharp filters.

First of all, a couple basics:

* Sometime ago, we moved CW Transmit from software to the FPGA in the radio hardware.  This means that CW transmit latency was already very low, really based upon your delay settings which are chosen to avoid any hot-switching of relays.

* It has always been the case that the Buffer Size setting on the Setup=>Audio/Primary tab effects latency.  The lower the size, the lower the latency.  However, the lower the size, the more CPU cycles are required.  Depending upon the speed of your computer, you may be limited in how low you can go.  Fortunately, this is not likely to have such a large impact on your latency.  For a very rough estimate of the latency due to this buffer, divide the buffer size by the sample rate.  For example, a buffer of size 256, at a sample rate of 192K, contributes only about 256/192000 = 1.33mS.

As of this release, there are some new features and corresponding controls to allow you to achieve much lower latency:

* Up until this release, "Filter Size" and "DSP Buffer Size" have been the same and there has only been one setting, called "DSP Buffer Size."  
Filter Size determines how sharp your filters are; higher filter size leads to sharper filters.  However, higher DSP Buffer Size leads to more latency because we must collect enough samples to fill the buffer before the buffer can be processed.  As of this release, DSP Buffer Size and Filter Size are separate and can be set by mode on the Setup=>DSP/Options tab.  So, using a very low DSP Buffer size minimizes latency and using a high Filter Size leads to sharper filters.  The trade-off here is that using lower DSP buffer sizes requires somewhat more CPU cycles and using a high Filter Size does as well.  With a reasonably fast computer, you will likely be able to run at a DSP Buffer Size of 64, the minimum, except, perhaps, for the FM mode.  With filter sizes of 1024 or 2048, the sharpness of our filters rival the best radios. However, larger sizes, up to 16384, are available if you need them.

* You now have a choice of Filter Type, with two types available:  
Linear Phase and Low Latency.  In the past, our filters have always been Linear Phase.  Linear Phase filters have the property that all frequencies are delayed by the same amount of time as the signal is processed through the filter.  This means that the time-domain waveform of a signal that is totally within the passband will look the same at the input of the filter and the output of the filter.  The Low Latency filter does not strictly comply with this same type of operation.  With the Low Latency filter, signals at frequencies very near the lower and upper edges of the passband may experience more delay than signals at other frequencies.  Comparing the two types of filters, beta testers have reported little, if any, difference in sound quality, no problems with several digital modes that have been tested, and no significant negative impacts at all from using the Low Latency filters.  However, both filter types are provided for your comparison and your choice.  Of course, the Low Latency filters provide lower latency.  In fact, the latency of Linear Phase filters increases linearly with Filter Size while the latency of the Low Latency filters is very low and nearly independent of Filter Size.

Benchmark Comparisons:

* For CW/SSB receive, using minimum Buffer Sizes and Low Latency filters, our beta testers have measured receive latencies in the 15mS to 20mS range.  Using minimum Buffer Sizes and Linear Phase filters, the latencies are 25mS to 30mS for a Filter Size of 1024 and 35mS to 40mS for a Filter Size of 2048.  Using features such as noise blankers, EQ, and noise reduction will add some amount to that, depending upon the
feature(s) and settings.  These numbers compare with ~65mS and ~120mS using DSP Buffer sizes of 1024 and 2048, respectively, in prior software releases.

MINOR CHANGES
The following list of values and states where added to the TX Profiles
- selection for mic in or line in
- 20dB mic boost
- line in gain
- CESSB state
- PureSignal state

# 3.3.9 (2016-6-15)
- waterfall display showing incorrect levels on the left hand section when using stitched receivers. In some instances caused the program to crash.
- focus problem when setup or cwx form is open 
- led font not being initialized properly
- 20dB Boost not being initialized properly

# 3.3.14 (2017-3-26)
- PureSignal updated to v2.0.
- Add capibities for the ANAN-8000DLE transceiver.
- Bryan, W4WMT added a bug fix to the VAC feature that dramatically reduced buffer overruns     when using smaller buffers.
- Corrected out of band errors for the Japan region
- Fixed CTUN so that the settings would be restored correctly after restarting the program.
- Added NR2 and SNB to the DSP menu when in the Collapsed mode.
- Added the following CAT Commands:
  -- ZZLI - Sets or Reads the PureSignal (PS-A) button status
  -- ZZNS - Sets or Reads the RX1 NR2 button status
  -- ZZNV - Sets or Reads the RX2 NR button status
  -- ZZNW - Sets or Reads the RX2 NR2 button status

# 3.3.15 (2017-3-31)
  - Corrected sporadic HIGH SWR message seen in v3.3.14
  
# 3.3.16 (2017-5-14)
- Corrects sporadic HIGH SWR message found in v3.3.14 & v3.3.15
- Chris, W2PA added support for the Behringer CMD Micro and CMD PL-1 MIDI controllers in the Midi2Cat interface. More information can be found in the BehringerMods_Midi2Cat_v2.pdf located in the PowerSDR working directory.
- Chris, W2PA added a RIT/XIT sync feature to the console that increments RIT and XIT the same amount when adjusting either of the values.
- Corrected an issue with the 'Limit Stitched Receivers' feature not updating when using the 8000DLE.
- Added 200W Meter Trim range for the 8000DLE.

# 3.3.17 (2017-5-16)
- Corrects a compatibility issue with DDUtil
- Corrects the 10x watt meter reading for the Anan-10/10E transceivers




