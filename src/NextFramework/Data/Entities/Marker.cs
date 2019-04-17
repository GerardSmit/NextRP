using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using NextFramework.Enums;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Entities
{
    internal class Marker : Entity, IMarker
    {
        public Color Color
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<Color>(Rage.Marker.Marker_GetColor(NativePointer));
            }
            set
            {
                CheckExistence();

                Rage.Marker.Marker_SetColor(NativePointer, value.R, value.G, value.B, value.A);
            }
        }

        public Vector3 Direction
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<Vector3>(Rage.Marker.Marker_GetDirection(NativePointer));
            }
            set
            {
                CheckExistence();

                Rage.Marker.Marker_SetDirection(NativePointer, value);
            }
        }

        public float Scale
        {
            get
            {
                CheckExistence();

                return Rage.Marker.Marker_GetScale(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Marker.Marker_SetScale(NativePointer, value);
            }
        }

        public bool IsVisible
        {
            get
            {
                CheckExistence();

                return Rage.Marker.Marker_IsVisible(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Marker.Marker_SetVisible(NativePointer, value);
            }
        }

        internal Marker(IntPtr nativePointer) : base(nativePointer, EntityType.Marker)
        {
        }

        public void ShowFor(IEnumerable<IPlayer> players)
        {
            Contract.NotNull(players, nameof(players));
            CheckExistence();

            var pointers = players.Select(x => x.NativePointer).ToArray();

            Rage.Marker.Marker_ShowFor(NativePointer, pointers, (ulong) pointers.Length);
        }

        public void HideFor(IEnumerable<IPlayer> players)
        {
            Contract.NotNull(players, nameof(players));
            CheckExistence();

            var pointers = players.Select(x => x.NativePointer).ToArray();

            Rage.Marker.Marker_HideFor(NativePointer, pointers, (ulong) pointers.Length);
        }
    }
}
