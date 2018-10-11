using System;
using System.Numerics;
using RageMP.Net.Entities;
using RageMP.Net.Interfaces;
using RageMP.Net.Native;

namespace RageMP.Net.Scripting.ScriptingClasses
{
    internal class VehiclePool : PoolBase<IVehicle>, IVehiclePool
    {
        public VehiclePool(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public IVehicle New(uint model, Vector3 position, float heading, string numberPlate, uint alpha, bool locked, bool engine, uint dimension)
        {
            var createdVehicle = Rage.VehiclePool.VehiclePool_New(_nativePointer, model, position, heading, numberPlate, alpha, locked, engine, dimension);

            return new Vehicle(createdVehicle);
        }
    }
}