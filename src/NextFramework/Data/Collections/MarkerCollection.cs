using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Enums;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class MarkerCollection : CollectionBase<IMarker>, IMarkerCollection
    {
        public MarkerCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<IMarker> NewAsync(MarkerType type, Vector3 position, Vector3 rotation, Vector3 direction, float scale, Color color, bool visible, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.MarkerPool.MarkerPool_New(_nativePointer, (uint) type, position, rotation, direction, scale, color, visible, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public Task<IMarker> NewAsync(uint type, Vector3 position, Vector3 rotation, Vector3 direction, float scale, Color color, bool visible, uint dimension)
        {
            return NewAsync((MarkerType) type, position, rotation, direction, scale, color, visible, dimension);
        }

        public Task<IMarker> NewAsync(int type, Vector3 position, Vector3 rotation, Vector3 direction, float scale, Color color, bool visible, uint dimension)
        {
            return NewAsync((MarkerType) type, position, rotation, direction, scale, color, visible, dimension);
        }

        protected override IMarker BuildEntity(IntPtr entityPointer)
        {
            return new Marker(entityPointer);
        }
    }
}
