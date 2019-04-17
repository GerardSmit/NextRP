using System;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class ColshapeCollection : CollectionBase<IColshape>, IColshapeCollection
    {
        public ColshapeCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<IColshape> NewCircleAsync(Vector2 position, float radius, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.ColshapePool.ColshapePool_NewCircle(_nativePointer, position, radius, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public async Task<IColshape> NewSphereAsync(Vector3 position, float radius, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.ColshapePool.ColshapePool_NewSphere(_nativePointer, position, radius, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public async Task<IColshape> NewTubeAsync(Vector3 position, float radius, float height, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.ColshapePool.ColshapePool_NewTube(_nativePointer, position, radius, height, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public async Task<IColshape> NewRectangleAsync(Vector2 position, Vector2 size, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.ColshapePool.ColshapePool_NewRectangle(_nativePointer, position, size, dimension));

            return CreateAndSaveEntity(pointer);
        }

        public async Task<IColshape> NewCubeAsync(Vector3 position, Vector3 size, uint dimension)
        {
            var pointer = await TickScheduler.Instance
                .Schedule(() => Rage.ColshapePool.ColshapePool_NewCube(_nativePointer, position, size, dimension));

            return CreateAndSaveEntity(pointer);
        }

        protected override IColshape BuildEntity(IntPtr entityPointer)
        {
            return new Colshape(entityPointer);
        }
    }
}
