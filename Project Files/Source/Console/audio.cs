//=================================================================
// audio.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2004-2009  FlexRadio Systems
// Copyright (C) 2010-2013  Doug Wigley
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// You may contact us via email at: sales@flex-radio.com.
// Paper mail may be sent to: 
//    FlexRadio Systems
//    8900 Marybank Dr.
//    Austin, TX 78750
//    USA
//=================================================================


//#define VAC_DEBUG
//#define MINMAX
//#define TIMER
//#define INTERLEAVED
//#define SPLIT_INTERLEAVED

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
    public class Audio
    {
        #region PowerSDR Specific Variables

        // ======================================================
        // PowerSDR Specific Variables
        // ======================================================

        public enum AudioState
        {
            DTTSP = 0,
            CW,
            /*SINL_COSR,
			SINL_SINR,			
			SINL_NOR,
			COSL_SINR,
			NOL_SINR,
			NOL_NOR,
			PIPE,*/
            //SWITCH,
        }

        public enum SignalSource
        {
            RADIO,
            SINE,
            SINE_TWO_TONE,
            SINE_LEFT_ONLY,
            SINE_RIGHT_ONLY,
            NOISE,
            TRIANGLE,
            SAWTOOTH,
            PULSE,
            SILENCE,
        }

#if(INTERLEAVED)
#if(SPLIT_INTERLEAVED)
		unsafe private static PA19.PaStreamCallback callback1 = new PA19.PaStreamCallback(Callback1ILDI);	// Init callbacks to prevent GC
		unsafe private static PA19.PaStreamCallback callbackVAC = new PA19.PaStreamCallback(CallbackVACILDI);
		unsafe private static PA19.PaStreamCallback callback4port = new PA19.PaStreamCallback(Callback4PortILDI);
#else
		unsafe private static PA19.PaStreamCallback callback1 = new PA19.PaStreamCallback(Callback1IL);	// Init callbacks to prevent GC
		unsafe private static PA19.PaStreamCallback callbackVAC = new PA19.PaStreamCallback(CallbackVACIL);
		unsafe private static PA19.PaStreamCallback callback4port = new PA19.PaStreamCallback(Callback4PortIL);
#endif
#else
        unsafe private static PA19.PaStreamCallback callback1 = Callback1; // Init callbacks to prevent GC
        unsafe private static PA19.PaStreamCallback callbackVAC = CallbackVAC;
        unsafe private static PA19.PaStreamCallback callbackVAC2 = CallbackVAC2;
        unsafe private static PA19.PaStreamCallback callback3port = Callback3Port;
        unsafe private static PA19.PaStreamCallback callback4port = Callback4Port;
        unsafe private static PA19.PaStreamCallback callback8 = Callback2;
#endif

        public static int callback_return;

        /*private static bool spike = false;
		public static bool Spike
		{
			get { return spike; }
			set { spike = value; }
		}*/

        private static bool rx2_auto_mute_tx = true;
        public static bool RX2AutoMuteTX
        {
            get { return rx2_auto_mute_tx; }
            set { rx2_auto_mute_tx = value; }
        }

        private static bool rx1_blank_display_tx = false;
        public static bool RX1BlankDisplayTX
        {
            get { return rx1_blank_display_tx; }
            set { rx1_blank_display_tx = value; }
        }
        
        private static bool rx2_blank_display_tx = false;
        public static bool RX2BlankDisplayTX
        {
            get { return rx2_blank_display_tx; }
            set { rx2_blank_display_tx = value; }
        }

        private static double source_scale = 1.0;
        public static double SourceScale
        {
            get { return source_scale; }
            set { source_scale = value; }
        }

        private static SignalSource rx1_input_signal = SignalSource.RADIO;
        public static SignalSource RX1InputSignal
        {
            get { return rx1_input_signal; }
            set { rx1_input_signal = value; }
        }

        private static SignalSource rx1_output_signal = SignalSource.RADIO;
        public static SignalSource RX1OutputSignal
        {
            get { return rx1_output_signal; }
            set { rx1_output_signal = value; }
        }

        private static SignalSource rx2_input_signal = SignalSource.RADIO;
        public static SignalSource RX2InputSignal
        {
            get { return rx2_input_signal; }
            set { rx2_input_signal = value; }
        }

        private static SignalSource rx2_output_signal = SignalSource.RADIO;
        public static SignalSource RX2OutputSignal
        {
            get { return rx2_output_signal; }
            set { rx2_output_signal = value; }
        }

        private static SignalSource tx_input_signal = SignalSource.RADIO;
        public static SignalSource TXInputSignal
        {
            get { return tx_input_signal; }
            set { tx_input_signal = value; }
        }

        private static SignalSource tx_output_signal = SignalSource.RADIO;
        public static SignalSource TXOutputSignal
        {
            get { return tx_output_signal; }
            set { tx_output_signal = value; }
        }

        private static bool record_rx_preprocessed = false;
        public static bool RecordRXPreProcessed
        {
            get { return record_rx_preprocessed; }
            set { record_rx_preprocessed = value; }
        }

        private static bool record_tx_preprocessed = true;
        public static bool RecordTXPreProcessed
        {
            get { return record_tx_preprocessed; }
            set { record_tx_preprocessed = value; }
        }

        private static short bit_depth = 32;
        public static short BitDepth
        {
            get { return bit_depth; }
            set { bit_depth = value; }
        }

        private static short format_tag = 3;
        public static short FormatTag
        {
            get { return format_tag; }
            set { format_tag = value; }
        }
        private static float peak = float.MinValue;
        public static float Peak
        {
            get { return peak; }
            set { peak = value; }
        }

        private static bool write_quad = false;
        public static bool WriteQuad
        {
            get { return write_quad; }
            set
            {
                write_quad = value;
            }
        }

        private static bool read_quad = false;
        public static bool ReadQuad
        {
            get { return read_quad; }
            set
            {
                read_quad = value;
            }
        }

        private static bool vox_enabled;
        public static bool VOXEnabled
        {
            get { return vox_enabled; }
            set { vox_enabled = value; }
        }

        private static float vox_threshold = 0.001f;
        public static float VOXThreshold
        {
            get { return vox_threshold; }
            set { vox_threshold = value; }
        }

        private static float vox_gain = 0.001f;
        public static float VOXGain
        {
            get { return vox_gain; }
            set { vox_gain = value; }
        }

        public static double TXScale
        {
            get { return high_swr_scale * radio_volume; }
        }

        public static double FWCTXScale
        {
            get { return high_swr_scale * temp_scale * radio_volume; }
        }

        private static double temp_scale = 1.0;
        public static double TempScale
        {
            get { return temp_scale; }
            set { temp_scale = value; }
        }

        private static double high_swr_scale = 1.0;
        public static double HighSWRScale
        {
            get { return high_swr_scale; }
            set { high_swr_scale = value; }
        }

        private static double mic_preamp = 1.0;
        public static double MicPreamp
        {
            get { return mic_preamp; }
            set { mic_preamp = value; }
        }

        private static double wave_preamp = 1.0;
        public static double WavePreamp
        {
            get { return wave_preamp; }
            set { wave_preamp = value; }
        }

        private static double monitor_volume = 0.0;
        public static double MonitorVolume
        {
            get { return monitor_volume; }
            set
            {
                monitor_volume = value;                
            }
        }

        private static double dsp_adjust = 1.0 / 0.98;
        public static double DspAdjust
        {
            get { return dsp_adjust; }
            set
            {
                dsp_adjust = value;
            }
        }

        //  public static double PennylanePowerBreakPoint = 0.4;
        private static double radio_volume = 0.0;
        public static double RadioVolume
        {
            get { return radio_volume; }
            set
            {
                radio_volume = value;
                if (console.CurrentModel == Model.HERMES || 
                                            console.PennyLanePresent ||
                                           (console.PennyPresent && console.CWFWKeyer))
                {
                    JanusAudio.SetOutputPower((float)(value * dsp_adjust));
                }
                else
                {
                    JanusAudio.SetOutputPower((float)0.0);
                }
            }
        }

        /*private static bool next_mox = false;
		public static bool NextMox
		{
			get { return next_mox; }
			set { next_mox = value; }
		}*/

        /*private static int ramp_samples = (int)(sample_rate1*0.005);
		private static double ramp_step = 1.0/ramp_samples;
		private static int ramp_count = 0;
		private static double ramp_val = 0.0;*/

        /*private static bool ramp_down = false;
		public static bool RampDown
		{
			get { return ramp_down; }
			set
			{
				ramp_down = value;
				ramp_samples = (int)(sample_rate1*0.005);
				ramp_step = 1.0/ramp_samples;
				ramp_count = 0;
				ramp_val = 1.0;
			}
		}

		private static bool ramp_up = false;
		public static bool RampUp
		{
			get { return ramp_up; }
			set
			{
				ramp_up = value;
				ramp_samples = (int)(sample_rate1*0.005);
				ramp_step = 1.0/ramp_samples;
				ramp_count = 0;
				ramp_val = 0.0;
			}
		}

		

		private static int ramp_up_num = 1;
		public static int RampUpNum
		{
			get { return ramp_up_num; }
			set	{ ramp_up_num = value; }
		}

		private static int switch_count = 1;
		public static int SwitchCount
		{
			get {return switch_count; }
			set {switch_count = value; }
		}*/

        private static AudioState current_audio_state1 = AudioState.DTTSP;
        public static AudioState CurrentAudioState1
        {
            get { return current_audio_state1; }
            set { current_audio_state1 = value; }
        }

        /*private static AudioState next_audio_state1 = AudioState.NOL_NOR;
		public static AudioState NextAudioState1
		{
			get { return next_audio_state1; }
			set { next_audio_state1 = value; }
		}*/

        /*private static AudioState save_audio_state1 = AudioState.NOL_NOR;
		public static AudioState SaveAudioState1
		{
			get { return save_audio_state1; }
			set { save_audio_state1 = value; }
		}*/

        private static double sine_freq1 = 1250.0;
        private static double phase_step1 = sine_freq1 / sample_rate1 * 2 * Math.PI;
        private static double phase_accumulator1;

        private static double sine_freq2 = 1900.0;
        private static double phase_step2 = sine_freq2 / sample_rate1 * 2 * Math.PI;
        private static double phase_accumulator2;

        public static double SineFreq1
        {
            get { return sine_freq1; }
            set
            {
                sine_freq1 = value;
                phase_step1 = sine_freq1 / sample_rate1 * 2 * Math.PI;
            }
        }

        public static double SineFreq2
        {
            get { return sine_freq2; }
            set
            {
                sine_freq2 = value;
                phase_step2 = sine_freq2 / sample_rate1 * 2 * Math.PI;
            }
        }

        private static int in_rx1_l;

        public static int IN_RX1_L
        {
            get { return in_rx1_l; }
            set { in_rx1_l = value; }
        }

        private static int in_rx1_r = 1;

        public static int IN_RX1_R
        {
            get { return in_rx1_r; }
            set { in_rx1_r = value; }
        }

        private static int in_rx2_l = 2;

        public static int IN_RX2_L
        {
            get { return in_rx2_l; }
            set { in_rx2_l = value; }
        }

        private static int in_rx2_r = 3;

        public static int IN_RX2_R
        {
            get { return in_rx2_r; }
            set { in_rx2_r = value; }
        }

        private static int in_tx_l = 4;

        public static int IN_TX_L
        {
            get { return in_tx_l; }
            set
            {
                in_tx_l = value;
                /* switch (in_tx_l)
                 {
                     case 4:
                     case 5:
                     case 6:
                         in_tx_r = in_tx_l + 1;
                         break;
                     case 7:
                         in_tx_r = 4;
                         break;
                 }*/
            }
        }

        private static int in_tx_r = 5;

        public static int IN_TX_R
        {
            get { return in_tx_r; }
            set { in_tx_r = value; }
        }

        private static bool rx2_enabled;

        public static bool RX2Enabled
        {
            get { return rx2_enabled; }
            set { rx2_enabled = value; }
        }

        public static Console console;
        unsafe private static void* stream1;
        unsafe private static void* stream2;
        // unsafe private static void* stream3;
        //private static int block_size2 = 2048;
        public static float[] phase_buf_l;
        public static float[] phase_buf_r;
        public static bool phase;
        public static bool scope;
        public static bool wave_record;
        public static bool wave_playback;
        public static WaveFileWriter wave_file_writer;
        public static WaveFileWriter wave_file_writer2;
        public static WaveFileReader wave_file_reader;
        public static WaveFileReader wave_file_reader2;
        public static bool two_tone;
        //public static Mutex phase_mutex = new Mutex();
        public static bool high_pwr_am;
        public static bool testing;
        private static bool localmox;

        private static int empty_buffers;

        public static int EmptyBuffers
        {
            get { return empty_buffers; }
        }

        #region VAC Variables

        // buffers for adaptive variable resamplers
        private static double[] resampBufVac1InWrite;
        private static double[] resampBufVac1InRead;
        private static double[] resampBufVac1OutWrite;
        private static double[] resampBufVac1OutRead;

        // adaptive resamplers for TX(In) and RX(Out)
        unsafe private static void* rmatchVac1In;
        unsafe public static void* RmatchVac1In
        {
            get { return rmatchVac1In; }
        }
        unsafe private static void* rmatchVac1Out;
        unsafe public static void* RmatchVac1Out
        {
            get { return rmatchVac1Out; }
        }

        private static bool varsampEnabledVAC1 = false;
        public static bool VarsampEnabledVAC1
        {
            set { varsampEnabledVAC1 = value; }
        }

        // W4WMT these are no longer used by anything but the deprecated callbacks
        // please remove when the old callbacks are factored out
        private static RingBufferFloat rb_vacIN_l;
        private static RingBufferFloat rb_vacIN_r;
        private static RingBufferFloat rb_vacOUT_l;
        private static RingBufferFloat rb_vacOUT_r;

        private static float[] res_inl;
        private static float[] res_inr;
        private static float[] res_outl;
        private static float[] res_outr;

        unsafe private static void* resampPtrIn_l;
        unsafe private static void* resampPtrIn_r;
        unsafe private static void* resampPtrOut_l;
        unsafe private static void* resampPtrOut_r;
        // end deprecated items to be removed

        private static bool vac_resample = false;
        private static bool vac_combine_input = false;
        public static bool VACCombineInput
        {
            get { return vac_combine_input; }
            set { vac_combine_input = value; }
        }

        #endregion

        #region VAC2 Variables

        // buffers for adaptive variable resamplers
        private static double[] resampBufVac2InWrite;
        private static double[] resampBufVac2InRead;
        private static double[] resampBufVac2OutWrite;
        private static double[] resampBufVac2OutRead;

        // adaptive resamplers for TX(In) and RX(Out)
        unsafe private static void* rmatchVac2In;
        unsafe public static void* RmatchVac2In
        {
            get { return rmatchVac2In; }
        }
        unsafe private static void* rmatchVac2Out;
        unsafe public static void* RmatchVac2Out
        {
            get { return rmatchVac2Out; }
        }

        private static bool varsampEnabledVAC2 = false;
        public static bool VarsampEnabledVAC2
        {
            set { varsampEnabledVAC2 = value; }
        }

        // W4WMT these are no longer used by anything but the deprecated callbacks
        // please remove when the old callbacks are factored out
        private static RingBufferFloat rb_vac2IN_l;
        private static RingBufferFloat rb_vac2IN_r;
        private static RingBufferFloat rb_vac2OUT_l;
        private static RingBufferFloat rb_vac2OUT_r;

        private static float[] res_vac2_inl;
        private static float[] res_vac2_inr;
        private static float[] res_vac2_outl;
        private static float[] res_vac2_outr;

        unsafe private static void* resampVAC2PtrIn_l;
        unsafe private static void* resampVAC2PtrIn_r;
        unsafe private static void* resampVAC2PtrOut_l;
        unsafe private static void* resampVAC2PtrOut_r;
        // end deprecated items to be removed

        private static bool vac2_resample = false;
        private static bool vac2_combine_input = false;
        public static bool VAC2CombineInput
        {
            get { return vac2_combine_input; }
            set { vac2_combine_input = value; }
        }

        #endregion

        #endregion

        #region Local Copies of External Properties

        private static bool mox = false;

        public static bool MOX
        {
            set { mox = value; }
        }

        unsafe private static void* cs_vac;
        unsafe private static void* cs_vacw;
        unsafe private static void* cs_vac2;
        unsafe private static void* cs_vac2w;

        private static bool mon;
        public static bool MON
        {
            set { mon = value; }
        }

        private static bool full_duplex = false;
        public static bool FullDuplex
        {
            set { full_duplex = value; }
        }

        private static bool vfob_tx = false;
        public static bool VFOBTX
        {
            set { vfob_tx = value; }
        }

        private static bool vac_enabled = false;
        public static bool VACEnabled
        {
            set
            {
                vac_enabled = value;
                if (vac_enabled) InitVAC();
                else CleanUpVAC();
            }
            get { return vac_enabled; }
        }

        private static bool vac2_enabled = false;
        public static bool VAC2Enabled
        {
            set
            {
                vac2_enabled = value;
                if (vac2_enabled) InitVAC2();
                else CleanUpVAC2();
            }
            get { return vac2_enabled; }
        }

        private static bool vac2_rx2 = true;
        public static bool VAC2RX2
        {
            get { return vac2_rx2; }
            set { vac2_rx2 = value; }
        }

        private static bool vac_bypass = false;
        public static bool VACBypass
        {
            get { return vac_bypass; }
            set { vac_bypass = value; }
        }

        private static bool vac_rb_reset = false;
        public static bool VACRBReset
        {
            set { vac_rb_reset = value; }
            get { return vac_rb_reset; }
        }

        private static bool vac2_rb_reset = false;
        public static bool VAC2RBReset
        {
            set { vac2_rb_reset = value; }
            get { return vac2_rb_reset; }
        }

        private static double vac_preamp = 1.0;
        public static double VACPreamp
        {
            get { return vac_preamp; }
            set
            {
                //Debug.WriteLine("vac_preamp: "+value.ToString("f3"));
                vac_preamp = value;
            }
        }

        private static double vac2_tx_scale = 1.0;
        public static double VAC2TXScale
        {
            get { return vac2_tx_scale; }
            set { vac2_tx_scale = value; }
        }

        private static double vac_rx_scale = 1.0;
        public static double VACRXScale
        {
            get { return vac_rx_scale; }
            set
            {
                //Debug.WriteLine("vac_rx_scale: "+value.ToString("f3"));
                vac_rx_scale = value;
            }
        }

        private static double vac2_rx_scale = 1.0;
        public static double VAC2RXScale
        {
            get { return vac2_rx_scale; }
            set
            {
                //Debug.WriteLine("vac_rx_scale: "+value.ToString("f3"));
                vac2_rx_scale = value;
            }
        }

        private static DSPMode rx1_dsp_mode = DSPMode.LSB;
        public static DSPMode RX1DSPMode
        {
            set { rx1_dsp_mode = value; }
        }

        private static DSPMode rx2_dsp_mode = DSPMode.LSB;
        public static DSPMode RX2DSPMode
        {
            set { rx2_dsp_mode = value; }
        }

        private static DSPMode tx_dsp_mode = DSPMode.LSB;
        public static DSPMode TXDSPMode
        {
            get { return tx_dsp_mode; }
            set { tx_dsp_mode = value; }
        }

        private static int sample_rate1 = 48000;
        public static int SampleRate1
        {
            get { return sample_rate1; }
            set
            {
                sample_rate1 = value;
                SineFreq1 = sine_freq1;
                SineFreq2 = sine_freq2;
                SetOutCount();
            }
        }

        private static int sample_rate2 = 48000;
        public static int SampleRate2
        {
            get { return sample_rate2; }
            set
            {
                sample_rate2 = value;
                if (vac_enabled) InitVAC();
            }
        }

        private static int sample_rate3 = 48000;
        public static int SampleRate3
        {
            get { return sample_rate3; }
            set
            {
                sample_rate3 = value;
                if (vac2_enabled) InitVAC2();
            }
        }

        private static int block_size1 = 1024;
        public static int BlockSize
        {
            get { return block_size1; }
            set 
            { 
                block_size1 = value;
                SetOutCount();
            }
        }

        private static int block_size_vac = 2048;
        public static int BlockSizeVAC
        {
            get { return block_size_vac; }
            set { block_size_vac = value; }
        }

        private static int block_size_vac2 = 2048;
        public static int BlockSizeVAC2
        {
            get { return block_size_vac2; }
            set { block_size_vac2 = value; }
        }

        private static double audio_volts1 = 0.8;
        public static double AudioVolts1
        {
            get { return audio_volts1; }
            set { audio_volts1 = value; }
        }

        private static bool vac_stereo = false;
        public static bool VACStereo
        {
            set { vac_stereo = value; }
        }

        private static bool vac2_stereo = false;
        public static bool VAC2Stereo
        {
            set { vac2_stereo = value; }
        }

        private static bool vac_output_iq = false;
        public static bool VACOutputIQ
        {
            set { vac_output_iq = value; }
        }

        private static bool vac2_output_iq = false;
        public static bool VAC2OutputIQ
        {
            set { vac2_output_iq = value; }
        }

        private static bool vac_output_rx2 = false;
        public static bool VACOutputRX2
        {
            set { vac_output_rx2 = value; }
        }

        private static float iq_phase = 0.0f;
        public static float IQPhase
        {
            set { iq_phase = value; }
        }

        private static float iq_gain = 1.0f;
        public static float IQGain
        {
            set { iq_gain = value; }
        }

        private static float iq_phase2 = 0.0f;
        public static float IQPhase2
        {
            set { iq_phase2 = value; }
        }

        private static float iq_gain2 = 1.0f;
        public static float IQGain2
        {
            set { iq_gain2 = value; }
        }

        private static bool vac_correct_iq = true;
        public static bool VACCorrectIQ
        {
            set { vac_correct_iq = value; }
        }

        private static bool vac2_correct_iq = true;
        public static bool VAC2CorrectIQ
        {
            set { vac2_correct_iq = value; }
        }

        private static SoundCard soundcard = SoundCard.UNSUPPORTED_CARD;
        public static SoundCard CurSoundCard
        {
            set { soundcard = value; }
        }

        private static bool vox_active = false;
        public static bool VOXActive
        {
            get { return vox_active; }
            set { vox_active = value; }
        }

        private static int num_channels = 2;
        public static int NumChannels
        {
            get { return num_channels; }
            set { num_channels = value; }
        }

        private static int host1 = 0;
        public static int Host1
        {
            get { return host1; }
            set { host1 = value; }
        }

        private static int host2 = 0;
        public static int Host2
        {
            get { return host2; }
            set { host2 = value; }
        }

        private static int host3 = 0;
        public static int Host3
        {
            get { return host3; }
            set { host3 = value; }
        }

        private static int input_dev1 = 0;
        public static int Input1
        {
            get { return input_dev1; }
            set { input_dev1 = value; }
        }

        private static int input_dev2 = 0;
        public static int Input2
        {
            get { return input_dev2; }
            set { input_dev2 = value; }
        }

        private static int input_dev3 = 0;
        public static int Input3
        {
            get { return input_dev3; }
            set { input_dev3 = value; }
        }

        private static int output_dev1 = 0;
        public static int Output1
        {
            get { return output_dev1; }
            set { output_dev1 = value; }
        }

        private static int output_dev2 = 0;
        public static int Output2
        {
            get { return output_dev2; }
            set { output_dev2 = value; }
        }

        private static int output_dev3 = 0;
        public static int Output3
        {
            get { return output_dev3; }
            set { output_dev3 = value; }
        }

        private static int latency1 = 0;
        public static int Latency1
        {
            get { return latency1; }
            set { latency1 = value; }
        }

        private static int latency2 = 120;
        public static int Latency2
        {
            set { latency2 = value; }
        }

        private static int latency3 = 120;
        public static int Latency3
        {
            set { latency3 = value; }
        }

        private static float min_in_l = float.MaxValue;
        public static float MinInL
        {
            get { return min_in_l; }
        }

        private static float min_in_r = float.MaxValue;
        public static float MinInR
        {
            get { return min_in_r; }
        }

        private static float min_out_l = float.MaxValue;
        public static float MinOutL
        {
            get { return min_out_l; }
        }

        private static float min_out_r = float.MaxValue;
        public static float MinOutR
        {
            get { return min_out_r; }
        }

        private static float max_in_l = float.MaxValue;
        public static float MaxInL
        {
            get { return max_in_l; }
        }

        private static float max_in_r = float.MaxValue;
        public static float MaxInR
        {
            get { return max_in_r; }
        }

        private static float max_out_l = float.MaxValue;

        public static float MaxOutL
        {
            get { return max_out_l; }
        }

        private static float max_out_r = float.MaxValue;

        public static float MaxOutR
        {
            get { return max_out_r; }
        }

        private static bool mute_output = false;
        public static bool MuteOutput
        {
            get { return mute_output; }
            set { mute_output = value; }
        }

        private static bool mute_rx1 = false;
        public static bool MuteRX1
        {
            get { return mute_rx1; }
            set { mute_rx1 = value; }
        }

        private static bool mute_rx2 = false;
        public static bool MuteRX2
        {
            get { return mute_rx2; }
            set { mute_rx2 = value; }
        }

        private static int out_rate = 48000;
        public static int OutRate
        {
            get { return out_rate; }
            set 
            { 
                out_rate = value;
                SetOutCount();
            }
        }

        private static void SetOutCount()
        {
            if (out_rate >= sample_rate1)
                OutCount = block_size1 * (out_rate / sample_rate1);
            else
                OutCount = block_size1 / (sample_rate1 / out_rate);
        }

        private static int out_count = 1024;
        public static int OutCount
        {
            get { return out_count; }
            set 
            { 
                out_count = value;
            }
        }

        #endregion

        #region Callback Routines

        // ======================================================
        // Callback Routines
        // ======================================================

        unsafe public static int Callback1(void* input, void* output, int frameCount,
                                           PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
#if(TIMER)
			t1.Start();
#endif

            int** array_ptr = (int**)input;
            float* in_l_ptr1 = (float*)array_ptr[0];
            float* in_r_ptr1 = (float*)array_ptr[1];
            array_ptr = (int**)output;
            float* out_l_ptr1 = (float*)array_ptr[0];
            float* out_r_ptr1 = (float*)array_ptr[1];

            float* in_l = null, in_r = null, out_l = null, out_r = null;

            in_l = in_l_ptr1;
            in_r = in_r_ptr1;

            out_l = out_l_ptr1;
            out_r = out_r_ptr1;

            localmox = mox;


            if (wave_playback)
                wave_file_reader.GetPlayBuffer(in_l_ptr1, in_r_ptr1);

            if ((wave_record && !localmox && record_rx_preprocessed) ||
                (wave_record && localmox && record_tx_preprocessed))
                wave_file_writer.AddWriteBuffer(in_l_ptr1, in_r_ptr1);

            if (phase)
            {
                //phase_mutex.WaitOne();
                Marshal.Copy(new IntPtr(in_l_ptr1), phase_buf_l, 0, frameCount);
                Marshal.Copy(new IntPtr(in_r_ptr1), phase_buf_r, 0, frameCount);
                //phase_mutex.ReleaseMutex();
            }


            switch (current_audio_state1)
            {
                case AudioState.DTTSP:
                    if (tx_dsp_mode == DSPMode.CWU || tx_dsp_mode == DSPMode.CWL)
                    {
                        // DttSP.CWtoneExchange(out_l_ptr1, out_r_ptr1, frameCount);
                    }

                    // handle VAC Input
                    if (vac_enabled &&
                        rb_vacOUT_l != null && rb_vacOUT_r != null)
                    {
                        if (vac_bypass || !localmox) // drain VAC Input ring buffer
                        {
                            if ((rb_vacIN_l.ReadSpace() >= frameCount) && (rb_vacIN_r.ReadSpace() >= frameCount))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacIN_l.ReadPtr(out_l_ptr1, frameCount);
                                rb_vacIN_r.ReadPtr(out_r_ptr1, frameCount);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                        }
                        else // VAC is on -- copy data for transmit mode
                        {
                            if (rb_vacIN_l.ReadSpace() >= frameCount)
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacIN_l.ReadPtr(in_l, frameCount);
                                rb_vacIN_r.ReadPtr(in_r, frameCount);
                                Win32.LeaveCriticalSection(cs_vac);
                                if (vac_combine_input)
                                    AddBuffer(in_l, in_r, frameCount);
                            }
                            else
                            {
                                ClearBuffer(in_l, frameCount);
                                ClearBuffer(in_r, frameCount);
                                VACDebug("rb_vacIN underflow 4inTX");
                            }
                            ScaleBuffer(in_l, in_l, frameCount, (float)vac_preamp);
                            ScaleBuffer(in_r, in_r, frameCount, (float)vac_preamp);
                        }
                    }

                    min_in_l = Math.Min(min_in_l, MinSample(in_l, frameCount));
                    min_in_r = Math.Min(min_in_r, MinSample(in_r, frameCount));
                    max_in_l = Math.Max(max_in_l, MaxSample(in_l, frameCount));
                    max_in_r = Math.Max(max_in_r, MaxSample(in_r, frameCount));

                    // scale input with mic preamp
                    if (localmox && ((!vac_enabled &&
                                      (tx_dsp_mode == DSPMode.LSB ||
                                       tx_dsp_mode == DSPMode.USB ||
                                       tx_dsp_mode == DSPMode.DSB ||
                                       tx_dsp_mode == DSPMode.AM ||
                                       tx_dsp_mode == DSPMode.SAM ||
                                       tx_dsp_mode == DSPMode.FM ||
                                       tx_dsp_mode == DSPMode.DIGL ||
                                       tx_dsp_mode == DSPMode.DIGU)) ||
                                     (vac_enabled && vac_bypass &&
                                      (tx_dsp_mode == DSPMode.DIGL ||
                                       tx_dsp_mode == DSPMode.DIGU ||
                                       tx_dsp_mode == DSPMode.LSB ||
                                       tx_dsp_mode == DSPMode.USB ||
                                       tx_dsp_mode == DSPMode.DSB ||
                                       tx_dsp_mode == DSPMode.AM ||
                                       tx_dsp_mode == DSPMode.SAM ||
                                       tx_dsp_mode == DSPMode.FM))))
                    {
                        if (wave_playback)
                        {
                            ScaleBuffer(in_l, in_l, frameCount, (float)wave_preamp);
                            ScaleBuffer(in_r, in_r, frameCount, (float)wave_preamp);
                        }
                        else
                        {
                            if (!vac_enabled && (tx_dsp_mode == DSPMode.DIGL || tx_dsp_mode == DSPMode.DIGU))
                            {
                                ScaleBuffer(in_l, in_l, frameCount, (float)vac_preamp);
                                ScaleBuffer(in_r, in_r, frameCount, (float)vac_preamp);
                            }
                            else
                            {
                                ScaleBuffer(in_l, in_l, frameCount, (float)mic_preamp);
                                ScaleBuffer(in_r, in_r, frameCount, (float)mic_preamp);
                            }
                        }
                    }

                    #region Input Signal Source

                    if (!localmox)
                    {
                        switch (rx1_input_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(in_l, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.NOISE:
                                Noise(in_l, frameCount);
                                Noise(in_r, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(in_l, frameCount);
                                CopyBuffer(in_l, in_r, frameCount);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(in_l, frameCount);
                                ClearBuffer(in_r, frameCount);
                                break;
                        }
                    }
                    else
                    {
                        switch (tx_input_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(in_l, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.NOISE:
                                Noise(in_l, frameCount);
                                Noise(in_r, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(in_l, frameCount);
                                CopyBuffer(in_l, in_r, frameCount);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(in_l, frameCount);
                                ClearBuffer(in_r, frameCount);
                                break;
                        }
                    }

                    #endregion

#if(MINMAX)
					Debug.Write(MaxSample(in_l, in_r, frameCount).ToString("f6")+",");
#endif

                    if (vac_enabled && vac_output_iq &&
                        rb_vacOUT_l != null && rb_vacOUT_r != null &&
                        in_l != null && in_r != null)
                    {
                        if (!localmox)
                        {
                            if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
                            {
                                if (vac_correct_iq)
                                    fixed (float* res_outl_ptr = &(res_outl[0]))
                                    fixed (float* res_outr_ptr = &(res_outr[0]))
                                    {
                                        CorrectIQBuffer(in_l, in_r, res_outl_ptr, res_outr_ptr, frameCount);

                                        Win32.EnterCriticalSection(cs_vac);
                                        rb_vacOUT_l.WritePtr(res_outl_ptr, frameCount);
                                        rb_vacOUT_r.WritePtr(res_outr_ptr, frameCount);
                                        Win32.LeaveCriticalSection(cs_vac);
                                    }
                                else
                                {
                                    Win32.EnterCriticalSection(cs_vac);
                                    rb_vacOUT_l.WritePtr(in_r, frameCount); // why are these reversed??
                                    rb_vacOUT_r.WritePtr(in_l, frameCount);
                                    Win32.LeaveCriticalSection(cs_vac);
                                }
                            }
                            else
                            {
                                VACDebug("rb_vacOUT_l I/Q overflow ");
                                vac_rb_reset = true;
                            }
                        }
                    }
                    else
                    {
                        //DttSP.ExchangeSamples(in_l, in_r, out_l, out_r, frameCount);  // NOT USED
                    }

#if(MINMAX)
					Debug.WriteLine(MaxSample(out_l, out_r, frameCount));
#endif

                    #region Output Signal Source

                    if (!localmox)
                    {
                        switch (rx1_output_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(out_l_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(out_r_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(out_l_ptr1, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(out_r_ptr1, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.NOISE:
                                Noise(out_l_ptr1, frameCount);
                                Noise(out_r_ptr1, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(out_l_ptr1, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(out_l_ptr1, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(out_l_ptr1, frameCount);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(out_l_ptr1, frameCount);
                                ClearBuffer(out_r_ptr1, frameCount);
                                break;
                        }
                    }
                    else
                    {
                        switch (tx_output_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(out_l_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(out_l_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(out_l_ptr1, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(out_r_ptr1, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.NOISE:
                                Noise(out_l_ptr1, frameCount);
                                Noise(out_r_ptr1, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(out_l_ptr1, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(out_l_ptr1, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(out_l_ptr1, frameCount);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(out_l_ptr1, frameCount);
                                ClearBuffer(out_r_ptr1, frameCount);
                                break;
                        }
                    }

                    #endregion

                    break;
                case AudioState.CW:
                    // DttSP.CWtoneExchange(out_l_ptr1, out_r_ptr1, frameCount);
                    break;
            }

            DoScope(out_l_ptr1, frameCount);

            if (wave_record)
            {
                if (!localmox)
                {
                    if (!record_rx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(out_l, out_r);
                    }
                }
                else
                {
                    if (!record_tx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(out_l, out_r);
                    }
                }
            }

            // scale output for VAC -- use input as spare buffer
            if (vac_enabled && !vac_output_iq &&
                rb_vacIN_l != null && rb_vacIN_r != null &&
                rb_vacOUT_l != null && rb_vacOUT_r != null)
            {
                ScaleBuffer(out_l, in_l, frameCount, (float)vac_rx_scale);
                ScaleBuffer(out_r, in_r, frameCount, (float)vac_rx_scale);

                if (sample_rate2 == sample_rate1) // no resample necessary
                {
                    if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacOUT_l.WritePtr(in_l, frameCount);
                        rb_vacOUT_r.WritePtr(in_r, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                    else
                    {
                        VACDebug("rb_vacOUT_l overflow");
                        VACDebug("rb_vacOUT_r overflow");
                    }
                }
                else
                {
                    if (vac_stereo)
                    {
                        fixed (float* res_outl_ptr = &(res_outl[0]))
                        fixed (float* res_outr_ptr = &(res_outr[0]))
                        {
                            int outsamps = 0;
                            //DttSP.DoResamplerF(in_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            wdsp.xresampleFV(in_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            //DttSP.DoResamplerF(in_r, res_outr_ptr, frameCount, &outsamps, resampPtrOut_r);
                            wdsp.xresampleFV(in_r, res_outr_ptr, frameCount, &outsamps, resampPtrOut_r);
                            if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                rb_vacOUT_r.WritePtr(res_outr_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                            else
                            {
                                VACDebug("rb_vacOUT_l overflow");
                                VACDebug("rb_vacOUT_r overflow");
                            }
                        }
                    }
                    else
                    {
                        fixed (float* res_outl_ptr = &(res_outl[0]))
                        {
                            int outsamps = 0;
                            //DttSP.DoResamplerF(in_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            wdsp.xresampleFV(in_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                rb_vacOUT_r.WritePtr(res_outl_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                            else
                            {
                                VACDebug("rb_vacOUT_l overflow");
                                VACDebug("rb_vacOUT_r overflow");
                            }
                        }
                    }
                }
            }

            double vol = monitor_volume;
            if (localmox)
            {
                vol = tx_output_signal != SignalSource.RADIO ? 1.0f : TXScale;
            }
            else // receive
            {
                if (rx1_output_signal != SignalSource.RADIO)
                    vol = 1.0f;
            }

            if (vol != 1.0f)
            {
                ScaleBuffer(out_l, out_l, frameCount, (float)vol);
                ScaleBuffer(out_r, out_r, frameCount, (float)vol);
            }

#if(MINMAX)
			Debug.Write(MaxSample(out_l, out_r, frameCount).ToString("f6")+",");

			float current_max = MaxSample(out_l, out_r, frameCount);
			if(current_max > max) max = current_max;
			Debug.WriteLine(" max: "+max.ToString("f6"));
#endif

            /*if((wave_record && !localmox && !record_rx_preprocessed) ||
				(wave_record && localmox && !record_tx_preprocessed))
				wave_file_writer.AddWriteBuffer(out_l_ptr1, out_r_ptr1);*/
#if(TIMER)
			t1.Stop();
			Debug.WriteLine(t1.Duration);
#endif

            min_out_l = Math.Min(min_out_l, MinSample(out_l, frameCount));
            min_out_r = Math.Min(min_out_r, MinSample(out_r, frameCount));
            max_out_l = Math.Max(max_out_l, MaxSample(out_l, frameCount));
            max_out_r = Math.Max(max_out_r, MaxSample(out_r, frameCount));

            return callback_return;
        }

#if(TIMER)
		private static HiPerfTimer t1 = new HiPerfTimer();
#endif
        unsafe static void dspExchange(void* ins, void* outs, int numsamples)
        {
            int i;
            int error;
            float** input = (float**)ins;
            float** output = (float**)outs;

            // fexchange0() test code
            /*double[] Cin  = new double[4096];
            double[] Cout = new double[4096];
            for (i = 0; i < block_size1; i++)
            {
                Cin[2 * i + 0] = (double)input[0][i];
                Cin[2 * i + 1] = (double)input[1][i];
            }
            fixed(double* pCin  = &Cin[0])
            fixed(double* pCout = &Cout[0])
                wdsp.fexchange0(wdsp.id(0, 0), pCin, pCout, &error);
            for (i = 0; i < numsamples; i++)
            {
                output[0][i] = (float)Cout[2 * i + 0];
                output[1][i] = (float)Cout[2 * i + 1];
            }*/

            wdsp.fexchange2(wdsp.id(0, 0), input[0], input[1], output[0], output[1], &error);
            wdsp.fexchange2(wdsp.id(0, 1), input[0], input[1], output[6], output[7], &error);
            if (console.radio.GetDSPRX(0, 1).Active)
            {
                for (i = 0; i < numsamples; i++)
                {
                    (output[0])[i] += (output[6])[i];
                    (output[1])[i] += (output[7])[i];
                }
            }
            wdsp.fexchange2(wdsp.id(2, 0), input[4], input[5], output[4], output[5], &error);
            wdsp.fexchange2(wdsp.id(1, 0), input[2], input[3], output[2], output[3], &error);
        }
        
        //Callback for HPSDR
        //private static int count = 0;
        unsafe public static int Callback3Port(void* input, void* output, int frameCount,
                                               PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
#if(TIMER)
			t1.Start();
#endif
            float* in_l = null, in_r = null;
            float* out_l1 = null, out_r1 = null, out_l2 = null, out_r2 = null;
            float* out_l3 = null, out_r3 = null, out_l4 = null, out_r4 = null;
            float* rx1_in_l = null, rx1_in_r = null, tx_in_l = null, tx_in_r = null;
            float* rx2_in_l = null, rx2_in_r = null;
            float* rx1_out_l = null, rx1_out_r = null, tx_out_l = null, tx_out_r = null;
            float* rx2_out_l = null, rx2_out_r = null;
            localmox = mox;

            // inputs
            void* ex_input = (int*)input;
            int** array_ptr_input = (int**)input;
            float* in_l_ptr1 = (float*)array_ptr_input[0]; //CallbackInLbufp RX1 I
            float* in_r_ptr1 = (float*)array_ptr_input[1]; //CallbackInRbufp RX1 Q
            float* in_l_ptr2 = (float*)array_ptr_input[2]; //CallbackInL2bufp  RX2 I
            float* in_r_ptr2 = (float*)array_ptr_input[3]; //CallbackInR2bufp  RX2 Q
            float* in_l_ptr3 = (float*)array_ptr_input[4]; //CallbackMicLbufp  Mic L
            float* in_r_ptr3 = (float*)array_ptr_input[5]; //CallbackMicRbufp  Mic R
            // float* in_l_ptr4 = (float*)array_ptr_input[6]; //CallbackInL3bufp  RX3 I
            //  float* in_r_ptr4 = (float*)array_ptr_input[7]; //CallbackInR3bufp  RX3 Q

            array_ptr_input[0] = (int*)in_l_ptr1; // CallbackInLbufp
            array_ptr_input[1] = (int*)in_r_ptr1; // CallbackInRbufp
            array_ptr_input[2] = (int*)in_l_ptr3; // CallbackMicLbufp
            array_ptr_input[3] = (int*)in_r_ptr3; // CallbackMicRbufp
            array_ptr_input[4] = (int*)in_l_ptr2; // CallbackInL2bufp
            array_ptr_input[5] = (int*)in_r_ptr2; // CallbackInR2bufp

            rx1_in_l = (float*)array_ptr_input[0]; //CallbackInLbufp
            rx1_in_r = (float*)array_ptr_input[1]; //CallbackInRbufp
            tx_in_l = (float*)array_ptr_input[2]; //CallbackMicLbufp
            tx_in_r = (float*)array_ptr_input[3]; //CallbackMicRbufp
            rx2_in_l = (float*)array_ptr_input[4]; //CallbackInL2bufp
            rx2_in_r = (float*)array_ptr_input[5]; //CallbackInR2bufp

            // outputs
            void* ex_output = (int*)output;
            int** array_ptr_output = (int**)output;
            float* out_l_ptr1 = (float*)array_ptr_output[0]; //CallbackMonOutLbufp RX1
            float* out_r_ptr1 = (float*)array_ptr_output[1]; //CallbackMonOutRbufp RX1
            float* out_l_ptr2 = (float*)array_ptr_output[2]; //CallbackOutLbufp TX
            float* out_r_ptr2 = (float*)array_ptr_output[3]; //CallbackOutRbufp TX
            float* out_l_ptr3 = (float*)array_ptr_output[4]; //CallbackOutL2bufp 
            float* out_r_ptr3 = (float*)array_ptr_output[5]; //CallbackOutR2bufp 
            float* out_l_ptr4 = (float*)array_ptr_output[6]; //CallbackOutL3bufp 
            float* out_r_ptr4 = (float*)array_ptr_output[7]; //CallbackOutR3bufp 

            rx1_out_l = (float*)array_ptr_output[0]; //CallbackMonOutLbufp
            rx1_out_r = (float*)array_ptr_output[1]; //CallbackMonOutRbufp
            tx_out_l = (float*)array_ptr_output[2]; //CallbackOutLbufp
            tx_out_r = (float*)array_ptr_output[3]; //CallbackOutRbufp
            rx2_out_l = (float*)array_ptr_output[4]; //CallbackOutL2bufp
            rx2_out_r = (float*)array_ptr_output[5]; //CallbackOutR2bufp

            if (!console.SetupForm.PureSignalDisabled)
            {
                int rx_idx, tx_idx;
                if (console.psform.RXrcvr == 1) rx_idx = 0;
                else rx_idx = 2 * console.psform.RXrcvr;
                if (console.psform.TXrcvr == 1) tx_idx = 0;
                else tx_idx = 2 * console.psform.TXrcvr;
                //rx_idx = 0;
                //tx_idx = 8;
                puresignal.psccF(wdsp.id(1, 0), frameCount,
                    (float*)array_ptr_input[tx_idx + 0],
                    (float*)array_ptr_input[tx_idx + 1],
                    (float*)array_ptr_input[rx_idx + 0],
                    (float*)array_ptr_input[rx_idx + 1], localmox, true);
            }

            if (!localmox)
            {
                in_l = rx1_in_l;
                in_r = rx1_in_r;
            }
            else
            {
                in_l = tx_in_l;
                in_r = tx_in_r;
            }

            if (localmox && rx1_blank_display_tx)
            {
                ClearBuffer(rx1_in_l, frameCount);
                ClearBuffer(rx1_in_r, frameCount);
            }

            if (localmox && rx2_enabled && rx2_blank_display_tx)
            {
                ClearBuffer(rx2_in_l, frameCount);
                ClearBuffer(rx2_in_r, frameCount);
            }

            if (wave_playback)
            {
                if (read_quad)
                {
                    wave_file_reader.GetPlayBuffer(in_l, in_r, rx2_in_l, rx2_in_r);
                }
                else
                {
                wave_file_reader.GetPlayBuffer(in_l, in_r);
                if (rx2_enabled)
                {
                    if (wave_file_reader2 != null)
                    {
                        wave_file_reader2.GetPlayBuffer(rx2_in_l, rx2_in_r);
                    }
                    else if (!localmox)
                    {
                        CopyBuffer(in_l, rx2_in_l, frameCount);
                        CopyBuffer(in_r, rx2_in_r, frameCount);
                    }
                }
            }
            }

            if (wave_record)
            {
                if (!localmox)
                {
                    if (record_rx_preprocessed)
                    {
                        if (write_quad)
                        {
                            wave_file_writer.AddWriteBuffer(rx1_in_l, rx1_in_r, rx2_in_l, rx2_in_r);
                        }
                        else
                        {
                        wave_file_writer.AddWriteBuffer(rx1_in_l, rx1_in_r);
                        if (wave_file_writer2 != null)
                            wave_file_writer2.AddWriteBuffer(rx2_in_l, rx2_in_r);
                    }
                }
                }
                else
                {
                    if (record_tx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(tx_in_l, tx_in_r);
                    }
                }
            }

            if (phase)
            {
                //phase_mutex.WaitOne();
                Marshal.Copy(new IntPtr(in_l), phase_buf_l, 0, frameCount);
                Marshal.Copy(new IntPtr(in_r), phase_buf_r, 0, frameCount);
                //phase_mutex.ReleaseMutex();
            }

            if (varsampEnabledVAC1)
            {
                // handle VAC Input - data from VAC goes to xmtr input
                if (vac_enabled)
                {
                    fixed (double* resampBufPtr = &(resampBufVac1InRead[0]))
                    {
                        wdsp.xrmatchOUT(rmatchVac1In, resampBufPtr);
                        if (vac_bypass || !localmox)
                        {
                            if (vox_enabled)
                            {
                                Deswizzle(in_l_ptr3, in_r_ptr3, resampBufPtr, frameCount);
                            }
                            else
                            {
                                Deswizzle(out_l_ptr2, out_r_ptr2, resampBufPtr, frameCount);
                            }
                        }
                        else
                        {
                            Deswizzle(tx_in_l, tx_in_r, resampBufPtr, frameCount);
                            if (vac_combine_input) AddBuffer(tx_in_l, tx_in_r, frameCount);

                            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac_preamp);
                            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac_preamp);
                        }
                    }
                }
            }
            else
            {
                // handle VAC Input - data from VAC goes to xmtr input
                if (vac_enabled &&
                    rb_vacOUT_l != null && rb_vacOUT_r != null)
                {
                    if (vac_bypass || !localmox) // drain VAC TX Input ring buffer
                    {
                        if (vox_enabled)
                        {
                            if ((rb_vacIN_l.ReadSpace() >= frameCount) &&
                                (rb_vacIN_r.ReadSpace() >= frameCount))
                            {
                                Win32.EnterCriticalSection(cs_vacw);
                                rb_vacIN_l.ReadPtr(in_l_ptr3, frameCount);  // TX in L - VOX can see the data here.
                                rb_vacIN_r.ReadPtr(in_r_ptr3, frameCount);  // TX in R
                                Win32.LeaveCriticalSection(cs_vacw);
                            }
                        }
                        else
                        {
                            if ((rb_vacIN_l.ReadSpace() >= frameCount) &&
                                (rb_vacIN_r.ReadSpace() >= frameCount))
                            {
                                Win32.EnterCriticalSection(cs_vacw);
                                rb_vacIN_l.ReadPtr(out_l_ptr2, frameCount); // TX out L - Dumping data?
                                rb_vacIN_r.ReadPtr(out_r_ptr2, frameCount); // TX out R
                                Win32.LeaveCriticalSection(cs_vacw);
                            }
                        }
                    }
                    else    // if VAC is !bypassed && localmox
                    {
                        if (rb_vacIN_l.ReadSpace() >= frameCount)
                        {
                            Win32.EnterCriticalSection(cs_vacw);
                            rb_vacIN_l.ReadPtr(tx_in_l, frameCount);    // TX in L
                            rb_vacIN_r.ReadPtr(tx_in_r, frameCount);    // TX in R
                            Win32.LeaveCriticalSection(cs_vacw);
                            if (vac_combine_input)
                                AddBuffer(tx_in_l, tx_in_r, frameCount);
                        }
                        else    // error - not enough samples
                        {
                            ClearBuffer(tx_in_l, frameCount);
                            ClearBuffer(tx_in_r, frameCount);
                            VACDebug("rb_vacIN underflow 4inTX");
                        }

                        ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac_preamp);
                        ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac_preamp);
                    }
                }
            }

            //// handle VAC Input - data from VAC goes to xmtr input
            //if (vac_enabled &&
            //    rb_vacOUT_l != null && rb_vacOUT_r != null)
            //{
            //    if (vac_bypass || !localmox) // drain VAC TX Input ring buffer
            //    {
            //        if (vox_enabled)
            //        {
            //            if ((rb_vacIN_l.ReadSpace() >= frameCount) &&
            //                (rb_vacIN_r.ReadSpace() >= frameCount))
            //            {
            //                Win32.EnterCriticalSection(cs_vacw);
            //                rb_vacIN_l.ReadPtr(in_l_ptr3, frameCount);  // TX in L - VOX can see the data here.
            //                rb_vacIN_r.ReadPtr(in_r_ptr3, frameCount);  // TX in R
            //                Win32.LeaveCriticalSection(cs_vacw);
            //            }
            //        }
            //        else
            //        {
            //            if ((rb_vacIN_l.ReadSpace() >= frameCount) &&
            //                (rb_vacIN_r.ReadSpace() >= frameCount))
            //            {
            //                Win32.EnterCriticalSection(cs_vacw);
            //                rb_vacIN_l.ReadPtr(out_l_ptr2, frameCount); // TX out L - Dumping data?
            //                rb_vacIN_r.ReadPtr(out_r_ptr2, frameCount); // TX out R
            //                Win32.LeaveCriticalSection(cs_vacw);
            //            }
            //        }
            //    }
            //    else    // if VAC is !bypassed && localmox
            //    {
            //        if (rb_vacIN_l.ReadSpace() >= frameCount)
            //        {
            //            Win32.EnterCriticalSection(cs_vacw);
            //            rb_vacIN_l.ReadPtr(tx_in_l, frameCount);    // TX in L
            //            rb_vacIN_r.ReadPtr(tx_in_r, frameCount);    // TX in R
            //            Win32.LeaveCriticalSection(cs_vacw);
            //            if (vac_combine_input)
            //                AddBuffer(tx_in_l, tx_in_r, frameCount);
            //        }
            //        else    // error - not enough samples
            //        {
            //            ClearBuffer(tx_in_l, frameCount);
            //            ClearBuffer(tx_in_r, frameCount);
            //            VACDebug("rb_vacIN underflow 4inTX");
            //        }

            //        ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac_preamp);
            //        ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac_preamp);
            //    }
            //}

            if (varsampEnabledVAC2)
            {
                // handle VAC2 Input
                if (vac2_enabled)
                {
                    fixed (double* resampBufPtr = &(resampBufVac2InRead[0]))
                    {
                        wdsp.xrmatchOUT(rmatchVac2In, resampBufPtr);
                        if (vac_bypass || !localmox || !vfob_tx) // drain VAC2 Input ring buffer
                        {
                            Deswizzle(out_l_ptr2, out_r_ptr2, resampBufPtr, frameCount);
                        }
                        else    // !vac_bypass && localmox && vfob_tx
                        {
                            Deswizzle(tx_in_l, tx_in_r, resampBufPtr, frameCount);
                            if (vac2_combine_input) AddBuffer(tx_in_l, tx_in_r, frameCount);
                            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac2_tx_scale);
                            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac2_tx_scale);
                        }
                    }
                }
            }
            else
            {
                // handle VAC2 Input
                if (vac2_enabled &&
                    rb_vac2OUT_l != null && rb_vac2OUT_r != null)
                {
                    if (vac_bypass || !localmox || !vfob_tx) // drain VAC2 Input ring buffer
                    {
                        if ((rb_vac2IN_l.ReadSpace() >= frameCount) && (rb_vac2IN_r.ReadSpace() >= frameCount))
                        {
                            Win32.EnterCriticalSection(cs_vac2w);
                            rb_vac2IN_l.ReadPtr(out_l_ptr2, frameCount);    // TX out L - Dumping data?
                            rb_vac2IN_r.ReadPtr(out_r_ptr2, frameCount);    // TX out R
                            Win32.LeaveCriticalSection(cs_vac2w);
                        }
                    }
                    else    // !vac_bypass && localmox && vfob_tx
                    {
                        if (rb_vac2IN_l.ReadSpace() >= frameCount)
                        {
                            Win32.EnterCriticalSection(cs_vac2w);
                            rb_vac2IN_l.ReadPtr(tx_in_l, frameCount);       // TX in L
                            rb_vac2IN_r.ReadPtr(tx_in_r, frameCount);       // TX in R
                            Win32.LeaveCriticalSection(cs_vac2w);
                            if (vac2_combine_input)
                                AddBuffer(tx_in_l, tx_in_r, frameCount);

                            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac2_tx_scale);
                            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac2_tx_scale);
                        }
                        else    // error - not enough samples
                        {
                            ClearBuffer(tx_in_l, frameCount);
                            ClearBuffer(tx_in_r, frameCount);
                            VACDebug("rb_vac2IN underflow 4inTX");
                        }
                    }
                }
            }

            //// handle VAC2 Input
            //if (vac2_enabled &&
            //    rb_vac2OUT_l != null && rb_vac2OUT_r != null)
            //{
            //    if (vac_bypass || !localmox || !vfob_tx) // drain VAC2 Input ring buffer
            //    {
            //        if ((rb_vac2IN_l.ReadSpace() >= frameCount) && (rb_vac2IN_r.ReadSpace() >= frameCount))
            //        {
            //            Win32.EnterCriticalSection(cs_vac2w);
            //            rb_vac2IN_l.ReadPtr(out_l_ptr2, frameCount);    // TX out L - Dumping data?
            //            rb_vac2IN_r.ReadPtr(out_r_ptr2, frameCount);    // TX out R
            //            Win32.LeaveCriticalSection(cs_vac2w);
            //        }
            //    }
            //    else    // !vac_bypass && localmox && vfob_tx
            //    {
            //        if (rb_vac2IN_l.ReadSpace() >= frameCount)
            //        {
            //            Win32.EnterCriticalSection(cs_vac2w);
            //            rb_vac2IN_l.ReadPtr(tx_in_l, frameCount);       // TX in L
            //            rb_vac2IN_r.ReadPtr(tx_in_r, frameCount);       // TX in R
            //            Win32.LeaveCriticalSection(cs_vac2w);
            //            if (vac2_combine_input)
            //                AddBuffer(tx_in_l, tx_in_r, frameCount);

            //            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac2_tx_scale);
            //            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac2_tx_scale);
            //        }
            //        else    // error - not enough samples
            //        {
            //            ClearBuffer(tx_in_l, frameCount);
            //            ClearBuffer(tx_in_r, frameCount);
            //            VACDebug("rb_vac2IN underflow 4inTX");
            //        }
            //    }
            //}

            #region VOX

            if (vox_enabled)
            {
                if (tx_dsp_mode == DSPMode.LSB ||
                    tx_dsp_mode == DSPMode.USB ||
                    tx_dsp_mode == DSPMode.DSB ||
                    tx_dsp_mode == DSPMode.AM ||
                    tx_dsp_mode == DSPMode.SAM ||
                    tx_dsp_mode == DSPMode.FM ||
                    tx_dsp_mode == DSPMode.DIGL ||
                    tx_dsp_mode == DSPMode.DIGU)
                {
                    peak = MaxSample(tx_in_l, tx_in_r, frameCount);

                    if (console.MicBoost)   
                        peak = peak / vox_gain; 
                    // compare power to threshold
                    vox_active = peak > vox_threshold;
                }
            }

            #endregion

            // scale input with mic preamp
            if ((!vac_enabled &&
                 (tx_dsp_mode == DSPMode.LSB ||
                  tx_dsp_mode == DSPMode.USB ||
                  tx_dsp_mode == DSPMode.DSB ||
                  tx_dsp_mode == DSPMode.AM ||
                  tx_dsp_mode == DSPMode.SAM ||
                  tx_dsp_mode == DSPMode.FM ||
                  tx_dsp_mode == DSPMode.DIGL ||
                  tx_dsp_mode == DSPMode.DIGU)) ||
                (vac_enabled && vac_bypass &&
                 (tx_dsp_mode == DSPMode.DIGL ||
                  tx_dsp_mode == DSPMode.DIGU ||
                  tx_dsp_mode == DSPMode.LSB ||
                  tx_dsp_mode == DSPMode.USB ||
                  tx_dsp_mode == DSPMode.DSB ||
                  tx_dsp_mode == DSPMode.AM ||
                  tx_dsp_mode == DSPMode.SAM ||
                  tx_dsp_mode == DSPMode.FM)))
            {
                if (wave_playback)
                {
                    ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)wave_preamp);
                    ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)wave_preamp);
                }
                else
                {
                    if (!vac_enabled && (tx_dsp_mode == DSPMode.DIGL || tx_dsp_mode == DSPMode.DIGU))
                    {
                        ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac_preamp);
                        ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac_preamp);
                    }
                    else
                    {
                        ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)mic_preamp);
                        ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)mic_preamp);
                    }
                }
            }

#if(MINMAX)
					Debug.Write(MaxSample(in_l, in_r, frameCount).ToString("f6")+",");
#endif


            if (varsampEnabledVAC1)
            {
                // handle Direct IQ for VAC - receive I/Q data is sent to VAC
                if (vac_enabled && vac_output_iq)
                {
                    if (vac_output_rx2)
                    {
                        fixed (double* resampBufPtr = &(resampBufVac1OutWrite[0]))
                        {
                            Swizzle(resampBufPtr, rx2_in_r, rx2_in_l, frameCount);
                            wdsp.xrmatchIN(rmatchVac1Out, resampBufPtr);
                        }
                    }
                    else
                    {
                        fixed (double* resampBufPtr = &(resampBufVac1OutWrite[0]))
                        {
                            Swizzle(resampBufPtr, rx1_in_r, rx1_in_l, frameCount);
                            wdsp.xrmatchIN(rmatchVac1Out, resampBufPtr);
                        }
                    }
                }
            }
            else
            {
                // handle Direct IQ for VAC - receive I/Q data is sent to VAC
                if (vac_enabled && vac_output_iq &&
                    rb_vacOUT_l != null && rb_vacOUT_r != null &&
                    rx1_in_l != null && rx1_in_r != null)
                {
                    if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
                    {
                        if (vac_correct_iq)
                            fixed (float* res_outl_ptr = &(res_outl[0]))    // use these buffers for corrected data
                            fixed (float* res_outr_ptr = &(res_outr[0]))
                            {
                                if (vac_output_rx2)
                                    CorrectIQBuffer(rx2_in_l, rx2_in_r, res_outl_ptr, res_outr_ptr, frameCount);
                                else
                                    CorrectIQBuffer(rx1_in_l, rx1_in_r,
                                    res_outl_ptr, res_outr_ptr, frameCount);
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outr_ptr, frameCount); // write the output
                                rb_vacOUT_r.WritePtr(res_outl_ptr, frameCount); // why are these reversed??
                                Win32.LeaveCriticalSection(cs_vac);

                            }
                        else
                        {
                            Win32.EnterCriticalSection(cs_vac);
                            if (vac_output_rx2)
                            {
                                rb_vacOUT_l.WritePtr(rx2_in_r, frameCount);     // write the output
                                rb_vacOUT_r.WritePtr(rx2_in_l, frameCount);
                            }
                            else
                            {
                                rb_vacOUT_l.WritePtr(rx1_in_r, frameCount);     // write the output
                                rb_vacOUT_r.WritePtr(rx1_in_l, frameCount);
                            }
                            Win32.LeaveCriticalSection(cs_vac);
                        }
                    }
                    else    // error - not enough write space
                    {
                        VACDebug("rb_vacOUT_l I/Q overflow ");
                        vac_rb_reset = true;
                    }
                }
            }

            //// handle Direct IQ for VAC - receive I/Q data is sent to VAC
            //if (vac_enabled && vac_output_iq &&
            //    rb_vacOUT_l != null && rb_vacOUT_r != null &&
            //    rx1_in_l != null && rx1_in_r != null)
            //{
            //    if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
            //    {
            //        if (vac_correct_iq)
            //            fixed (float* res_outl_ptr = &(res_outl[0]))    // use these buffers for corrected data
            //            fixed (float* res_outr_ptr = &(res_outr[0]))
            //            {
            //                if (vac_output_rx2)
            //                    CorrectIQBuffer(rx2_in_l, rx2_in_r, res_outl_ptr, res_outr_ptr, frameCount);
            //                else
            //                    CorrectIQBuffer(rx1_in_l, rx1_in_r,
            //                    res_outl_ptr, res_outr_ptr, frameCount);
            //                Win32.EnterCriticalSection(cs_vac);
            //                rb_vacOUT_l.WritePtr(res_outr_ptr, frameCount); // write the output
            //                rb_vacOUT_r.WritePtr(res_outl_ptr, frameCount); // why are these reversed??
            //                Win32.LeaveCriticalSection(cs_vac);

            //            }
            //        else
            //        {
            //            Win32.EnterCriticalSection(cs_vac);
            //            if (vac_output_rx2)
            //            {
            //                rb_vacOUT_l.WritePtr(rx2_in_r, frameCount);     // write the output
            //                rb_vacOUT_r.WritePtr(rx2_in_l, frameCount);
            //            }
            //            else
            //            {
            //                rb_vacOUT_l.WritePtr(rx1_in_r, frameCount);     // write the output
            //                rb_vacOUT_r.WritePtr(rx1_in_l, frameCount);
            //            }
            //            Win32.LeaveCriticalSection(cs_vac);
            //        }
            //    }
            //    else    // error - not enough write space
            //    {
            //        VACDebug("rb_vacOUT_l I/Q overflow ");
            //        vac_rb_reset = true;
            //    }
            //}

            if (varsampEnabledVAC2)
            {
                // handle Direct IQ for VAC2
                if (vac2_enabled && vac2_output_iq)
                {
                    fixed (double* resampBufPtr = &(resampBufVac2OutWrite[0]))
                    {
                        Swizzle(resampBufPtr, rx1_in_r, rx1_in_l, frameCount);
                        wdsp.xrmatchIN(rmatchVac2Out, resampBufPtr);
                    }
                }
            }
            else
            {
                // handle Direct IQ for VAC2
                if (vac2_enabled && vac2_output_iq &&
                    rb_vac2OUT_l != null && rb_vac2OUT_r != null)
                {
                    if ((rb_vac2OUT_l.WriteSpace() >= frameCount) && (rb_vac2OUT_r.WriteSpace() >= frameCount))
                    {
                        if (vac_correct_iq)
                            fixed (float* res_outl_ptr = &(res_vac2_outl[0]))
                            fixed (float* res_outr_ptr = &(res_vac2_outr[0]))
                            {
                                if (vac2_rx2)
                                    CorrectIQBuffer(rx2_in_l, rx2_in_r, res_outl_ptr, res_outr_ptr, frameCount);
                                else
                                    CorrectIQBuffer(rx1_in_l, rx1_in_r, res_outl_ptr, res_outr_ptr, frameCount);

                                Win32.EnterCriticalSection(cs_vac2);
                                rb_vac2OUT_l.WritePtr(res_outr_ptr, frameCount); // why are these reversed??
                                rb_vac2OUT_r.WritePtr(res_outl_ptr, frameCount);
                                Win32.LeaveCriticalSection(cs_vac2);

                            }
                        else
                        {
                            Win32.EnterCriticalSection(cs_vac2);
                            if (vac2_rx2)
                            {
                                rb_vac2OUT_l.WritePtr(rx2_in_r, frameCount);
                                rb_vac2OUT_r.WritePtr(rx2_in_l, frameCount);
                            }
                            else
                            {
                                rb_vac2OUT_l.WritePtr(rx1_in_r, frameCount);
                                rb_vac2OUT_r.WritePtr(rx1_in_l, frameCount);
                            }
                            Win32.LeaveCriticalSection(cs_vac2);
                        }
                    }
                    else
                    {
                        VACDebug("rb_vac2OUT_l I/Q overflow ");
                        vac2_rb_reset = true;
                    }
                }
            }

            //// handle Direct IQ for VAC2
            //if (vac2_enabled && vac2_output_iq &&
            //    rb_vac2OUT_l != null && rb_vac2OUT_r != null)
            //{
            //    if ((rb_vac2OUT_l.WriteSpace() >= frameCount) && (rb_vac2OUT_r.WriteSpace() >= frameCount))
            //    {
            //        if (vac_correct_iq)
            //            fixed (float* res_outl_ptr = &(res_vac2_outl[0]))
            //            fixed (float* res_outr_ptr = &(res_vac2_outr[0]))
            //            {
            //                if (vac2_rx2)
            //                    CorrectIQBuffer(rx2_in_l, rx2_in_r, res_outl_ptr, res_outr_ptr, frameCount);
            //                else
            //                    CorrectIQBuffer(rx1_in_l, rx1_in_r, res_outl_ptr, res_outr_ptr, frameCount);

            //                Win32.EnterCriticalSection(cs_vac2);
            //                rb_vac2OUT_l.WritePtr(res_outr_ptr, frameCount); // why are these reversed??
            //                rb_vac2OUT_r.WritePtr(res_outl_ptr, frameCount);
            //                Win32.LeaveCriticalSection(cs_vac2);

            //            }
            //        else
            //        {
            //            Win32.EnterCriticalSection(cs_vac2);
            //            if (vac2_rx2)
            //            {
            //                rb_vac2OUT_l.WritePtr(rx2_in_r, frameCount);
            //                rb_vac2OUT_r.WritePtr(rx2_in_l, frameCount);
            //            }
            //            else
            //            {
            //                rb_vac2OUT_l.WritePtr(rx1_in_r, frameCount);
            //                rb_vac2OUT_r.WritePtr(rx1_in_l, frameCount);
            //            }
            //            Win32.LeaveCriticalSection(cs_vac2);
            //        }
            //    }
            //    else
            //    {
            //        VACDebug("rb_vac2OUT_l I/Q overflow ");
            //        vac2_rb_reset = true;
            //    }
            //}

           // bool enable_bottom_pan;
            float* bottom_pan_l;
            float* bottom_pan_r;
            if (console.psform.DISPrcvr >= 0)
            {
                int bottom_pan_idx = 2 * console.psform.DISPrcvr;
                bottom_pan_l = (float*)array_ptr_input[bottom_pan_idx + 0];
                bottom_pan_r = (float*)array_ptr_input[bottom_pan_idx + 1];
                //enable_bottom_pan = true;
            }
            else
            {
                bottom_pan_l = (float*)array_ptr_input[10]; // will be empty for ANAN-10B
                bottom_pan_r = (float*)array_ptr_input[11];
               // enable_bottom_pan = false;
            }
            switch (console.NReceivers)
            {
                case 1:
                    {
                        if (console.specRX.GetSpecRX(0).NBOn)
                        {
                            SpecHPSDRDLL.xanbEXTF(0, rx1_in_l, rx1_in_r);
                        }

                        if (console.specRX.GetSpecRX(0).NB2On)
                        {
                            SpecHPSDRDLL.xnobEXTF(0, rx1_in_l, rx1_in_r);
                        }

                        if (console.SpecDisplay)
                        {
                            SpecHPSDRDLL.Spectrum(0, 0, 0, rx1_in_r, rx1_in_l);
                        }
                        break;
                    }
                case 2:
                case 3:
                    {
                        if (console.specRX.GetSpecRX(0).NBOn)
                        {
                            SpecHPSDRDLL.xanbEXTF(0, rx1_in_l, rx1_in_r);
                        }

                        if (console.specRX.GetSpecRX(1).NBOn)
                        {
                            SpecHPSDRDLL.xanbEXTF(3, rx2_in_l, rx2_in_r);
                        }

                        if (console.specRX.GetSpecRX(0).NB2On)
                        {
                            SpecHPSDRDLL.xnobEXTF(0, rx1_in_l, rx1_in_r);
                        }

                        if (console.specRX.GetSpecRX(1).NB2On)
                        {
                            SpecHPSDRDLL.xnobEXTF(3, rx2_in_l, rx2_in_r);
                        }

                        if (console.SpecDisplay)
                        {
                            SpecHPSDRDLL.Spectrum(0, 0, 0, rx1_in_r, rx1_in_l);
                        }

                        if (console.RX2Enabled) // && enable_bottom_pan)
                            SpecHPSDRDLL.Spectrum(1, 0, 0, bottom_pan_r, bottom_pan_l);
                        break;
                    }
                case 4:
                case 5:
                    {
                        if (console.specRX.GetSpecRX(0).NBOn)
                        {
                            switch (console.StitchedReceivers)
                            {
                                case 1:
                                    SpecHPSDRDLL.xanbEXTF(0, rx1_in_l, rx1_in_r);
                                    break;
                                case 3:
                                    SpecHPSDRDLL.xanbEXTF(0, (float*)array_ptr_input[6], (float*)array_ptr_input[7]); // rx3
                                    SpecHPSDRDLL.xanbEXTF(1, rx1_in_l, rx1_in_r);
                                    SpecHPSDRDLL.xanbEXTF(2, (float*)array_ptr_input[8], (float*)array_ptr_input[9]); // rx4
                                    break;
                            }
                        }

                        if (console.specRX.GetSpecRX(1).NBOn)
                        {
                            SpecHPSDRDLL.xanbEXTF(3, rx2_in_l, rx2_in_r);
                        }

                        if (console.specRX.GetSpecRX(0).NB2On)
                        {
                            switch (console.StitchedReceivers)
                            {
                                case 1:
                                    SpecHPSDRDLL.xnobEXTF(0, rx1_in_l, rx1_in_r);
                                    break;
                                case 3:
                                    SpecHPSDRDLL.xnobEXTF(0, (float*)array_ptr_input[6], (float*)array_ptr_input[7]); // rx3
                                    SpecHPSDRDLL.xnobEXTF(1, rx1_in_l, rx1_in_r);
                                    SpecHPSDRDLL.xnobEXTF(2, (float*)array_ptr_input[8], (float*)array_ptr_input[9]); // rx4
                                    break;
                            }
                        }

                        if (console.specRX.GetSpecRX(1).NB2On)
                    {
                            SpecHPSDRDLL.xnobEXTF(3, rx2_in_l, rx2_in_r);
                        }

                        if (console.SpecDisplay)
                        {
                            switch (console.StitchedReceivers)
                            {
                                case 1:
                                    SpecHPSDRDLL.Spectrum(0, 0, 0, rx1_in_r, rx1_in_l);
                                    break;
                                case 3:
                                    SpecHPSDRDLL.Spectrum(0, 0, 0, (float*)array_ptr_input[7], (float*)array_ptr_input[6]); //rx3
                                    SpecHPSDRDLL.Spectrum(0, 1, 0, rx1_in_r, rx1_in_l);
                                    SpecHPSDRDLL.Spectrum(0, 2, 0, (float*)array_ptr_input[9], (float*)array_ptr_input[8]); //rx4
                                    break;
                            }
                        }

                        if (console.RX2Enabled) // && enable_bottom_pan)
                            SpecHPSDRDLL.Spectrum(1, 0, 0, bottom_pan_r, bottom_pan_l);
                        break;
                    }
            }

            if (!localmox)
            {
                ClearBuffer(tx_out_l, frameCount);
                ClearBuffer(tx_out_r, frameCount);
            }

            if (localmox && (tx_dsp_mode == DSPMode.CWL || tx_dsp_mode == DSPMode.CWU))
            {
                dspExchange(ex_input, ex_output, out_count);
                ClearBuffer(tx_out_l, frameCount);
                ClearBuffer(tx_out_r, frameCount);
            }
            else if (tx_dsp_mode == DSPMode.CWL || tx_dsp_mode == DSPMode.CWU)
            {
                ClearBuffer(tx_out_l, frameCount);
                ClearBuffer(tx_out_r, frameCount);
                dspExchange(ex_input, ex_output, out_count);
            }
            else
            {
                dspExchange(ex_input, ex_output, out_count);
            }
#if(MINMAX)
			Debug.Write(MaxSample(out_l_ptr2, frameCount).ToString("f6")+",");
			Debug.Write(MaxSample(out_r_ptr2, frameCount).ToString("f6")+"\n");
#endif
            /*if (scope)    //rx1_rx4_envelope_scope_hack
            {
                float* rx4_in_l = (float*)array_ptr_input[8];
                float* rx4_in_r = (float*)array_ptr_input[9];
                for (int i = 0; i < frameCount; i++)
                {
                    rx1_in_l[i] = 2.0f * (float)Math.Sqrt(rx1_in_l[i] * rx1_in_l[i] + rx1_in_r[i] * rx1_in_r[i]);
                    rx1_in_r[i] = 2.0f * (float)Math.Sqrt(rx4_in_l[i] * rx4_in_l[i] + rx4_in_r[i] * rx4_in_r[i]);
                }
                DoScope(rx1_in_l, frameCount);
                DoScope2(rx1_in_r, frameCount);
            }*/
            
            if (scope)
            {
                if (!localmox)
                {
                    DoScope(rx1_out_l, out_count);
                    DoScope2(rx1_out_r, out_count);
                }
                else
                {
                    DoScope(tx_out_l, out_count);
                    DoScope2(tx_out_r, out_count);
                }
            }
            
            if (wave_record)
            {
                if (!localmox)
                {
                    if (!record_rx_preprocessed)
                    {
                        if (wave_file_writer.BaseRate != out_rate)
                        {
                            int outsamps;
                            wdsp.xresampleFV(out_l_ptr1, out_l_ptr4, out_count, &outsamps, wave_file_writer.ResampL);
                            wdsp.xresampleFV(out_r_ptr1, out_r_ptr4, out_count, &outsamps, wave_file_writer.ResampR);
                            wave_file_writer.AddWriteBuffer(out_l_ptr4, out_r_ptr4);
                        }
                        else
                            wave_file_writer.AddWriteBuffer(out_l_ptr1, out_r_ptr1);

                        if (wave_file_writer2 != null)
                        {
                            if (wave_file_writer2.BaseRate != out_rate)
                            {
                                int outsamps;
                                wdsp.xresampleFV(out_l_ptr3, out_l_ptr4, out_count, &outsamps, wave_file_writer2.ResampL);
                                wdsp.xresampleFV(out_r_ptr3, out_r_ptr4, out_count, &outsamps, wave_file_writer2.ResampR);
                                wave_file_writer2.AddWriteBuffer(out_l_ptr4, out_r_ptr4);
                            }
                            else
                                wave_file_writer2.AddWriteBuffer(rx2_out_l, rx2_out_r);
                        }
                    }
                }
                else
                {
                    if (!record_tx_preprocessed)
                    {
                        if (wave_file_writer.BaseRate != out_rate)
                        {
                            int outsamps;
                            wdsp.xresampleFV(out_l_ptr2, out_l_ptr4, out_count, &outsamps, wave_file_writer.ResampL);
                            wdsp.xresampleFV(out_r_ptr2, out_r_ptr4, out_count, &outsamps, wave_file_writer.ResampR);
                            wave_file_writer.AddWriteBuffer(out_l_ptr4, out_r_ptr4);
                        }
                        else
                            wave_file_writer.AddWriteBuffer(out_l_ptr2, out_r_ptr2);
                    }
                }
            }

            out_l1 = rx1_out_l; // 0 CallbackMonOutLbufp Audio L
            out_r1 = rx1_out_r; // 1 CallbackMonOutRbufp Audio R
            out_l2 = out_l_ptr2; // 2 CallbackOutLbufp TX I
            out_r2 = out_r_ptr2; // 3 CallbackOutRbufp TX Q
            out_l3 = out_l_ptr3; // 4 CallbackOutL2bufp RX2 L
            out_r3 = out_r_ptr3; // 5 CallbackOutR2bufp RX2 R
            out_l4 = out_l_ptr4; // 6 CallbackOutL3bufp VAC/RX1 L 
            out_r4 = out_r_ptr4; // 7 CallbackOutR3bufp VAC/RX1 R

            if (varsampEnabledVAC1)
            {
                // scale output for VAC -- use chan 4 as spare buffer
                if (vac_enabled && !vac_output_iq)
                {
                    if (!localmox)
                    {
                        ScaleBuffer(out_l1, out_l4, out_count, (float)vac_rx_scale);
                        ScaleBuffer(out_r1, out_r4, out_count, (float)vac_rx_scale);
                    }
                    else if (mon)
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, (float)vac_rx_scale);
                        ScaleBuffer(out_r2, out_r4, out_count, (float)vac_rx_scale);
                    }
                    else // zero samples going back to VAC since TX monitor is off
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, 0.0f);
                        ScaleBuffer(out_r2, out_r4, out_count, 0.0f);
                    }

                    // receiver output or monitor(transmitter) output goes to VAC
                    fixed (double* resampBufPtr = &(resampBufVac1OutWrite[0]))
                    {
                        Swizzle(resampBufPtr, out_l4, out_r4, out_count);
                        wdsp.xrmatchIN(rmatchVac1Out, resampBufPtr);
                    }
                }
            }
            else
            {
                // scale output for VAC -- use chan 4 as spare buffer
                if (vac_enabled && !vac_output_iq &&
                    rb_vacIN_l != null && rb_vacIN_r != null &&
                    rb_vacOUT_l != null && rb_vacOUT_r != null)
                {
                    if (!localmox)
                    {
                        ScaleBuffer(out_l1, out_l4, out_count, (float)vac_rx_scale);
                        ScaleBuffer(out_r1, out_r4, out_count, (float)vac_rx_scale);
                    }
                    else if (mon)
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, (float)vac_rx_scale);
                        ScaleBuffer(out_r2, out_r4, out_count, (float)vac_rx_scale);
                    }
                    else // zero samples going back to VAC since TX monitor is off
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, 0.0f);
                        ScaleBuffer(out_r2, out_r4, out_count, 0.0f);
                    }
                    // receiver output or monitor(transmitter) output goes to VAC
                    if (sample_rate2 == out_rate)
                    {
                        if ((rb_vacOUT_l.WriteSpace() >= out_count) && (rb_vacOUT_r.WriteSpace() >= out_count))
                        {
                            Win32.EnterCriticalSection(cs_vac);
                            rb_vacOUT_l.WritePtr(out_l4, out_count);
                            rb_vacOUT_r.WritePtr(out_r4, out_count);
                            Win32.LeaveCriticalSection(cs_vac);
                        }
                        else
                        {
                            VACDebug("rb_vacOUT_l overflow ");
                            vac_rb_reset = true;
                        }
                    }
                    else
                    {
                        if (vac_stereo)
                        {
                            fixed (float* res_outl_ptr = &(res_outl[0]))
                            fixed (float* res_outr_ptr = &(res_outr[0]))
                            {
                                int outsamps = 0;
                                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampPtrOut_l);
                                wdsp.xresampleFV(out_r4, res_outr_ptr, out_count, &outsamps, resampPtrOut_r);
                                if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                                {
                                    Win32.EnterCriticalSection(cs_vac);
                                    rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                    rb_vacOUT_r.WritePtr(res_outr_ptr, outsamps);
                                    Win32.LeaveCriticalSection(cs_vac);
                                }
                                else
                                {
                                    vac_rb_reset = true;
                                    VACDebug("rb_vacOUT_l overflow");
                                }
                            }
                        }
                        else
                        {
                            fixed (float* res_outl_ptr = &(res_outl[0]))
                            {
                                int outsamps = 0;
                                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampPtrOut_l);
                                if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                                {
                                    Win32.EnterCriticalSection(cs_vac);
                                    rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                    rb_vacOUT_r.WritePtr(res_outl_ptr, outsamps);
                                    Win32.LeaveCriticalSection(cs_vac);
                                }
                                else
                                {
                                    vac_rb_reset = true;
                                    VACDebug("rb_vacOUT_l overflow");
                                }
                            }
                        }
                    }
                }
            }

            //// scale output for VAC -- use chan 4 as spare buffer
            //if (vac_enabled && !vac_output_iq &&
            //    rb_vacIN_l != null && rb_vacIN_r != null &&
            //    rb_vacOUT_l != null && rb_vacOUT_r != null)
            //{
            //    if (!localmox)
            //    {
            //        ScaleBuffer(out_l1, out_l4, out_count, (float)vac_rx_scale);
            //        ScaleBuffer(out_r1, out_r4, out_count, (float)vac_rx_scale);
            //    }
            //    else if (mon)
            //    {
            //        ScaleBuffer(out_l2, out_l4, out_count, (float)vac_rx_scale);
            //        ScaleBuffer(out_r2, out_r4, out_count, (float)vac_rx_scale);
            //    }
            //    else // zero samples going back to VAC since TX monitor is off
            //    {
            //        ScaleBuffer(out_l2, out_l4, out_count, 0.0f);
            //        ScaleBuffer(out_r2, out_r4, out_count, 0.0f);
            //    }
            //    // receiver output or monitor(transmitter) output goes to VAC
            //    if (sample_rate2 == out_rate)
            //    {
            //        if ((rb_vacOUT_l.WriteSpace() >= out_count) && (rb_vacOUT_r.WriteSpace() >= out_count))
            //        {
            //            Win32.EnterCriticalSection(cs_vac);
            //            rb_vacOUT_l.WritePtr(out_l4, out_count);
            //            rb_vacOUT_r.WritePtr(out_r4, out_count);
            //            Win32.LeaveCriticalSection(cs_vac);
            //        }
            //        else
            //        {
            //            VACDebug("rb_vacOUT_l overflow ");
            //            vac_rb_reset = true;
            //        }
            //    }
            //    else
            //    {
            //        if (vac_stereo)
            //        {
            //            fixed (float* res_outl_ptr = &(res_outl[0]))
            //            fixed (float* res_outr_ptr = &(res_outr[0]))
            //            {
            //                int outsamps = 0;
            //                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampPtrOut_l);
            //                wdsp.xresampleFV(out_r4, res_outr_ptr, out_count, &outsamps, resampPtrOut_r);
            //                if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
            //                {
            //                    Win32.EnterCriticalSection(cs_vac);
            //                    rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
            //                    rb_vacOUT_r.WritePtr(res_outr_ptr, outsamps);
            //                    Win32.LeaveCriticalSection(cs_vac);
            //                }
            //                else
            //                {
            //                    vac_rb_reset = true;
            //                    VACDebug("rb_vacOUT_l overflow");
            //                }
            //            }
            //        }
            //        else
            //        {
            //            fixed (float* res_outl_ptr = &(res_outl[0]))
            //            {
            //                int outsamps = 0;
            //                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampPtrOut_l);
            //                if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
            //                {
            //                    Win32.EnterCriticalSection(cs_vac);
            //                    rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
            //                    rb_vacOUT_r.WritePtr(res_outl_ptr, outsamps);
            //                    Win32.LeaveCriticalSection(cs_vac);
            //                }
            //                else
            //                {
            //                    vac_rb_reset = true;
            //                    VACDebug("rb_vacOUT_l overflow");
            //                }
            //            }
            //        }
            //    }
            //}

            if (varsampEnabledVAC2)
            {
                // scale output for VAC2 -- use chan 4 as spare buffer
                if (vac2_enabled && !vac2_output_iq)
                {
                    if (!localmox || (localmox && !vfob_tx))
                    {
                        ScaleBuffer(out_l3, out_l4, out_count, (float)vac2_rx_scale);
                        ScaleBuffer(out_r3, out_r4, out_count, (float)vac2_rx_scale);
                    }
                    else if (mon)
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, (float)vac2_rx_scale);
                        ScaleBuffer(out_r2, out_r4, out_count, (float)vac2_rx_scale);
                    }
                    else // zero samples going back to VAC since TX monitor is off
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, 0.0f);
                        ScaleBuffer(out_r2, out_r4, out_count, 0.0f);
                    }

                    fixed (double* resampBufPtr = &(resampBufVac2OutWrite[0]))
                    {
                        Swizzle(resampBufPtr, out_l4, out_r4, out_count);
                        wdsp.xrmatchIN(rmatchVac2Out, resampBufPtr);
                    }
                }
            }
            else
            {
                // scale output for VAC2 -- use chan 4 as spare buffer
                if (vac2_enabled && !vac2_output_iq &&
                    rb_vac2IN_l != null && rb_vac2IN_r != null &&
                    rb_vac2OUT_l != null && rb_vac2OUT_r != null)
                {
                    if (!localmox || (localmox && !vfob_tx))
                    {
                        if (!vac2_rx2)
                        {
                            ScaleBuffer(out_l1, out_l4, out_count, (float)vac2_rx_scale);
                            ScaleBuffer(out_r1, out_r4, out_count, (float)vac2_rx_scale);
                        }
                        else
                        {
                            ScaleBuffer(out_l3, out_l4, out_count, (float)vac2_rx_scale);
                            ScaleBuffer(out_r3, out_r4, out_count, (float)vac2_rx_scale);
                        }
                    }
                    else if (mon)
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, (float)vac2_rx_scale);
                        ScaleBuffer(out_r2, out_r4, out_count, (float)vac2_rx_scale);
                    }
                    else // zero samples going back to VAC since TX monitor is off
                    {
                        ScaleBuffer(out_l2, out_l4, out_count, 0.0f);
                        ScaleBuffer(out_r2, out_r4, out_count, 0.0f);
                    }

                    if (sample_rate3 == out_rate)
                    {
                        if ((rb_vac2OUT_l.WriteSpace() >= out_count) && (rb_vac2OUT_r.WriteSpace() >= out_count))
                        {
                            Win32.EnterCriticalSection(cs_vac2);
                            rb_vac2OUT_l.WritePtr(out_l4, out_count);
                            rb_vac2OUT_r.WritePtr(out_r4, out_count);
                            Win32.LeaveCriticalSection(cs_vac2);
                        }
                        else
                        {
                            VACDebug("rb_vac2OUT_l overflow ");
                            vac2_rb_reset = true;
                        }
                    }
                    else
                    {
                        if (vac2_stereo)
                        {
                            fixed (float* res_outl_ptr = &(res_vac2_outl[0]))
                            fixed (float* res_outr_ptr = &(res_vac2_outr[0]))
                            {
                                int outsamps = 0;
                                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampVAC2PtrOut_l);
                                wdsp.xresampleFV(out_r4, res_outr_ptr, out_count, &outsamps, resampVAC2PtrOut_r);
                                if ((rb_vac2OUT_l.WriteSpace() >= outsamps) && (rb_vac2OUT_r.WriteSpace() >= outsamps))
                                {
                                    Win32.EnterCriticalSection(cs_vac2);
                                    rb_vac2OUT_l.WritePtr(res_outl_ptr, outsamps);
                                    rb_vac2OUT_r.WritePtr(res_outr_ptr, outsamps);
                                    Win32.LeaveCriticalSection(cs_vac2);
                                }
                                else
                                {
                                    vac2_rb_reset = true;
                                    VACDebug("rb_vac2OUT_l overflow");
                                }
                            }
                        }
                        else
                        {
                            fixed (float* res_outl_ptr = &(res_vac2_outl[0]))
                            {
                                int outsamps = 0;
                                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampVAC2PtrOut_l);
                                if ((rb_vac2OUT_l.WriteSpace() >= outsamps) && (rb_vac2OUT_r.WriteSpace() >= outsamps))
                                {
                                    Win32.EnterCriticalSection(cs_vac2);
                                    rb_vac2OUT_l.WritePtr(res_outl_ptr, outsamps);
                                    rb_vac2OUT_r.WritePtr(res_outl_ptr, outsamps);
                                    Win32.LeaveCriticalSection(cs_vac2);
                                }
                                else
                                {
                                    vac2_rb_reset = true;
                                    VACDebug("rb_vac2OUT_l overflow");
                                }
                            }
                        }
                    }
                }
            }

            //// scale output for VAC2 -- use chan 4 as spare buffer
            //if (vac2_enabled && !vac2_output_iq &&
            //    rb_vac2IN_l != null && rb_vac2IN_r != null &&
            //    rb_vac2OUT_l != null && rb_vac2OUT_r != null)
            //{
            //    if (!localmox || (localmox && !vfob_tx))
            //    {
            //        if (!vac2_rx2)
            //        {
            //            ScaleBuffer(out_l1, out_l4, out_count, (float)vac2_rx_scale);
            //            ScaleBuffer(out_r1, out_r4, out_count, (float)vac2_rx_scale);
            //        }
            //        else
            //        {
            //            ScaleBuffer(out_l3, out_l4, out_count, (float)vac2_rx_scale);
            //            ScaleBuffer(out_r3, out_r4, out_count, (float)vac2_rx_scale);
            //        }
            //    }
            //    else if (mon)
            //    {
            //        ScaleBuffer(out_l2, out_l4, out_count, (float)vac2_rx_scale);
            //        ScaleBuffer(out_r2, out_r4, out_count, (float)vac2_rx_scale);
            //    }
            //    else // zero samples going back to VAC since TX monitor is off
            //    {
            //        ScaleBuffer(out_l2, out_l4, out_count, 0.0f);
            //        ScaleBuffer(out_r2, out_r4, out_count, 0.0f);
            //    }

            //    if (sample_rate3 == out_rate)
            //    {
            //        if ((rb_vac2OUT_l.WriteSpace() >= out_count) && (rb_vac2OUT_r.WriteSpace() >= out_count))
            //        {
            //            Win32.EnterCriticalSection(cs_vac2);
            //            rb_vac2OUT_l.WritePtr(out_l4, out_count);
            //            rb_vac2OUT_r.WritePtr(out_r4, out_count);
            //            Win32.LeaveCriticalSection(cs_vac2);
            //        }
            //        else
            //        {
            //            VACDebug("rb_vac2OUT_l overflow ");
            //            vac2_rb_reset = true;
            //        }
            //    }
            //    else
            //    {
            //        if (vac2_stereo)
            //        {
            //            fixed (float* res_outl_ptr = &(res_vac2_outl[0]))
            //            fixed (float* res_outr_ptr = &(res_vac2_outr[0]))
            //            {
            //                int outsamps = 0;
            //                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampVAC2PtrOut_l);
            //                wdsp.xresampleFV(out_r4, res_outr_ptr, out_count, &outsamps, resampVAC2PtrOut_r);
            //                if ((rb_vac2OUT_l.WriteSpace() >= outsamps) && (rb_vac2OUT_r.WriteSpace() >= outsamps))
            //                {
            //                    Win32.EnterCriticalSection(cs_vac2);
            //                    rb_vac2OUT_l.WritePtr(res_outl_ptr, outsamps);
            //                    rb_vac2OUT_r.WritePtr(res_outr_ptr, outsamps);
            //                    Win32.LeaveCriticalSection(cs_vac2);
            //                }
            //                else
            //                {
            //                    vac2_rb_reset = true;
            //                    VACDebug("rb_vac2OUT_l overflow");
            //                }
            //            }
            //        }
            //        else
            //        {
            //            fixed (float* res_outl_ptr = &(res_vac2_outl[0]))
            //            {
            //                int outsamps = 0;
            //                wdsp.xresampleFV(out_l4, res_outl_ptr, out_count, &outsamps, resampVAC2PtrOut_l);
            //                if ((rb_vac2OUT_l.WriteSpace() >= outsamps) && (rb_vac2OUT_r.WriteSpace() >= outsamps))
            //                {
            //                    Win32.EnterCriticalSection(cs_vac2);
            //                    rb_vac2OUT_l.WritePtr(res_outl_ptr, outsamps);
            //                    rb_vac2OUT_r.WritePtr(res_outl_ptr, outsamps);
            //                    Win32.LeaveCriticalSection(cs_vac2);
            //                }
            //                else
            //                {
            //                    vac2_rb_reset = true;
            //                    VACDebug("rb_vac2OUT_l overflow");
            //                }
            //            }
            //        }
            //    }
            //}

            // output from DSP is organized as follows
            //=========================================================
            //Channel |   0      1    |    2      3   |   4      5    |
            //Signal  | RX1 L  RX1 R  |  TX L   TX R  | RX2 L  RX2 R  |
            //Pointer | out_l1 out_r1 | out_l2 out_r2 | out_l3 out_r3 |
            //=========================================================

            // output DAC lineup for HPSDR
            //====================================================================================================
            //Channel |   0       1    |      2           3       |     4       5      |      6       |    7     |
            //Signal  |Audio L Audio R |     TX I        TX Q     |                    |              |          |
            //Pointer | out_l1  out_r1 |    out_l2      out_r2    |   out_l3  out_r3   |    out_l4    |  out_r4  |
            //====================================================================================================

            // double tx_vol = TXScale;
            // if (tx_vol > 1.0) tx_vol = 1.0; // above 1.0 creates spurs

            if (mute_rx1)
            {
                ClearBuffer(out_l1, out_count);
                ClearBuffer(out_r1, out_count);
            }

            if (mute_rx2)
            {
                ClearBuffer(out_l3, out_count);
                ClearBuffer(out_r3, out_count);
            }

            //redirect main audio to spare buffer
            CopyBuffer(out_l1, out_l4, out_count);
            CopyBuffer(out_r1, out_r4, out_count);

            if (console.CurrentModel != Model.HERMES && !console.PennyLanePresent)
            // Hermes power level set by command and control to programmable gain amp .. 
            //no need to do digital scaling  for power 
            {
                ScaleBuffer(out_l2, out_l2, out_count, (float)TXScale);
                ScaleBuffer(out_r2, out_r2, out_count, (float)TXScale);
            }

            if (!full_duplex)
            {
                if (!localmox) // RX Mode
                {
                    if (rx2_enabled)
                    {
                        // Scale the output for Mercury
                        AddBuffer(out_l4, out_l3, out_count);
                        AddBuffer(out_r4, out_r3, out_count);
                    }
                    ScaleBuffer(out_l4, out_r1, out_count, (float)monitor_volume);
                    ScaleBuffer(out_r4, out_l1, out_count, (float)monitor_volume);
                }
                else if (mon) // TX + Monitor
                {
                    // scale monitor output to match receiver level (half scale)
                    ScaleBuffer(out_l2, out_l1, out_count, 0.5f);
                    ScaleBuffer(out_r2, out_r1, out_count, 0.5f);

                    // if RX2 is present, combine the outputs
                    if (rx2_enabled && !rx2_auto_mute_tx && console.psform.DISPrcvr > 0)
                    {
                        AddBuffer(out_l1, out_r3, out_count);
                        AddBuffer(out_r1, out_l3, out_count);
                    }

                    // Scale the output for Mercury
                    ScaleBuffer(out_l1, out_l1, out_count, (float)monitor_volume);
                    ScaleBuffer(out_r1, out_r1, out_count, (float)monitor_volume);
                }
                else // TX (w/o Monitor)
                {
                    // if RX2 is present, use that output
                    if (rx2_enabled && !rx2_auto_mute_tx && console.psform.DISPrcvr > 0)
                    {
                        // Scale the output for Mercury
                        ScaleBuffer(out_l3, out_r1, out_count, (float)monitor_volume);
                        ScaleBuffer(out_r3, out_l1, out_count, (float)monitor_volume);
                    }
                    else // no RX2, so output RX1
                    {
                        // output silence to Mercury
                        ClearBuffer(out_l1, out_count);
                        ClearBuffer(out_r1, out_count);
                    }
                }
            }
            else // Full Duplex
            {
                if (!rx2_enabled)
                {
                    // monitor is on, should hear TX audio
                    //  if (localmox)
                    {
                        if (mon)
                        {
                            // scale monitor output to match receiver level (half scale)
                            ScaleBuffer(out_l2, out_l1, out_count, 0.5f);
                            ScaleBuffer(out_r2, out_r1, out_count, 0.5f);

                            ScaleBuffer(out_l1, out_l1, out_count, (float)monitor_volume);
                            ScaleBuffer(out_r1, out_r1, out_count, (float)monitor_volume);
                        }
                        else // monitor is off, should hear RX audio
                        {
                            // Scale the output for Mercury
                            ScaleBuffer(out_l4, out_r1, out_count, (float)monitor_volume);
                            ScaleBuffer(out_r4, out_l1, out_count, (float)monitor_volume);
                        }
                    }
                }
                else // RX2 is enabled
                {
                    if (!localmox)
                    {
                        // combine RX2 audio with RX1
                        AddBuffer(out_l4, out_l3, out_count);
                        AddBuffer(out_r4, out_r3, out_count);

                        // Scale the output for Mercury
                        ScaleBuffer(out_l4, out_r1, out_count, (float)monitor_volume);
                        ScaleBuffer(out_r4, out_l1, out_count, (float)monitor_volume);
                    }
                    else if (mon) // monitor is on, should hear RX1 + TX audio
                    {
                        // scale monitor output to match receiver level (half scale)
                        ScaleBuffer(out_l2, out_l1, out_count, 0.5f);
                        ScaleBuffer(out_r2, out_r1, out_count, 0.5f);

                        // combine the RX1 and TX audio
                        AddBuffer(out_l4, out_l2, out_count);
                        AddBuffer(out_r4, out_r2, out_count);

                        // Scale the output for Mercury
                        ScaleBuffer(out_l4, out_r1, out_count, (float)monitor_volume);
                        ScaleBuffer(out_r4, out_l1, out_count, (float)monitor_volume);
                    }
                    else // monitor is off, should hear RX1 audio
                    {
                        // Scale the output for the headphones
                        ScaleBuffer(out_l4, out_r1, out_count, (float)monitor_volume);
                        ScaleBuffer(out_r4, out_l1, out_count, (float)monitor_volume);
                    }
                }
            } 

 

#if(MINMAX)
			Debug.Write(MaxSample(out_l2, out_r2, frameCount).ToString("f6")+",");

			float current_max = MaxSample(out_l2, out_r2, frameCount);
			if(current_max > max) max = current_max;
			Debug.WriteLine(" max: "+max.ToString("f6"));
#endif

#if(TIMER)
			t1.Stop();
			Debug.WriteLine(t1.Duration);
#endif
            return callback_return;
        }


#if(TIMER)
		private static HiPerfTimer t1 = new HiPerfTimer();
#endif
        //private static int count = 0;
        unsafe public static int Callback4Port(void* input, void* output, int frameCount,
                                               PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
#if(TIMER)
			t1.Start();
#endif
            float* in_l = null, in_r = null, out_l = null, out_r = null;
            float* out_l1 = null, out_r1 = null, out_l2 = null, out_r2 = null;
            localmox = mox;

            void* ex_input = input;
            void* ex_output = output;
            int** array_ptr_input = (int**)input;
            float* in_l_ptr1 = (float*)array_ptr_input[0];
            float* in_r_ptr1 = (float*)array_ptr_input[1];
            float* in_l_ptr2 = (float*)array_ptr_input[2];
            float* in_r_ptr2 = (float*)array_ptr_input[3];
            int** array_ptr_output = (int**)output;
            float* out_l_ptr1 = (float*)array_ptr_output[0];
            float* out_r_ptr1 = (float*)array_ptr_output[1];
            float* out_l_ptr2 = (float*)array_ptr_output[2];
            float* out_r_ptr2 = (float*)array_ptr_output[3];

            // arrange input buffers in the following order:
            // RX1 Left, RX1 Right, TX Left, TX Right, RX2 Left, RX2 Right
            int** array_ptr = (int**)input;
            switch (in_rx1_l)
            {
                case 0:
                    array_ptr[0] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr[0] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr[0] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr[0] = (int*)in_r_ptr2;
                    break;
            }

            switch (in_rx1_r)
            {
                case 0:
                    array_ptr[1] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr[1] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr[1] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr[1] = (int*)in_r_ptr2;
                    break;
            }

            switch (in_tx_l)
            {
                case 0:
                    array_ptr[2] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr[2] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr[2] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr[2] = (int*)in_r_ptr2;
                    break;
            }

            switch (in_tx_r)
            {
                case 0:
                    array_ptr[3] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr[3] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr[3] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr[3] = (int*)in_r_ptr2;
                    break;
            }

            /*switch(in_rx2_l)
            {
                case 0: break;
                case 1: array_ptr[4] = (int*)in_r_ptr1; break;
                case 2: array_ptr[4] = (int*)in_l_ptr2; break;
                case 3: array_ptr[4] = (int*)in_r_ptr2; break;
            }
            switch(in_rx2_r)
            {
                case 0: break;
                case 1: array_ptr[5] = (int*)in_r_ptr1; break;
                case 2: array_ptr[5] = (int*)in_l_ptr2; break;
                case 3: array_ptr[5] = (int*)in_r_ptr2; break;
            }*/

            if (!localmox)
            {
                in_l = (float*)array_ptr_input[0];
                in_r = (float*)array_ptr_input[1];
            }
            else
            {
                in_l = (float*)array_ptr_input[2];
                in_r = (float*)array_ptr_input[3];
            }

            if (wave_playback)
                wave_file_reader.GetPlayBuffer(in_l, in_r);
            if (wave_record)
            {
                if (!localmox)
                {
                    if (record_rx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(in_l, in_r);
                    }
                }
                else
                {
                    if (record_tx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(in_l, in_r);
                    }
                }
            }

            if (phase)
            {
                //phase_mutex.WaitOne();
                Marshal.Copy(new IntPtr(in_l), phase_buf_l, 0, frameCount);
                Marshal.Copy(new IntPtr(in_r), phase_buf_r, 0, frameCount);
                //phase_mutex.ReleaseMutex();
            }

            // handle VAC Input
            if (vac_enabled &&
                rb_vacIN_l != null && rb_vacIN_r != null &&
                rb_vacOUT_l != null && rb_vacOUT_r != null)
            {
                if (vac_bypass || !localmox) // drain VAC Input ring buffer
                {
                    if ((rb_vacIN_l.ReadSpace() >= frameCount) && (rb_vacIN_r.ReadSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacIN_l.ReadPtr(out_l_ptr2, frameCount);
                        rb_vacIN_r.ReadPtr(out_r_ptr2, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                }
                else
                {
                    if (rb_vacIN_l.ReadSpace() >= frameCount)
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacIN_l.ReadPtr(in_l, frameCount);
                        rb_vacIN_r.ReadPtr(in_r, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                        if (vac_combine_input)
                            AddBuffer(in_l, in_r, frameCount);
                    }
                    else
                    {
                        ClearBuffer(in_l, frameCount);
                        ClearBuffer(in_r, frameCount);
                        VACDebug("rb_vacIN underflow 4inTX");
                    }
                    ScaleBuffer(in_l, in_l, frameCount, (float)vac_preamp);
                    ScaleBuffer(in_r, in_r, frameCount, (float)vac_preamp);
                }
            }

            switch (current_audio_state1)
            {
                case AudioState.DTTSP:

                    #region VOX

                    if (vox_enabled)
                    {
                        float* vox_l = null, vox_r = null;
                        switch (soundcard)
                        {
                            case SoundCard.FIREBOX:
                            case SoundCard.EDIROL_FA_66:
                                vox_l = in_l_ptr1;
                                vox_r = in_r_ptr1;
                                break;
                            case SoundCard.DELTA_44:
                            default:
                                vox_l = in_l_ptr2;
                                vox_r = in_r_ptr2;
                                break;
                        }

                        if (tx_dsp_mode == DSPMode.LSB ||
                            tx_dsp_mode == DSPMode.USB ||
                            tx_dsp_mode == DSPMode.DSB ||
                            tx_dsp_mode == DSPMode.AM ||
                            tx_dsp_mode == DSPMode.SAM ||
                            tx_dsp_mode == DSPMode.FM)
                        {
                            peak = MaxSample(vox_l, vox_r, frameCount);

                            // compare power to threshold
                            vox_active = peak > vox_threshold;
                        }
                    }

                    #endregion

                    if (tx_dsp_mode == DSPMode.CWU || tx_dsp_mode == DSPMode.CWL)
                    {
                        //DttSP.CWtoneExchange(out_l_ptr1, out_r_ptr1, frameCount);
                    }

                    // scale input with mic preamp
                    if ((!vac_enabled &&
                         (tx_dsp_mode == DSPMode.LSB ||
                          tx_dsp_mode == DSPMode.USB ||
                          tx_dsp_mode == DSPMode.DSB ||
                          tx_dsp_mode == DSPMode.AM ||
                          tx_dsp_mode == DSPMode.SAM ||
                          tx_dsp_mode == DSPMode.FM ||
                          tx_dsp_mode == DSPMode.DIGL ||
                          tx_dsp_mode == DSPMode.DIGU)) ||
                        (vac_enabled && vac_bypass &&
                         (tx_dsp_mode == DSPMode.DIGL ||
                          tx_dsp_mode == DSPMode.DIGU ||
                          tx_dsp_mode == DSPMode.LSB ||
                          tx_dsp_mode == DSPMode.USB ||
                          tx_dsp_mode == DSPMode.DSB ||
                          tx_dsp_mode == DSPMode.AM ||
                          tx_dsp_mode == DSPMode.SAM ||
                          tx_dsp_mode == DSPMode.FM)))
                    {
                        if (wave_playback)
                        {
                            ScaleBuffer(in_l, in_l, frameCount, (float)wave_preamp);
                            ScaleBuffer(in_r, in_r, frameCount, (float)wave_preamp);
                        }
                        else
                        {
                            if (localmox)
                            {
                                if (!vac_enabled && (tx_dsp_mode == DSPMode.DIGL || tx_dsp_mode == DSPMode.DIGU))
                                {
                                    ScaleBuffer(in_l, in_l, frameCount, (float)vac_preamp);
                                    ScaleBuffer(in_r, in_r, frameCount, (float)vac_preamp);
                                }
                                else
                                {
                                    ScaleBuffer(in_l, in_l, frameCount, (float)mic_preamp);
                                    ScaleBuffer(in_r, in_r, frameCount, (float)mic_preamp);
                                }
                            }
                        }
                    }

                    #region Input Signal Source

                    if (!mox)
                    {
                        switch (rx1_input_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(in_l, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_LEFT_ONLY:
                                phase_accumulator1 = SineWave(in_l, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ClearBuffer(in_r, frameCount);
                                break;
                            case SignalSource.SINE_RIGHT_ONLY:
                                phase_accumulator1 = SineWave(in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                ClearBuffer(in_l, frameCount);
                                break;
                            case SignalSource.NOISE:
                                Noise(in_l, frameCount);
                                Noise(in_r, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(in_l, frameCount);
                                CopyBuffer(in_l, in_r, frameCount);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(in_l, frameCount);
                                ClearBuffer(in_r, frameCount);
                                break;
                        }
                    }
                    else
                    {
                        switch (tx_input_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(in_l, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.NOISE:
                                Noise(in_l, frameCount);
                                Noise(in_r, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(in_l, frameCount, sine_freq1);
                                CopyBuffer(in_l, in_r, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(in_l, frameCount);
                                CopyBuffer(in_l, in_r, frameCount);
                                ScaleBuffer(in_l, in_l, frameCount, (float)source_scale);
                                ScaleBuffer(in_r, in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(in_l, frameCount);
                                ClearBuffer(in_r, frameCount);
                                break;
                        }
                    }

                    #endregion

#if(MINMAX)
					Debug.Write(MaxSample(in_l, in_r, frameCount).ToString("f6")+",");
#endif

                    //DttSP.ExchangeSamples2(ex_input, ex_output, frameCount);
                    dspExchange(ex_input, ex_output, frameCount);

#if(MINMAX)
					Debug.Write(MaxSample(out_l_ptr1, out_r_ptr1, frameCount).ToString("f6")+",");
#endif

                    #region Output Signal Source

                    if (!mox)
                    {
                        switch (rx1_output_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(out_l_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(out_r_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(out_l_ptr1, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(out_r_ptr1, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_LEFT_ONLY:
                                phase_accumulator1 = SineWave(out_l_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ClearBuffer(out_r_ptr1, frameCount);
                                break;
                            case SignalSource.SINE_RIGHT_ONLY:
                                phase_accumulator1 = SineWave(out_r_ptr1, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                ClearBuffer(out_l_ptr1, frameCount);
                                break;
                            case SignalSource.NOISE:
                                Noise(out_l_ptr1, frameCount);
                                Noise(out_r_ptr1, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(out_l_ptr1, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(out_l_ptr1, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(out_l_ptr1, frameCount);
                                CopyBuffer(out_l_ptr1, out_r_ptr1, frameCount);
                                ScaleBuffer(out_l_ptr1, out_l_ptr1, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr1, out_r_ptr1, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(out_l_ptr1, frameCount);
                                ClearBuffer(out_r_ptr1, frameCount);
                                break;
                        }
                    }
                    else
                    {
                        switch (tx_output_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(out_l_ptr2, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(out_r_ptr2, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(out_l_ptr2, out_l_ptr2, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr2, out_r_ptr2, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(out_l_ptr2, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(out_r_ptr2, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(out_l_ptr2, out_l_ptr2, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr2, out_r_ptr2, frameCount, (float)source_scale);
                                break;
                            case SignalSource.NOISE:
                                Noise(out_l_ptr2, frameCount);
                                Noise(out_r_ptr2, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(out_l_ptr2, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr2, out_r_ptr2, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(out_l_ptr2, frameCount, sine_freq1);
                                CopyBuffer(out_l_ptr2, out_r_ptr2, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(out_l_ptr2, frameCount);
                                CopyBuffer(out_l_ptr2, out_r_ptr2, frameCount);
                                ScaleBuffer(out_l_ptr2, out_l_ptr2, frameCount, (float)source_scale);
                                ScaleBuffer(out_r_ptr2, out_r_ptr2, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(out_l_ptr2, frameCount);
                                ClearBuffer(out_r_ptr2, frameCount);
                                break;
                        }
                    }

                    #endregion

                    break;
                case AudioState.CW:

                    #region oldswitch

                    /*if(next_audio_state1 == AudioState.SWITCH)
					{
						ClearBuffer(in_l_ptr1, frameCount);
						ClearBuffer(in_r_ptr1, frameCount);
						if (vac_enabled) 
						{
							if((rb_vacIN_l.ReadSpace() >= frameCount)&&(rb_vacIN_r.ReadSpace() >= frameCount))
							{
								Win32.EnterCriticalSection(cs_vac);
								rb_vacIN_l.ReadPtr(in_l_ptr1,frameCount);
								rb_vacIN_r.ReadPtr(in_r_ptr1,frameCount);
								Win32.LeaveCriticalSection(cs_vac);
							}
							else
							{
								VACDebug("rb_vacIN_l underflow 4inTX");
							}
						}
						DttSP.ExchangeSamples2(array_ptr_input, array_ptr_output, frameCount);
						if (switch_count == 0) next_audio_state1 = AudioState.CW;
						switch_count--;
					}*/

                    #endregion

                    //DttSP.CWtoneExchange(out_l_ptr2, out_r_ptr2, frameCount);
                    //					if (cw_delay == 1) cw_delay = 0;
                    break;
                /*case AudioState.SINL_COSR:
                if(!mox)
                {
                    out_l = out_l_ptr1;
                    out_r = out_r_ptr1;
                }
                else
                {
                    out_l = out_l_ptr2;
                    out_r = out_r_ptr2;
                }

                if(two_tone)
                {
                    double dump;
						
                    SineWave2Tone(out_l, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out dump, out dump);
							
                    CosineWave2Tone(out_r, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out phase_accumulator1, out phase_accumulator2);
                }
                else
                {
                    SineWave(out_l, frameCount, phase_accumulator1, sine_freq1);
                    phase_accumulator1 = CosineWave(out_r, frameCount, phase_accumulator1, sine_freq1);
                }
                break;					
            case AudioState.SINL_SINR:
                if(!mox)
                {
                    out_l = out_l_ptr1;
                    out_r = out_r_ptr1;
                }
                else
                {
                    out_l = out_l_ptr2;
                    out_r = out_r_ptr2;
                }

                if(two_tone)
                {
                    SineWave2Tone(out_l, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out phase_accumulator1, out phase_accumulator2);
                    CopyBuffer(out_l, out_r, frameCount);							
                }
                else
                {
                    phase_accumulator1 = SineWave(out_l, frameCount, phase_accumulator1, sine_freq1);
                    CopyBuffer(out_l, out_r, frameCount);
                }
                break;
            case AudioState.SINL_NOR:
                if(!mox)
                {
                    out_l = out_l_ptr1;
                    out_r = out_r_ptr1;
                }
                else
                {
                    out_l = out_l_ptr2;
                    out_r = out_r_ptr2;
                }
                if(two_tone)
                {
                    SineWave2Tone(out_l, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out phase_accumulator1, out phase_accumulator2);
                    ClearBuffer(out_r, frameCount);		
                }
                else
                {
                    phase_accumulator1 = SineWave(out_l, frameCount, phase_accumulator1, sine_freq1);
                    ClearBuffer(out_r, frameCount);
                }
                break;
            case AudioState.COSL_SINR:
                if(!mox)
                {
                    out_l = out_l_ptr1;
                    out_r = out_r_ptr1;
                }
                else
                {
                    out_l = out_l_ptr2;
                    out_r = out_r_ptr2;
                }

                if(two_tone)
                {
                    double dump;
						
                    CosineWave2Tone(out_l, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out dump, out dump);

                    SineWave2Tone(out_r, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out phase_accumulator1, out phase_accumulator2);
                }
                else
                {
                    CosineWave(out_l, frameCount, phase_accumulator1, sine_freq1);
                    phase_accumulator1 = SineWave(out_r, frameCount, phase_accumulator1, sine_freq1);
                }
                break;
            case AudioState.NOL_SINR:
                if(!mox)
                {
                    out_l = out_l_ptr1;
                    out_r = out_r_ptr1;
                }
                else
                {
                    out_l = out_l_ptr2;
                    out_r = out_r_ptr2;
                }

                if(two_tone)
                {
                    ClearBuffer(out_l, frameCount);
                    SineWave2Tone(out_r, frameCount,
                        phase_accumulator1, phase_accumulator2,
                        sine_freq1, sine_freq2,
                        out phase_accumulator1, out phase_accumulator2);
                }
                else
                {
                    ClearBuffer(out_l, frameCount);
                    phase_accumulator1 = SineWave(out_r, frameCount, phase_accumulator1, sine_freq1);
                }
                break;
            case AudioState.NOL_NOR:
                if(!mox)
                {
                    out_l = out_l_ptr1;
                    out_r = out_r_ptr1;
                }
                else
                {
                    out_l = out_l_ptr2;
                    out_r = out_r_ptr2;
                }

                ClearBuffer(out_l, frameCount);
                ClearBuffer(out_r, frameCount);
                break;
            case AudioState.PIPE:
                CopyBuffer(in_l_ptr1, out_l_ptr1, frameCount);
                CopyBuffer(in_r_ptr1, out_r_ptr1, frameCount);
                CopyBuffer(in_l_ptr2, out_l_ptr2, frameCount);
                CopyBuffer(in_r_ptr2, out_r_ptr2, frameCount);
                break;*/

                #region oldswitch2_vac

                /*case AudioState.SWITCH:
						if(!ramp_down && !ramp_up)
						{
							switch(dsp_mode)
							{
								case DSPMode.CWL:
								case DSPMode.CWU:
									break;
								default:
									ClearBuffer(in_l_ptr2, frameCount);
									ClearBuffer(in_r_ptr2, frameCount);
									break;
							}
							if(mox != next_mox) mox = next_mox;
						}

						if(vac_enabled)
						{
							if((rb_vacIN_l.ReadSpace() >= frameCount)&&(rb_vacIN_r.ReadSpace() >= frameCount))
							{
								Win32.EnterCriticalSection(cs_vac);
								rb_vacIN_l.ReadPtr(in_l_ptr2,frameCount);
								rb_vacIN_r.ReadPtr(in_r_ptr2,frameCount);
								Win32.LeaveCriticalSection(cs_vac);
							}
							else
							{
								VACDebug("rb_vacIN_l underflow 4inTX");
							}
						}
						DttSP.ExchangeSamples2(array_ptr_input, array_ptr_output, frameCount);
						if(ramp_down)
						{
							int i;
							for(i=0; i<frameCount; i++)
							{
								float w = (float)Math.Sin(ramp_val * Math.PI / 2.0);
								out_l_ptr1[i] *= w;
								out_r_ptr1[i] *= w;
								ramp_val += ramp_step;
								if(++ramp_count >= ramp_samples)
								{
									ramp_down = false;
									break;
								}
							}

							if(ramp_down)
							{
								for(; i<frameCount; i++)
								{
									out_l_ptr1[i] = 0.0f;
									out_r_ptr1[i] = 0.0f;
								}
							}
						}
						else if(ramp_up)
						{
							for(int i=0; i<frameCount; i++)
							{
								float w = (float)Math.Sin(ramp_val * Math.PI / 2.0);
								out_l_ptr1[i] *= w;
								out_r_ptr1[i] *= w;
								ramp_val += ramp_step;
								if(++ramp_count >= ramp_samples)
								{
									ramp_up = false;
									break;
								}
							}
						}
						else
						{
							ClearBuffer(out_l_ptr1, frameCount);
							ClearBuffer(out_r_ptr1, frameCount);
						}

						if (next_audio_state1 == AudioState.CW) 
						{
							//cw_delay = 1;
							DttSP.CWtoneExchange(out_l_ptr2, out_r_ptr2, frameCount);
						}  
						else if(switch_count == 1)
							DttSP.CWRingRestart();
					
						switch_count--;
						//if(switch_count == ramp_up_num) RampUp = true;
						if(switch_count == 0)
							current_audio_state1 = next_audio_state1;
						break;*/

                #endregion
            }

            DoScope(!localmox ? out_l_ptr1 : out_l_ptr2, frameCount);

            out_l1 = out_l_ptr1;
            out_r1 = out_r_ptr1;
            out_l2 = out_l_ptr2;
            out_r2 = out_r_ptr2;

            if (wave_record)
            {
                if (!localmox)
                {
                    if (!record_rx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(out_l_ptr1, out_r_ptr1);
                    }
                }
                else
                {
                    if (!record_tx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(out_l_ptr2, out_r_ptr2);
                    }
                }
            }

            // scale output for VAC
            if (vac_enabled &&
                rb_vacIN_l != null && rb_vacIN_r != null &&
                rb_vacOUT_l != null && rb_vacOUT_r != null)
            {
                if (!localmox)
                {
                    ScaleBuffer(out_l1, out_l2, frameCount, (float)vac_rx_scale);
                    ScaleBuffer(out_r1, out_r2, frameCount, (float)vac_rx_scale);
                }
                else if (mon)
                {
                    ScaleBuffer(out_l2, out_l1, frameCount, (float)vac_rx_scale);
                    ScaleBuffer(out_r2, out_r1, frameCount, (float)vac_rx_scale);
                }
                else // zero samples going back to VAC since TX monitor is off
                {
                    ScaleBuffer(out_l2, out_l1, frameCount, 0.0f);
                    ScaleBuffer(out_r2, out_r1, frameCount, 0.0f);
                }

                float* vac_l, vac_r;
                if (!localmox)
                {
                    vac_l = out_l2;
                    vac_r = out_r2;
                }
                else
                {
                    vac_l = out_l1;
                    vac_r = out_r1;
                }

                if (sample_rate2 == sample_rate1)
                {
                    if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacOUT_l.WritePtr(vac_l, frameCount);
                        rb_vacOUT_r.WritePtr(vac_r, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                    else
                    {
                        VACDebug("rb_vacOUT_l overflow ");
                        vac_rb_reset = true;
                    }
                }
                else
                {
                    if (vac_stereo)
                    {
                        fixed (float* res_outl_ptr = &(res_outl[0]))
                        fixed (float* res_outr_ptr = &(res_outr[0]))
                        {
                            int outsamps = 0;
                            //DttSP.DoResamplerF(vac_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            wdsp.xresampleFV(vac_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            //DttSP.DoResamplerF(vac_r, res_outr_ptr, frameCount, &outsamps, resampPtrOut_r);
                            wdsp.xresampleFV(vac_r, res_outr_ptr, frameCount, &outsamps, resampPtrOut_r);
                            //Debug.WriteLine("Outsamps: "+outsamps.ToString());
                            if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                rb_vacOUT_r.WritePtr(res_outr_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                            else
                            {
                                vac_rb_reset = true;
                                VACDebug("rb_vacOUT_l overflow");
                            }
                        }
                    }
                    else
                    {
                        fixed (float* res_outl_ptr = &(res_outl[0]))
                        {
                            int outsamps = 0;
                            //DttSP.DoResamplerF(vac_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            wdsp.xresampleFV(vac_l, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            //Debug.WriteLine("Framecount: "+frameCount.ToString() + " Outsamps: "+outsamps.ToString());
                            if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                rb_vacOUT_r.WritePtr(res_outl_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                            else
                            {
                                vac_rb_reset = true;
                                VACDebug("rb_vacOUT_l overflow");
                            }
                        }
                    }
                }
            }

            // Scale output to SDR-1000
            if (!localmox)
            {
                ScaleBuffer(out_l1, out_l1, frameCount, (float)monitor_volume);
                ScaleBuffer(out_r1, out_r1, frameCount, (float)monitor_volume);
                ClearBuffer(out_l2, frameCount);
                ClearBuffer(out_r2, frameCount);
            }
            else
            {
                double tx_vol = TXScale;
                if (tx_output_signal != SignalSource.RADIO)
                    tx_vol = 1.0;

                ScaleBuffer(out_l2, out_l1, frameCount, (float)monitor_volume);
                ScaleBuffer(out_l2, out_l2, frameCount, (float)tx_vol);
                ScaleBuffer(out_r2, out_r1, frameCount, (float)monitor_volume);
                ScaleBuffer(out_r2, out_r2, frameCount, (float)tx_vol);
            }

            /*Debug.WriteLine("Max 1L: "+MaxSample(out_l1, frameCount).ToString("f5")+" 1R: "+MaxSample(out_r1, frameCount).ToString("f5")+
                " 2L: "+MaxSample(out_l2, frameCount).ToString("f5")+" 2R: "+MaxSample(out_r2, frameCount).ToString("f5"));*/

            if (!testing && soundcard != SoundCard.DELTA_44)
            {
                // clip radio output to prevent overdrive
                var clip_thresh = (float)(1.5f / audio_volts1);
                for (int i = 0; i < frameCount; i++)
                {
                    if (out_l2[i] > clip_thresh)
                    {
                        //Debug.WriteLine("Clip Left High: "+out_l2[i].ToString("f5"));
                        out_l2[i] = clip_thresh;
                    }
                    else if (out_l2[i] < -clip_thresh)
                    {
                        //Debug.WriteLine("Clip Left Low: "+out_l2[i].ToString("f5"));
                        out_l2[i] = -clip_thresh;
                    }

                    if (out_r2[i] > clip_thresh)
                    {
                        //Debug.WriteLine("Clip Right High: "+out_l2[i].ToString("f5"));
                        out_r2[i] = clip_thresh;
                    }
                    else if (out_r2[i] < -clip_thresh)
                    {
                        //Debug.WriteLine("Clip Right Low: "+out_l2[i].ToString("f5"));
                        out_r2[i] = -clip_thresh;
                    }

                    /*// Branchless clipping -- testing found this was more costly overall and especially when 
                     * when dealing with samples that mostly do not need clipping

                    float x1 = Math.Abs(out_l2[i]+clip_thresh);
                    float x2 = Math.Abs(out_l2[i]-clip_thresh);
                    x1 -= x2;
                    out_l2[i] = x1 * 0.5f;
                    x1 = Math.Abs(out_r2[i]+clip_thresh);
                    x2 = Math.Abs(out_r2[i]-clip_thresh);
                    x1 -= x2;
                    out_r2[i] = x1 * 0.5f;*/
                }

                if (audio_volts1 > 1.5f)
                {
                    // scale FireBox monitor output to prevent overdrive
                    ScaleBuffer(out_l1, out_l1, frameCount, (float)(1.5f / audio_volts1));
                    ScaleBuffer(out_r1, out_r1, frameCount, (float)(1.5f / audio_volts1));
                }
            }

#if(MINMAX)
			Debug.Write(MaxSample(out_l2, out_r2, frameCount).ToString("f6")+",");

			float current_max = MaxSample(out_l2, out_r2, frameCount);
			if(current_max > max) max = current_max;
			Debug.WriteLine(" max: "+max.ToString("f6"));
#endif

#if(TIMER)
			t1.Stop();
			Debug.WriteLine(t1.Duration);
#endif
            return callback_return;
        }

#if(MINMAX)
		private static float max = float.MinValue;
#endif
        //private static HiPerfTimer t2 = new HiPerfTimer();
        //private static double period = 0.0;

        unsafe public static int Callback2(void* input, void* output, int frameCount,
                                           PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
#if(TIMER)
			t1.Start();
#endif
            //t2.Start();
            float* in_l = null, in_r = null;
            float* out_l1 = null, out_r1 = null, out_l2 = null, out_r2 = null;
            float* out_l3 = null, out_r3 = null, out_l4 = null, out_r4 = null;
            float* rx1_in_l = null, rx1_in_r = null, tx_in_l = null, tx_in_r = null;
            float* rx2_in_l = null, rx2_in_r = null;
            float* rx1_out_l = null, rx1_out_r = null, tx_out_l = null, tx_out_r = null;
            float* rx2_out_l = null, rx2_out_r = null;
            localmox = mox;

            void* ex_input = input;
            void* ex_output = output;
            var array_ptr_input = (int**)input;
            var in_l_ptr1 = (float*)array_ptr_input[0];
            var in_r_ptr1 = (float*)array_ptr_input[1];
            var in_l_ptr2 = (float*)array_ptr_input[2];
            var in_r_ptr2 = (float*)array_ptr_input[3];
            var in_l_ptr3 = (float*)array_ptr_input[4];
            var in_r_ptr3 = (float*)array_ptr_input[5];
            var in_l_ptr4 = (float*)array_ptr_input[6];
            var in_r_ptr4 = (float*)array_ptr_input[7];
            var array_ptr_output = (int**)output;
            var out_l_ptr1 = (float*)array_ptr_output[0];
            var out_r_ptr1 = (float*)array_ptr_output[1];
            var out_l_ptr2 = (float*)array_ptr_output[2];
            var out_r_ptr2 = (float*)array_ptr_output[3];
            var out_l_ptr3 = (float*)array_ptr_output[4];
            var out_r_ptr3 = (float*)array_ptr_output[5];
            var out_l_ptr4 = (float*)array_ptr_output[6];
            var out_r_ptr4 = (float*)array_ptr_output[7];

            // arrange input buffers in the following order:
            // RX1 Left, RX1 Right, TX Left, TX Right, RX2 Left, RX2 Right
            //int* array_ptr = (int *)input;
            switch (in_rx1_l)
            {
                case 0:
                    array_ptr_input[0] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr_input[0] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr_input[0] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr_input[0] = (int*)in_r_ptr2;
                    break;
                case 4:
                    array_ptr_input[0] = (int*)in_l_ptr3;
                    break;
                case 5:
                    array_ptr_input[0] = (int*)in_r_ptr3;
                    break;
                case 6:
                    array_ptr_input[0] = (int*)in_l_ptr4;
                    break;
                case 7:
                    array_ptr_input[0] = (int*)in_r_ptr4;
                    break;
            }

            switch (in_rx1_r)
            {
                case 0:
                    array_ptr_input[1] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr_input[1] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr_input[1] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr_input[1] = (int*)in_r_ptr2;
                    break;
                case 4:
                    array_ptr_input[1] = (int*)in_l_ptr3;
                    break;
                case 5:
                    array_ptr_input[1] = (int*)in_r_ptr3;
                    break;
                case 6:
                    array_ptr_input[1] = (int*)in_l_ptr4;
                    break;
                case 7:
                    array_ptr_input[1] = (int*)in_r_ptr4;
                    break;
            }

            switch (in_tx_l)
            {
                case 0:
                    array_ptr_input[2] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr_input[2] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr_input[2] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr_input[2] = (int*)in_r_ptr2;
                    break;
                case 4:
                    array_ptr_input[2] = (int*)in_l_ptr3;
                    break;
                case 5:
                    array_ptr_input[2] = (int*)in_r_ptr3;
                    break;
                case 6:
                    array_ptr_input[2] = (int*)in_l_ptr4;
                    break;
                case 7:
                    array_ptr_input[2] = (int*)in_r_ptr4;
                    break;
            }

            switch (in_tx_r)
            {
                case 0:
                    array_ptr_input[3] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr_input[3] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr_input[3] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr_input[3] = (int*)in_r_ptr2;
                    break;
                case 4:
                    array_ptr_input[3] = (int*)in_l_ptr3;
                    break;
                case 5:
                    array_ptr_input[3] = (int*)in_r_ptr3;
                    break;
                case 6:
                    array_ptr_input[3] = (int*)in_l_ptr4;
                    break;
                case 7:
                    array_ptr_input[3] = (int*)in_r_ptr4;
                    break;
            }

            switch (in_rx2_l)
            {
                case 0:
                    array_ptr_input[4] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr_input[4] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr_input[4] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr_input[4] = (int*)in_r_ptr2;
                    break;
                case 4:
                    array_ptr_input[4] = (int*)in_l_ptr3;
                    break;
                case 5:
                    array_ptr_input[4] = (int*)in_r_ptr3;
                    break;
                case 6:
                    array_ptr_input[4] = (int*)in_l_ptr4;
                    break;
                case 7:
                    array_ptr_input[4] = (int*)in_r_ptr4;
                    break;
            }
            switch (in_rx2_r)
            {
                case 0:
                    array_ptr_input[5] = (int*)in_l_ptr1;
                    break;
                case 1:
                    array_ptr_input[5] = (int*)in_r_ptr1;
                    break;
                case 2:
                    array_ptr_input[5] = (int*)in_l_ptr2;
                    break;
                case 3:
                    array_ptr_input[5] = (int*)in_r_ptr2;
                    break;
                case 4:
                    array_ptr_input[5] = (int*)in_l_ptr3;
                    break;
                case 5:
                    array_ptr_input[5] = (int*)in_r_ptr3;
                    break;
                case 6:
                    array_ptr_input[5] = (int*)in_l_ptr4;
                    break;
                case 7:
                    array_ptr_input[5] = (int*)in_r_ptr4;
                    break;
            }

            rx1_in_l = (float*)array_ptr_input[0];
            rx1_in_r = (float*)array_ptr_input[1];
            tx_in_l = (float*)array_ptr_input[2];
            tx_in_r = (float*)array_ptr_input[3];
            rx2_in_l = (float*)array_ptr_input[4];
            rx2_in_r = (float*)array_ptr_input[5];

            rx1_out_l = (float*)array_ptr_output[0];
            rx1_out_r = (float*)array_ptr_output[1];
            tx_out_l = (float*)array_ptr_output[2];
            tx_out_r = (float*)array_ptr_output[3];
            rx2_out_l = (float*)array_ptr_output[4];
            rx2_out_r = (float*)array_ptr_output[5];

            if (!localmox)
            {
                in_l = rx1_in_l;
                in_r = rx1_in_r;
            }
            else
            {
                in_l = tx_in_l;
                in_r = tx_in_r;
            }

            float sum = SumBuffer(rx1_in_l, frameCount);
            if (sum == 0.0f)
                empty_buffers++;
            else empty_buffers = 0;

#if true
            // EHR RX2 QSK
            if (localmox && rx2_enabled && rx2_auto_mute_tx)
            {
                ClearBuffer(rx2_in_l, frameCount);
                ClearBuffer(rx2_in_r, frameCount);
            }
#endif

            if (wave_playback)
            {
                wave_file_reader.GetPlayBuffer(in_l, in_r);
                if (rx2_enabled)
                {
                    if (wave_file_reader2 != null)
                    {
                        wave_file_reader2.GetPlayBuffer(rx2_in_l, rx2_in_r);
                    }
                    else if (!localmox)
                    {
                        CopyBuffer(in_l, rx2_in_l, frameCount);
                        CopyBuffer(in_r, rx2_in_r, frameCount);
                    }
                }
            }

            if (wave_record)
            {
                if (!localmox)
                {
                    if (record_rx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(rx1_in_l, rx1_in_r);
                        if (wave_file_writer2 != null)
                            wave_file_writer2.AddWriteBuffer(rx2_in_l, rx2_in_r);
                    }
                }
                else
                {
                    if (record_tx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(tx_in_l, tx_in_r);
                    }
                }
            }

            if (phase)
            {
                //phase_mutex.WaitOne();
                Marshal.Copy(new IntPtr(in_l), phase_buf_l, 0, frameCount);
                Marshal.Copy(new IntPtr(in_r), phase_buf_r, 0, frameCount);
                //phase_mutex.ReleaseMutex();
            }

            // handle VAC Input
            if (vac_enabled &&
                rb_vacOUT_l != null && rb_vacOUT_r != null)
            {
                if (vac_bypass || !localmox) // drain VAC Input ring buffer
                {
                    if ((rb_vacIN_l.ReadSpace() >= frameCount) && (rb_vacIN_r.ReadSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacIN_l.ReadPtr(out_l_ptr2, frameCount);
                        rb_vacIN_r.ReadPtr(out_r_ptr2, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                }
                else
                {
                    if (rb_vacIN_l.ReadSpace() >= frameCount)
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacIN_l.ReadPtr(tx_in_l, frameCount);
                        rb_vacIN_r.ReadPtr(tx_in_r, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                        if (vac_combine_input)
                            AddBuffer(tx_in_l, tx_in_r, frameCount);
                    }
                    else
                    {
                        ClearBuffer(tx_in_l, frameCount);
                        ClearBuffer(tx_in_r, frameCount);
                        VACDebug("rb_vacIN underflow 4inTX");
                    }
                    ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac_preamp);
                    ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac_preamp);
                }
            }

            switch (current_audio_state1)
            {
                case AudioState.DTTSP:

                    #region VOX

                    if (vox_enabled)
                    {
                        if (tx_dsp_mode == DSPMode.LSB ||
                            tx_dsp_mode == DSPMode.USB ||
                            tx_dsp_mode == DSPMode.DSB ||
                            tx_dsp_mode == DSPMode.AM ||
                            tx_dsp_mode == DSPMode.SAM ||
                            tx_dsp_mode == DSPMode.FM ||
                            tx_dsp_mode == DSPMode.DIGL ||
                            tx_dsp_mode == DSPMode.DIGU)
                        {
                            peak = MaxSample(tx_in_l, tx_in_r, frameCount);

                            // compare power to threshold
                            vox_active = peak > vox_threshold;
                        }
                    }

                    #endregion

                    // scale input with mic preamp
                    if ((!vac_enabled &&
                         (tx_dsp_mode == DSPMode.LSB ||
                          tx_dsp_mode == DSPMode.USB ||
                          tx_dsp_mode == DSPMode.DSB ||
                          tx_dsp_mode == DSPMode.AM ||
                          tx_dsp_mode == DSPMode.SAM ||
                          tx_dsp_mode == DSPMode.FM ||
                          tx_dsp_mode == DSPMode.DIGL ||
                          tx_dsp_mode == DSPMode.DIGU)) ||
                        (vac_enabled && vac_bypass &&
                         (tx_dsp_mode == DSPMode.DIGL ||
                          tx_dsp_mode == DSPMode.DIGU ||
                          tx_dsp_mode == DSPMode.LSB ||
                          tx_dsp_mode == DSPMode.USB ||
                          tx_dsp_mode == DSPMode.DSB ||
                          tx_dsp_mode == DSPMode.AM ||
                          tx_dsp_mode == DSPMode.SAM ||
                          tx_dsp_mode == DSPMode.FM)))
                    {
                        if (wave_playback)
                        {
                            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)wave_preamp);
                            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)wave_preamp);
                        }
                        else
                        {
                            if (!vac_enabled && (tx_dsp_mode == DSPMode.DIGL || tx_dsp_mode == DSPMode.DIGU))
                            {
                                ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)vac_preamp);
                                ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)vac_preamp);
                            }
                            else
                            {
                                ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)mic_preamp);
                                ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)mic_preamp);
                            }
                        }
                    }

                    #region Input Signal Source

                    switch (rx1_input_signal)
                    {
                        case SignalSource.RADIO:
                            break;
                        case SignalSource.SINE:
                            SineWave(rx1_in_l, frameCount, phase_accumulator1, sine_freq1);
                            phase_accumulator1 = CosineWave(rx1_in_r, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(rx1_in_l, rx1_in_l, frameCount, (float)source_scale);
                            ScaleBuffer(rx1_in_r, rx1_in_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.SINE_TWO_TONE:
                            double dump;
                            SineWave2Tone(rx1_in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                          sine_freq1, sine_freq2, out dump, out dump);
                            CosineWave2Tone(rx1_in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                            sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                            ScaleBuffer(rx1_in_l, rx1_in_l, frameCount, (float)source_scale);
                            ScaleBuffer(rx1_in_r, rx1_in_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.SINE_LEFT_ONLY:
                            phase_accumulator1 = SineWave(rx1_in_l, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(rx1_in_l, rx1_in_l, frameCount, (float)source_scale);
                            ClearBuffer(rx1_in_r, frameCount);
                            break;
                        case SignalSource.SINE_RIGHT_ONLY:
                            phase_accumulator1 = SineWave(rx1_in_r, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(rx1_in_r, rx1_in_r, frameCount, (float)source_scale);
                            ClearBuffer(rx1_in_l, frameCount);
                            break;
                        case SignalSource.NOISE:
                            Noise(rx1_in_l, frameCount);
                            Noise(rx1_in_r, frameCount);
                            break;
                        case SignalSource.TRIANGLE:
                            Triangle(rx1_in_l, frameCount, sine_freq1);
                            CopyBuffer(rx1_in_l, rx1_in_r, frameCount);
                            break;
                        case SignalSource.SAWTOOTH:
                            Sawtooth(rx1_in_l, frameCount, sine_freq1);
                            CopyBuffer(rx1_in_l, rx1_in_r, frameCount);
                            break;
                        case SignalSource.SILENCE:
                            ClearBuffer(rx1_in_l, frameCount);
                            ClearBuffer(rx1_in_r, frameCount);
                            break;
                    }

                    if (rx2_enabled)
                    {
                        switch (rx2_input_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(rx2_in_l, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(rx2_in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(rx2_in_l, rx2_in_l, frameCount, (float)source_scale);
                                ScaleBuffer(rx2_in_r, rx2_in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(rx2_in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(rx2_in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(rx2_in_l, rx2_in_l, frameCount, (float)source_scale);
                                ScaleBuffer(rx2_in_r, rx2_in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_LEFT_ONLY:
                                phase_accumulator1 = SineWave(rx2_in_l, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(rx2_in_l, rx2_in_l, frameCount, (float)source_scale);
                                ClearBuffer(rx2_in_r, frameCount);
                                break;
                            case SignalSource.SINE_RIGHT_ONLY:
                                phase_accumulator1 = SineWave(rx2_in_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(rx2_in_r, rx2_in_r, frameCount, (float)source_scale);
                                ClearBuffer(rx2_in_l, frameCount);
                                break;
                            case SignalSource.NOISE:
                                Noise(rx2_in_l, frameCount);
                                Noise(rx2_in_r, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(rx2_in_l, frameCount, sine_freq1);
                                CopyBuffer(rx2_in_l, rx2_in_r, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(rx2_in_l, frameCount, sine_freq1);
                                CopyBuffer(rx2_in_l, rx2_in_r, frameCount);
                                break;
                            case SignalSource.PULSE:
                                Pulse(rx2_in_l, frameCount);
                                CopyBuffer(rx2_in_l, rx2_in_r, frameCount);
                                ScaleBuffer(rx2_in_l, rx2_in_l, frameCount, (float)source_scale);
                                ScaleBuffer(rx2_in_r, rx2_in_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(rx2_in_l, frameCount);
                                ClearBuffer(rx2_in_r, frameCount);
                                break;
                        }
                    }

                    switch (tx_input_signal)
                    {
                        case SignalSource.RADIO:
                            break;
                        case SignalSource.SINE:
                            SineWave(tx_in_l, frameCount, phase_accumulator1, sine_freq1);
                            phase_accumulator1 = CosineWave(tx_in_r, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)source_scale);
                            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.SINE_TWO_TONE:
                            double dump;
                            SineWave2Tone(tx_in_l, frameCount, phase_accumulator1, phase_accumulator2,
                                          sine_freq1, sine_freq2, out dump, out dump);
                            CosineWave2Tone(tx_in_r, frameCount, phase_accumulator1, phase_accumulator2,
                                            sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                            ScaleBuffer(tx_in_l, tx_in_l, frameCount, (float)source_scale);
                            ScaleBuffer(tx_in_r, tx_in_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.NOISE:
                            Noise(tx_in_l, frameCount);
                            Noise(tx_in_r, frameCount);
                            break;
                        case SignalSource.TRIANGLE:
                            Triangle(tx_in_l, frameCount, sine_freq1);
                            CopyBuffer(tx_in_l, tx_in_r, frameCount);
                            break;
                        case SignalSource.SAWTOOTH:
                            Sawtooth(tx_in_l, frameCount, sine_freq1);
                            CopyBuffer(tx_in_l, tx_in_r, frameCount);
                            break;
                        case SignalSource.SILENCE:
                            ClearBuffer(tx_in_l, frameCount);
                            ClearBuffer(tx_in_r, frameCount);
                            break;
                    }

                    #endregion

#if(MINMAX)
    /*float local_max = MaxSample(in_l, in_r, frameCount);
					if(local_max > max)
					{
						max = local_max;
						Debug.WriteLine("max in: "+max.ToString("f6"));
					}*/

					Debug.Write(MaxSample(in_l, in_r, frameCount).ToString("f6")+",");
#endif

                    if (vac_enabled && vac_output_iq &&
                        rb_vacOUT_l != null && rb_vacOUT_r != null &&
                        rx1_in_l != null && rx1_in_r != null)
                    {
                        if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
                        {
                            if (vac_correct_iq)
                                fixed (float* res_outl_ptr = &(res_outl[0]))
                                fixed (float* res_outr_ptr = &(res_outr[0]))
                                {
                                    if (vac_output_rx2)
                                        CorrectIQBuffer(rx2_in_l, rx2_in_r, res_outl_ptr, res_outr_ptr, frameCount);
                                    else
                                        CorrectIQBuffer(rx1_in_l, rx1_in_r, res_outl_ptr, res_outr_ptr, frameCount);

                                    Win32.EnterCriticalSection(cs_vac);
                                    rb_vacOUT_l.WritePtr(res_outr_ptr, frameCount);
                                    rb_vacOUT_r.WritePtr(res_outl_ptr, frameCount);
                                    Win32.LeaveCriticalSection(cs_vac);
                                }
                            else
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                if (vac_output_rx2)
                                {
                                    rb_vacOUT_l.WritePtr(rx2_in_r, frameCount);
                                    rb_vacOUT_r.WritePtr(rx2_in_l, frameCount);
                                }
                                else
                                {
                                    rb_vacOUT_l.WritePtr(rx1_in_r, frameCount);
                                    rb_vacOUT_r.WritePtr(rx1_in_l, frameCount);
                                }
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                        }
                        else
                        {
                            VACDebug("rb_vacOUT_l I/Q overflow ");
                            vac_rb_reset = true;
                        }
                    }

                    //DttSP.ExchangeSamples2(ex_input, ex_output, frameCount);
                    dspExchange(ex_input, ex_output, frameCount);

                    if (tx_dsp_mode == DSPMode.CWU || tx_dsp_mode == DSPMode.CWL)
                    {
                        //t2.Stop();
                        //period = t2.DurationMsec;
                        // DttSP.CWtoneExchange(out_l_ptr2, out_r_ptr2, frameCount);
                        //t2.Start();
                    }

#if(MINMAX)
					Debug.Write(MaxSample(out_l_ptr2, frameCount).ToString("f6")+",");
					Debug.Write(MaxSample(out_r_ptr2, frameCount).ToString("f6")+"\n");
#endif

                    #region Output Signal Source

                    switch (rx1_output_signal)
                    {
                        case SignalSource.RADIO:
                            break;
                        case SignalSource.SINE:
                            SineWave(rx1_out_l, frameCount, phase_accumulator1, sine_freq1);
                            phase_accumulator1 = CosineWave(rx1_out_r, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(rx1_out_l, rx1_out_l, frameCount, (float)source_scale);
                            ScaleBuffer(rx1_out_r, rx1_out_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.SINE_TWO_TONE:
                            double dump;
                            SineWave2Tone(rx1_out_l, frameCount, phase_accumulator1, phase_accumulator2,
                                          sine_freq1, sine_freq2, out dump, out dump);
                            CosineWave2Tone(rx1_out_r, frameCount, phase_accumulator1, phase_accumulator2,
                                            sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                            ScaleBuffer(rx1_out_l, rx1_out_l, frameCount, (float)source_scale);
                            ScaleBuffer(rx1_out_r, rx1_out_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.SINE_LEFT_ONLY:
                            phase_accumulator1 = SineWave(rx1_out_l, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(rx1_out_l, rx1_out_l, frameCount, (float)source_scale);
                            ClearBuffer(rx1_out_r, frameCount);
                            break;
                        case SignalSource.SINE_RIGHT_ONLY:
                            phase_accumulator1 = SineWave(rx1_out_r, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(rx1_out_r, rx1_out_r, frameCount, (float)source_scale);
                            ClearBuffer(rx1_out_l, frameCount);
                            break;
                        case SignalSource.NOISE:
                            Noise(rx1_out_l, frameCount);
                            Noise(rx1_out_r, frameCount);
                            break;
                        case SignalSource.TRIANGLE:
                            Triangle(rx1_out_l, frameCount, sine_freq1);
                            CopyBuffer(rx1_out_l, rx1_out_r, frameCount);
                            break;
                        case SignalSource.SAWTOOTH:
                            Sawtooth(rx1_out_l, frameCount, sine_freq1);
                            CopyBuffer(rx1_out_l, rx1_out_r, frameCount);
                            break;
                        case SignalSource.SILENCE:
                            ClearBuffer(rx1_out_l, frameCount);
                            ClearBuffer(rx1_out_r, frameCount);
                            break;
                    }

                    if (rx2_enabled)
                    {
                        switch (rx2_output_signal)
                        {
                            case SignalSource.RADIO:
                                break;
                            case SignalSource.SINE:
                                SineWave(rx2_out_l, frameCount, phase_accumulator1, sine_freq1);
                                phase_accumulator1 = CosineWave(rx2_out_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(rx2_out_l, rx2_out_l, frameCount, (float)source_scale);
                                ScaleBuffer(rx2_out_r, rx2_out_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_TWO_TONE:
                                double dump;
                                SineWave2Tone(rx2_out_l, frameCount, phase_accumulator1, phase_accumulator2,
                                              sine_freq1, sine_freq2, out dump, out dump);
                                CosineWave2Tone(rx2_out_r, frameCount, phase_accumulator1, phase_accumulator2,
                                                sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                                ScaleBuffer(rx2_out_l, rx2_out_l, frameCount, (float)source_scale);
                                ScaleBuffer(rx2_out_r, rx2_out_r, frameCount, (float)source_scale);
                                break;
                            case SignalSource.SINE_LEFT_ONLY:
                                phase_accumulator1 = SineWave(rx2_out_l, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(rx2_out_l, rx2_out_l, frameCount, (float)source_scale);
                                ClearBuffer(rx2_out_r, frameCount);
                                break;
                            case SignalSource.SINE_RIGHT_ONLY:
                                phase_accumulator1 = SineWave(rx2_out_r, frameCount, phase_accumulator1, sine_freq1);
                                ScaleBuffer(rx2_out_r, rx2_out_r, frameCount, (float)source_scale);
                                ClearBuffer(rx2_out_l, frameCount);
                                break;
                            case SignalSource.NOISE:
                                Noise(rx2_out_l, frameCount);
                                Noise(rx2_out_r, frameCount);
                                break;
                            case SignalSource.TRIANGLE:
                                Triangle(rx2_out_l, frameCount, sine_freq1);
                                CopyBuffer(rx2_out_l, rx2_out_r, frameCount);
                                break;
                            case SignalSource.SAWTOOTH:
                                Sawtooth(rx2_out_l, frameCount, sine_freq1);
                                CopyBuffer(rx2_out_l, rx2_out_r, frameCount);
                                break;
                            case SignalSource.SILENCE:
                                ClearBuffer(rx2_out_l, frameCount);
                                ClearBuffer(rx2_out_r, frameCount);
                                break;
                        }
                    }

                    switch (tx_output_signal)
                    {
                        case SignalSource.RADIO:
                            break;
                        case SignalSource.SINE:
                            SineWave(tx_out_l, frameCount, phase_accumulator1, sine_freq1);
                            phase_accumulator1 = CosineWave(tx_out_r, frameCount, phase_accumulator1, sine_freq1);
                            ScaleBuffer(tx_out_l, tx_out_l, frameCount, (float)source_scale);
                            ScaleBuffer(tx_out_r, tx_out_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.SINE_TWO_TONE:
                            double dump;
                            SineWave2Tone(tx_out_l, frameCount, phase_accumulator1, phase_accumulator2,
                                          sine_freq1, sine_freq2, out dump, out dump);
                            CosineWave2Tone(tx_out_r, frameCount, phase_accumulator1, phase_accumulator2,
                                            sine_freq1, sine_freq2, out phase_accumulator1, out phase_accumulator2);
                            ScaleBuffer(tx_out_l, tx_out_l, frameCount, (float)source_scale);
                            ScaleBuffer(tx_out_r, tx_out_r, frameCount, (float)source_scale);
                            break;
                        case SignalSource.NOISE:
                            Noise(tx_out_l, frameCount);
                            Noise(tx_out_r, frameCount);
                            break;
                        case SignalSource.TRIANGLE:
                            Triangle(tx_out_l, frameCount, sine_freq1);
                            CopyBuffer(tx_out_l, tx_out_r, frameCount);
                            break;
                        case SignalSource.SAWTOOTH:
                            Sawtooth(tx_out_l, frameCount, sine_freq1);
                            CopyBuffer(tx_out_l, tx_out_r, frameCount);
                            break;
                        case SignalSource.SILENCE:
                            ClearBuffer(tx_out_l, frameCount);
                            ClearBuffer(tx_out_r, frameCount);
                            break;
                    }

                    #endregion

                    break;
                case AudioState.CW:
                    //DttSP.ExchangeSamples2(array_ptr_input, array_ptr_output, frameCount);
                    //DttSP.ExchangeSamples2(ex_input, ex_output, frameCount);

                    //t2.Stop();
                    //period = t2.DurationMsec;
                    // DttSP.CWtoneExchange(out_l_ptr2, out_r_ptr2, frameCount);
                    //t2.Start();
                    break;
            }

            DoScope(!localmox ? out_l_ptr1 : out_l_ptr2, frameCount);

            if (wave_record)
            {
                if (!localmox)
                {
                    if (!record_rx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(out_l_ptr1, out_r_ptr1);
                    }
                }
                else
                {
                    if (!record_tx_preprocessed)
                    {
                        wave_file_writer.AddWriteBuffer(out_l_ptr2, out_r_ptr2);
                    }
                }
            }

            out_l1 = rx1_out_l;
            out_r1 = rx1_out_r;
            out_l2 = out_l_ptr2;
            out_r2 = out_r_ptr2;
            out_l3 = out_l_ptr3;
            out_r3 = out_r_ptr3;
            out_l4 = out_l_ptr4;
            out_r4 = out_r_ptr4;

            // scale output for VAC -- use chan 4 as spare buffer
            if (vac_enabled && !vac_output_iq &&
                rb_vacIN_l != null && rb_vacIN_r != null &&
                rb_vacOUT_l != null && rb_vacOUT_r != null)
            {
                if (!localmox)
                {
                    ScaleBuffer(out_l1, out_l4, frameCount, (float)vac_rx_scale);
                    ScaleBuffer(out_r1, out_r4, frameCount, (float)vac_rx_scale);
                }
                else if (mon)
                {
                    ScaleBuffer(out_l2, out_l4, frameCount, (float)vac_rx_scale);
                    ScaleBuffer(out_r2, out_r4, frameCount, (float)vac_rx_scale);
                }
                else // zero samples going back to VAC since TX monitor is off
                {
                    ScaleBuffer(out_l2, out_l4, frameCount, 0.0f);
                    ScaleBuffer(out_r2, out_r4, frameCount, 0.0f);
                }

                if (sample_rate2 == sample_rate1)
                {
                    if ((rb_vacOUT_l.WriteSpace() >= frameCount) && (rb_vacOUT_r.WriteSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacOUT_l.WritePtr(out_l4, frameCount);
                        rb_vacOUT_r.WritePtr(out_r4, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                    else
                    {
                        VACDebug("rb_vacOUT_l overflow ");
                        vac_rb_reset = true;
                    }
                }
                else
                {
                    if (vac_stereo)
                    {
                        fixed (float* res_outl_ptr = &(res_outl[0]))
                        fixed (float* res_outr_ptr = &(res_outr[0]))
                        {
                            int outsamps = 0;
                            //DttSP.DoResamplerF(out_l4, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            wdsp.xresampleFV(out_l4, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            //DttSP.DoResamplerF(out_r4, res_outr_ptr, frameCount, &outsamps, resampPtrOut_r);
                            wdsp.xresampleFV(out_r4, res_outr_ptr, frameCount, &outsamps, resampPtrOut_r);
                            if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                rb_vacOUT_r.WritePtr(res_outr_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                            else
                            {
                                vac_rb_reset = true;
                                VACDebug("rb_vacOUT_l overflow");
                            }
                        }
                    }
                    else
                    {
                        fixed (float* res_outl_ptr = &(res_outl[0]))
                        {
                            int outsamps = 0;
                            //DttSP.DoResamplerF(out_l4, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            wdsp.xresampleFV(out_l4, res_outl_ptr, frameCount, &outsamps, resampPtrOut_l);
                            if ((rb_vacOUT_l.WriteSpace() >= outsamps) && (rb_vacOUT_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac);
                                rb_vacOUT_l.WritePtr(res_outl_ptr, outsamps);
                                rb_vacOUT_r.WritePtr(res_outl_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac);
                            }
                            else
                            {
                                vac_rb_reset = true;
                                VACDebug("rb_vacOUT_l overflow");
                            }
                        }
                    }
                }
            }

            double tx_vol = FWCTXScale;
            if (tx_output_signal != SignalSource.RADIO)
                tx_vol = 1.0;
            // Scale output to FLEX-5000
            if (full_duplex)
            {
                if (mon)
                {
                    ScaleBuffer(out_l2, out_l1, frameCount, (float)tx_vol);
                    ScaleBuffer(out_l2, out_l2, frameCount, (float)monitor_volume);
                    CopyBuffer(out_l2, out_l3, frameCount);
                    CopyBuffer(out_l2, out_l4, frameCount);
                    ScaleBuffer(out_r2, out_r1, frameCount, (float)tx_vol);
                    ScaleBuffer(out_r2, out_r2, frameCount, (float)monitor_volume);
                    CopyBuffer(out_r2, out_r3, frameCount);
                    CopyBuffer(out_r2, out_r4, frameCount);
                }
                else
                {
                    ScaleBuffer(out_l1, out_r3, frameCount, (float)monitor_volume);
                    ScaleBuffer(out_r1, out_l3, frameCount, (float)monitor_volume);
                    ScaleBuffer(out_l2, out_l1, frameCount, (float)tx_vol);
                    ScaleBuffer(out_r2, out_r1, frameCount, (float)tx_vol);
                    CopyBuffer(out_l3, out_l2, frameCount);
                    CopyBuffer(out_r3, out_r2, frameCount);
                    CopyBuffer(out_l3, out_l4, frameCount);
                    CopyBuffer(out_r3, out_r4, frameCount);
                }
            }
            else if (!localmox)
            {
                if (rx2_enabled)
                {
                    AddBuffer(out_l1, out_l3, frameCount);
                    AddBuffer(out_r1, out_r3, frameCount);
                }
                ScaleBuffer(out_l1, out_r3, frameCount, (float)monitor_volume);
                ScaleBuffer(out_r1, out_l3, frameCount, (float)monitor_volume);
                ScaleBuffer(out_l2, out_l1, frameCount, (float)tx_vol);
                ScaleBuffer(out_r2, out_r1, frameCount, (float)tx_vol);
                CopyBuffer(out_l3, out_l2, frameCount);
                CopyBuffer(out_r3, out_r2, frameCount);
                CopyBuffer(out_l3, out_l4, frameCount);
                CopyBuffer(out_r3, out_r4, frameCount);
                //ClearBuffer(out_l1, frameCount);
                //ClearBuffer(out_r1, frameCount);
            }
            else
            {
                if (high_pwr_am)
                {
                    if (tx_dsp_mode == DSPMode.AM ||
                        tx_dsp_mode == DSPMode.SAM)
                        tx_vol *= 1.414;
                }

                ScaleBuffer(out_l2, out_l1, frameCount, (float)tx_vol);
                if (mon)
                {
                    ScaleBuffer(out_l2, out_l2, frameCount, (float)monitor_volume);
                    CopyBuffer(out_l2, out_l3, frameCount);
                    CopyBuffer(out_l2, out_l4, frameCount);
                }
                else
                {
                    if (rx2_enabled && !rx2_auto_mute_tx)
                    {
                        ScaleBuffer(out_l3, out_l2, frameCount, (float)monitor_volume);
                        CopyBuffer(out_l2, out_l3, frameCount);
                        CopyBuffer(out_l2, out_l4, frameCount);
                    }
                    else
                    {
                        ClearBuffer(out_l2, frameCount);
                        ClearBuffer(out_l3, frameCount);
                        ClearBuffer(out_l4, frameCount);
                    }
                }

                ScaleBuffer(out_r2, out_r1, frameCount, (float)tx_vol);
                if (mon)
                {
                    ScaleBuffer(out_r2, out_r2, frameCount, (float)monitor_volume);
                    CopyBuffer(out_r2, out_r3, frameCount);
                    CopyBuffer(out_r2, out_r4, frameCount);
                }
                else
                {
                    if (rx2_enabled && !rx2_auto_mute_tx)
                    {
                        ScaleBuffer(out_r3, out_r2, frameCount, (float)monitor_volume);
                        CopyBuffer(out_r2, out_r3, frameCount);
                        CopyBuffer(out_r2, out_r4, frameCount);
                    }
                    else
                    {
                        ClearBuffer(out_r2, frameCount);
                        ClearBuffer(out_r3, frameCount);
                        ClearBuffer(out_r4, frameCount);
                    }
                }
            }

#if(MINMAX)
            /*Debug.Write(MaxSample(out_l2, out_r2, frameCount).ToString("f6")+",");

			float current_max = MaxSample(out_l2, out_r2, frameCount);
			if(current_max > max) max = current_max;
			Debug.WriteLine(" max: "+max.ToString("f6"));*/
#endif

            //Debug.WriteLine(MaxSample(out_l2, out_r2, frameCount).ToString("f6"));
            //if(period > 8) Debug.
            //    WriteLine("period: " + period.ToString("f2"));
#if(TIMER)
			t1.Stop();
			Debug.WriteLine(t1.Duration);
#endif
            /*t2.Stop();
            period = t2.DurationMsec;
            if(period > 1.0 || statusFlags != 0)
                Debug.WriteLine("flags: " + statusFlags.ToString("X") + "  period: " + period.ToString("f2"));*/

            return callback_return;
        }

        // The VAC callback from 1.8.0 patched for rmatch/varsamp
        unsafe public static int CallbackVAC(void* input, void* output, int frameCount,
            PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            if (!vac_enabled) return 0;

            int** array_ptr = (int**)input;
            float* in_l_ptr1 = (float*)array_ptr[0];
            float* in_r_ptr1 = null;
            if (vac_stereo || vac_output_iq) in_r_ptr1 = (float*)array_ptr[1];
            array_ptr = (int**)output;
            float* out_l_ptr1 = (float*)array_ptr[0];
            float* out_r_ptr1 = null;
            if (vac_stereo || vac_output_iq) out_r_ptr1 = (float*)array_ptr[1];

            if (varsampEnabledVAC1)
            {
                if (vac_stereo || vac_output_iq)
                {
                    fixed (double* resampBufPtr = &(resampBufVac1InWrite[0]))
                    {
                        Swizzle(resampBufPtr, in_l_ptr1, in_r_ptr1, frameCount);
                        wdsp.xrmatchIN(rmatchVac1In, resampBufPtr);
                    }

                    fixed (double* resampBufPtr = &(resampBufVac1OutRead[0]))
                    {
                        wdsp.xrmatchOUT(rmatchVac1Out, resampBufPtr);
                        Deswizzle(out_l_ptr1, out_r_ptr1, resampBufPtr, frameCount);
                    }
                }
                else
                {
                    fixed (double* resampBufPtr = &(resampBufVac1InWrite[0]))
                    {
                        Swizzle(resampBufPtr, in_l_ptr1, in_l_ptr1, frameCount);
                        wdsp.xrmatchIN(rmatchVac1In, resampBufPtr);
                    }

                    fixed (double* resampBufPtr = &(resampBufVac1OutRead[0]))
                    {
                        wdsp.xrmatchOUT(rmatchVac1Out, resampBufPtr);
                        Deswizzle(out_l_ptr1, out_l_ptr1, resampBufPtr, frameCount);
                    }
                }
            }
            else
            {
                if (vac_rb_reset)
                {
                    vac_rb_reset = false;
                    ClearBuffer(out_l_ptr1, frameCount);
                    if (vac_stereo || vac_output_iq) ClearBuffer(out_r_ptr1, frameCount);
                    Win32.EnterCriticalSection(cs_vacw);
                    rb_vacIN_l.Reset();
                    rb_vacIN_r.Reset();
                    Win32.LeaveCriticalSection(cs_vacw);
                    Win32.EnterCriticalSection(cs_vac);
                    rb_vacOUT_l.Reset();
                    rb_vacOUT_r.Reset();
                    Win32.LeaveCriticalSection(cs_vac);
                    return 0;
                }
                if (vac_stereo || vac_output_iq)
                {
                    if (vac_resample)
                    {
                        int outsamps = 0;
                        fixed (float* res_inl_ptr = &(res_inl[0]))
                        fixed (float* res_inr_ptr = &(res_inr[0]))
                        {
                            //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
                            wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
                            //DttSP.DoResamplerF(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampPtrIn_r);
                            wdsp.xresampleFV(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampPtrIn_r);
                            if ((rb_vacIN_l.WriteSpace() >= outsamps) && (rb_vacIN_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vacw);
                                rb_vacIN_l.WritePtr(res_inl_ptr, outsamps);
                                rb_vacIN_r.WritePtr(res_inr_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vacw);
                            }
                            else
                            {
                                vac_rb_reset = true;
                                VACDebug("rb_vacIN overflow stereo CBvac");
                            }
                        }
                    }
                    else
                    {
                        if ((rb_vacIN_l.WriteSpace() >= frameCount) && (rb_vacIN_r.WriteSpace() >= frameCount))
                        {
                            Win32.EnterCriticalSection(cs_vacw);
                            rb_vacIN_l.WritePtr(in_l_ptr1, frameCount);
                            rb_vacIN_r.WritePtr(in_r_ptr1, frameCount);
                            Win32.LeaveCriticalSection(cs_vacw);
                        }
                        else
                        {
                            //vac_rb_reset = true;
                            VACDebug("rb_vacIN overflow mono CBvac");
                        }
                    }

                    if ((rb_vacOUT_l.ReadSpace() >= frameCount) && (rb_vacOUT_r.ReadSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacOUT_l.ReadPtr(out_l_ptr1, frameCount);
                        rb_vacOUT_r.ReadPtr(out_r_ptr1, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                    else
                    {
                        ClearBuffer(out_l_ptr1, frameCount);
                        ClearBuffer(out_r_ptr1, frameCount);
                        VACDebug("rb_vacOUT underflow");
                    }
                }
                else
                {
                    if (vac_resample)
                    {
                        int outsamps = 0;
                        fixed (float* res_inl_ptr = &(res_inl[0]))
                        {
                            //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
                            wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
                            if ((rb_vacIN_l.WriteSpace() >= outsamps) && (rb_vacIN_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vacw);
                                rb_vacIN_l.WritePtr(res_inl_ptr, outsamps);
                                rb_vacIN_r.WritePtr(res_inl_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vacw);
                            }
                            else
                            {
                                //vac_rb_reset = true;
                                VACDebug("rb_vacIN_l overflow");
                            }
                        }
                    }
                    else
                    {
                        if ((rb_vacIN_l.WriteSpace() >= frameCount) && (rb_vacIN_r.WriteSpace() >= frameCount))
                        {
                            Win32.EnterCriticalSection(cs_vacw);
                            rb_vacIN_l.WritePtr(in_l_ptr1, frameCount);
                            rb_vacIN_r.WritePtr(in_l_ptr1, frameCount);
                            Win32.LeaveCriticalSection(cs_vacw);
                        }
                        else
                        {
                            //vac_rb_reset = true;
                            VACDebug("rb_vacIN_l overflow");
                        }
                    }
                    if ((rb_vacOUT_l.ReadSpace() >= frameCount) && (rb_vacOUT_r.ReadSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac);
                        rb_vacOUT_l.ReadPtr(out_l_ptr1, frameCount);
                        rb_vacOUT_r.ReadPtr(out_l_ptr1, frameCount);
                        Win32.LeaveCriticalSection(cs_vac);
                    }
                    else
                    {
                        ClearBuffer(out_l_ptr1, frameCount);
                        VACDebug("rb_vacOUT_l underflow");
                    }
                }
            }


            return 0;
        }

        //// The VAC callback from 1.8.0 untouched in any way.
        //unsafe public static int CallbackVAC(void* input, void* output, int frameCount,
        //    PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        //{
        //    if (!vac_enabled) return 0;

        //    int** array_ptr = (int**)input;
        //    float* in_l_ptr1 = (float*)array_ptr[0];
        //    float* in_r_ptr1 = null;
        //    if (vac_stereo || vac_output_iq) in_r_ptr1 = (float*)array_ptr[1];
        //    array_ptr = (int**)output;
        //    float* out_l_ptr1 = (float*)array_ptr[0];
        //    float* out_r_ptr1 = null;
        //    if (vac_stereo || vac_output_iq) out_r_ptr1 = (float*)array_ptr[1];

        //    if (vac_rb_reset)
        //    {
        //        vac_rb_reset = false;
        //        ClearBuffer(out_l_ptr1, frameCount);
        //        if (vac_stereo || vac_output_iq) ClearBuffer(out_r_ptr1, frameCount);
        //        Win32.EnterCriticalSection(cs_vacw);
        //        rb_vacIN_l.Reset();
        //        rb_vacIN_r.Reset();
        //        Win32.LeaveCriticalSection(cs_vacw);
        //        Win32.EnterCriticalSection(cs_vac);
        //        rb_vacOUT_l.Reset();
        //        rb_vacOUT_r.Reset();
        //        Win32.LeaveCriticalSection(cs_vac);
        //        return 0;
        //    }
        //    if (vac_stereo || vac_output_iq)
        //    {
        //        if (vac_resample)
        //        {
        //            int outsamps = 0;
        //            fixed (float* res_inl_ptr = &(res_inl[0]))
        //            fixed (float* res_inr_ptr = &(res_inr[0]))
        //            {
        //                //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
        //                wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
        //                //DttSP.DoResamplerF(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampPtrIn_r);
        //                wdsp.xresampleFV(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampPtrIn_r);
        //                if ((rb_vacIN_l.WriteSpace() >= outsamps) && (rb_vacIN_r.WriteSpace() >= outsamps))
        //                {
        //                    Win32.EnterCriticalSection(cs_vacw);
        //                    rb_vacIN_l.WritePtr(res_inl_ptr, outsamps);
        //                    rb_vacIN_r.WritePtr(res_inr_ptr, outsamps);
        //                    Win32.LeaveCriticalSection(cs_vacw);
        //                }
        //                else
        //                {
        //                    vac_rb_reset = true;
        //                    VACDebug("rb_vacIN overflow stereo CBvac");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if ((rb_vacIN_l.WriteSpace() >= frameCount) && (rb_vacIN_r.WriteSpace() >= frameCount))
        //            {
        //                Win32.EnterCriticalSection(cs_vacw);
        //                rb_vacIN_l.WritePtr(in_l_ptr1, frameCount);
        //                rb_vacIN_r.WritePtr(in_r_ptr1, frameCount);
        //                Win32.LeaveCriticalSection(cs_vacw);
        //            }
        //            else
        //            {
        //                //vac_rb_reset = true;
        //                VACDebug("rb_vacIN overflow mono CBvac");
        //            }
        //        }

        //        if ((rb_vacOUT_l.ReadSpace() >= frameCount) && (rb_vacOUT_r.ReadSpace() >= frameCount))
        //        {
        //            Win32.EnterCriticalSection(cs_vac);
        //            rb_vacOUT_l.ReadPtr(out_l_ptr1, frameCount);
        //            rb_vacOUT_r.ReadPtr(out_r_ptr1, frameCount);
        //            Win32.LeaveCriticalSection(cs_vac);
        //        }
        //        else
        //        {
        //            ClearBuffer(out_l_ptr1, frameCount);
        //            ClearBuffer(out_r_ptr1, frameCount);
        //            VACDebug("rb_vacOUT underflow");
        //        }
        //    }
        //    else
        //    {
        //        if (vac_resample)
        //        {
        //            int outsamps = 0;
        //            fixed (float* res_inl_ptr = &(res_inl[0]))
        //            {
        //                //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
        //                wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampPtrIn_l);
        //                if ((rb_vacIN_l.WriteSpace() >= outsamps) && (rb_vacIN_r.WriteSpace() >= outsamps))
        //                {
        //                    Win32.EnterCriticalSection(cs_vacw);
        //                    rb_vacIN_l.WritePtr(res_inl_ptr, outsamps);
        //                    rb_vacIN_r.WritePtr(res_inl_ptr, outsamps);
        //                    Win32.LeaveCriticalSection(cs_vacw);
        //                }
        //                else
        //                {
        //                    //vac_rb_reset = true;
        //                    VACDebug("rb_vacIN_l overflow");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if ((rb_vacIN_l.WriteSpace() >= frameCount) && (rb_vacIN_r.WriteSpace() >= frameCount))
        //            {
        //                Win32.EnterCriticalSection(cs_vacw);
        //                rb_vacIN_l.WritePtr(in_l_ptr1, frameCount);
        //                rb_vacIN_r.WritePtr(in_l_ptr1, frameCount);
        //                Win32.LeaveCriticalSection(cs_vacw);
        //            }
        //            else
        //            {
        //                //vac_rb_reset = true;
        //                VACDebug("rb_vacIN_l overflow");
        //            }
        //        }
        //        if ((rb_vacOUT_l.ReadSpace() >= frameCount) && (rb_vacOUT_r.ReadSpace() >= frameCount))
        //        {
        //            Win32.EnterCriticalSection(cs_vac);
        //            rb_vacOUT_l.ReadPtr(out_l_ptr1, frameCount);
        //            rb_vacOUT_r.ReadPtr(out_l_ptr1, frameCount);
        //            Win32.LeaveCriticalSection(cs_vac);
        //        }
        //        else
        //        {
        //            ClearBuffer(out_l_ptr1, frameCount);
        //            VACDebug("rb_vacOUT_l underflow");
        //        }
        //    }

        //    return 0;
        //}

        unsafe public static int CallbackVAC2(void* input, void* output, int frameCount,
            PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            if (!vac2_enabled) return 0;

            int** array_ptr = (int**)input;
            float* in_l_ptr1 = (float*)array_ptr[0];
            float* in_r_ptr1 = null;
            if (vac2_stereo || vac2_output_iq) in_r_ptr1 = (float*)array_ptr[1];
            array_ptr = (int**)output;
            float* out_l_ptr1 = (float*)array_ptr[0];
            float* out_r_ptr1 = null;
            if (vac2_stereo || vac2_output_iq) out_r_ptr1 = (float*)array_ptr[1];

            if (varsampEnabledVAC2)
            {
                if (vac2_stereo || vac2_output_iq)
                {
                    fixed (double* resampBufPtr = &(resampBufVac2InWrite[0]))
                    {
                        Swizzle(resampBufPtr, in_l_ptr1, in_r_ptr1, frameCount);
                        wdsp.xrmatchIN(rmatchVac2In, resampBufPtr);
                    }

                    fixed (double* resampBufPtr = &(resampBufVac2OutRead[0]))
                    {
                        wdsp.xrmatchOUT(rmatchVac2Out, resampBufPtr);
                        Deswizzle(out_l_ptr1, out_r_ptr1, resampBufPtr, frameCount);
                    }
                }
                else
                {
                    fixed (double* resampBufPtr = &(resampBufVac2InWrite[0]))
                    {
                        Swizzle(resampBufPtr, in_l_ptr1, in_l_ptr1, frameCount);
                        wdsp.xrmatchIN(rmatchVac2In, resampBufPtr);
                    }

                    fixed (double* resampBufPtr = &(resampBufVac2OutRead[0]))
                    {
                        wdsp.xrmatchOUT(rmatchVac2Out, resampBufPtr);
                        Deswizzle(out_l_ptr1, out_l_ptr1, resampBufPtr, frameCount);
                    }
                }
            }
            else
            {
                if (vac2_rb_reset)
                {
                    vac2_rb_reset = false;
                    ClearBuffer(out_l_ptr1, frameCount);
                    if (vac2_stereo || vac2_output_iq) ClearBuffer(out_r_ptr1, frameCount);
                    Win32.EnterCriticalSection(cs_vac2w);
                    rb_vac2IN_l.Reset();
                    rb_vac2IN_r.Reset();
                    Win32.LeaveCriticalSection(cs_vac2w);
                    Win32.EnterCriticalSection(cs_vac2);
                    rb_vac2OUT_l.Reset();
                    rb_vac2OUT_r.Reset();
                    Win32.LeaveCriticalSection(cs_vac2);
                    return 0;
                }
                if (vac2_stereo || vac2_output_iq)
                {
                    if (vac2_resample)
                    {
                        int outsamps = 0;
                        fixed (float* res_inl_ptr = &(res_vac2_inl[0]))
                        fixed (float* res_inr_ptr = &(res_vac2_inr[0]))
                        {
                            //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
                            wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
                            //DttSP.DoResamplerF(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampVAC2PtrIn_r);
                            wdsp.xresampleFV(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampVAC2PtrIn_r);
                            if ((rb_vac2IN_l.WriteSpace() >= outsamps) && (rb_vac2IN_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac2w);
                                rb_vac2IN_l.WritePtr(res_inl_ptr, outsamps);
                                rb_vac2IN_r.WritePtr(res_inr_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac2w);
                            }
                            else
                            {
                                vac2_rb_reset = true;
                                VACDebug("rb_vac2IN overflow stereo CBvac");
                            }
                        }
                    }
                    else
                    {
                        if ((rb_vac2IN_l.WriteSpace() >= frameCount) && (rb_vac2IN_r.WriteSpace() >= frameCount))
                        {
                            Win32.EnterCriticalSection(cs_vac2w);
                            rb_vac2IN_l.WritePtr(in_l_ptr1, frameCount);
                            rb_vac2IN_r.WritePtr(in_r_ptr1, frameCount);
                            Win32.LeaveCriticalSection(cs_vac2w);
                        }
                        else
                        {
                            //vac2_rb_reset = true;
                            VACDebug("rb_vac2IN overflow mono CBvac");
                        }
                    }

                    if ((rb_vac2OUT_l.ReadSpace() >= frameCount) && (rb_vac2OUT_r.ReadSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac2);
                        rb_vac2OUT_l.ReadPtr(out_l_ptr1, frameCount);
                        rb_vac2OUT_r.ReadPtr(out_r_ptr1, frameCount);
                        Win32.LeaveCriticalSection(cs_vac2);
                    }
                    else
                    {
                        ClearBuffer(out_l_ptr1, frameCount);
                        ClearBuffer(out_r_ptr1, frameCount);
                        VACDebug("rb_vac2OUT underflow");
                    }
                }
                else
                {
                    if (vac2_resample)
                    {
                        int outsamps = 0;
                        fixed (float* res_inl_ptr = &(res_vac2_inl[0]))
                        {
                            //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
                            wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
                            if ((rb_vac2IN_l.WriteSpace() >= outsamps) && (rb_vac2IN_r.WriteSpace() >= outsamps))
                            {
                                Win32.EnterCriticalSection(cs_vac2w);
                                rb_vac2IN_l.WritePtr(res_inl_ptr, outsamps);
                                rb_vac2IN_r.WritePtr(res_inl_ptr, outsamps);
                                Win32.LeaveCriticalSection(cs_vac2w);
                            }
                            else
                            {
                                //vac_rb_reset = true;
                                VACDebug("rb_vac2IN_l overflow");
                            }
                        }
                    }
                    else
                    {
                        if ((rb_vac2IN_l.WriteSpace() >= frameCount) && (rb_vac2IN_r.WriteSpace() >= frameCount))
                        {
                            Win32.EnterCriticalSection(cs_vac2w);
                            rb_vac2IN_l.WritePtr(in_l_ptr1, frameCount);
                            rb_vac2IN_r.WritePtr(in_l_ptr1, frameCount);
                            Win32.LeaveCriticalSection(cs_vac2w);
                        }
                        else
                        {
                            //vac_rb_reset = true;
                            VACDebug("rb_vac2IN_l overflow");
                        }
                    }
                    if ((rb_vac2OUT_l.ReadSpace() >= frameCount) && (rb_vac2OUT_r.ReadSpace() >= frameCount))
                    {
                        Win32.EnterCriticalSection(cs_vac2);
                        rb_vac2OUT_l.ReadPtr(out_l_ptr1, frameCount);
                        rb_vac2OUT_r.ReadPtr(out_l_ptr1, frameCount);
                        Win32.LeaveCriticalSection(cs_vac2);
                    }
                    else
                    {
                        ClearBuffer(out_l_ptr1, frameCount);
                        VACDebug("rb_vac2OUT_l underflow");
                    }
                }

            }

            return 0;
        }

        //unsafe public static int CallbackVAC2(void* input, void* output, int frameCount,
        //    PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        //{
        //    if (!vac2_enabled) return 0;

        //    int** array_ptr = (int**)input;
        //    float* in_l_ptr1 = (float*)array_ptr[0];
        //    float* in_r_ptr1 = null;
        //    if (vac2_stereo || vac2_output_iq) in_r_ptr1 = (float*)array_ptr[1];
        //    array_ptr = (int**)output;
        //    float* out_l_ptr1 = (float*)array_ptr[0];
        //    float* out_r_ptr1 = null;
        //    if (vac2_stereo || vac2_output_iq) out_r_ptr1 = (float*)array_ptr[1];

        //    if (vac2_rb_reset)
        //    {
        //        vac2_rb_reset = false;
        //        ClearBuffer(out_l_ptr1, frameCount);
        //        if (vac2_stereo || vac2_output_iq) ClearBuffer(out_r_ptr1, frameCount);
        //        Win32.EnterCriticalSection(cs_vac2w);
        //        rb_vac2IN_l.Reset();
        //        rb_vac2IN_r.Reset();
        //        Win32.LeaveCriticalSection(cs_vac2w);
        //        Win32.EnterCriticalSection(cs_vac2);
        //        rb_vac2OUT_l.Reset();
        //        rb_vac2OUT_r.Reset();
        //        Win32.LeaveCriticalSection(cs_vac2);
        //        return 0;
        //    }
        //    if (vac2_stereo || vac2_output_iq)
        //    {
        //        if (vac2_resample)
        //        {
        //            int outsamps = 0;
        //            fixed (float* res_inl_ptr = &(res_vac2_inl[0]))
        //            fixed (float* res_inr_ptr = &(res_vac2_inr[0]))
        //            {
        //                //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
        //                wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
        //                //DttSP.DoResamplerF(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampVAC2PtrIn_r);
        //                wdsp.xresampleFV(in_r_ptr1, res_inr_ptr, frameCount, &outsamps, resampVAC2PtrIn_r);
        //                if ((rb_vac2IN_l.WriteSpace() >= outsamps) && (rb_vac2IN_r.WriteSpace() >= outsamps))
        //                {
        //                    Win32.EnterCriticalSection(cs_vac2w);
        //                    rb_vac2IN_l.WritePtr(res_inl_ptr, outsamps);
        //                    rb_vac2IN_r.WritePtr(res_inr_ptr, outsamps);
        //                    Win32.LeaveCriticalSection(cs_vac2w);
        //                }
        //                else
        //                {
        //                    vac2_rb_reset = true;
        //                    VACDebug("rb_vac2IN overflow stereo CBvac");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if ((rb_vac2IN_l.WriteSpace() >= frameCount) && (rb_vac2IN_r.WriteSpace() >= frameCount))
        //            {
        //                Win32.EnterCriticalSection(cs_vac2w);
        //                rb_vac2IN_l.WritePtr(in_l_ptr1, frameCount);
        //                rb_vac2IN_r.WritePtr(in_r_ptr1, frameCount);
        //                Win32.LeaveCriticalSection(cs_vac2w);
        //            }
        //            else
        //            {
        //                //vac2_rb_reset = true;
        //                VACDebug("rb_vac2IN overflow mono CBvac");
        //            }
        //        }

        //        if ((rb_vac2OUT_l.ReadSpace() >= frameCount) && (rb_vac2OUT_r.ReadSpace() >= frameCount))
        //        {
        //            Win32.EnterCriticalSection(cs_vac2);
        //            rb_vac2OUT_l.ReadPtr(out_l_ptr1, frameCount);
        //            rb_vac2OUT_r.ReadPtr(out_r_ptr1, frameCount);
        //            Win32.LeaveCriticalSection(cs_vac2);
        //        }
        //        else
        //        {
        //            ClearBuffer(out_l_ptr1, frameCount);
        //            ClearBuffer(out_r_ptr1, frameCount);
        //            VACDebug("rb_vac2OUT underflow");
        //        }
        //    }
        //    else
        //    {
        //        if (vac2_resample)
        //        {
        //            int outsamps = 0;
        //            fixed (float* res_inl_ptr = &(res_vac2_inl[0]))
        //            {
        //                //DttSP.DoResamplerF(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
        //                wdsp.xresampleFV(in_l_ptr1, res_inl_ptr, frameCount, &outsamps, resampVAC2PtrIn_l);
        //                if ((rb_vac2IN_l.WriteSpace() >= outsamps) && (rb_vac2IN_r.WriteSpace() >= outsamps))
        //                {
        //                    Win32.EnterCriticalSection(cs_vac2w);
        //                    rb_vac2IN_l.WritePtr(res_inl_ptr, outsamps);
        //                    rb_vac2IN_r.WritePtr(res_inl_ptr, outsamps);
        //                    Win32.LeaveCriticalSection(cs_vac2w);
        //                }
        //                else
        //                {
        //                    //vac_rb_reset = true;
        //                    VACDebug("rb_vac2IN_l overflow");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if ((rb_vac2IN_l.WriteSpace() >= frameCount) && (rb_vac2IN_r.WriteSpace() >= frameCount))
        //            {
        //                Win32.EnterCriticalSection(cs_vac2w);
        //                rb_vac2IN_l.WritePtr(in_l_ptr1, frameCount);
        //                rb_vac2IN_r.WritePtr(in_l_ptr1, frameCount);
        //                Win32.LeaveCriticalSection(cs_vac2w);
        //            }
        //            else
        //            {
        //                //vac_rb_reset = true;
        //                VACDebug("rb_vac2IN_l overflow");
        //            }
        //        }
        //        if ((rb_vac2OUT_l.ReadSpace() >= frameCount) && (rb_vac2OUT_r.ReadSpace() >= frameCount))
        //        {
        //            Win32.EnterCriticalSection(cs_vac2);
        //            rb_vac2OUT_l.ReadPtr(out_l_ptr1, frameCount);
        //            rb_vac2OUT_r.ReadPtr(out_l_ptr1, frameCount);
        //            Win32.LeaveCriticalSection(cs_vac2);
        //        }
        //        else
        //        {
        //            ClearBuffer(out_l_ptr1, frameCount);
        //            VACDebug("rb_vac2OUT_l underflow");
        //        }
        //    }

        //    return 0;
        //}

        unsafe public static int Pipe(void* input, void* output, int frameCount,
            PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            float** inptr = (float**)input;
            float** outptr = (float**)output;

            for (int i = 0; i < frameCount; i++)
            {
                **outptr++ = **inptr++;
                **outptr++ = **inptr++;
            }
            return 0;
        }

        #endregion

        #region Buffer Operations

        unsafe private static float SumBuffer(float* buf, int samples)
        {
            float temp = 0.0f;
            for (int i = 0; i < samples; i++)
                temp += buf[i];
            return temp;
        }

        unsafe private static void ClearBuffer(float* buf, int samples)
        {
            Win32.memset(buf, 0, samples * sizeof(float));
        }

        unsafe private static void CopyBuffer(float* inbuf, float* outbuf, int samples)
        {
            Win32.memcpy(outbuf, inbuf, samples * sizeof(float));
        }

        unsafe private static void ScaleBuffer(float* inbuf, float* outbuf, int samples, float scale)
        {
            if (scale == 1.0f)
            {
                CopyBuffer(inbuf, outbuf, samples);
                return;
            }

            for (int i = 0; i < samples; i++)
                outbuf[i] = inbuf[i] * scale;
        }

        unsafe private static void AddBuffer(float* dest, float* buftoadd, int samples)
        {
            for (int i = 0; i < samples; i++)
                dest[i] += buftoadd[i];
        }

        unsafe public static float MaxSample(float* buf, int samples)
        {
            float max = float.MinValue;
            for (int i = 0; i < samples; i++)
                max = Math.Max(max, buf[i]);

            return max;
        }

        unsafe public static float MaxSample(float* buf1, float* buf2, int samples)
        {
            float max = float.MinValue;
            for (int i = 0; i < samples; i++)
            {
                max = Math.Max(max, buf1[i]);
                max = Math.Max(max, buf2[i]);
            }
            return max;
        }

        unsafe public static float MinSample(float* buf, int samples)
        {
            float min = float.MaxValue;
            for (int i = 0; i < samples; i++)
                min = Math.Min(min, buf[i]);

            return min;
        }

        unsafe public static float MinSample(float* buf1, float* buf2, int samples)
        {
            float min = float.MaxValue;
            for (int i = 0; i < samples; i++)
            {
                min = Math.Min(min, buf1[i]);
                min = Math.Min(min, buf2[i]);
            }

            return min;
        }

        unsafe private static void CorrectIQBuffer(float* inbufI, float* inbufQ, float* outbufI, float* outbufQ,
                                                   int samples)
        {
            //float phase = (float) (0.001 * console.radio.GetDSPRX(0, 0).RXCorrectIQPhase);
            //float gain = (float) (1.0 + 0.001 * console.radio.GetDSPRX(0, 0).RXCorrectIQGain);
            for (int i = 0; i < samples; i++)
            {
                outbufI[i] = inbufI[i] + iq_phase * inbufQ[i];
                outbufQ[i] = inbufQ[i] * iq_gain;
            }
        }

        // returns updated phase accumulator
        unsafe public static double SineWave(float* buf, int samples, double sinphase, double freq)
        {
            var phaseStep = freq / sample_rate1 * 2 * Math.PI;
            var cosval = Math.Cos(sinphase);
            var sinval = Math.Sin(sinphase);
            var cosdelta = Math.Cos(phaseStep);
            var sindelta = Math.Sin(phaseStep);

            for (var i = 0; i < samples; i++)
            {
                var tmpval = cosval * cosdelta - sinval * sindelta;
                sinval = cosval * sindelta + sinval * cosdelta;
                cosval = tmpval;

                buf[i] = (float)(sinval);

                sinphase += phaseStep;
            }

            return sinphase;
        }

        // returns updated phase accumulator
        unsafe public static double CosineWave(float* buf, int samples, double cosphase, double freq)
        {
            var phaseStep = freq / sample_rate1 * 2 * Math.PI;
            var cosval = Math.Cos(cosphase);
            var sinval = Math.Sin(cosphase);
            var cosdelta = Math.Cos(phaseStep);
            var sindelta = Math.Sin(phaseStep);

            for (var i = 0; i < samples; i++)
            {
                var tmpval = cosval * cosdelta - sinval * sindelta;
                sinval = cosval * sindelta + sinval * cosdelta;
                cosval = tmpval;

                buf[i] = (float)(cosval);

                cosphase += phaseStep;
            }

            return cosphase;
        }

        unsafe public static void SineWave2Tone(float* buf, int samples,
            double phase1, double phase2,
            double freq1, double freq2,
            out double updated_phase1, out double updated_phase2)
        {
            double phase_step1 = freq1 / sample_rate1 * 2 * Math.PI;
            double cosval1 = Math.Cos(phase1);
            double sinval1 = Math.Sin(phase1);
            double cosdelta1 = Math.Cos(phase_step1);
            double sindelta1 = Math.Sin(phase_step1);

            double phase_step2 = freq2 / sample_rate1 * 2 * Math.PI;
            double cosval2 = Math.Cos(phase2);
            double sinval2 = Math.Sin(phase2);
            double cosdelta2 = Math.Cos(phase_step2);
            double sindelta2 = Math.Sin(phase_step2);
            double tmpval;

            for (int i = 0; i < samples; i++)
            {
                tmpval = cosval1 * cosdelta1 - sinval1 * sindelta1;
                sinval1 = cosval1 * sindelta1 + sinval1 * cosdelta1;
                cosval1 = tmpval;

                tmpval = cosval2 * cosdelta2 - sinval2 * sindelta2;
                sinval2 = cosval2 * sindelta2 + sinval2 * cosdelta2;
                cosval2 = tmpval;

                buf[i] = (float)(sinval1 * 0.5 + sinval2 * 0.5);

                phase1 += phase_step1;
                phase2 += phase_step2;
            }

            updated_phase1 = phase1;
            updated_phase2 = phase2;
        }

        unsafe public static void CosineWave2Tone(float* buf, int samples,
            double phase1, double phase2,
            double freq1, double freq2,
            out double updated_phase1, out double updated_phase2)
        {
            double phase_step1 = freq1 / sample_rate1 * 2 * Math.PI;
            double cosval1 = Math.Cos(phase1);
            double sinval1 = Math.Sin(phase1);
            double cosdelta1 = Math.Cos(phase_step1);
            double sindelta1 = Math.Sin(phase_step1);

            double phase_step2 = freq2 / sample_rate1 * 2 * Math.PI;
            double cosval2 = Math.Cos(phase2);
            double sinval2 = Math.Sin(phase2);
            double cosdelta2 = Math.Cos(phase_step2);
            double sindelta2 = Math.Sin(phase_step2);
            double tmpval;

            for (int i = 0; i < samples; i++)
            {
                tmpval = cosval1 * cosdelta1 - sinval1 * sindelta1;
                sinval1 = cosval1 * sindelta1 + sinval1 * cosdelta1;
                cosval1 = tmpval;

                tmpval = cosval2 * cosdelta2 - sinval2 * sindelta2;
                sinval2 = cosval2 * sindelta2 + sinval2 * cosdelta2;
                cosval2 = tmpval;

                buf[i] = (float)(cosval1 * 0.5 + cosval2 * 0.5);

                phase1 += phase_step1;
                phase2 += phase_step2;
            }

            updated_phase1 = phase1;
            updated_phase2 = phase2;
        }


        private static readonly Random r = new Random();
        private static double _y2;
        private static bool _useLast;

        private static double Boxmuller(double m, double s)
        {
            double y1;
            if (_useLast) /* use value from previous call */
            {
                y1 = _y2;
                _useLast = false;
            }
            else
            {
                double x1, x2, w;
                do
                {
                    x1 = (2.0 * r.NextDouble() - 1.0);
                    x2 = (2.0 * r.NextDouble() - 1.0);
                    w = x1 * x1 + x2 * x2;
                } while (w >= 1.0);

                w = Math.Sqrt((-2.0 * Math.Log(w)) / w);
                y1 = x1 * w;
                _y2 = x2 * w;
                _useLast = true;
            }

            return (m + y1 * s);
        }

        unsafe public static void Noise(float* buf, int samples)
        {
            for (int i = 0; i < samples; i++)
            {
                buf[i] = (float)Boxmuller(0.0, 0.2);
            }
        }

        private static double tri_val;
        private static int tri_direction = 1;

        unsafe public static void Triangle(float* buf, int samples, double freq)
        {
            double step = freq / sample_rate1 * 2 * tri_direction;
            for (int i = 0; i < samples; i++)
            {
                buf[i] = (float)tri_val;
                tri_val += step;
                if (tri_val >= 1.0 || tri_val <= -1.0)
                {
                    step = -step;
                    tri_val += 2 * step;
                    if (step < 0) tri_direction = -1;
                    else tri_direction = 1;
                }
            }
        }

        private static double saw_val;
        private static int saw_direction = 1;

        unsafe public static void Sawtooth(float* buf, int samples, double freq)
        {
            double step = freq / sample_rate1 * saw_direction;
            for (int i = 0; i < samples; i++)
            {
                buf[i] = (float)saw_val;
                saw_val += step;
                if (saw_val >= 1.0) saw_val -= 2.0;
                if (saw_val <= -1.0) saw_val += 2.0;
            }
        }

        unsafe public static void AddConstant(float* buf, int samples, float val)
        {
            for (int i = 0; i < samples; i++)
                buf[i] += val;
        }

        private static double pulse_duty = 0.01; // percent of total

        public static double PulseDuty
        {
            set { pulse_duty = value; }
        }

        private static double pulse_period = 1.0; // in sec

        public static double PulsePeriod
        {
            set { pulse_period = value; }
        }

        private static double pulse_sine_phase;
        private static int pulse_on_count;
        private static int pulse_off_count;
        private static int pulse_state; // for pulse state machine

        unsafe private static void Pulse(float* buf, int samples)
        {
            double phase_step = sine_freq1 / sample_rate1 * 2 * Math.PI;
            double cosval = Math.Cos(pulse_sine_phase);
            double sinval = Math.Sin(pulse_sine_phase);
            double cosdelta = Math.Cos(phase_step);
            double sindelta = Math.Sin(phase_step);

            var pulse_samples = (int)(pulse_period * sample_rate1 * pulse_duty);
            var off_samples = (int)(pulse_period * sample_rate1 * (1 - pulse_duty));

            for (int i = 0; i < samples; i++)
            {
                switch (pulse_state)
                {
                    case 0: // pulse on state                       
                        double tmpval = cosval * cosdelta - sinval * sindelta;
                        sinval = cosval * sindelta + sinval * cosdelta;
                        cosval = tmpval;

                        buf[i] = (float)(sinval);

                        pulse_sine_phase += phase_step;

                        if (pulse_on_count++ > pulse_samples)
                        {
                            // go to off state
                            pulse_state = 1;
                            pulse_off_count = 0;
                        }
                        break;
                    case 1: // pulse off state
                        buf[i] = 0.0f;

                        if (pulse_off_count++ > off_samples)
                        {
                            // goto on state
                            pulse_state = 0;
                            pulse_on_count = 0;

                            pulse_sine_phase = 0.0; // reset phase for max front end pulse
                        }
                        break;
                }
            }
        }

        // convert non-interleaved floats into interleaved doubles
        unsafe public static void Swizzle(double* dest, float* sourceL, float* sourceR, int count)
        {
            for (int i = 0; i < count; i++)
            {
                dest[2 * i] = (double)sourceL[i];
                dest[2 * i + 1] = (double)(sourceR[i]);
            }
        }

        // convert interleaved doubles into non-interleaved floats
        unsafe public static void Deswizzle(float* destL, float* destR, double* source, int count)
        {
            for (int i = 0; i < count; i++)
            {
                destL[i] = (float)source[2 * i];
                destR[i] = (float)source[2 * i + 1];
            }
        }

        #endregion

        #region Misc Routines

        // ======================================================
        // Misc Routines
        // ======================================================

        public static void ResetMinMax()
        {
            min_in_l = float.MaxValue;
            min_in_r = float.MaxValue;
            min_out_l = float.MaxValue;
            min_out_r = float.MaxValue;

            max_in_l = float.MinValue;
            max_in_r = float.MinValue;
            max_out_l = float.MinValue;
            max_out_r = float.MinValue;
        }

        private static void VACDebug(string s)
        {
#if(VAC_DEBUG)
			Debug.WriteLine(s);
#endif
        }

        unsafe private static void InitVAC()
        {
            if (varsampEnabledVAC1)
            {
                // size the ringbuffers
                int n, insertSize, vac_in_ringbuffer_size, vac_out_ringbuffer_size = 0;
                if (!vac_output_iq)
                {
                    if (sample_rate1 > sample_rate2)
                        insertSize = block_size_vac * (sample_rate1 / sample_rate2);
                    else
                        insertSize = block_size_vac / (sample_rate2 / sample_rate1);
                    n = sample_rate2 * latency2 / 1000;
                    if (insertSize > n) n = insertSize;
                    if (block_size1 > n) n = block_size1;
                    vac_in_ringbuffer_size = 4 * n;

                    if (sample_rate2 > out_rate)
                        insertSize = out_count * (sample_rate2 / out_rate);
                    else
                        insertSize = out_count / (out_rate / sample_rate2);
                    n = sample_rate2 * latency2 / 1000;
                    if (insertSize > n) n = insertSize;
                    if (block_size_vac > n) n = block_size_vac;
                    vac_out_ringbuffer_size = 4 * n;
                }
                else
                {
                    n = sample_rate1 * latency2 / 1000;
                    if (block_size1 > n) n = block_size1;
                    vac_in_ringbuffer_size = 4 * n;
                    vac_out_ringbuffer_size = 4 * n;
                }

                // adaptive variable resamplers
                // buffers for rmatch i/o
                if (!vac_output_iq)
                {
                    rmatchVac1In = wdsp.create_rmatchLegacyV(block_size_vac, block_size1, sample_rate2, sample_rate1, vac_in_ringbuffer_size);
                    rmatchVac1Out = wdsp.create_rmatchLegacyV(out_count, block_size_vac, out_rate, sample_rate2, vac_out_ringbuffer_size);
                    resampBufVac1InWrite = new double[2 * block_size_vac];
                    resampBufVac1InRead = new double[2 * block_size1];
                    resampBufVac1OutWrite = new double[2 * out_count];
                    resampBufVac1OutRead = new double[2 * block_size_vac];
                }
                else
                {
                    rmatchVac1In = wdsp.create_rmatchLegacyV(block_size1, block_size1, sample_rate1, sample_rate1, vac_in_ringbuffer_size);
                    rmatchVac1Out = wdsp.create_rmatchLegacyV(block_size1, block_size1, sample_rate1, sample_rate1, vac_out_ringbuffer_size);
                    resampBufVac1InWrite = new double[2 * block_size1];
                    resampBufVac1InRead = new double[2 * block_size1];
                    resampBufVac1OutWrite = new double[2 * block_size1];
                    resampBufVac1OutRead = new double[2 * block_size1];
                }

                Trace.Write("****** InitVAC ******");
                Trace.Write("Pri Block Size " + block_size1);
                Trace.Write("Pri Sample Rate " + sample_rate1);
                Trace.Write("VAC Block Size " + block_size_vac);
                Trace.Write("VAC Sample Rate  " + sample_rate2);
                Trace.Write("Latency " + latency2);
                Trace.Write("Ringbuffer Size " + vac_in_ringbuffer_size + " In");
                Trace.Write("Ringbuffer Size " + vac_out_ringbuffer_size + " Out");
                Trace.Write("*********************");
            }
            else
            {
                //K5IT - Size the VAC ring buffer to hold twice the samples of the target latency
                int vac_ringbuffer_size = 2 * sample_rate2 * latency2 / 1000;
                if (block_size_vac * 2 > vac_ringbuffer_size) //minimum ringbuffer size is two audio buffers
                {
                    vac_ringbuffer_size = block_size_vac * 2;
                }
                else if (vac_ringbuffer_size % block_size_vac > 0) //ringbuffer should hold an even number of buffers (prevents write past end)
                {
                    vac_ringbuffer_size = vac_ringbuffer_size + block_size_vac - (vac_ringbuffer_size % block_size_vac); //round up to nearest buffer size
                }
                VACDebug(string.Format("VAC ringbuffer size {0}", vac_ringbuffer_size));

                if (rb_vacOUT_l == null) rb_vacOUT_l = new RingBufferFloat(vac_ringbuffer_size);
                rb_vacOUT_l.Restart(vac_output_iq ? block_size1 : block_size_vac);

                if (rb_vacOUT_r == null) rb_vacOUT_r = new RingBufferFloat(vac_ringbuffer_size);
                rb_vacOUT_r.Restart(vac_output_iq ? block_size1 : block_size_vac);

                if (rb_vacIN_l == null) rb_vacIN_l = new RingBufferFloat(vac_ringbuffer_size);
                rb_vacIN_l.Restart(block_size_vac);

                if (rb_vacIN_r == null) rb_vacIN_r = new RingBufferFloat(vac_ringbuffer_size);
                rb_vacIN_r.Restart(block_size_vac);

                if (sample_rate2 != sample_rate1 && !vac_output_iq)
                {
                    vac_resample = true;
                    //if (res_outl == null) res_outl = new float[65536];
                    //if (res_outr == null) res_outr = new float[65536];
                    if (res_inl == null) res_inl = new float[4 * 65536];
                    if (res_inr == null) res_inr = new float[4 * 65536];

                    if (resampPtrIn_l != null)
                        wdsp.destroy_resampleFV(resampPtrIn_l);
                    resampPtrIn_l = wdsp.create_resampleFV(sample_rate2, sample_rate1);

                    if (resampPtrIn_r != null)
                        wdsp.destroy_resampleFV(resampPtrIn_r);
                    resampPtrIn_r = wdsp.create_resampleFV(sample_rate2, sample_rate1);

                    /*if (resampPtrOut_l != null)
                        wdsp.destroy_resampleFV(resampPtrOut_l);
                    resampPtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate2);

                    if (resampPtrOut_r != null)
                        wdsp.destroy_resampleFV(resampPtrOut_r);
                    resampPtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate2);*/
                }
                else
                {
                    vac_resample = false;
                    /*if (vac_output_iq)
                    {
                        if (res_outl == null) res_outl = new float[65536];
                        if (res_outr == null) res_outr = new float[65536];
                    }*/
                }

                if (sample_rate2 != out_rate && !vac_output_iq)
                {
                    if (res_outl == null) res_outl = new float[2 * 65536];
                    if (res_outr == null) res_outr = new float[2 * 65536];

                    if (resampPtrOut_l != null)
                        wdsp.destroy_resampleFV(resampPtrOut_l);
                    resampPtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate2);

                    if (resampPtrOut_r != null)
                        wdsp.destroy_resampleFV(resampPtrOut_r);
                    resampPtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate2);
                }
                else
                {
                    if (vac_output_iq)
                    {
                        if (res_outl == null) res_outl = new float[2 * 65536];
                        if (res_outr == null) res_outr = new float[2 * 65536];
                    }
                }

                cs_vac = (void*)0x0;
                cs_vac = Win32.NewCriticalSection();
                if (Win32.InitializeCriticalSectionAndSpinCount(cs_vac, 0x00000080) == 0)
                {
                    vac_enabled = false;
                    Debug.WriteLine("CriticalSection Failed");
                }
                cs_vacw = (void*)0x0;
                cs_vacw = Win32.NewCriticalSection();
                if (Win32.InitializeCriticalSectionAndSpinCount(cs_vacw, 0x00000080) == 0)
                {
                    vac_enabled = false;
                    Debug.WriteLine("CriticalSection Failed");
                }
            }
        }

        //unsafe private static void InitVAC()
        //{
        //    //K5IT - Size the VAC ring buffer to hold twice the samples of the target latency
        //    int vac_ringbuffer_size = 2 * sample_rate2 * latency2 / 1000;
        //    if (block_size_vac * 2 > vac_ringbuffer_size) //minimum ringbuffer size is two audio buffers
        //    {
        //        vac_ringbuffer_size = block_size_vac * 2;
        //    }
        //    else if (vac_ringbuffer_size % block_size_vac > 0) //ringbuffer should hold an even number of buffers (prevents write past end)
        //    {
        //        vac_ringbuffer_size = vac_ringbuffer_size + block_size_vac - (vac_ringbuffer_size % block_size_vac); //round up to nearest buffer size
        //    }
        //    VACDebug(string.Format("VAC ringbuffer size {0}", vac_ringbuffer_size));

        //    if (rb_vacOUT_l == null) rb_vacOUT_l = new RingBufferFloat(vac_ringbuffer_size);
        //    rb_vacOUT_l.Restart(vac_output_iq ? block_size1 : block_size_vac);

        //    if (rb_vacOUT_r == null) rb_vacOUT_r = new RingBufferFloat(vac_ringbuffer_size);
        //    rb_vacOUT_r.Restart(vac_output_iq ? block_size1 : block_size_vac);

        //    if (rb_vacIN_l == null) rb_vacIN_l = new RingBufferFloat(vac_ringbuffer_size); 
        //    rb_vacIN_l.Restart(block_size_vac);

        //    if (rb_vacIN_r == null) rb_vacIN_r = new RingBufferFloat(vac_ringbuffer_size);
        //    rb_vacIN_r.Restart(block_size_vac);

        //    if (sample_rate2 != sample_rate1 && !vac_output_iq)
        //    {
        //        vac_resample = true;
        //        //if (res_outl == null) res_outl = new float[65536];
        //        //if (res_outr == null) res_outr = new float[65536];
        //        if (res_inl == null) res_inl = new float[4 * 65536];
        //        if (res_inr == null) res_inr = new float[4 * 65536];

        //        if (resampPtrIn_l != null)
        //            wdsp.destroy_resampleFV(resampPtrIn_l);
        //        resampPtrIn_l = wdsp.create_resampleFV(sample_rate2, sample_rate1);

        //        if (resampPtrIn_r != null)
        //            wdsp.destroy_resampleFV(resampPtrIn_r);
        //        resampPtrIn_r = wdsp.create_resampleFV(sample_rate2, sample_rate1);

        //        /*if (resampPtrOut_l != null)
        //            wdsp.destroy_resampleFV(resampPtrOut_l);
        //        resampPtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate2);

        //        if (resampPtrOut_r != null)
        //            wdsp.destroy_resampleFV(resampPtrOut_r);
        //        resampPtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate2);*/
        //    }
        //    else
        //    {
        //        vac_resample = false;
        //        /*if (vac_output_iq)
        //        {
        //            if (res_outl == null) res_outl = new float[65536];
        //            if (res_outr == null) res_outr = new float[65536];
        //        }*/
        //    }

        //    if (sample_rate2 != out_rate && !vac_output_iq)
        //    {
        //        if (res_outl == null) res_outl = new float[2 * 65536];
        //        if (res_outr == null) res_outr = new float[2 * 65536];

        //        if (resampPtrOut_l != null)
        //            wdsp.destroy_resampleFV(resampPtrOut_l);
        //        resampPtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate2);

        //        if (resampPtrOut_r != null)
        //            wdsp.destroy_resampleFV(resampPtrOut_r);
        //        resampPtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate2);
        //    }
        //    else
        //    {
        //        if (vac_output_iq)
        //        {
        //            if (res_outl == null) res_outl = new float[2 * 65536];
        //            if (res_outr == null) res_outr = new float[2 * 65536];
        //        }
        //    }

        //    cs_vac = (void*)0x0;
        //    cs_vac = Win32.NewCriticalSection();
        //    if (Win32.InitializeCriticalSectionAndSpinCount(cs_vac, 0x00000080) == 0)
        //    {
        //        vac_enabled = false;
        //        Debug.WriteLine("CriticalSection Failed");
        //    }
        //    cs_vacw = (void*)0x0;
        //    cs_vacw = Win32.NewCriticalSection();
        //    if (Win32.InitializeCriticalSectionAndSpinCount(cs_vacw, 0x00000080) == 0)
        //    {
        //        vac_enabled = false;
        //        Debug.WriteLine("CriticalSection Failed");
        //    }
        //}

        unsafe private static void InitVAC2()
        {
            if (varsampEnabledVAC2)
            {
                // size the ringbuffers
                int n, insertSize, vac2_in_ringbuffer_size, vac2_out_ringbuffer_size = 0;
                if (!vac2_output_iq)
                {
                    if (sample_rate1 > sample_rate3)
                        insertSize = block_size_vac2 * (sample_rate1 / sample_rate3);
                    else
                        insertSize = block_size_vac2 / (sample_rate3 / sample_rate1);
                    n = sample_rate3 * latency3 / 1000;
                    if (insertSize > n) n = insertSize;
                    if (block_size1 > n) n = block_size1;
                    vac2_in_ringbuffer_size = 4 * n;

                    if (sample_rate3 > out_rate)
                        insertSize = out_count * (sample_rate3 / out_rate);
                    else
                        insertSize = out_count / (out_rate / sample_rate3);
                    n = sample_rate3 * latency3 / 1000;
                    if (insertSize > n) n = insertSize;
                    if (block_size_vac2 > n) n = block_size_vac2;
                    vac2_out_ringbuffer_size = 4 * n;
                }
                else
                {
                    n = sample_rate1 * latency3 / 1000;
                    if (block_size1 > n) n = block_size1;
                    vac2_in_ringbuffer_size = 4 * n;
                    vac2_out_ringbuffer_size = 4 * n;
                }

                // adaptive variable resamplers
                // buffers for rmatch i/o
                if (!vac2_output_iq)
                {
                    rmatchVac2In = wdsp.create_rmatchLegacyV(block_size_vac2, block_size1, sample_rate3, sample_rate1, vac2_in_ringbuffer_size);
                    rmatchVac2Out = wdsp.create_rmatchLegacyV(out_count, block_size_vac2, out_rate, sample_rate3, vac2_out_ringbuffer_size);
                    resampBufVac2InWrite = new double[2 * block_size_vac2];
                    resampBufVac2InRead = new double[2 * block_size1];
                    resampBufVac2OutWrite = new double[2 * out_count];
                    resampBufVac2OutRead = new double[2 * block_size_vac2];
                }
                else
                {
                    rmatchVac2In = wdsp.create_rmatchLegacyV(block_size1, block_size1, sample_rate1, sample_rate1, vac2_in_ringbuffer_size);
                    rmatchVac2Out = wdsp.create_rmatchLegacyV(block_size1, block_size1, sample_rate1, sample_rate1, vac2_out_ringbuffer_size);
                    resampBufVac2InWrite = new double[2 * block_size1];
                    resampBufVac2InRead = new double[2 * block_size1];
                    resampBufVac2OutWrite = new double[2 * block_size1];
                    resampBufVac2OutRead = new double[2 * block_size1];
                }

                Trace.Write("****** InitVAC2 ******");
                Trace.Write("Pri Block Size " + block_size1);
                Trace.Write("Pri Sample Rate " + sample_rate1);
                Trace.Write("VAC Block Size " + block_size_vac2);
                Trace.Write("VAC Sample Rate  " + sample_rate3);
                Trace.Write("Latency " + latency3);
                Trace.Write("Ringbuffer Size " + vac2_in_ringbuffer_size + " In");
                Trace.Write("Ringbuffer Size " + vac2_out_ringbuffer_size + " Out");
                Trace.Write("**********************");
            }
            else
            {
                int block_size = block_size_vac2;
                if (vac2_output_iq) block_size = block_size1;

                //K5IT - Size the VAC2 ring buffer to hold twice the samples of the target latency
                int vac2_ringbuffer_size = 2 * sample_rate3 * latency3 / 1000;
                if (block_size_vac2 * 2 > vac2_ringbuffer_size) //minimum ringbuffer size is two audio buffers
                {
                    vac2_ringbuffer_size = block_size_vac2 * 2;
                }
                else if (vac2_ringbuffer_size % block_size_vac2 > 0) //ringbuffer should hold an even number of buffers (prevents write past end)
                {
                    vac2_ringbuffer_size = vac2_ringbuffer_size + block_size_vac2 - (vac2_ringbuffer_size % block_size_vac2); //round up to nearest buffer size
                }
                VACDebug(string.Format("VAC2 ringbuffer size {0}", vac2_ringbuffer_size));

                if (rb_vac2OUT_l == null) rb_vac2OUT_l = new RingBufferFloat(vac2_ringbuffer_size);
                rb_vac2OUT_l.Restart(block_size);

                if (rb_vac2OUT_r == null) rb_vac2OUT_r = new RingBufferFloat(vac2_ringbuffer_size);
                rb_vac2OUT_r.Restart(block_size);

                if (rb_vac2IN_l == null) rb_vac2IN_l = new RingBufferFloat(vac2_ringbuffer_size);
                rb_vac2IN_l.Restart(block_size_vac2);

                if (rb_vac2IN_r == null) rb_vac2IN_r = new RingBufferFloat(vac2_ringbuffer_size);
                rb_vac2IN_r.Restart(block_size_vac2);

                if (sample_rate3 != sample_rate1 && !vac2_output_iq)
                {
                    vac2_resample = true;
                    //if (res_vac2_outl == null) res_vac2_outl = new float[65536];
                    //if (res_vac2_outr == null) res_vac2_outr = new float[65536];
                    if (res_vac2_inl == null) res_vac2_inl = new float[4 * 65536];
                    if (res_vac2_inr == null) res_vac2_inr = new float[4 * 65536];

                    if (resampVAC2PtrIn_l != null)
                        wdsp.destroy_resampleFV(resampVAC2PtrIn_l);
                    resampVAC2PtrIn_l = wdsp.create_resampleFV(sample_rate3, sample_rate1);

                    if (resampVAC2PtrIn_r != null)
                        wdsp.destroy_resampleFV(resampVAC2PtrIn_r);
                    resampVAC2PtrIn_r = wdsp.create_resampleFV(sample_rate3, sample_rate1);

                    /*if (resampVAC2PtrOut_l != null)
                        wdsp.destroy_resampleFV(resampVAC2PtrOut_l);
                    resampVAC2PtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate3);

                    if (resampVAC2PtrOut_r != null)
                        wdsp.destroy_resampleFV(resampVAC2PtrOut_r);
                    resampVAC2PtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate3);*/
                }
                else
                {
                    vac2_resample = false;
                    /*if (vac2_output_iq)
                    {
                        if (res_vac2_outl == null) res_vac2_outl = new float[65536];
                        if (res_vac2_outr == null) res_vac2_outr = new float[65536];
                    }*/
                }

                if (sample_rate3 != out_rate && !vac2_output_iq)
                {
                    if (res_vac2_outl == null) res_vac2_outl = new float[2 * 65536];
                    if (res_vac2_outr == null) res_vac2_outr = new float[2 * 65536];

                    if (resampVAC2PtrOut_l != null)
                        wdsp.destroy_resampleFV(resampVAC2PtrOut_l);
                    resampVAC2PtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate3);

                    if (resampVAC2PtrOut_r != null)
                        wdsp.destroy_resampleFV(resampVAC2PtrOut_r);
                    resampVAC2PtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate3);
                }
                else
                {
                    if (vac2_output_iq)
                    {
                        if (res_vac2_outl == null) res_vac2_outl = new float[2 * 65536];
                        if (res_vac2_outr == null) res_vac2_outr = new float[2 * 65536];
                    }
                }

                cs_vac2 = (void*)0x0;
                cs_vac2 = Win32.NewCriticalSection();
                if (Win32.InitializeCriticalSectionAndSpinCount(cs_vac2, 0x00000080) == 0)
                {
                    vac2_enabled = false;
                    Debug.WriteLine("CriticalSection Failed");
                }
                cs_vac2w = (void*)0x0;
                cs_vac2w = Win32.NewCriticalSection();
                if (Win32.InitializeCriticalSectionAndSpinCount(cs_vac2w, 0x00000080) == 0)
                {
                    vac2_enabled = false;
                    Debug.WriteLine("CriticalSection Failed");
                }
            }
        }

        //unsafe private static void InitVAC2()
        //{
        //    int block_size = block_size_vac2;
        //    if (vac2_output_iq) block_size = block_size1;

        //    //K5IT - Size the VAC2 ring buffer to hold twice the samples of the target latency
        //    int vac2_ringbuffer_size = 2 * sample_rate3 * latency3 / 1000;
        //    if (block_size_vac2 * 2 > vac2_ringbuffer_size) //minimum ringbuffer size is two audio buffers
        //    {
        //        vac2_ringbuffer_size = block_size_vac2 * 2;
        //    }
        //    else if (vac2_ringbuffer_size % block_size_vac2 > 0) //ringbuffer should hold an even number of buffers (prevents write past end)
        //    {
        //        vac2_ringbuffer_size = vac2_ringbuffer_size + block_size_vac2 - (vac2_ringbuffer_size % block_size_vac2); //round up to nearest buffer size
        //    }
        //    VACDebug(string.Format("VAC2 ringbuffer size {0}", vac2_ringbuffer_size));

        //    if (rb_vac2OUT_l == null) rb_vac2OUT_l = new RingBufferFloat(vac2_ringbuffer_size);
        //    rb_vac2OUT_l.Restart(block_size);

        //    if (rb_vac2OUT_r == null) rb_vac2OUT_r = new RingBufferFloat(vac2_ringbuffer_size);
        //    rb_vac2OUT_r.Restart(block_size);

        //    if (rb_vac2IN_l == null) rb_vac2IN_l = new RingBufferFloat(vac2_ringbuffer_size);
        //    rb_vac2IN_l.Restart(block_size_vac2);

        //    if (rb_vac2IN_r == null) rb_vac2IN_r = new RingBufferFloat(vac2_ringbuffer_size);
        //    rb_vac2IN_r.Restart(block_size_vac2);

        //    if (sample_rate3 != sample_rate1 && !vac2_output_iq)
        //    {
        //        vac2_resample = true;
        //        //if (res_vac2_outl == null) res_vac2_outl = new float[65536];
        //        //if (res_vac2_outr == null) res_vac2_outr = new float[65536];
        //        if (res_vac2_inl == null) res_vac2_inl = new float[4 * 65536];
        //        if (res_vac2_inr == null) res_vac2_inr = new float[4 * 65536];

        //        if (resampVAC2PtrIn_l != null)
        //            wdsp.destroy_resampleFV(resampVAC2PtrIn_l);
        //        resampVAC2PtrIn_l = wdsp.create_resampleFV(sample_rate3, sample_rate1);

        //        if (resampVAC2PtrIn_r != null)
        //            wdsp.destroy_resampleFV(resampVAC2PtrIn_r);
        //        resampVAC2PtrIn_r = wdsp.create_resampleFV(sample_rate3, sample_rate1);

        //        /*if (resampVAC2PtrOut_l != null)
        //            wdsp.destroy_resampleFV(resampVAC2PtrOut_l);
        //        resampVAC2PtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate3);

        //        if (resampVAC2PtrOut_r != null)
        //            wdsp.destroy_resampleFV(resampVAC2PtrOut_r);
        //        resampVAC2PtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate3);*/
        //    }
        //    else
        //    {
        //        vac2_resample = false;
        //        /*if (vac2_output_iq)
        //        {
        //            if (res_vac2_outl == null) res_vac2_outl = new float[65536];
        //            if (res_vac2_outr == null) res_vac2_outr = new float[65536];
        //        }*/
        //    }

        //    if (sample_rate3 != out_rate && !vac2_output_iq)
        //    {
        //        if (res_vac2_outl == null) res_vac2_outl = new float[2 * 65536];
        //        if (res_vac2_outr == null) res_vac2_outr = new float[2 * 65536];

        //        if (resampVAC2PtrOut_l != null)
        //            wdsp.destroy_resampleFV(resampVAC2PtrOut_l);
        //        resampVAC2PtrOut_l = wdsp.create_resampleFV(out_rate, sample_rate3);

        //        if (resampVAC2PtrOut_r != null)
        //            wdsp.destroy_resampleFV(resampVAC2PtrOut_r);
        //        resampVAC2PtrOut_r = wdsp.create_resampleFV(out_rate, sample_rate3);
        //    }
        //    else
        //    {
        //        if (vac2_output_iq)
        //        {
        //            if (res_vac2_outl == null) res_vac2_outl = new float[2 * 65536];
        //            if (res_vac2_outr == null) res_vac2_outr = new float[2 * 65536];
        //        }
        //    }

        //    cs_vac2 = (void*)0x0;
        //    cs_vac2 = Win32.NewCriticalSection();
        //    if (Win32.InitializeCriticalSectionAndSpinCount(cs_vac2, 0x00000080) == 0)
        //    {
        //        vac2_enabled = false;
        //        Debug.WriteLine("CriticalSection Failed");
        //    }
        //    cs_vac2w = (void*)0x0;
        //    cs_vac2w = Win32.NewCriticalSection();
        //    if (Win32.InitializeCriticalSectionAndSpinCount(cs_vac2w, 0x00000080) == 0)
        //    {
        //        vac2_enabled = false;
        //        Debug.WriteLine("CriticalSection Failed");
        //    }
        //}

        unsafe private static void CleanUpVAC()
        {
            if (varsampEnabledVAC1)
            {
                resampBufVac1InWrite = null;
                resampBufVac1InRead = null;
                resampBufVac1OutWrite = null;
                resampBufVac1OutRead = null;

                wdsp.destroy_rmatchV(rmatchVac1In);
                wdsp.destroy_rmatchV(rmatchVac1Out);
                Trace.Write("CleanUpVAC ");
            }
            else
            {
                Win32.DeleteCriticalSection(cs_vac);
                rb_vacOUT_l = null;
                rb_vacOUT_r = null;
                rb_vacIN_l = null;
                rb_vacIN_r = null;

                res_outl = null;
                res_outr = null;
                res_inl = null;
                res_inr = null;

                if (resampPtrIn_l != null)
                {
                    //DttSP.DelResamplerF(resampPtrIn_l);
                    wdsp.destroy_resampleFV(resampPtrIn_l);
                    resampPtrIn_l = null;
                }

                if (resampPtrIn_r != null)
                {
                    //DttSP.DelResamplerF(resampPtrIn_r);
                    wdsp.destroy_resampleFV(resampPtrIn_r);
                    resampPtrIn_r = null;
                }

                if (resampPtrOut_l != null)
                {
                    //DttSP.DelResamplerF(resampPtrOut_l);
                    wdsp.destroy_resampleFV(resampPtrOut_l);
                    resampPtrOut_l = null;
                }

                if (resampPtrOut_r != null)
                {
                    //DttSP.DelResamplerF(resampPtrOut_r);
                    wdsp.destroy_resampleFV(resampPtrOut_r);
                    resampPtrOut_r = null;
                }

                Win32.DestroyCriticalSection(cs_vac);
                Win32.DestroyCriticalSection(cs_vacw);
            }
        }

        //unsafe private static void CleanUpVAC()
        //{
        //    Win32.DeleteCriticalSection(cs_vac);
        //    rb_vacOUT_l = null;
        //    rb_vacOUT_r = null;
        //    rb_vacIN_l = null;
        //    rb_vacIN_r = null;

        //    res_outl = null;
        //    res_outr = null;
        //    res_inl = null;
        //    res_inr = null;

        //    if (resampPtrIn_l != null)
        //    {
        //        //DttSP.DelResamplerF(resampPtrIn_l);
        //        wdsp.destroy_resampleFV(resampPtrIn_l);
        //        resampPtrIn_l = null;
        //    }

        //    if (resampPtrIn_r != null)
        //    {
        //        //DttSP.DelResamplerF(resampPtrIn_r);
        //        wdsp.destroy_resampleFV(resampPtrIn_r);
        //        resampPtrIn_r = null;
        //    }

        //    if (resampPtrOut_l != null)
        //    {
        //        //DttSP.DelResamplerF(resampPtrOut_l);
        //        wdsp.destroy_resampleFV(resampPtrOut_l);
        //        resampPtrOut_l = null;
        //    }

        //    if (resampPtrOut_r != null)
        //    {
        //        //DttSP.DelResamplerF(resampPtrOut_r);
        //        wdsp.destroy_resampleFV(resampPtrOut_r);
        //        resampPtrOut_r = null;
        //    }

        //    Win32.DestroyCriticalSection(cs_vac);
        //    Win32.DestroyCriticalSection(cs_vacw);
        //}

        unsafe private static void CleanUpVAC2()
        {
            if (varsampEnabledVAC2)
            {
                resampBufVac2InWrite = null;
                resampBufVac2InRead = null;
                resampBufVac2OutWrite = null;
                resampBufVac2OutRead = null;

                wdsp.destroy_rmatchV(rmatchVac2In);
                wdsp.destroy_rmatchV(rmatchVac2Out);
                Trace.Write("CleanUpVAC2 ");
            }
            else
            {
                Win32.DeleteCriticalSection(cs_vac2);
                rb_vac2OUT_l = null;
                rb_vac2OUT_r = null;
                rb_vac2IN_l = null;
                rb_vac2IN_r = null;

                res_vac2_outl = null;
                res_vac2_outr = null;
                res_vac2_inl = null;
                res_vac2_inr = null;

                if (resampVAC2PtrIn_l != null)
                {
                    //DttSP.DelResamplerF(resampVAC2PtrIn_l);
                    wdsp.destroy_resampleFV(resampVAC2PtrIn_l);
                    resampVAC2PtrIn_l = null;
                }

                if (resampVAC2PtrIn_r != null)
                {
                    //DttSP.DelResamplerF(resampVAC2PtrIn_r);
                    wdsp.destroy_resampleFV(resampVAC2PtrIn_r);
                    resampVAC2PtrIn_r = null;
                }

                if (resampVAC2PtrOut_l != null)
                {
                    //DttSP.DelResamplerF(resampVAC2PtrOut_l);
                    wdsp.destroy_resampleFV(resampVAC2PtrOut_l);
                    resampVAC2PtrOut_l = null;
                }

                if (resampVAC2PtrOut_r != null)
                {
                    //DttSP.DelResamplerF(resampVAC2PtrOut_r);
                    wdsp.destroy_resampleFV(resampVAC2PtrOut_r);
                    resampVAC2PtrOut_r = null;
                }

                Win32.DestroyCriticalSection(cs_vac2);
                Win32.DestroyCriticalSection(cs_vac2w);
            }
        }

        //unsafe private static void CleanUpVAC2()
        //{
        //    Win32.DeleteCriticalSection(cs_vac2);
        //    rb_vac2OUT_l = null;
        //    rb_vac2OUT_r = null;
        //    rb_vac2IN_l = null;
        //    rb_vac2IN_r = null;

        //    res_vac2_outl = null;
        //    res_vac2_outr = null;
        //    res_vac2_inl = null;
        //    res_vac2_inr = null;

        //    if (resampVAC2PtrIn_l != null)
        //    {
        //        //DttSP.DelResamplerF(resampVAC2PtrIn_l);
        //        wdsp.destroy_resampleFV(resampVAC2PtrIn_l);
        //        resampVAC2PtrIn_l = null;
        //    }

        //    if (resampVAC2PtrIn_r != null)
        //    {
        //        //DttSP.DelResamplerF(resampVAC2PtrIn_r);
        //        wdsp.destroy_resampleFV(resampVAC2PtrIn_r);
        //        resampVAC2PtrIn_r = null;
        //    }

        //    if (resampVAC2PtrOut_l != null)
        //    {
        //        //DttSP.DelResamplerF(resampVAC2PtrOut_l);
        //        wdsp.destroy_resampleFV(resampVAC2PtrOut_l);
        //        resampVAC2PtrOut_l = null;
        //    }

        //    if (resampVAC2PtrOut_r != null)
        //    {
        //        //DttSP.DelResamplerF(resampVAC2PtrOut_r);
        //        wdsp.destroy_resampleFV(resampVAC2PtrOut_r);
        //        resampVAC2PtrOut_r = null;
        //    }

        //    Win32.DestroyCriticalSection(cs_vac2);
        //    Win32.DestroyCriticalSection(cs_vac2w);
        //}

        unsafe public static double GetCPULoad()
        {
            return PA19.PA_GetStreamCpuLoad(stream1);
        }

        public static ArrayList GetPAHosts() // returns a text list of driver types
        {
            var a = new ArrayList();
            for (int i = 0; i < PA19.PA_GetHostApiCount(); i++)
            {
                PA19.PaHostApiInfo info = PA19.PA_GetHostApiInfo(i);
                a.Add(info.name);
            }
            a.Add("HPSDR (USB/UDP)");
            return a;
        }

        public static ArrayList GetPAInputDevices(int hostIndex)
        {
            var a = new ArrayList();

            if (hostIndex >= PA19.PA_GetHostApiCount()) //xylowolf 
            {
                a.Add(new PADeviceInfo("HPSDR (PCM A/D)", 0));
                return a;
            }

            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxInputChannels > 0)
                {
                    string name = devInfo.name;
                    int index = name.IndexOf("- "); // find case for things like "Microphone (2- FLEX-1500)"
                    if (index > 0)
                    {
                        char c = name[index - 1]; // make sure this is what we're looking for
                        if (c >= '0' && c <= '9') // it is... remove index
                        {
                            int x = name.IndexOf("(");
                            name = devInfo.name.Substring(0, x + 1); // grab first part of string including "("
                            name += devInfo.name.Substring(index + 2, devInfo.name.Length - index - 2); // add end of string;
                        }
                    }
                    a.Add(new PADeviceInfo(name, i)/* + " - " + devIndex*/);
                }
            }
            return a;
        }

        public static bool CheckPAInputDevices(int hostIndex, string name)
        {
            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxInputChannels > 0 && devInfo.name.Contains(name))
                    return true;
            }
            return false;
        }

        public static ArrayList GetPAOutputDevices(int hostIndex)
        {
            var a = new ArrayList();

            if (hostIndex >= PA19.PA_GetHostApiCount())
            {
                a.Add(new PADeviceInfo("HPSDR (PWM D/A)", 0));
                return a;
            }

            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxOutputChannels > 0)
                {
                    string name = devInfo.name;
                    int index = name.IndexOf("- "); // find case for things like "Microphone (2- FLEX-1500)"
                    if (index > 0)
                    {
                        char c = name[index - 1]; // make sure this is what we're looking for
                        if (c >= '0' && c <= '9') // it is... remove index
                        {
                            int x = name.IndexOf("(");
                            name = devInfo.name.Substring(0, x + 1); // grab first part of string including "("
                            name += devInfo.name.Substring(index + 2, devInfo.name.Length - index - 2); // add end of string;
                        }
                    }
                    a.Add(new PADeviceInfo(name, i)/* + " - " + devIndex*/);
                }
            }
            return a;
        }

        public static bool CheckPAOutputDevices(int hostIndex, string name)
        {
            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxOutputChannels > 0 && devInfo.name.Contains(name))
                    return true;
            }
            return false;
        }

        public static bool Start(int nr)
        {
            bool retval = false;
            phase_buf_l = new float[2048];
            phase_buf_r = new float[2048];

            unsafe
            {
                JanusAudio.SetNRx(nr); //set number of receivers
                JanusAudio.SetDuplex(1); // set full duplex mode

                retval = StartAudio(ref callback3port,
                                    (uint)block_size1,
                                    sample_rate1);
            }

            if (!retval) return retval;

            if (vac_enabled)
                unsafe
                {
                    int num_chan = 1;
                    // ehr add for multirate iq to vac
                    int sample_rate = sample_rate2;
                    int block_size = block_size_vac;
                    int latency = latency2;
                    if (vac_output_iq)
                    {
                        num_chan = 2;
                        sample_rate = sample_rate1;
                        block_size = block_size1;
                        //latency = 250;
                    }
                    else if (vac_stereo) num_chan = 2;
                    // ehr end				
                    vac_rb_reset = true;
                    try
                    {
                        retval = StartAudio_NonJanus(ref callbackVAC, (uint)block_size, sample_rate, host2, input_dev2,
                                                     output_dev2, num_chan, 0, latency);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The program is having trouble starting the VAC audio streams.\n" +
                            "Please examine the VAC related settings on the Setup Form -> Audio Tab and try again.",
                            "VAC Audio Stream Startup Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                }

            if (vac2_enabled)
                unsafe
                {
                    int num_chan = 1;
                    // ehr add for multirate iq to vac
                    int sample_rate = sample_rate3;
                    int block_size = block_size_vac2;
                    int latency = latency3;
                    if (vac2_output_iq)
                    {
                        num_chan = 2;
                        sample_rate = sample_rate1;
                        block_size = block_size1;
                        //latency = 250;
                    }
                    else if (vac2_stereo) num_chan = 2;
                    // ehr end				
                    vac2_rb_reset = true;
                    try
                    {
                        retval = StartAudio_NonJanus(ref callbackVAC2, (uint)block_size, sample_rate, host3, input_dev3,
                                                     output_dev3, num_chan, 1, latency);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The program is having trouble starting the VAC audio streams.\n" +
                            "Please examine the VAC related settings on the Setup Form -> Audio Tab and try again.",
                            "VAC2 Audio Stream Startup Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                }

            return retval;
        }

       public static unsafe bool StartAudio(ref PA19.PaStreamCallback callback,
                                              uint block_size, double sample_rate)
        {
            // System.Console.WriteLine("using Ozy/Janus callback");
            int rc;
            int no_send = 0;
            int sample_bits = 24;
            if (console.Force16bitIQ)
            {
                sample_bits = 16;
            }
            if (console.NoJanusSend)
            {
                no_send = 1;
            }
            rc = JanusAudio.StartAudio((int)sample_rate, (int)block_size, callback, sample_bits, no_send);
            if (rc != 0)
            {
                //System.Console.WriteLine("JanusAudio.StartAudio failed w/ rc: " + rc);
                if (rc == -101) // firmware version error; 
                {
                    string fw_err = JanusAudio.getFWVersionErrorMsg();
                    if (fw_err == null)
                    {
                        fw_err = "Bad Firmware levels";
                    }
                    MessageBox.Show(fw_err, "HPSDR Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    MessageBox.Show("Error starting HPSDR hardware, is it connected and powered?", "HPSDR Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        unsafe public static bool StartAudio_NonJanus(ref PA19.PaStreamCallback callback,
            uint block_size,
            double sample_rate,
            int host_api_index,
            int input_dev_index,
            int output_dev_index,
            int num_channels,
            int callback_num,
            int latency_ms)
        {
            empty_buffers = 0;

            int in_dev = PA19.PA_HostApiDeviceIndexToDeviceIndex(host_api_index, input_dev_index);
            int out_dev = PA19.PA_HostApiDeviceIndexToDeviceIndex(host_api_index, output_dev_index);

            var inparam = new PA19.PaStreamParameters();
            var outparam = new PA19.PaStreamParameters();

            inparam.device = in_dev;
            inparam.channelCount = num_channels;
#if(INTERLEAVED)
			inparam.sampleFormat = PA19.paFloat32;
#else
            inparam.sampleFormat = PA19.paFloat32 | PA19.paNonInterleaved;
#endif
            inparam.suggestedLatency = ((float)latency_ms / 1000);

            outparam.device = out_dev;
            outparam.channelCount = num_channels;
#if(INTERLEAVED)
			outparam.sampleFormat = PA19.paFloat32;
#else
            outparam.sampleFormat = PA19.paFloat32 | PA19.paNonInterleaved;
#endif
            outparam.suggestedLatency = ((float)latency_ms / 1000);

            int error = 0;

            switch (callback_num)
            {
                case 0: // VAC1
                    error = PA19.PA_OpenStream(out stream1, &inparam, &outparam, sample_rate, block_size, 0, callback, 0);
                    break;
                case 1: // VAC2
                    error = PA19.PA_OpenStream(out stream2, &inparam, &outparam, sample_rate, block_size, 0, callback, 1);
                    break;
            }

            if (error != 0)
            {
                PortAudioErrorMessageBox(error);
                return false;
            }

            switch (callback_num)
            {
                case 0: error = PA19.PA_StartStream(stream1); break;
                case 1: error = PA19.PA_StartStream(stream2); break;
            }

            if (error != 0)
            {
                PortAudioErrorMessageBox(error);
                return false;
            }

            return true;
        }

        private static void PortAudioErrorMessageBox(int error)
        {
            switch (error)
            {
                case (int)PA19.PaErrorCode.paInvalidDevice:
                    string s = "Whoops!  Looks like something has gone wrong in the\n" +
                        "Audio section.  Go look in the Setup Form -> Audio Tab to\n" +
                        "verify the settings there.";
                    if (vac_enabled) s += "  Since VAC is enabled, make sure\n" +
                         "you look at those settings as well.";
                    MessageBox.Show(s, "Audio Subsystem Error: Invalid Device",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show(PA19.PA_GetErrorText(error), "PortAudio Error: " + error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        unsafe public static void StopAudioVAC()
        {
            int error = 0;
            PA19.PA_AbortStream(stream1);
            error = PA19.PA_CloseStream(stream1);
            if (error != 0) PortAudioErrorMessageBox(error);
        }

        unsafe public static void StopAudioVAC2()
        {
            int error = 0;
            PA19.PA_AbortStream(stream2);
            error = PA19.PA_CloseStream(stream2);
            if (error != 0) PortAudioErrorMessageBox(error);
        }

        #endregion

        #region Scope Stuff

        private static int scope_samples_per_pixel = 512;

        public static int ScopeSamplesPerPixel
        {
            get { return scope_samples_per_pixel; }
            set { scope_samples_per_pixel = value; }
        }

        private static int scope_display_width = 704;

        public static int ScopeDisplayWidth
        {
            get { return scope_display_width; }
            set { scope_display_width = value; }
        }

        private static int scope_sample_index;
        private static int scope_pixel_index;
        private static float scope_pixel_min = float.MaxValue;
        private static float scope_pixel_max = float.MinValue;
        private static float[] scope_min;

        public static float[] ScopeMin
        {
            set { scope_min = value; }
        }

        public static float[] scope_max;

        public static float[] ScopeMax
        {
            set { scope_max = value; }
        }

        unsafe private static void DoScope(float* buf, int frameCount)
        {
            if (scope_min == null || scope_min.Length < scope_display_width)
            {
                if (Display.ScopeMin == null || Display.ScopeMin.Length < scope_display_width)
                    Display.ScopeMin = new float[scope_display_width];
                scope_min = Display.ScopeMin;
            }
            if (scope_max == null || scope_max.Length < scope_display_width)
            {
                if (Display.ScopeMax == null || Display.ScopeMax.Length < scope_display_width)
                    Display.ScopeMax = new float[scope_display_width];
                scope_max = Display.ScopeMax;
            }

            for (int i = 0; i < frameCount; i++)
            {
                if (Display.CurrentDisplayMode == DisplayMode.SCOPE)
                {
                    if (buf[i] < scope_pixel_min)
                        scope_pixel_min = buf[i];
                    if (buf[i] > scope_pixel_max)
                        scope_pixel_max = buf[i];
                }
                else
                {
                    scope_pixel_min = buf[i];
                    scope_pixel_max = buf[i];
                }


                scope_sample_index++;
                if (scope_sample_index >= scope_samples_per_pixel)
                {
                    scope_sample_index = 0;
                    scope_min[scope_pixel_index] = scope_pixel_min;
                    scope_max[scope_pixel_index] = scope_pixel_max;

                    scope_pixel_min = float.MaxValue;
                    scope_pixel_max = float.MinValue;

                    scope_pixel_index++;
                    if (scope_pixel_index >= scope_display_width)
                        scope_pixel_index = 0;
                }
            }
        }

        #endregion

        #region Scope2 Stuff

        //private static int scope_samples_per_pixel = 512;
        // public static int ScopeSamplesPerPixel
        // {
        //     get { return scope_samples_per_pixel; }
        //     set { scope_samples_per_pixel = value; }
        // }

        //private static int scope_display_width = 704;
        //public static int ScopeDisplayWidth
        // {
        //     get { return scope_display_width; }
        //     set { scope_display_width = value; }
        // }

        private static int scope2_sample_index;
        private static int scope2_pixel_index;
        // private static float scope2_pixel_min = float.MaxValue;
        private static float scope2_pixel_max = float.MinValue;
        // private static float[] scope2_min;
        // public static float[] Scope2Min
        // {
        //     set { scope2_min = value; }
        // }
        public static float[] scope2_max;

        public static float[] Scope2Max
        {
            set { scope2_max = value; }
        }

        unsafe private static void DoScope2(float* buf, int frameCount)
        {
            //  if (scope2_min == null || scope2_min.Length < scope_display_width)
            //  {
            //     if (Display.Scope2Min == null || Display.Scope2Min.Length < scope_display_width)
            //         Display.Scope2Min = new float[scope_display_width];
            //     scope2_min = Display.Scope2Min;
            // }
            if (scope2_max == null || scope2_max.Length < scope_display_width)
            {
                if (Display.Scope2Max == null || Display.Scope2Max.Length < scope_display_width)
                    Display.Scope2Max = new float[scope_display_width];
                scope2_max = Display.Scope2Max;
            }

            for (int i = 0; i < frameCount; i++)
            {
                // if (buf[i] < scope2_pixel_min) scope2_pixel_min = buf[i];
                // if (buf[i] > scope2_pixel_max) scope2_pixel_max = buf[i];
                // scope2_pixel_min = buf[i];
                scope2_pixel_max = buf[i];

                scope2_sample_index++;
                if (scope2_sample_index >= scope_samples_per_pixel)
                {
                    scope2_sample_index = 0;
                    //  scope2_min[scope2_pixel_index] = scope2_pixel_min;
                    scope2_max[scope2_pixel_index] = scope2_pixel_max;

                    //  scope2_pixel_min = float.MaxValue;
                    scope2_pixel_max = float.MinValue;

                    scope2_pixel_index++;
                    if (scope2_pixel_index >= scope_display_width)
                        scope2_pixel_index = 0;
                }
            }
        }

        #endregion
    }
}