using System;
using System.Collections.Generic;
using NextFramework.Enums;
using NextFramework.Native;
using NextFramework.Scripting.ScriptingClasses;

namespace NextFramework.Data.Collections
{
    internal static class Collections
    {
        private static Dictionary<byte, IInternalCollection> _entityPoolMapping;

        internal static PlayerCollection PlayerCollection { get; private set; }

        internal static VehicleCollection VehicleCollection { get; private set; }

        internal static BlipCollection BlipCollection { get; private set; }

        internal static CheckpointCollection CheckpointCollection { get; private set; }

        internal static ColshapeCollection ColshapeCollection { get; private set; }

        internal static MarkerCollection MarkerCollection { get; private set; }

        internal static ObjectCollection ObjectCollection { get; private set; }

        internal static TextLabelCollection TextLabelCollection { get; private set; }

        internal static Config Config { get; private set; }

        internal static World World { get; private set; }

        public static void Initialize(IntPtr multiplayer)
        {
            PlayerCollection = CreateNativeManager<PlayerCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetPlayerPool);
            VehicleCollection = CreateNativeManager<VehicleCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetVehiclePool);
            BlipCollection = CreateNativeManager<BlipCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetBlipPool);
            CheckpointCollection = CreateNativeManager<CheckpointCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetCheckpointPool);
            ColshapeCollection = CreateNativeManager<ColshapeCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetColshapePool);
            MarkerCollection = CreateNativeManager<MarkerCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetMarkerPool);
            ObjectCollection = CreateNativeManager<ObjectCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetObjectPool);
            TextLabelCollection = CreateNativeManager<TextLabelCollection>(multiplayer, Rage.Multiplayer.Multiplayer_GetLabelPool);

            Config = CreateNativeManager<Config>(multiplayer, Rage.Multiplayer.Multiplayer_GetConfig);
            World = CreateNativeManager<World>(multiplayer, Rage.Multiplayer.Multiplayer_GetWorld);

            _entityPoolMapping = new Dictionary<byte, IInternalCollection>
            {
                { (byte)EntityType.Player, PlayerCollection },
                { (byte)EntityType.Vehicle, VehicleCollection },
                { (byte)EntityType.Blip, BlipCollection },
                { (byte)EntityType.Checkpoint, CheckpointCollection },
                { (byte)EntityType.Colshape, ColshapeCollection },
                { (byte)EntityType.Marker, MarkerCollection },
                { (byte)EntityType.Object, ObjectCollection },
                { (byte)EntityType.TextLabel, TextLabelCollection }
            };
        }

        private static T CreateNativeManager<T>(IntPtr multiplayer, Func<IntPtr, IntPtr> pointerReceiver) where T : class
        {
            return (T)Activator.CreateInstance(typeof(T), pointerReceiver(multiplayer));
        }

        public static bool TryGetPool(byte entityType, out IInternalCollection collection)
        {
            return _entityPoolMapping.TryGetValue(entityType, out collection);
        }
    }
}
