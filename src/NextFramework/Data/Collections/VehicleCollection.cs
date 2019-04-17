using System;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Enums;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class VehicleCollection : CollectionBase<IVehicle>, IVehicleCollection
    {
        public VehicleCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<IVehicle> NewAsync(VehicleHash model, Vector3 position, float heading, string numberPlate, uint alpha, bool locked, bool engine, uint dimension)
        {
            Contract.NotNull(numberPlate, nameof(numberPlate));
            
            using (var converter = new StringConverter())
            {
                var numberplatePointer = converter.StringToPointer(numberPlate);

                var pointer = await TickScheduler.Instance
                    .Schedule(() => Rage.VehiclePool.VehiclePool_New(_nativePointer, (uint) model, position, heading, numberplatePointer, alpha, locked, engine, dimension))
                    .ConfigureAwait(false);

                return CreateAndSaveEntity(pointer);
            }
        }

        public Task<IVehicle> NewAsync(uint model, Vector3 position, float heading, string numberPlate, uint alpha, bool locked, bool engine, uint dimension)
        {
            return NewAsync((VehicleHash) model, position, heading, numberPlate, alpha, locked, engine, dimension);
        }

        public Task<IVehicle> NewAsync(int model, Vector3 position, float heading, string numberPlate, int alpha, bool locked, bool engine, uint dimension)
        {
            return NewAsync((VehicleHash) model, position, heading, numberPlate, (uint) alpha, locked, engine, dimension);
        }

        protected override IVehicle BuildEntity(IntPtr entityPointer)
        {
            return new Vehicle(entityPointer);
        }
    }
}
