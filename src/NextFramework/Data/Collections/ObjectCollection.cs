using System;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class ObjectCollection : CollectionBase<IObject>, IObjectCollection
    {
        public ObjectCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<IObject> NewAsync(uint model, Vector3 position, Vector3 rotation, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.ObjectPool.ObjectPool_New(_nativePointer, model, position, rotation, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public Task<IObject> NewAsync(int model, Vector3 position, Vector3 rotation, uint dimension)
        {
            return NewAsync((uint) model, position, rotation, dimension);
        }

        protected override IObject BuildEntity(IntPtr entityPointer)
        {
            return new Entities.Object(entityPointer);
        }
    }
}
