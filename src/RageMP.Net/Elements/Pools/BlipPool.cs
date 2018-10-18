using System;
using System.Numerics;
using System.Threading.Tasks;
using RageMP.Net.Elements.Entities;
using RageMP.Net.Helpers;
using RageMP.Net.Interfaces;
using RageMP.Net.Native;

namespace RageMP.Net.Elements.Pools
{
    internal class BlipPool : PoolBase<IBlip>, IBlipPool
    {
        internal BlipPool(IntPtr nativePointer, Plugin plugin) : base(nativePointer, plugin)
        {
        }

        public async Task<IBlip> NewAsync(uint sprite, Vector3 position, float scale, uint color, string name, uint alpha, float drawDistance, bool shortRange, int rotation, uint dimension)
        {
            using (var converter = new StringConverter())
            {
                var namePointer = converter.StringToPointer(name);

                var pointer = await _plugin
                    .Schedule(() => Rage.BlipPool.BlipPool_New(_nativePointer, sprite, position, scale, color, namePointer, alpha, drawDistance, shortRange, rotation, dimension))
                    .ConfigureAwait(false);

                return CreateAndSaveEntity(pointer);
            }
        }

        protected override IBlip BuildEntity(IntPtr entity)
        {
            return new Blip(entity, _plugin);
        }
    }
}