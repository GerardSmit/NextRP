using System;
using System.Numerics;
using System.Runtime.InteropServices;
using NextFramework.Data;

namespace NextFramework.Native
{

    internal static partial class Rage
    {
        internal static class TextLabelPool
        {
            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern IntPtr TextLabelPool_New(IntPtr pool, Vector3 position, IntPtr text, uint font, ColorRgba color, float drawDistance, bool los, uint dimension);
        }
    }
}
