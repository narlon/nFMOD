using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace nFMOD
{
    public class SoundSystem : Handle, ISpectrumWave
    {
        #region Externs
        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Create"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Create(ref IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Release"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Release(IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Init"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Init(IntPtr system, int Maxchannels, InitFlags Flags, IntPtr Extradriverdata);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Close"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CloseSystem(IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Update"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Update(IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_UpdateFinished"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode UpdateFinished(IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetNetworkProxy"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetNetworkProxy(IntPtr system, string proxy);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetNetworkProxy"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetNetworkProxy(IntPtr system, StringBuilder proxy, int proxylen);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetNetworkTimeout"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetNetworkTimeout(IntPtr system, int timeout);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetNetworkTimeout"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetNetworkTimeout(IntPtr system, ref int timeout);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateDSP"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateDSP(IntPtr system, ref Dsp.DSPDescription description, ref IntPtr dsp);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateDSPByType"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateDspByType(IntPtr system, DspType type, ref IntPtr dsp);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_PlayDSP"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode PlayDsp(IntPtr system, ChannelIndex channelid, IntPtr dsp, bool paused, ref IntPtr channel);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_AddDSP"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode AddDSP(IntPtr system, IntPtr dsp, ref IntPtr connection);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_LockDSP"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode LockDSP(IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_UnlockDSP"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode UnlockDSP(IntPtr system);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDSPClock"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDSPClock(IntPtr system, ref uint hi, ref uint lo);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetSpectrum"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetSpectrum(IntPtr system, [MarshalAs(UnmanagedType.LPArray)] float[] spectrumarray, int numvalues, int channeloffset, FFTWindow windowtype);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetWaveData"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetWaveData(IntPtr system, [MarshalAs(UnmanagedType.LPArray)] float[] wavearray, int numvalues, int channeloffset);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateReverb"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateReverb(IntPtr system, ref IntPtr reverb);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetReverbProperties"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetReverbProperties(IntPtr system, ref ReverbProperties prop);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetReverbProperties"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetReverbProperties(IntPtr system, ref ReverbProperties prop);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetReverbAmbientProperties"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetReverbAmbientProperties(IntPtr system, ref ReverbProperties prop);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetReverbAmbientProperties"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetReverbAmbientProperties(IntPtr system, ref ReverbProperties prop);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetRecordNumDrivers"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetRecordNumDrivers(IntPtr system, out int numdrivers);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetRecordDriverInfo"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetRecordDriverInfo(IntPtr system, int id, StringBuilder name, int namelen, out Guid guid);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetRecordDriverInfoW"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode FMOD_System_GetRecordDriverInfoW(IntPtr system, int id, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int namelen, out Guid guid);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetRecordDriverCaps"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetRecordDriverCaps(IntPtr system, int id, out Capabilities caps, out int minfrequency, out int maxfrequency);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetNumDrivers"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetNumDrivers(IntPtr system, out int Numdrivers);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDriverInfo"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDriverInfo(IntPtr system, int id, StringBuilder name, int namelen, out Guid guid);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDriverInfoW"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDriverInfoW(IntPtr system, int id, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int namelen, out Guid guid);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDriverCaps"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDriverCaps(IntPtr system, int id, out Capabilities caps, out int minfrequency, out int maxfrequency, out SpeakerMode controlpanelspeakermode);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetDriver"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetDriver(IntPtr system, int driver);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDriver"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDriver(IntPtr system, out int driver);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDSPHead"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDSPHead(IntPtr system, ref IntPtr dsp);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetHardwareChannels"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetHardwareChannels(IntPtr system, int min2d, int max2d, int min3d, int max3d);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetHardwareChannels"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetHardwareChannels(IntPtr system, ref int num2d, ref int num3d, ref int total);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetSoftwareChannels"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetSoftwareChannels(IntPtr system, int numsoftwarechannels);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetSoftwareChannels"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetSoftwareChannels(IntPtr system, ref int numsoftwarechannels);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetSoftwareFormat"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetSoftwareFormat(IntPtr system, int samplerate, Sound.Format format, int numoutputchannels, int maxinputchannels, Resampler resamplemethod);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetSoftwareFormat"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetSoftwareFormat(IntPtr system, ref int samplerate, ref Sound.Format format, ref int numoutputchannels, ref int maxinputchannels, ref Resampler resamplemethod, ref int bits);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetDSPBufferSize"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetDSPBufferSize(IntPtr system, uint bufferlength, int numbuffers);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetDSPBufferSize"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetDSPBufferSize(IntPtr system, ref uint bufferlength, ref int numbuffers);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetFileSystem"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetFileSystem(IntPtr system, File_OpenDelegate useropen, File_CloseDelegate userclose, File_ReadDelegate userread, File_SeekDelegate userseek, File_AsyncReadDelegate userasyncread, File_AsyncCancelDelegate userasynccancel, int blockalign);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_AttachFileSystem"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode AttachFileSystem(IntPtr system, File_OpenDelegate useropen, File_CloseDelegate userclose, File_ReadDelegate userread, File_SeekDelegate userseek);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetPluginPath"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetPluginPath(IntPtr system, string path);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_LoadPlugin"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode LoadPlugin(IntPtr system, string filename, ref uint handle, uint priority);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_UnloadPlugin"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode UnloadPlugin(IntPtr system, uint handle);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetNumPlugins"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetNumPlugins(IntPtr system, Plugin.Type plugintype, ref int numplugins);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetPluginHandle"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetPluginHandle(IntPtr system, Plugin.Type plugintype, int index, ref uint handle);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetPluginInfo"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetPluginInfo(IntPtr system, uint handle, ref PluginType plugintype, StringBuilder name, int namelen, ref uint version);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateDSPByPlugin"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateDSPByPlugin(IntPtr system, uint handle, ref IntPtr dsp);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateCodec"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateCodec(IntPtr system, IntPtr description, uint priority);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetOutputByPlugin"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetOutputByPlugin(IntPtr system, uint handle);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetOutputByPlugin"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetOutputByPlugin(IntPtr system, ref uint handle);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetAdvancedSettings"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetAdvancedSettings(IntPtr system, ref AdvancedSettings settings);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetAdvancedSettings"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetAdvancedSettings(IntPtr system, ref AdvancedSettings settings);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetSpeakerMode"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetSpeakerMode(IntPtr system, SpeakerMode speakermode);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetSpeakerMode"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetSpeakerMode(IntPtr system, ref SpeakerMode speakermode);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Set3DRolloffCallback"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Set3DRolloffCallback(IntPtr system, CB_3D_RollOffDelegate callback);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Set3DSpeakerPosition"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Set3DSpeakerPosition(IntPtr system, Speaker speaker, float x, float y, int active);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Get3DSpeakerPosition"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Get3DSpeakerPosition(IntPtr system, Speaker speaker, ref float x, ref float y, ref int active);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Set3DSettings"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Set3DSettings(IntPtr system, float dopplerscale, float distancefactor, float rolloffscale);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Get3DSettings"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Get3DSettings(IntPtr system, ref float dopplerscale, ref float distancefactor, ref float rolloffscale);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Set3DNumListeners"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Set3DNumListeners(IntPtr system, int numlisteners);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Get3DNumListeners"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Get3DNumListeners(IntPtr system, ref int numlisteners);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Set3DListenerAttributes"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Set3DListenerAttributes(IntPtr system, int listener, ref Vector3 pos, ref Vector3 vel, ref Vector3 forward, ref Vector3 up);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_Get3DListenerAttributes"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Get3DListenerAttributes(IntPtr system, int listener, ref Vector3 pos, ref Vector3 vel, ref Vector3 forward, ref Vector3 up);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetFileBufferSize"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetFileBufferSize(IntPtr system, int sizebytes);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetFileBufferSize"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetFileBufferSize(IntPtr system, ref int sizebytes);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetStreamBufferSize"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetStreamBufferSize(IntPtr system, uint filebuffersize, TimeUnit filebuffersizetype);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetStreamBufferSize"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetStreamBufferSize(IntPtr system, ref uint filebuffersize, ref TimeUnit filebuffersizetype);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetOutputHandle"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetOutputHandle(IntPtr system, ref IntPtr handle);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetCPUUsage"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetCPUUsage(IntPtr system, ref float dsp, ref float stream, ref float geometry, ref float update, ref float total);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetSoundRAM"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetSoundRAM(IntPtr system, ref int currentalloced, ref int maxalloced, ref int total);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetNumCDROMDrives"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetNumCDROMDrives(IntPtr system, ref int numdrives);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetCDROMDriveName"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetCDROMDriveName(IntPtr system, int drive, StringBuilder drivename, int drivenamelen, StringBuilder scsiname, int scsinamelen, StringBuilder devicename, int devicenamelen);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetChannel"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetChannel(IntPtr system, int channelid, ref IntPtr channel);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetMasterChannelGroup"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetMasterChannelGroup(IntPtr system, ref IntPtr channelgroup);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetMasterSoundGroup"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetMasterSoundGroup(IntPtr system, ref IntPtr soundgroup);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateGeometry"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateGeometry(IntPtr system, int maxpolygons, int maxvertices, ref IntPtr geometry);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetGeometrySettings"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetGeometrySettings(IntPtr system, float maxworldsize);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetGeometrySettings"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetGeometrySettings(IntPtr system, ref float maxworldsize);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_LoadGeometry"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode LoadGeometry(IntPtr system, IntPtr data, int datasize, ref IntPtr geometry);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetGeometryOcclusion"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetGeometryOcclusion(IntPtr system, ref Vector3 listener, ref Vector3 source, ref float direct, ref float reverb);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetUserData"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetUserData(IntPtr system, IntPtr userdata);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetUserData"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetUserData(IntPtr system, ref IntPtr userdata);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetMemoryInfo"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetMemoryInfo(IntPtr system, uint memorybits, uint event_memorybits, ref uint memoryused, ref MemoryUsageDetails memoryused_details);


        //Old Methods

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_SetDSPBufferSize"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode SetDSPBufferSize(IntPtr system, int Bufferlength, int Numbuffers);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetDSPBufferSize"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetDSPBufferSize(IntPtr system, ref int Bufferlength, ref int Numbuffers);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_SetFileSystem"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode SetFileSystem(IntPtr system, int useropen, int userclose, int userread, int userseek, int Buffersize);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_AttachFileSystem"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode AttachFileSystem(IntPtr system, int useropen, int userclose, int userread, int userseek);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_LoadPlugin"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode LoadPlugin(IntPtr system, string Filename, ref int Handle, int Priority);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_UnloadPlugin"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode UnloadPlugin(IntPtr system, int Handle);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetPluginHandle"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetPluginHandle(IntPtr system, Plugin.Type Plugintype, int Index, ref int Handle);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetPluginInfo"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetPluginInfo(IntPtr system, int Handle, ref Plugin.Type Plugintype, ref byte name, int namelen, ref int version);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_SetOutputByPlugin"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode SetOutputByPlugin(IntPtr system, int Handle);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetOutputByPlugin"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetOutputByPlugin(IntPtr system, ref int Handle);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_CreateDSPByPlugin"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode CreateDSPByPlugin(IntPtr system, int Handle, ref int Dsp);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_CreateCodec"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode CreateCodec(IntPtr system, int CodecDescription);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_Set3DRolloffCallback"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode Set3DRolloffCallback(IntPtr system, int Callback);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_Get3DSpeakerPosition"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode Get3DSpeakerPosition(IntPtr system, ref Speaker speaker, ref float X, ref float Y, ref int active);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_SetStreamBufferSize"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode SetStreamBufferSize(IntPtr system, int Filebuffersize, TimeUnit Filebuffersizetype);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetStreamBufferSize"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetStreamBufferSize(IntPtr system, ref int Filebuffersize, ref TimeUnit Filebuffersizetype);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetOutputHandle"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetOutputHandle(IntPtr system, ref int Handle);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetCDROMDriveName"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetCDROMDriveName(IntPtr system, int Drive, ref byte Drivename, int Drivenamelen, ref byte Scsiname, int Scsinamelen, ref byte Devicename, int Devicenamelen);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_CreateDSP"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode CreateDSP(IntPtr system, ref Dsp.DSPDescription description, ref int Dsp);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_CreateDSPByType"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode CreateDSPByType(IntPtr system, DspType dsptype, ref int Dsp);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_CreateDSPByIndex"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode CreateDSPByIndex(IntPtr system, int Index, ref int Dsp);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_PlayDSP"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode PlayDSP(IntPtr system, ChannelIndex channelid, int Dsp, int paused, ref int channel);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetChannel"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetChannel(IntPtr system, int channelid, ref int channel);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetMasterChannelGroup"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetMasterChannelGroup(IntPtr system, ref int Channelgroup);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetMasterSoundGroup"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetMasterSoundGroup(IntPtr system, ref int soundgroup);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_CreateGeometry"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode CreateGeometry(IntPtr system, int MaxPolygons, int MaxVertices, ref int Geometryf);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_LoadGeometry"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode LoadGeometry(IntPtr system, int Data, int DataSize, ref int Geometry);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_SetUserData"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode SetUserData(IntPtr system, int userdata);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "FMOD_System_GetUserData"), SuppressUnmanagedCodeSecurity]
        public static extern ErrorCode GetUserData(IntPtr system, ref int userdata);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetCallback"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode FMOD_System_SetCallback(IntPtr system, SystemDelegate callback);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetVersion"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetVersion(IntPtr system, ref uint version);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_SetOutput"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetOutput(IntPtr system, OutputType output);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetOutput"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetOutput(IntPtr system, ref OutputType output);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetChannelsPlaying"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetChannelsPlaying(IntPtr system, ref int channels);

        
        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, EntryPoint = "FMOD_System_CreateChannelGroup"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateChannelGroup(IntPtr system, string name, ref IntPtr channelgroup);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, EntryPoint = "FMOD_System_CreateSoundGroup"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateSoundGroup(IntPtr system, string name, ref IntPtr soundgroup);
        #endregion

        public SoundSystem()
        {
            IntPtr SoundSystemHandle = IntPtr.Zero;

            Errors.ThrowIfError(Create(ref SoundSystemHandle));

            SetHandle(SoundSystemHandle);

            if (Version < Common.FMOD_DLL_MINIMUM_VERSION) {
                Release(handle);
                SetHandleAsInvalid();
                throw new NotSupportedException("The current version of Fmod isnt supported.");
            }
        }

        protected override bool ReleaseHandle()
        {
            if (IsInvalid)
                return true;

            CloseSystem(handle);
            Release(handle);
            SetHandleAsInvalid();

            return true;
        }

        public void Init(int Maxchannels = 32, InitFlags Flags = InitFlags.Normal | InitFlags.RightHanded3D)
        {
            Init(Maxchannels, Flags, IntPtr.Zero);
        }

        public void Init(int Maxchannels, InitFlags Flags, IntPtr Extradriverdata)
        {
            Errors.ThrowIfError(Init(handle, Maxchannels, Flags, Extradriverdata));
        }

        public void CloseSystem()
        {
            CloseSystem(DangerousGetHandle());
        }

        #region Events
        public delegate void SystemDelegate(SoundSystem system); //TODO Implement SoundSystem Events.        

        /// <summary>
        /// Called when the enumerated list of devices has changed.
        /// </summary>
        public event SystemDelegate DeviceListChanged;

        /// <summary>
        /// Called from System::update when an output device has been lost
        /// due to control panel parameter changes and FMOD cannot automatically recover.
        /// </summary>
        public event SystemDelegate DeviceLost;

        /// <summary>
        /// Called directly when a memory allocation fails somewhere in FMOD.
        /// </summary>
        public event SystemDelegate MemoryAllocationFailed;

        /// <summary>
        /// Called directly when a thread is created.
        /// </summary>
        public event SystemDelegate ThreadCreated;

        /// <summary>
        /// Called when a bad connection was made with DSP::addInput.
        /// Usually called from mixer thread because that is where the connections are made.
        /// </summary>
        public event SystemDelegate BadDspConnection;

        /// <summary>
        /// Called when too many effects were added exceeding the maximum tree depth of 128.
        /// This is most likely caused by accidentally adding too many DSP effects.
        /// Usually called from mixer thread because that is where the connections are made.
        /// </summary>
        public event SystemDelegate BadDspLevel;

        private ErrorCode HandleCallback(IntPtr systemraw, CallbackType type, IntPtr commanddata1, IntPtr commanddata2)
        {
            return ErrorCode.OK;
        }       
        #endregion

        #region Properties wrapping non-managed code
        public string NetworkProxy
        {
            get
            {
                var result = new StringBuilder(255);
                Errors.ThrowIfError(GetNetworkProxy(DangerousGetHandle(), result, result.Capacity));
                return result.ToString();
            }
            set
            {
                Errors.ThrowIfError(SetNetworkProxy(DangerousGetHandle(), value));
            }
        }

        public int NetworkTimeout
        {
            get
            {
                int result = 0;
                Errors.ThrowIfError(GetNetworkTimeout(DangerousGetHandle(), ref result));
                return result;
            }
            set
            {
                Errors.ThrowIfError(SetNetworkTimeout(DangerousGetHandle(), value));
            }
        }                
        
        public uint Version
        {
            get
            {
                uint result = 0;
                Errors.ThrowIfError(GetVersion(DangerousGetHandle(), ref result));
                return result;
            }
        }        
       
        public OutputType Output
        {
            get
            {
                var result = OutputType.Unknown;
                Errors.ThrowIfError(GetOutput(DangerousGetHandle(), ref result));
                return result;
            }
            set
            {
                Errors.ThrowIfError(SetOutput(DangerousGetHandle(), value));
            }
        }

        public bool IsPlaying
        {
            get
            {
                return ChannelsPlaying <= 0;
            }
        }

        public int ChannelsPlaying
        {
            get
            {
                int result = 0;
                Errors.ThrowIfError(GetChannelsPlaying(DangerousGetHandle(), ref result));
                return result;
            }
        }
        #endregion
      
        #region Sound
        public Sound CreateSound(string path)
        {
            return CreateSound(path, Mode.Default);
        }

        public Sound CreateSound(string path, Mode mode)
        {
            IntPtr result = IntPtr.Zero;
            Errors.ThrowIfError(CreateSound(DangerousGetHandle(), path, mode, 0, ref result));
            return new Sound(result);
        }

        public Sound CreateSound(string path, Mode mode, Sound.SoundInfo exinfo)
        {
            IntPtr result = IntPtr.Zero;
            Errors.ThrowIfError(CreateSound(DangerousGetHandle(), path, mode, ref exinfo, ref result));
            return new Sound(result);
        }

        public Sound CreateSound(byte[] data, Mode mode = Mode.Default)
        {
            IntPtr SoundHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateSound(DangerousGetHandle(), data, mode, 0, ref SoundHandle));
            return new Sound(SoundHandle);
        }

        public Sound CreateSound(byte[] data, Mode mode, Sound.SoundInfo exinfo)
        {
            IntPtr SoundHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateSound(DangerousGetHandle(), data, mode, ref exinfo, ref SoundHandle));
            return new Sound(SoundHandle);
        }

        public Channel PlaySound(Sound snd)
        {
            return PlaySound(snd, false);
        }

        public Channel PlaySound(Sound snd, bool paused)
        {
            IntPtr ChannelHandle = IntPtr.Zero;
            Errors.ThrowIfError(PlaySound(DangerousGetHandle(), ChannelIndex.Free, snd.DangerousGetHandle(), paused, ref ChannelHandle));
            return new Channel(ChannelHandle);
        }

        private void PlaySound(Sound snd, bool paused, Channel chn)
        {
            //TODO FIXME The handle is changed most of the time on some system.
            //Only use the other metods to be safe.

            IntPtr channel = chn.DangerousGetHandle();
            Errors.ThrowIfError(PlaySound(DangerousGetHandle(), ChannelIndex.Reuse, snd.DangerousGetHandle(), paused, ref channel));
            //This can't really happend.
            //Check just in case...
            if (chn.DangerousGetHandle() == channel)
                throw new Exception("Channel handle got changed by Fmod.");
        }

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, EntryPoint = "FMOD_System_CreateSound"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateSound(IntPtr system, string name, Mode mode, ref Sound.SoundInfo exinfo, ref IntPtr Sound);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, EntryPoint = "FMOD_System_CreateSound"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateSound(IntPtr system, string name, Mode mode, int exinfo, ref IntPtr sound);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateSound"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateSound(IntPtr system, byte[] data, Mode mode, ref Sound.SoundInfo exinfo, ref IntPtr sound);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateSound"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateSound(IntPtr system, byte[] data, Mode mode, int exinfo, ref IntPtr sound);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_PlaySound"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode PlaySound(IntPtr system, ChannelIndex channelid, IntPtr Sound, bool paused, ref IntPtr channel);

        #endregion

        #region Stream

        public Sound CreateStream(string path, Mode mode)
        {
            IntPtr SoundHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateStream(DangerousGetHandle(), path, mode, 0, ref SoundHandle));
            return new Sound(SoundHandle);
        }

        public Sound CreateStream(string path, Mode mode, Sound.SoundInfo exinfo)
        {
            IntPtr SoundHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateStream(DangerousGetHandle(), path, mode, ref exinfo, ref SoundHandle));
            return new Sound(SoundHandle);
        }

        public Sound CreateStream(byte[] data, Mode mode)
        {
            IntPtr SoundHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateStream(DangerousGetHandle(), data, mode, 0, ref SoundHandle));
            return new Sound(SoundHandle);
        }

        public Sound CreateStream(byte[] data, Mode mode, Sound.SoundInfo exinfo)
        {
            IntPtr SoundHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateStream(DangerousGetHandle(), data, mode, ref exinfo, ref SoundHandle));
            return new Sound(SoundHandle);
        }

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, EntryPoint = "FMOD_System_CreateStream"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateStream(IntPtr system, string name, Mode mode, ref Sound.SoundInfo exinfo, ref IntPtr Sound);

        [DllImport(Common.FMOD_DLL_NAME, CharSet = CharSet.Ansi, EntryPoint = "FMOD_System_CreateStream"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateStream(IntPtr system, string name, Mode mode, int exinfo, ref IntPtr sound);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateStream"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateStream(IntPtr system, byte[] data, Mode mode, ref Sound.SoundInfo exinfo, ref IntPtr sound);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_CreateStream"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode CreateStream(IntPtr system, byte[] data, Mode mode, int exinfo, ref IntPtr sound);

        #endregion

        #endregion

        #region Recording

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_GetRecordPosition"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetRecordPosition(IntPtr system, int id, ref uint position);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_RecordStart"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode RecordStart(IntPtr system, int id, IntPtr sound, int loop);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_RecordStop"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode RecordStop(IntPtr system, int id);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_System_IsRecording"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode IsRecording(IntPtr system, int id, ref int recording);

        #endregion

        #region DSP
        public Dsp CreateDSP(ref Dsp.DSPDescription description)
        {
            IntPtr DspHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateDSP(DangerousGetHandle(), ref description, ref DspHandle));
            return new Dsp(DspHandle);
        }

        public Dsp CreateDspByType(DspType type)
        {
            IntPtr DspHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateDspByType(DangerousGetHandle(), type, ref DspHandle));
            return new Dsp(DspHandle);
        }

        public Channel PlayDsp(Dsp dsp)
        {
            return PlayDsp(dsp, false);
        }

        public Channel PlayDsp(Dsp dsp, bool paused)
        {
            IntPtr ChannelHandle = IntPtr.Zero;
            Errors.ThrowIfError(PlayDsp(DangerousGetHandle(), ChannelIndex.Free, dsp.DangerousGetHandle(), paused, ref ChannelHandle));
            return new Channel(ChannelHandle);
        }

        public void PlayDsp(Dsp dsp, bool paused, Channel chn)
        {
            IntPtr channel = chn.DangerousGetHandle();
            Errors.ThrowIfError(PlayDsp(DangerousGetHandle(), ChannelIndex.Reuse, dsp.DangerousGetHandle(), paused, ref channel));
            //TODO: This can't really happend.
            //Check just in case...
            if (chn.DangerousGetHandle() != channel)
                throw new Exception("Channel handle got changed by Fmod.");
        }

        public DspConnection AddDsp(Dsp dsp)
        {
            IntPtr ConnectionHandle = IntPtr.Zero;
            Errors.ThrowIfError(AddDSP(DangerousGetHandle(), dsp.DangerousGetHandle(), ref ConnectionHandle));
            return new DspConnection(ConnectionHandle);
        }

        public void LockDSP()
        {
            Errors.ThrowIfError(LockDSP(DangerousGetHandle()));
        }

        public void UnlockDSP()
        {
            Errors.ThrowIfError(UnlockDSP(DangerousGetHandle()));
        }

        public ulong DSPClock
        {
            get
            {
                uint hi = 0, low = 0;
                Errors.ThrowIfError(GetDSPClock(DangerousGetHandle(), ref hi, ref low));
                return (hi << 32) | low;
            }
        }
        #endregion

        #region Reverb
        public Reverb CreateReverb()
        {
            IntPtr ReverbHandle = IntPtr.Zero;
            Errors.ThrowIfError(CreateReverb(DangerousGetHandle(), ref ReverbHandle));
            return new Reverb(ReverbHandle);
        }

        public ReverbProperties ReverbProperties
        {
            get
            {
                var result = ReverbProperties.Generic;
                Errors.ThrowIfError(GetReverbProperties(DangerousGetHandle(), ref result));
                return result;
            }
            set
            {
                Errors.ThrowIfError(SetReverbProperties(DangerousGetHandle(), ref value));
            }
        }

        public ReverbProperties ReverbAmbientProperties
        {
            get
            {
                var Val = ReverbProperties.Generic;
                Errors.ThrowIfError(GetReverbAmbientProperties(DangerousGetHandle(), ref Val));
                return Val;
            }

            set
            {
                Errors.ThrowIfError(SetReverbAmbientProperties(DangerousGetHandle(), ref value));
            }
        }
        #endregion

        #region Record driver
        public IEnumerable<RecordDriverDTO> RecordDrivers
        {
            get
            {
                int Numb = NumberRecordDrivers;
                for (int i = 0; i < Numb; i++) {
                    yield return GetRecordDriver(i);
                }
            }
        }

        private int NumberRecordDrivers
        {
            get
            {
                int numdrivers;
                Errors.ThrowIfError(GetRecordNumDrivers(DangerousGetHandle(), out numdrivers));
                return numdrivers;
            }
        }

        private void GetRecordDriverInfo(int Id, out string Name, out Guid DriverGuid)
        {
            var str = new StringBuilder(255);
            Errors.ThrowIfError(GetRecordDriverInfo(DangerousGetHandle(), Id, str, str.Capacity, out DriverGuid));
            Name = str.ToString();
        }

        private void GetRecordDriverCapabilities(int id, out Capabilities caps, out int minfrequency, out int maxfrequency)
        {
            Errors.ThrowIfError(GetRecordDriverCaps(DangerousGetHandle(), id, out caps, out minfrequency, out maxfrequency));
        }

        private RecordDriverDTO GetRecordDriver(int Id)
        {
            Guid DriverGuid;
            string DriverName;
            GetRecordDriverInfo(Id, out DriverName, out DriverGuid);

            Capabilities caps;
            int minfrequency, maxfrequency;
            GetRecordDriverCapabilities(Id, out caps, out minfrequency, out maxfrequency);

            return new RecordDriverDTO {
                Id = Id,
                Name = DriverName,
                Guid = DriverGuid,

                Capabilities = caps,
                MinimumFrequency = minfrequency,
                MaximumFrequency = maxfrequency,
            };
        }
        #endregion

        #region Output driver
        public OutputDriverDTO OutputDriver
        {
            get
            {
                int driver;
                Errors.ThrowIfError(GetDriver(DangerousGetHandle(), out driver));
                return GetOutputDriver(driver);
            }
            set
            {
                Errors.ThrowIfError(SetDriver(DangerousGetHandle(), value.Id));
            }
        }

        public IEnumerable<OutputDriverDTO> OutputDrivers
        {
            get
            {
                int Numb = NumberOutputDrivers;
                for (int i = 0; i < Numb; i++) {
                    yield return GetOutputDriver(i);
                }
            }
        }

        private int NumberOutputDrivers
        {
            get
            {
                int numdrivers;
                Errors.ThrowIfError(GetNumDrivers(DangerousGetHandle(), out numdrivers));
                return numdrivers;
            }
        }

        private void GetOutputDriverInfo(int Id, out string Name, out Guid DriverGuid)
        {
            var str = new StringBuilder(255);
            Errors.ThrowIfError(GetDriverInfo(DangerousGetHandle(), Id, str, str.Capacity, out DriverGuid));
            Name = str.ToString();
        }

        private void GetOutputDriverCapabilities(int Id, out Capabilities caps, out int minfrequency, out int maxfrequency, out SpeakerMode controlpanelspeakermode)
        {
            Errors.ThrowIfError(GetDriverCaps(DangerousGetHandle(), Id, out caps, out minfrequency, out maxfrequency, out controlpanelspeakermode));
        }

        private OutputDriverDTO GetOutputDriver(int Id)
        {
            Guid DriverGuid;
            string DriverName;
            GetOutputDriverInfo(Id, out DriverName, out DriverGuid);

            Capabilities caps;
            int minfrequency, maxfrequency;
            SpeakerMode controlpanelspeakermode;
            GetOutputDriverCapabilities(Id, out caps, out minfrequency, out maxfrequency, out controlpanelspeakermode);

            return new OutputDriverDTO {
                Id = Id,
                Name = DriverName,
                Guid = DriverGuid,

                Capabilities = caps,
                MinimumFrequency = minfrequency,
                MaximumFrequency = maxfrequency,
                SpeakerMode = controlpanelspeakermode
            };
        }
        #endregion

        #region Spectrum/Wave

        public float[] GetSpectrum(int numvalues, int channeloffset, FFTWindow windowtype)
        {
            float[] SpectrumArray = new float[numvalues];
            GetSpectrum(SpectrumArray, numvalues, channeloffset, windowtype);
            return SpectrumArray;
        }

        public void GetSpectrum(float[] spectrumarray, int numvalues, int channeloffset, FFTWindow windowtype)
        {
            GetSpectrum(DangerousGetHandle(), spectrumarray, numvalues, channeloffset, windowtype);
        }

        public float[] GetWaveData(int numvalues, int channeloffset)
        {
            float[] WaveArray = new float[numvalues];
            GetWaveData(WaveArray, numvalues, channeloffset);
            return WaveArray;
        }

        public void GetWaveData(float[] wavearray, int numvalues, int channeloffset)
        {
            GetWaveData(DangerousGetHandle(), wavearray, numvalues, channeloffset);
        }
        #endregion

        #region Update

        public void Update()
        {
            Update(DangerousGetHandle());
        }

        public void UpdateFinished()
        {
            UpdateFinished(DangerousGetHandle());
        }
        #endregion
    }
}
