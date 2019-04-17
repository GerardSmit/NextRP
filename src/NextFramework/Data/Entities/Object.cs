using System;
using NextFramework.Enums;

namespace NextFramework.Data.Entities
{
    internal class Object : Entity, IObject
    {
        internal Object(IntPtr nativePointer) : base(nativePointer, EntityType.Object)
        {
        }
    }
}
