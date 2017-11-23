# PowerSDR_mRX_PS 3.4.6.0 November 20, 2017


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
- added control value updates on file read/write for the "numericUpDown_mSec_between_measurements" control and the "numericUpDown_measurements_per_point" control
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

# 3.4.1 (2017-4-1)
Database Enhancements that you've ALL been waiting for!  (Courtesy of Chris, W2PA)
To make it easier to retain settings from one release to the next, the Import Database function has been re-written.  You no longer have to re-create your settings, either in the main window or in the various Setup options, when you install a new version of OpenSDR-PowerSDR mRX PS. 

When you start up a new release for the first time, the program will detect that your database file was created by a previous version and attempt to create a new database version from it. The first thing that happens is a database reset, so that you start with a fresh one that's completely valid for the current release, and then the program closes as always for a reset. When you re-start it, if the old database file is intact and was working with the previous release, it should import fine and a pop-up window will tell you it succeeded.  When you close this info window by clicking "OK" the program will start-up normally and have all your previous settings and options, looking just like it did before you upgraded to the new version.  The old version of your database file is saved unchanged in the DB_Archive folder, and a new database.xml file replaces it in same place it was before. There should no longer be any need for Setup window screen shots. 

If desired, you can still open the Setup window, manually reset the database using the "Reset Database" button, and then import another valid database file by clicking the "Import Database" button, or start personalizing from scratch as before. 

The new database importer also checks for badly corrupted database files and rejects them, directing the user to reset and start over, or try importing another database file that is known to have functioned in the past.  In most cases, even with a very old database file, at least some of the previous settings and options will be imported. 

In addition to handling complete database files created in an old release, the new Import Database function in Setup lets you import *partial* databases, too.  This means if you have a database file that contains a subset of settings or options, the Import function will simply add them to what is already present in the running database (still requiring a re-start, as usual).  For example, if you have a database (XML) file containing a single Transmit Profile definition, you can send it to a friend and they can import it so they can use your customized profile too. 

Therefore, to support this kind of TXProfile sharing, there is also a new button in the Setup - Transmit tab labeled "Export Current Profile." When clicked, it produces this kind of XML file containing only the currently selected Transmit Profile.  The file will be stored in the same directory where your database.xml file is located, and given a file name that's the same as the exported transmit profile's name.  You can then send it to another ham for import into their own database using the Import Profile button in Setup.

To summarize, the following controls in the Setup - Transmit tab support working with transmit profiles and databases:
1) The "Profiles" section at the upper left, where you can select which profile is the current one from your personal collection.  This is where you can also save a profile you've just set up, under a new name, or delete one you no longer need.
2) The "More Profiles" section at the upper right.  When you check the "More Profiles" checkbox, you get a scrollable list of additional default profiles that come with the installation. You can then bring the one you highlight (click on) into your own group of profiles (see #1) by clicking the "Include" button just below this list.
3) At the center extreme right side is a new button labeled "Export Current Profile" that you can use to export the currently active transmit profile (see #1) to send to someone or archive for yourself.
4) At lower left are three Database buttons that operate as before, except the "Import Database" function can now be used to import transmit profiles that were produced by the Export button - either by you or someone else.  (These three buttons appear no matter what tab you've selected in setup.)

In addition to using Setup, you can select which of your saved profiles is to be used as the current (operational) profile in the main window using the pull-down list at lower right near the other audio controls, which appears when a voice mode (e.g. SSB, DSB, AM, or FM) is selected.

To export a transmit profile:
1) Click the Setup menu item to bring up the Setup window, and select the Transmit tab.
2) Select the profile you want to export using the "Profiles" pull-down list at upper left.
3) Click the "Export Current Profile" button at center, extreme right.
4) A file with the same name as the profile is written in your database directory

To import a transmit profile:
1) Click the Setup menu item to bring up the Setup window, and select the Transmit tab.
2) Click the "Import Database" button at lower left.
3) Select the database file containing the profile you want to import, and click OK
4) If the import is successful, you will be notified that the program will shut down.
5) Re-start OpenSDR-PowerSDR mRX PS. 
6) The newly imported profile will appear as an additional choice in the Profiles pull-down list in Setup-Transmit.
New Transmit Modes
For some time, on the DSP => AM/SAM tab in Setup, you have been able to select to receive only one sideband when operating in SAM mode.  This is useful to eliminate interference from nearby stations on one side or the other of your QSO.  Now, you have a similar capability for transmit!  On this same tab, you can select to transmit both sidebands (normal AM/SAM transmission) or only one of the sidebands, Upper or Lower, when operating in AM and SAM modes.
New Audio Tools
Do you have a rack of expensive audio equipment that you use to process your MIC signal before feeding it into your radio?  Do you run the digital equivalent, a Digital Audio Workstation (DAW), on your computer?  Well, if so, you can certainly continue to do that.  However, included in this release, we are providing some important audio processing tools that enable you to IMPROVE YOUR TRANSMIT AUDIO and INCREASE THE "DENSITY" AND AVERAGE POWER of your signal without requiring these external options.  The new tools are primarily a Phase Rotator and a Continuous Frequency Compressor (CFC).  Accompanying these advances are additions to the TX Profiles and enhancements in the TX Equalizer and in ALC Compression control.
Rob, W1AEX, has generously prepared a "CFC Quick Setup Guide" as well as two videos, one focusing on the CFC and another focusing on the Phase Rotator.  Thanks Rob!!
The Guide and Videos can be found here:
 Guide:  CFC Quick Setup Guide  
CFC Video:  <https://www.youtube.com/watch?v=j84LuuI70O4 >  
Phase Rotator Video:  <https://www.youtube.com/watch?v=NM2x2tk0UbY>  
Also, Scott, WU2O, has developed an excellent block diagram of the TX Chain for reference during setup.  It can be found here:  TX Chain  
* Enhanced TX Equalizer
Equalizer frequencies are now operator-adjustable.  Also note, that as it has been since the installation of the WDSP library a few years ago, this is a Continuous Gain Equalizer, NOT a multi-band equalizer.  This provides smooth gain transitions across the spectrum as opposed to abrupt transitions at band edges.
* Phase Rotator
In some AM transmitters, it is possible to boost the peak output power by having an asymmetrical audio waveform (positive peaks greater than negative peaks) and modulating to greater than 100% on positive peaks while restricting to <=100% on negative peaks to avoid "pinch-off."

In other AM transmitters and in the case of our digital-up-conversion (DUC) SDRs, this is not the correct approach.  We have hard-limits which cannot be exceeded, such as the dynamic range of the DAC.  The application of an asymmetrical audio waveform, with positive peaks greater than negative peaks, cannot further increase modulation in the positive direction; it instead REDUCES average power.  The correct approach in such cases is to make the audio waveform as symmetrical as possible, i.e., equal positive and negative peaks.

The Phase Rotator makes the audio waveform more symmetrical.  It does so by shifting the phase of various audio frequencies by varying amounts, thereby changing the shape of the waveform away from the somewhat typical asymmetrical waveform of human speech.  Experimentation and analysis show that a wide range of phase shift versus frequency generally tends to improve symmetry.  This wide range leads to an implementation with multiple identical stages, where the total phase shift is the sum of the shifts obtained in each stage.  The stages have a specified "corner frequency" where the phase shift of the stage is equal to one-half of the total that the stage provides at the maximum frequency.

While the above explanation focused on AM, note that this feature can be used to increase average power for any speech mode.  Note also, however, that it could be detrimental for any digital mode that requires coherent phase versus frequency. 

Controls are found on the new DSP => CFC tab in Setup.
* Continuous Frequency Compressor (CFC)
Many audio racks and Digital Audio Workstations provide "Multiband Compressors" which allow specifying different amounts of compression for different audio frequency bands.  The CFC offers a superset of that concept where, instead of having multiple bands with constant compression in each band, the compression varies smoothly between frequency points at which it is specified.  This concept is similar to the operation of the WDSP Equalizer function; however, in this case, we are varying the compression level across frequencies rather than varying the gain.  A Post-CFC Equalizer is also provided as an integral part of this function  to provide a final tailoring of the desired audio frequency spectrum.

Use of this function for speech modes can significantly increase the "density" and average power of the signal.  Note also that use for digital modes may be detrimental, depending upon the nature of the mode.

Controls are found on the new DSP => CFC tab in Setup.
* ALC Compression Control
On the DSP => AGC/ALC tab, you'll now find an "ALC Max Gain" control.  Adjusting the gain above 0dB (the default) has a couple purposes:  (1) if NOT using COMP/CESSB, it allows you to set the CFC output to peak at ~0dB and still get some compression (which is often desirable) in the ALC stage, and (2) if USING COMP/CESSB, it allows you to get some ALC compression even though the output of those stages does not exceed ~0dB.
* CESSB Reminder
Just as a reminder, for quite some time we have had available a CESSB Overshoot Control function.  This is yet another feature to increase average transmitted power at the same peak power.  The CESSB algorithm was published by David Herschberger, W9GR, in the NOV/DEC 2014 issue of QEX.  Dave's focus was on SSB transmission.  However, in our implementation, we support its use in all voice modes.  NOTE THAT A "LINEAR PHASE" TRANSMIT FILTER MUST BE SELECTED FOR THIS FUNCTION TO OPERATE PROPERLY.

This can be enabled on the Transmit tab in Setup.  Note that COMP must be enabled (even at 0dB compression if you prefer) for this feature to function.
* New TX Profiles
Need some help getting all these new audio controls set up?  Some TX Profiles are provided to give you some starting points.  Specifically, courtesy of Rob, W1AEX, four new TX Profiles are included with the PowerSDR mRX application.  These are:
- SSB 2.8_CFC (2.8k wide - very helpful on 60 meters)
- SSB 3.0_CFC (3.0k wide)
- SSB 3.3_CFC (3.3k wide)
- AM 10_CFC (5k + 5k for 10k total width)
Other Fixes, Changes, and Enhancements
* Four new CAT commands have been added to support the CW Audio Peaking Filter:
-- ZZAP Audio Peaking Filter On/Off
-- ZZAT APF Tune
-- ZZAB APF Bandwidth
-- ZZAA APF Gain
* A change in the format of packets sent out by N1MM+ caused an incompatibility with the FocusMaster feature.  This has been fixed.
* The locations of XIT and RIT have been interchanged on the console.
* There is a change in labeling of the CW Break-in checkbox.  This change was made so that the label more accurately reflects the actual function of the box.  This should eliminate some questions and confusion.  The checkbox was previously labeled "Enabled" which would imply either ON or OFF for Break-in.  Instead, what this box actually does is to allow you to select either FULL Break-in or SEMI Break-in.

# 3.4.2 (2017-7-5)
# CTUN operation:
CTUN has been modified to make mode changes behave in similar ways, whether CTUN is on or off, and are identical to the way they always worked with CTUN off in previous versions.  Behavior when tuning has also changed.  As the VFO approachs the edge of the display, instead of disappearing off the edge or stopping, the display re-centers itself so tuning is continuous, even in CTUN mode. The re-centering occurs as the edge of the passband hits the edge of the display, in order to keep any signals of interest visible even as it approaches the edge.  In addition, zooming in while in CTUN mode automatically centers the VFO in the spectrum display so that a signal of interest (i.e. the one you are tuned to) gets zoomed in on, as is usually the intent.  When zooming out, re-centering does not occur, since that would not cause the VFO to disappear off the edge of the display.
 
# Band Stacks - import and size: 
This modification adds the capability to import BandStack information from an older database, especially useful on starting up a new version, or simply importing while already  running on a current version database.  In 3.4.1 this was not yet handled.  In addition, the bandstacks have been increased to be 5-deep instead of 3.

# CW Filter controls and setup:
This modification fixes the problem of CW filters not getting saved, and worse, being lost whenever band changes or mode changes are made. It also makes the actions of the width/shift/hi/lo/CWpitch controls all act more consistently and intuitively, specifically for CW operation.  See details section below.

# CW Filter operation details:
There are several adjustments that affect the receiver filter settings in CW modes.  They are: Filter buttons, Width, Shift, High, Low, and CWPitch, and they are somewhat interdependent.

Filter Buttons
The filter selection buttons choose pre-defined receiver bandpass filters. They are customizable by right clicking on each button and then choosing its width, or low and high limits.  For CW it is recommended that you initially choose a passband centered on the CW Pitch frequency, since CW filters will automatically be centered whenever the CW Pitch is changed.

Width
Sliding the Width control automatically switches to the Var1 filter so that your pre-defined Filter buttons aren't changed from the width you set them up for and labeled them as.  Moving the slider left decreases the passband width while sliding right increases it.  As you increase width, one of the passband edges (the upper edge in the "Lower" modes such as CWL, or the lower edge in the "Upper" modes, like CWU) approaches the limit where opposite sideband images appear (i.e. a value of zero (0) in High or Low).  When this happens, the width continues to increase but only in one direction - downward in a "Lower" mode, and upward in an "Upper" mode - so as to keep from hearing these images.  If you subsequently move the passband across the sideband (image) boundary, enforcement of the boundary ceases and you can change the width centered on wherever you've moved the passband with Shift. When you move the passband back across this boundary, the width control again obeys this limit. Clicking on a filter button other than Var1 resets the receiver to the filter settings assigned to that button, but Var1 remains as you set it, until you change it again, either by clicking on Var1 or having it be automatically selected by using one of the adjustments.

Shift
Sliding the Shift control automatically switches to the Var1 filter so that your Filter buttons aren't changed from how you set them up.  Moving the slider left shifts the passband down in frequency while moving it right shifts the passband upward. The passband shift is not restricted the way the Width control is and can freely slide up and down from one sideband to the other (and affects how the Width control operates as described above).  The "Reset" button returns only the Shift slider to its original position.  Clicking on another filter button resets the receiver to the filter settings assigned to that button, but Var1 remains as you set it, until you change it again.

High/Low
The High control shifts only the high frequency edge of the passband.  It is inactive when a pre-defined Filter button is selected, but becomes active when Var1 or Var2 is selected.  It is also possible to control it using the CAT interface or a MIDI controller.  When you do that while a pre-set filter button is selected, Var1 is automatically selected, just as with the Width and Shift sliders.  The Low control works the same way, but affects the low frequency edge of the passband. Mapping a MIDI controller knob to these functions gives you a control that operates just like the Low Cut and High Cut adjustments some transceivers provide.

CW Pitch
The CW Pitch control determines how far (in Hz) a CW signal is offset so that it produces an audible tone when the VFO is tuned to indicate the signal's actual frequency.  Without this offset, tuning a CW signal to zero-beat would be at the actual zero-beat point where no audible tone would be present because its frequency is zero.  Thus, when you tune the VFO to a point where you hear a station's tone exactly match the CW Pitch setting, you are tuned to transmit at that station's exact frequency. 

Changing the CW Pitch control has several effects, and its interaction with the filter buttons can get a little complicated.  First, this tone at "zero-beat" changes, and so does the sidetone as an aid to tuning in a station to match the CW Pitch (and offset).  Second, the CW filters are all adjusted to keep themselves at your originally set bandwidths and centered on the CW Pitch (offset) frequency. That way, whenever a CW signal is tuned to its exact frequency, it's positioned in the center of the passband.  

The CW Pitch isn't usually adjusted as part of tuning in a station and tweaking filters to reduce interference. And normally, when you customize your CW Filter button settings, you configure all of them while keeping the CW Pitch setting constant, using the Width (or Low and High) setting for each button, centered around the CW Pitch frequency.  Once set that way, they will always return to these settings whenever you choose that particular CW Pitch.  When you vary the pitch from that value, the CW filters change themselves to track the CW Pitch as described above (but, of course, they retain their width as originally set to match their button's label).  Note, however, if you customize a CW filter button in a way that is not centered on the CW Pitch frequency, the next time you change CW Pitch that filter will center itself.  Bandwidth takes priority over Low/High setting values for the filter selection buttons, so that their labels always match their bandwidths.  There is one exception: If you lower the CW Pitch below the point where the passband edge hits the sideband (image) limit, the passband stops moving while you can continue to lower the pitch - but it will no longer be centered in the passband.

# 3.4.4 (2017-11-19)
# Improved CTUN mode operation:
A "CTUN Scroll" check box has been added to the Setup-General-Options tab.
When this box is checked:
- The display scrolls when the VFO gets near the display edges allowing tuning to continue. The VFO cursor stays near the scrolling edge.
- Frequency changes greater than 500kHz cause a re-centering (e.g. when recalling a memory from a far removed frequency)
When this box is unchecked:
- CTUN behaves as before these CTUN enhancements - VFO tuning stops at the receiver bandwidth edges (e.g. 192kHz edges)

Forcing CTUN to turn OFF when selecting Split or MultiRX has been eliminated.

# MIDI controller support 
MIDI controller mapping now supports the Behringer CMD Studio 2a.  In fact, it may work with all the Behringer controllers now, barring unforseen additional behavior of specific controllers that differs from the currently supported ones (the CMD PL-1, CMD Micro, and now CMD Studio 2a).

Fixed MIDI/CAT VFO manipulations (A>B, B>A, A<>B) so they behave exactly like the corresponding buttons on the console. Previously the CAT versions of these commands only changed frequency and nothing else associated with the VFO (e.g. mode).

Added handling of variable codes coming from wheels in the Hercules Compact controller; it was resulting in digital "backlash."

# MIDI VFO sensitivity control:
VFO knob sensitivity (speed) can now be adjusted. In the past, as you turned a MIDI knob (usually one of the big jog wheels), you got one tune step per MIDI wheel or knob MIDI message output. Now you can change the wheel sensitivity by specifying the number of MIDI updates required to produce one frequency step.  

Two new up/down controls have been added to the Setup/CAT Control tab next to the "Configure MIDI" button, labeled as ""MIDI Wheel updates/step.  These controls set the minimum and maximum number of MIDI wheel updates per frequency step (i.e. maximum and minimum wheel sensitivity, respectively).  These two values can then be alternated between using a new MIDI command and mapping as below.

# The following three functions and mappings are new:
1) "Increase wheel rotation per VFO tune step" - increments the number of wheel updates per tuning step, mappable to a button, staring at 1 and increasing by factors of 2 up to a maximum of 32.
2) "Decrease wheel rotation per VFO tune step" - decreases the number of wheel updates per tuning step, mappable to a button, starting at the current setting and decreasing by a factor of 2 down to a minimum of 1.
3) "VFO Wheel Sensitivity High/Low Toggle" - toggles between the high and low values set in MIDI setup, mappable to a button

The typical use for "Toggle" would be as a high/low tuning sensitivity (speed) control. You can zip across a band at high speed, then switch to low speed as you get close to a signal of interest. 

All of these settings, combined with the existing Tune Step MIDI settings, should allow a much greater range of tuning "feel" and control than before.  Note that the Behringer controllers' jog wheels that enable variable tuning rates continue to work as before and now can be further tailored using these settings.
 
Disabled audio processing in digital modes
CFC is now disabled automatically when switching to DIGL or DIGU.  This operates in the same way the disabling of other processing functions, such as TX EQ, operates now, in that it simply disables the function in the currently selected transmit profile to ensure that CFC isn't used in digital modes.  Note: A better way to handle this, as some are already doing, is to create a transmit profile for digital modes and switch to it before selecting a digital mode. 

# Miscellaneous
Support added for the ANAN-7000DLE transceiver.
Packet ReOrdering (PRO Latency) feature to resequence out of order packets.
Added feature to choose between receive antenna and transmit antenna.
Added the ability to block a receive antenna from being used as a tansmit antenna for antenna ports 2 and 3.
Fixed bug that caused PowerSDR to crash if console closed after Setup form was closed.
Modified VHF band stacks to be 5-deep like the others.
Eliminated most display scale shifting when going between transmit and receive.
Fixed a bug causing a crash when zoomed in past the point where the passband fits in the display. 
Fixed bugs in split VFO operation when RX2 is on.
Fixed a bug resulting in incorrect vertical display scale in transmit under certain circumstances.

# 3.4.5 (2017-11-20)
- Bug fix for .NET unhandled exception error when starting without radio online.

# 3.4.6 (2017-11-20)
- Corrected problem with blank waterfall display in Panafall mode.
