using System;
using System.Numerics;
using NextFramework.Enums;
using NextFramework.Native;

namespace NextFramework.Data.Entities
{
    internal class Colshape : Entity, IColshape
    {
        public ColshapeType ShapeType
        {
            get
            {
                CheckExistence();

                return (ColshapeType) Rage.Colshape.Colshape_GetShapeType(NativePointer);
            }
        }

        internal Colshape(IntPtr nativePointer) : base(nativePointer, EntityType.Colshape)
        {
        }

        public bool IsPointWhithin(Vector3 position)
        {
            CheckExistence();

            return Rage.Colshape.Colshape_IsPointWithin(NativePointer, position);
        }
    }
}
