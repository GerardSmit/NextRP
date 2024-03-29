using System;
using System.Runtime.InteropServices;
using NextFramework.Data;

namespace NextFramework.Native
{
    internal static partial class Rage
    {
        internal static class World
        {
            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern IntPtr World_GetTime(IntPtr world);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_SetTime(IntPtr world, TimeData time);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern IntPtr World_GetWeather(IntPtr world);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_SetWeather(IntPtr world, IntPtr weather);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_SetWeatherTransition(IntPtr world, IntPtr weather, float time);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_RequestIpl(IntPtr world, IntPtr ipl);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_RemoveIpl(IntPtr world, IntPtr ipl);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern bool World_AreTrafficLightsLocked(IntPtr world);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_LockTrafficLights(IntPtr world, bool toggle);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern int World_GetTrafficLightsState(IntPtr world);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void World_SetTrafficLightsState(IntPtr world, int state);
        }
    }
}
