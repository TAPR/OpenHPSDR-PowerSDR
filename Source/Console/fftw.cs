/*   FFTW  -- Copyright 2004 FlexRadio Systems  /*
 */

using System;
using System.Runtime.InteropServices;

using fftw_real = System.Double;

namespace PowerSDR.DSP
{
	/// <summary>
	/// Summary description for FFTW
	/// </summary>
	unsafe class FFTW
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct fftw_rader_data
		{
			fftw_plan_struct *plan;
			fftw_complex *omega;
			int g, ginv;
			int p, flags, refcount;
			fftw_rader_data *next;
			fftw_codelet_desc *cdesc;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct fftw_twiddle
		{
			int n;
			fftw_codelet_desc *cdesc;
			fftw_complex *twarray;
			fftw_twiddle *next;
			int refcnt;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct fftw_codelet_desc
		{
			char *name;				// name of the codelet
			codelet *code;			// pointer to the codelet itself
			int size;				// size of the codelet
			fftw_direction dir;		// direction
			fftw_node_type type;	// TWIDDLE or NO_TWIDDLE 
			int signature;			// unique id 
			int ntwiddle;			// number of twiddle factors
			int *twiddle_order;		// array that determines the order 
									// in which the codelet expects
									// the twiddle factors
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct fftw_complex
		{
			fftw_real re, im;
		}

		public delegate void codelet();
		public delegate void fftw_notw_codelet(fftw_complex *c1, fftw_complex *c2, int i1, int i2);
		public delegate void fftw_twiddle_codelet(fftw_complex *c1, fftw_complex *c2, int i1, int i2, int i3);
		public delegate void fftw_generic_codelet(fftw_complex *c1, fftw_complex *c2, int i1, int i2, int i3, int i4);
		public delegate void fftw_rader_codelet(fftw_complex *c1, fftw_complex *c2, int i1,	int i2, int i3, fftw_rader_data *r);
		public delegate void fftw_real2hc_codelet(fftw_real *r1, fftw_real *r2, fftw_real *r3, int i1, int i2, int i3);
		public delegate void fftw_hc2real_codelet(fftw_real *r1, fftw_real *r2,	fftw_real *r3, int i1, int i2, int i3);
		public delegate void fftw_hc2hc_codelet(fftw_real *r1, fftw_complex *c1, int i1, int i2, int i3);
		public delegate void fftw_rgeneric_codelet(fftw_real *r1, fftw_complex *c1, int i1,	int i2, int i3, int i4);

		public enum fftw_direction
		{
			FFTW_FORWARD = -1,
			FFTW_BACKWARD = 1
		}

		public enum fftw_recurse_kind
		{
			FFTW_NORMAL_RECURSE = 0,
			FFTW_VECTOR_RECURSE = 1
		}

		public enum fftw_node_type 
		{
			FFTW_NOTW, FFTW_TWIDDLE, FFTW_GENERIC, FFTW_RADER,
			FFTW_REAL2HC, FFTW_HC2REAL, FFTW_HC2HC, FFTW_RGENERIC
		}

		[StructLayout(LayoutKind.Explicit)]
			public struct fftw_plan_node
		{
			[FieldOffset(0)] 
			fftw_node_type type;

			/* nodes of type FFTW_NOTW */
			[FieldOffset(1)] 
				struct notw
			{
				int size;
				fftw_notw_codelet *codelet;
				fftw_codelet_desc *codelet_desc;
			}

			/* nodes of type FFTW_TWIDDLE */
			[FieldOffset(1)] 
				struct twiddle
			{
				int size;
				fftw_twiddle_codelet *codelet;
				fftw_twiddle *tw;
				fftw_plan_node *recurse;
				fftw_codelet_desc *codelet_desc;
			}

			/* nodes of type FFTW_GENERIC */
			[FieldOffset(1)] 
				struct generic
			{
				int size;
				fftw_generic_codelet *codelet;
				fftw_twiddle *tw;
				fftw_plan_node *recurse;
			}

			/* nodes of type FFTW_RADER */
			[FieldOffset(1)] 
				struct rader 
			{
				int size;
				fftw_rader_codelet *codelet;
				fftw_rader_data *rader_data;
				fftw_twiddle *tw;
				fftw_plan_node *recurse;
			}

			/* nodes of type FFTW_REAL2HC */
			[FieldOffset(1)] 
				struct real2hc
			{
				int size;
				fftw_real2hc_codelet *codelet;
				fftw_codelet_desc *codelet_desc;
			}

			/* nodes of type FFTW_HC2REAL */
			[FieldOffset(1)] 
				struct hc2real
			{
				int size;
				fftw_hc2real_codelet *codelet;
				fftw_codelet_desc *codelet_desc;
			}

			/* nodes of type FFTW_HC2HC */
			[FieldOffset(1)] 
				struct hc2hc
			{
				int size;
				fftw_direction dir;
				fftw_hc2hc_codelet *codelet;
				fftw_twiddle *tw;
				fftw_plan_node *recurse;
				fftw_codelet_desc *codelet_desc;
			}

			/* nodes of type FFTW_RGENERIC */
			[FieldOffset(1)] 
				struct rgeneric
			{
				int size;
				fftw_direction dir;
				fftw_rgeneric_codelet *codelet;
				fftw_twiddle *tw;
				fftw_plan_node *recurse;
			}
			[FieldOffset(2)] 
			int refcnt;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct fftw_plan_struct 
		{
			int n;
			int refcnt;
			fftw_direction dir;
			int flags;
			int wisdom_signature;
			fftw_node_type wisdom_type;
			fftw_plan_struct *next;
			fftw_plan_node *root;
			double cost;
			fftw_recurse_kind recurse_kind;
			int vector_size;
		}

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_create_plan_specific();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_create_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_print_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_destroy_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_one();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_die();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_malloc();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_free();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_check_memory_leaks();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_print_max_memory_usage();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_malloc_hook();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_free_hook();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_die_hook();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_forget_wisdom();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_export_wisdom();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_import_wisdom();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_export_wisdom_to_file();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_import_wisdom_from_file();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_export_wisdom_to_string();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_import_wisdom_from_string();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_fprint_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw2d_create_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw3d_create_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd_create_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw2d_create_plan_specific();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw3d_create_plan_specific();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd_create_plan_specific();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd_destroy_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd_fprint_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd_print_plan();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftwnd_one();

		[DllImport("FFTW2dll.dll")]
		public static extern void GetPerfTime();

		[DllImport("FFTW2dll.dll")]
		public static extern void GetPerfSec();

		[DllImport("FFTW2dll.dll")]
		public static extern void fftw_sizeof_fftw_real();
	}
}