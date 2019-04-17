using System;
using System.Runtime.InteropServices;
using NextFramework.Enums;

namespace NextFramework.Native
{
    internal static partial class Rage
    {
        internal static class Events
        {
            internal static void RegisterEventHandler<T>(EventType type, T callback) => RegisterEventHandler((int) type, Marshal.GetFunctionPointerForDelegate(callback));

            internal static void UnregisterEventHandler(EventType type) => UnregisterEventHandler((int)type);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void RegisterEventHandler(int type, IntPtr callback);

            [DllImport(_dllName, CallingConvention = _callingConvention)]
            internal static extern void UnregisterEventHandler(int type);
        }
    }
}
