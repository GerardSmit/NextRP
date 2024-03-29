using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace NextFramework.Native
{
    internal static partial class Rage
    {
        internal static class BlipPool
        {
            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern IntPtr BlipPool_New(IntPtr blipPool, uint sprite, Vector3 position, float scale, uint color, IntPtr name, uint alpha, float drawDistance,
                bool shortRange, int rotation, uint dimension);
        }
    }
}
