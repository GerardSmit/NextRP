using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class CheckpointCollection : CollectionBase<ICheckpoint>, ICheckpointCollection
    {
        public CheckpointCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<ICheckpoint> NewAsync(uint type, Vector3 position, Vector3 nextPosition, float radius, Color color, bool visible, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.CheckpointPool.CheckpointPool_New(_nativePointer, type, position, nextPosition, radius, color, visible, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public Task<ICheckpoint> NewAsync(int type, Vector3 position, Vector3 nextPosition, float radius, Color color, bool visible, uint dimension)
        {
            return NewAsync((uint) type, position, nextPosition, radius, color, visible, dimension);
        }

        protected override ICheckpoint BuildEntity(IntPtr entityPointer)
        {
            return new Checkpoint(entityPointer);
        }
    }
}
