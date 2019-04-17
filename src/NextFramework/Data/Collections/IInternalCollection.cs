using System;

namespace NextFramework.Data.Collections
{
    internal interface IInternalCollection
    {
        IEntity GetEntity(IntPtr entity);

        bool RemoveEntity(IntPtr entityPointer, out IEntity entity);

        bool CreateAndSaveEntity(IntPtr entityPointer, out IEntity entity);
    }
}
