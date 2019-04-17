using System;
using System.Runtime.InteropServices;

namespace NextFramework.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ArgumentsData
    {
        public ulong Length;
        public IntPtr Arguments;
    }
}
