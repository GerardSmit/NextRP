using System;
using System.Numerics;
using System.Runtime.InteropServices;
using NextFramework.Data;

namespace NextFramework.Native
{
    internal static partial class Rage
    {
        internal static class MarkerPool
        {
            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern IntPtr MarkerPool_New(IntPtr pool, uint model, Vector3 position, Vector3 rotation, Vector3 direction, float scale, ColorRgba color, bool visible,
                uint dimension);
        }
    }
}
