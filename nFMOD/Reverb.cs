using System;
using System.Runtime.InteropServices;
using System.Security;

namespace nFMOD
{
    public partial class Reverb : Handle
    {
        #region Externs
        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Get3DAttributes(IntPtr reverb, ref Vector3 position, ref float mindistance, ref float maxdistance);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetActive(IntPtr reverb, ref int active);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetMemoryInfo(IntPtr reverb, uint memorybits, uint event_memorybits, ref uint memoryused, ref MemoryUsageDetails memoryused_details);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_Reverb_GetProperties"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetProperties(IntPtr reverb, ref ReverbProperties properties);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode GetUserData(IntPtr reverb, ref IntPtr userdata);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_Reverb_Release"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Release(IntPtr reverb);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode Set3DAttributes(IntPtr reverb, ref Vector3 position, float mindistance, float maxdistance);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetActive(IntPtr reverb, int active);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = "FMOD_Reverb_SetProperties"), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetProperties(IntPtr reverb, ref ReverbProperties properties);

        [DllImport(Common.FMOD_DLL_NAME, EntryPoint = ""), SuppressUnmanagedCodeSecurity]
        private static extern ErrorCode SetUserData(IntPtr reverb, IntPtr userdata);
        #endregion

        private Reverb()
        {
        }

        internal Reverb(IntPtr hnd)
        {
            SetHandle(hnd);
        }

        protected override bool ReleaseHandle()
        {
            if (IsInvalid)
                return true;

            Release(handle);
            SetHandleAsInvalid();
            return true;
        }

        #region Presets
        /// <summary>
        /// A set of predefined environment PARAMETERS, created by Creative Labs
        /// These are used to initialize an FMOD_REVERB_PROPERTIES structure statically.
        /// ie FMOD_REVERB_PROPERTIES prop = FMOD_PRESET_GENERIC;
        /// </summary>
        public static class Presets
        {

            public static readonly ReverbProperties Off = new ReverbProperties {
                Instance = 0,
                Environment = -1,
                EnvironmentSize = 7.5f,
                EnvironmentDiffusion = 1.00f,
                Room = -10000,
                RoomHighFrequencies = -10000,
                RoomLowFrequencies = 0,
                DecayTime = 1.00f,
                DecayHighFrequencyRatio = 1.00f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -2602,
                ReflectionsDelay = 0.007f,
                Reverb = 200,
                ReverbDelay = 0.011f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = (ReverbFlags)0x33F
            };

            public static readonly ReverbProperties Generic = new ReverbProperties {
                Instance = 0,
                Environment = 0,
                EnvironmentSize = 7.5f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -100,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.83f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -2602,
                ReflectionsDelay = 0.007f,
                Reverb = 200,
                ReverbDelay = 0.011f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties PaddedCell = new ReverbProperties {
                Instance = 0,
                Environment = 1,
                EnvironmentSize = 1.4f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -6000,
                RoomLowFrequencies = 0,
                DecayTime = 0.17f,
                DecayHighFrequencyRatio = 0.10f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1204,
                ReflectionsDelay = 0.001f,
                Reverb = 207,
                ReverbDelay = 0.002f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Room = new ReverbProperties {
                Instance = 0,
                Environment = 2,
                EnvironmentSize = 1.9f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -454,
                RoomLowFrequencies = 0,
                DecayTime = 0.40f,
                DecayHighFrequencyRatio = 0.83f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1646,
                ReflectionsDelay = 0.002f,
                Reverb = 53,
                ReverbDelay = 0.003f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Bathroom = new ReverbProperties {
                Instance = 0,
                Environment = 3,
                EnvironmentSize = 1.4f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -1200,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.54f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -370,
                ReflectionsDelay = 0.007f,
                Reverb = 1030,
                ReverbDelay = 0.011f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties LivingRoom = new ReverbProperties {
                Instance = 0,
                Environment = 4,
                EnvironmentSize = 2.5f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -6000,
                RoomLowFrequencies = 0,
                DecayTime = 0.50f,
                DecayHighFrequencyRatio = 0.10f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1376,
                ReflectionsDelay = 0.003f,
                Reverb = -1104,
                ReverbDelay = 0.004f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties StoneRoom = new ReverbProperties {
                Instance = 0,
                Environment = 5,
                EnvironmentSize = 11.6f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -300,
                RoomLowFrequencies = 0,
                DecayTime = 2.31f,
                DecayHighFrequencyRatio = 0.64f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -711,
                ReflectionsDelay = 0.012f,
                Reverb = 83,
                ReverbDelay = 0.017f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Auditorium = new ReverbProperties {
                Instance = 0,
                Environment = 6,
                EnvironmentSize = 21.6f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -476,
                RoomLowFrequencies = 0,
                DecayTime = 4.32f,
                DecayHighFrequencyRatio = 0.59f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -789,
                ReflectionsDelay = 0.020f,
                Reverb = -289,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties ConcertHall = new ReverbProperties {
                Instance = 0,
                Environment = 7,
                EnvironmentSize = 19.6f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -500,
                RoomLowFrequencies = 0,
                DecayTime = 3.92f,
                DecayHighFrequencyRatio = 0.70f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1230,
                ReflectionsDelay = 0.020f,
                Reverb = -2,
                ReverbDelay = 0.029f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Cave = new ReverbProperties {
                Instance = 0,
                Environment = 8,
                EnvironmentSize = 14.6f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = 0,
                RoomLowFrequencies = 0,
                DecayTime = 2.91f,
                DecayHighFrequencyRatio = 1.30f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -602,
                ReflectionsDelay = 0.015f,
                Reverb = -302,
                ReverbDelay = 0.022f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.DecayTimeScale | ReverbFlags.ReflectionsScale |
                        ReverbFlags.ReflectionsDelayScale | ReverbFlags.ReverbScale | ReverbFlags.ReverbDelayScale
            };

            public static readonly ReverbProperties Arena = new ReverbProperties {
                Instance = 0,
                Environment = 9,
                EnvironmentSize = 36.2f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -698,
                RoomLowFrequencies = 0,
                DecayTime = 7.24f,
                DecayHighFrequencyRatio = 0.33f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1166,
                ReflectionsDelay = 0.020f,
                Reverb = 16,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Hangar = new ReverbProperties {
                Instance = 0,
                Environment = 10,
                EnvironmentSize = 50.3f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -1000,
                RoomLowFrequencies = 0,
                DecayTime = 10.05f,
                DecayHighFrequencyRatio = 0.23f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -602,
                ReflectionsDelay = 0.020f,
                Reverb = 198,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties CarpettedHallway = new ReverbProperties {
                Instance = 0,
                Environment = 11,
                EnvironmentSize = 1.9f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -4000,
                RoomLowFrequencies = 0,
                DecayTime = 0.30f,
                DecayHighFrequencyRatio = 0.10f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1831,
                ReflectionsDelay = 0.002f,
                Reverb = -1630,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Hallway = new ReverbProperties {
                Instance = 0,
                Environment = 12,
                EnvironmentSize = 1.8f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -300,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.59f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1219,
                ReflectionsDelay = 0.007f,
                Reverb = 441,
                ReverbDelay = 0.011f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties StoneCorridor = new ReverbProperties {
                Instance = 0,
                Environment = 13,
                EnvironmentSize = 13.5f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -237,
                RoomLowFrequencies = 0,
                DecayTime = 2.70f,
                DecayHighFrequencyRatio = 0.79f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1214,
                ReflectionsDelay = 0.013f,
                Reverb = 395,
                ReverbDelay = 0.020f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Alley = new ReverbProperties {
                Instance = 0,
                Environment = 14,
                EnvironmentSize = 7.5f,
                EnvironmentDiffusion = 0.30f,
                Room = -1000,
                RoomHighFrequencies = -270,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.86f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1204,
                ReflectionsDelay = 0.007f,
                Reverb = -4,
                ReverbDelay = 0.011f,
                EchoTime = 0.125f,
                EchoDepth = 0.95f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Forest = new ReverbProperties {
                Instance = 0,
                Environment = 15,
                EnvironmentSize = 38.0f,
                EnvironmentDiffusion = 0.30f,
                Room = -1000,
                RoomHighFrequencies = -3300,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.54f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -2560,
                ReflectionsDelay = 0.162f,
                Reverb = -229,
                ReverbDelay = 0.088f,
                EchoTime = 0.125f,
                EchoDepth = 1.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties City = new ReverbProperties {
                Instance = 0,
                Environment = 16,
                EnvironmentSize = 7.5f,
                EnvironmentDiffusion = 0.50f,
                Room = -1000,
                RoomHighFrequencies = -800,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.67f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -2273,
                ReflectionsDelay = 0.007f,
                Reverb = -1691,
                ReverbDelay = 0.011f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Mountains = new ReverbProperties {
                Instance = 0,
                Environment = 17,
                EnvironmentSize = 100.0f,
                EnvironmentDiffusion = 0.27f,
                Room = -1000,
                RoomHighFrequencies = -2500,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.21f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -2780,
                ReflectionsDelay = 0.299f,
                Reverb = -1434,
                ReverbDelay = 0.0999f,
                EchoTime = 0.250f,
                EchoDepth = 1.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.DecayTimeScale | ReverbFlags.ReflectionsScale |
                        ReverbFlags.ReflectionsDelayScale | ReverbFlags.ReverbScale | ReverbFlags.ReverbDelayScale
            };

            public static readonly ReverbProperties Quarry = new ReverbProperties {
                Instance = 0,
                Environment = 18,
                EnvironmentSize = 17.5f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -1000,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.83f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -10000,
                ReflectionsDelay = 0.061f,
                Reverb = 500,
                ReverbDelay = 0.025f,
                EchoTime = 0.125f,
                EchoDepth = 0.70f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Plain = new ReverbProperties {
                Instance = 0,
                Environment = 19,
                EnvironmentSize = 42.5f,
                EnvironmentDiffusion = 0.21f,
                Room = -1000,
                RoomHighFrequencies = -2000,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.50f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -2466,
                ReflectionsDelay = 0.179f,
                Reverb = -1926,
                ReverbDelay = 0.0999f,
                EchoTime = 0.250f,
                EchoDepth = 1.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Parkinglot = new ReverbProperties {
                Instance = 0,
                Environment = 20,
                EnvironmentSize = 8.3f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = 0,
                RoomLowFrequencies = 0,
                DecayTime = 1.65f,
                DecayHighFrequencyRatio = 1.50f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1363,
                ReflectionsDelay = 0.008f,
                Reverb = -1153,
                ReverbDelay = 0.012f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.DecayTimeScale | ReverbFlags.ReflectionsScale |
                        ReverbFlags.ReflectionsDelayScale | ReverbFlags.ReverbScale | ReverbFlags.ReverbDelayScale
            };

            public static readonly ReverbProperties SewerPipe = new ReverbProperties {
                Instance = 0,
                Environment = 21,
                EnvironmentSize = 1.7f,
                EnvironmentDiffusion = 0.80f,
                Room = -1000,
                RoomHighFrequencies = -1000,
                RoomLowFrequencies = 0,
                DecayTime = 2.81f,
                DecayHighFrequencyRatio = 0.14f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = 429,
                ReflectionsDelay = 0.014f,
                Reverb = 1023,
                ReverbDelay = 0.021f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 0.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Underwater = new ReverbProperties {
                Instance = 0,
                Environment = 22,
                EnvironmentSize = 1.8f,
                EnvironmentDiffusion = 1.00f,
                Room = -1000,
                RoomHighFrequencies = -4000,
                RoomLowFrequencies = 0,
                DecayTime = 1.49f,
                DecayHighFrequencyRatio = 0.10f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -449,
                ReflectionsDelay = 0.007f,
                Reverb = 1700,
                ReverbDelay = 0.011f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 1.18f,
                ModulationDepth = 0.348f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.Default
            };

            public static readonly ReverbProperties Drugged = new ReverbProperties {
                Instance = 0,
                Environment = 23,
                EnvironmentSize = 1.9f,
                EnvironmentDiffusion = 0.50f,
                Room = -1000,
                RoomHighFrequencies = 0,
                RoomLowFrequencies = 0,
                DecayTime = 8.39f,
                DecayHighFrequencyRatio = 1.39f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -115,
                ReflectionsDelay = 0.002f,
                Reverb = 985,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 0.25f,
                ModulationDepth = 1.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.DecayTimeScale | ReverbFlags.ReflectionsScale |
                        ReverbFlags.ReflectionsDelayScale | ReverbFlags.ReverbScale | ReverbFlags.ReverbDelayScale
            };

            public static readonly ReverbProperties Dizzy = new ReverbProperties {
                Instance = 0,
                Environment = 24,
                EnvironmentSize = 1.8f,
                EnvironmentDiffusion = 0.60f,
                Room = -1000,
                RoomHighFrequencies = -400,
                RoomLowFrequencies = 0,
                DecayTime = 17.23f,
                DecayHighFrequencyRatio = 0.56f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -1713,
                ReflectionsDelay = 0.020f,
                Reverb = -613,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 1.00f,
                ModulationTime = 0.81f,
                ModulationDepth = 0.310f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.DecayTimeScale | ReverbFlags.ReflectionsScale |
                        ReverbFlags.ReflectionsDelayScale | ReverbFlags.ReverbScale | ReverbFlags.ReverbDelayScale
            };

            public static readonly ReverbProperties Psychotic = new ReverbProperties {
                Instance = 0,
                Environment = 25,
                EnvironmentSize = 1.0f,
                EnvironmentDiffusion = 0.50f,
                Room = -1000,
                RoomHighFrequencies = -151,
                RoomLowFrequencies = 0,
                DecayTime = 7.56f,
                DecayHighFrequencyRatio = 0.91f,
                DecayLowFrequencyRatio = 1.0f,
                Reflections = -626,
                ReflectionsDelay = 0.020f,
                Reverb = 774,
                ReverbDelay = 0.030f,
                EchoTime = 0.250f,
                EchoDepth = 0.00f,
                ModulationTime = 4.00f,
                ModulationDepth = 1.000f,
                AirAbsorptionHighFrequencies = -5.0f,
                HighFrequencyReference = 5000.0f,
                LowFrequencyReference = 250.0f,
                RoomRolloffFactor = 0.0f,
                Flags = ReverbFlags.DecayTimeScale | ReverbFlags.ReflectionsScale |
                        ReverbFlags.ReflectionsDelayScale | ReverbFlags.ReverbScale | ReverbFlags.ReverbDelayScale
            };
        }
        #endregion
    }
}

