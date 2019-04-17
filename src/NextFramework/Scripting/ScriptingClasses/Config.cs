using System;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Scripting.ScriptingClasses
{
    internal class Config : IConfig
    {
        private readonly IntPtr _nativePointer;
        
        public Config(IntPtr nativePointer)
        {
            _nativePointer = nativePointer;
        }

        public int GetInt(string key, int defaultValue)
        {
            Contract.NotEmpty(key, nameof(key));

            using (var converter = new StringConverter())
            {
                return Rage.Config.Config_GetInt(_nativePointer, converter.StringToPointer(key), defaultValue);
            }
        }

        public string GetString(string key, string defaultValue)
        {
            Contract.NotEmpty(key, nameof(key));

            using (var converter = new StringConverter())
            {
                var result = Rage.Config.Config_GetString(_nativePointer, converter.StringToPointer(key), converter.StringToPointer(defaultValue));

                return StringConverter.PointerToString(result);
            }
        }
    }
}
