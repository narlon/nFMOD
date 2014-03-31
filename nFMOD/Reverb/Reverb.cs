using System;
using System.Runtime.InteropServices;
using System.Security;

namespace nFMOD.Reverb
{
	public class Reverb : Handle
	{
		
		#region Create/Release
		
		private Reverb()
		{
		}
		internal Reverb (IntPtr hnd) : base()
		{
			this.SetHandle (hnd);
		}

		protected override bool ReleaseHandle ()
		{
			if (this.IsInvalid)
				return true;
			
			Release (this.handle);
			this.SetHandleAsInvalid ();
			
			return true;
		}

		[DllImport("fmodex", EntryPoint = "FMOD_Reverb_Release"), SuppressUnmanagedCodeSecurity]
		private static extern Error.Code Release (IntPtr reverb);
		
		#endregion
		
		public Properties Properties {
			get {
				Properties Val = Properties.Generic;
				Error.Code ReturnCode = GetProperties(this.DangerousGetHandle(), ref Val);
				Error.Errors.ThrowError(ReturnCode);
				
				return Val;
			}
			
			set {
				Error.Code ReturnCode = SetProperties(this.DangerousGetHandle(), ref value);
				Error.Errors.ThrowError(ReturnCode);
			}
		}
		
		[DllImport("fmodex", EntryPoint = "FMOD_Reverb_SetProperties"), SuppressUnmanagedCodeSecurity]
		private static extern Error.Code SetProperties (IntPtr reverb, ref Properties properties);

		[DllImport("fmodex", EntryPoint = "FMOD_Reverb_GetProperties"), SuppressUnmanagedCodeSecurity]
		private static extern Error.Code GetProperties (IntPtr reverb, ref Properties properties);
		
		
		//TODO Implement extern funcitons
		
		/*

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_Set3DAttributes (IntPtr reverb, ref VECTOR position, float mindistance, float maxdistance);

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_Get3DAttributes (IntPtr reverb, ref VECTOR position, ref float mindistance, ref float maxdistance);

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_SetActive (IntPtr reverb, int active);

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_GetActive (IntPtr reverb, ref int active);

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_SetUserData (IntPtr reverb, IntPtr userdata);

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_GetUserData (IntPtr reverb, ref IntPtr userdata);

		[DllImport(VERSION.dll), SuppressUnmanagedCodeSecurity]
		private static extern RESULT FMOD_Reverb_GetMemoryInfo (IntPtr reverb, uint memorybits, uint event_memorybits, ref uint memoryused, ref MEMORY_USAGE_DETAILS memoryused_details);
		*/
	}
}
