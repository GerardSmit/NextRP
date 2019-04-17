using System;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class BlipCollection : CollectionBase<IBlip>, IBlipCollection
    {
        public BlipCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<IBlip> NewAsync(uint sprite, Vector3 position, float scale, uint color, string name, uint alpha, float drawDistance, bool shortRange, int rotation, uint dimension)
        {
            Contract.NotNull(name, nameof(name));
            
            using (var converter = new StringConverter())
            {
                var namePointer = converter.StringToPointer(name);

                var pointer = await TickScheduler.Instance
                    .Schedule(() => Rage.BlipPool.BlipPool_New(_nativePointer, sprite, position, scale, color, namePointer, alpha, drawDistance, shortRange, rotation, dimension));

                return CreateAndSaveEntity(pointer);
            }
        }

        public Task<IBlip> NewAsync(int sprite, Vector3 position, float scale, int color, string name, int alpha, float drawDistance, bool shortRange, int rotation, uint dimension)
        {
            return NewAsync((uint) sprite, position, scale, (uint) color, name, (uint) alpha, drawDistance, shortRange, rotation, dimension);
        }

        protected override IBlip BuildEntity(IntPtr entity)
        {
            return new Blip(entity);
        }
    }
}
