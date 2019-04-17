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
    internal class Checkpoint : Entity, ICheckpoint
    {
        public Color Color
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<Color>(Rage.Checkpoint.Checkpoint_GetColor(NativePointer));
            }
            set
            {
                CheckExistence();

                Rage.Checkpoint.Checkpoint_SetColor(NativePointer, value.R, value.G, value.B, value.A);
            }
        }

        public Vector3 Direction
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<Vector3>(Rage.Checkpoint.Checkpoint_GetDirection(NativePointer));
            }
            set
            {
                CheckExistence();

                Rage.Checkpoint.Checkpoint_SetDirection(NativePointer, value);
            }
        }

        public float Radius
        {
            get
            {
                CheckExistence();

                return Rage.Checkpoint.Checkpoint_GetRadius(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Checkpoint.Checkpoint_SetRadius(NativePointer, value);
            }
        }

        public bool IsVisible
        {
            get
            {
                CheckExistence();

                return Rage.Checkpoint.Checkpoint_IsVisible(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Checkpoint.Checkpoint_SetVisible(NativePointer, value);
            }
        }

        internal Checkpoint(IntPtr nativePointer) : base(nativePointer, EntityType.Checkpoint)
        {
        }

        public void ShowFor(IEnumerable<IPlayer> players)
        {
            Contract.NotNull(players, nameof(players));
            CheckExistence();

            var playerPointers = players.Select(x => x.NativePointer).ToArray();

            Rage.Checkpoint.Checkpoint_ShowFor(NativePointer, playerPointers, (ulong) playerPointers.Length);
        }

        public void HideFor(IEnumerable<IPlayer> players)
        {
            Contract.NotNull(players, nameof(players));
            CheckExistence();

            var playerPointers = players.Select(x => x.NativePointer).ToArray();

            Rage.Checkpoint.Checkpoint_HideFor(NativePointer, playerPointers, (ulong) playerPointers.Length);
        }
    }
}
