using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NextFramework.Data;
using NextFramework.Data.Collections;
using NextFramework.Data.Entities;
using NextFramework.Enums;
using NextFramework.Events;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Scripting.ScriptingClasses
{
    public class EventScripting : IHostedService
    {
        private readonly ILogger<EventScripting> _logger;
        private readonly ConcurrentDictionary<IntPtr, IEntity> _entityCache = new ConcurrentDictionary<IntPtr, IEntity>();
        private readonly IEventManager _eventManager;
        private readonly TickEvent _tickEvent = new TickEvent();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly NativeTickDelegate _tick;

        private readonly NativeEntityCreatedDelegate _entityCreated;
        private readonly NativeEntityDestroyedDelegate _entityDestroyed;
        private readonly NativeEntityModelChangeDelegate _entityModelChange;

        private readonly NativePlayerJoinDelegate _playerJoin;
        private readonly NativePlayerQuitDelegate _playerQuit;
        private readonly NativePlayerChatDelegate _playerChat;
        private readonly NativePlayerDeathDelegate _playerDeath;
        private readonly NativePlayerReadyDelegate _playerReady;
        private readonly NativePlayerSpawnDelegate _playerSpawn;
        private readonly NativePlayerDamageDelegate _playerDamage;
        private readonly NativePlayerCommandDelegate _playerCommand;
        private readonly NativePlayerWeaponChangeDelegate _playerWeaponChange;
        private readonly NativePlayerRemoteEventDelegate _playerRemote;
        private readonly NativePlayerStartEnterVehicleDelegate _playerStartEnterVehicle;
        private readonly NativePlayerEnterVehicleDelegate _playerEnterVehicle;
        private readonly NativePlayerStartExitVehicleDelegate _playerStartExitVehicle;
        private readonly NativePlayerExitVehicleDelegate _playerExitVehicle;

        private readonly NativePlayerEnterColshapeDelegate _playerEnterColshape;
        private readonly NativePlayerExitColshapeDelegate _playerExitColshape;

        private readonly NativePlayerEnterCheckpointDelegate _playerEnterCheckpoint;
        private readonly NativePlayerExitCheckpointDelegate _playerExitCheckpoint;

        private readonly NativePlayerCreateWaypointDelegate _playerCreateWaypoint;
        private readonly NativePlayerReachWaypointDelegate _playerReachWaypoint;

        private readonly NativePlayerStreamInDelegate _playerStreamIn;
        private readonly NativePlayerStreamOutDelegate _playerStreamOut;

        private readonly NativeVehicleDamageDelegate _vehicleDamage;
        private readonly NativeVehicleDeathDelegate _vehicleDeath;
        private readonly NativeVehicleHornToggleDelegate _vehicleHornToggle;
        private readonly NativeVehicleTrailerAttachedDelegate _vehicleTrailerAttached;
        private readonly NativeVehicleSirenToggleDelegate _vehicleSirenToggle;

        public EventScripting(IEventManager eventManager, ILogger<EventScripting> logger)
        {
            _eventManager = eventManager;
            _logger = logger;
            _tick = Tick;
            _entityCreated = EntityCreated;
            _entityDestroyed = EntityDestroyed;
            _entityModelChange = EntityModelChange;
            _playerJoin = PlayerJoin;
            _playerQuit = PlayerQuit;
            _playerChat = PlayerChat;
            _playerDeath = PlayerDeath;
            _playerReady = PlayerReady;
            _playerSpawn = PlayerSpawn;
            _playerDamage = PlayerDamage;
            _playerCommand = PlayerCommand;
            _playerWeaponChange = PlayerWeaponChange;
            _playerRemote = PlayerRemote;
            _playerStartEnterVehicle = PlayerStartEnterVehicle;
            _playerEnterVehicle = PlayerEnterVehicle;
            _playerStartExitVehicle = PlayerStartExitVehicle;
            _playerExitVehicle = PlayerExitVehicle;
            _playerEnterColshape = PlayerEnterColshape;
            _playerExitColshape = PlayerExitColshape;
            _playerEnterCheckpoint = PlayerEnterCheckpoint;
            _playerExitCheckpoint = PlayerExitCheckpoint;
            _playerCreateWaypoint = PlayerCreateWaypoint;
            _playerReachWaypoint = PlayerReachWaypoint;
            _playerStreamIn = PlayerStreamIn;
            _playerStreamOut = PlayerStreamOut;
            _vehicleDamage = VehicleDamage;
            _vehicleDeath = VehicleDeath;
            _vehicleHornToggle = VehicleHornToggle;
            _vehicleTrailerAttached = VehicleTrailerAttached;
            _vehicleSirenToggle = VehicleSirenToggle;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Rage.Events.RegisterEventHandler(EventType.Tick, _tick);

            Rage.Events.RegisterEventHandler(EventType.EntityCreated, _entityCreated);
            Rage.Events.RegisterEventHandler(EventType.EntityDestroyed, _entityDestroyed);
            Rage.Events.RegisterEventHandler(EventType.EntityModelChanged, _entityModelChange);

            Rage.Events.RegisterEventHandler(EventType.PlayerJoin, _playerJoin);
            Rage.Events.RegisterEventHandler(EventType.PlayerQuit, _playerQuit);
            Rage.Events.RegisterEventHandler(EventType.PlayerChat, _playerChat);
            Rage.Events.RegisterEventHandler(EventType.PlayerDeath, _playerDeath);
            Rage.Events.RegisterEventHandler(EventType.PlayerReady, _playerReady);
            Rage.Events.RegisterEventHandler(EventType.PlayerSpawn, _playerSpawn);
            Rage.Events.RegisterEventHandler(EventType.PlayerDamage, _playerDamage);

            Rage.Events.RegisterEventHandler(EventType.PlayerCommand, _playerCommand);
            Rage.Events.RegisterEventHandler(EventType.PlayerWeaponChange, _playerWeaponChange);
            Rage.Events.RegisterEventHandler(EventType.PlayerRemoteEvent, _playerRemote);
            Rage.Events.RegisterEventHandler(EventType.PlayerStartEnterVehicle, _playerStartEnterVehicle);
            Rage.Events.RegisterEventHandler(EventType.PlayerEnterVehicle, _playerEnterVehicle);
            Rage.Events.RegisterEventHandler(EventType.PlayerStartExitVehicle, _playerStartExitVehicle);
            Rage.Events.RegisterEventHandler(EventType.PlayerExitVehicle, _playerExitVehicle);

            Rage.Events.RegisterEventHandler(EventType.PlayerEnterColshape, _playerEnterColshape);
            Rage.Events.RegisterEventHandler(EventType.PlayerExitColshape, _playerExitColshape);

            Rage.Events.RegisterEventHandler(EventType.PlayerEnterCheckpoint, _playerEnterCheckpoint);
            Rage.Events.RegisterEventHandler(EventType.PlayerExitCheckpoint, _playerExitCheckpoint);

            Rage.Events.RegisterEventHandler(EventType.PlayerCreateWaypoint, _playerCreateWaypoint);
            Rage.Events.RegisterEventHandler(EventType.PlayerReachWaypoint, _playerReachWaypoint);

            Rage.Events.RegisterEventHandler(EventType.PlayerStreamIn, _playerStreamIn);
            Rage.Events.RegisterEventHandler(EventType.PlayerStreamOut, _playerStreamOut);

            Rage.Events.RegisterEventHandler(EventType.VehicleDamage, _vehicleDamage);
            Rage.Events.RegisterEventHandler(EventType.VehicleDeath, _vehicleDeath);
            Rage.Events.RegisterEventHandler(EventType.VehicleHornToggle, _vehicleHornToggle);
            Rage.Events.RegisterEventHandler(EventType.VehicleSirenToggle, _vehicleSirenToggle);
            Rage.Events.RegisterEventHandler(EventType.VehicleTrailerAttached, _vehicleTrailerAttached);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Rage.Events.UnregisterEventHandler(EventType.Tick);

            Rage.Events.UnregisterEventHandler(EventType.EntityCreated);
            Rage.Events.UnregisterEventHandler(EventType.EntityDestroyed);
            Rage.Events.UnregisterEventHandler(EventType.EntityModelChanged);

            Rage.Events.UnregisterEventHandler(EventType.PlayerJoin);
            Rage.Events.UnregisterEventHandler(EventType.PlayerQuit);
            Rage.Events.UnregisterEventHandler(EventType.PlayerChat);
            Rage.Events.UnregisterEventHandler(EventType.PlayerDeath);
            Rage.Events.UnregisterEventHandler(EventType.PlayerReady);
            Rage.Events.UnregisterEventHandler(EventType.PlayerSpawn);
            Rage.Events.UnregisterEventHandler(EventType.PlayerDamage);

            Rage.Events.UnregisterEventHandler(EventType.PlayerCommand);
            Rage.Events.UnregisterEventHandler(EventType.PlayerWeaponChange);
            Rage.Events.UnregisterEventHandler(EventType.PlayerRemoteEvent);
            Rage.Events.UnregisterEventHandler(EventType.PlayerStartEnterVehicle);
            Rage.Events.UnregisterEventHandler(EventType.PlayerEnterVehicle);
            Rage.Events.UnregisterEventHandler(EventType.PlayerStartExitVehicle);
            Rage.Events.UnregisterEventHandler(EventType.PlayerExitVehicle);

            Rage.Events.UnregisterEventHandler(EventType.PlayerEnterColshape);
            Rage.Events.UnregisterEventHandler(EventType.PlayerExitColshape);

            Rage.Events.UnregisterEventHandler(EventType.PlayerEnterCheckpoint);
            Rage.Events.UnregisterEventHandler(EventType.PlayerExitCheckpoint);

            Rage.Events.UnregisterEventHandler(EventType.PlayerCreateWaypoint);
            Rage.Events.UnregisterEventHandler(EventType.PlayerReachWaypoint);

            Rage.Events.UnregisterEventHandler(EventType.PlayerStreamIn);
            Rage.Events.UnregisterEventHandler(EventType.PlayerStreamOut);

            Rage.Events.UnregisterEventHandler(EventType.VehicleDamage);
            Rage.Events.UnregisterEventHandler(EventType.VehicleDeath);
            Rage.Events.UnregisterEventHandler(EventType.VehicleHornToggle);
            Rage.Events.UnregisterEventHandler(EventType.VehicleSirenToggle);
            Rage.Events.UnregisterEventHandler(EventType.VehicleTrailerAttached);

            return Task.CompletedTask;
        }

        private void VehicleTrailerAttached(IntPtr vehiclepointer, IntPtr trailerpointer)
        {
            var vehicle = Collections.VehicleCollection[vehiclepointer];
            var trailer = Collections.VehicleCollection[trailerpointer];

            _eventManager.CallAsync(new VehicleTrailerAttachedEvent(vehicle, trailer));
        }

        private void VehicleHornToggle(IntPtr vehiclepointer, bool toggle)
        {
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new VehicleHornToggleEvent(vehicle, toggle));
        }

        private void VehicleSirenToggle(IntPtr vehiclepointer, bool toggle)
        {
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new VehicleSirenToggleEvent(vehicle, toggle));
        }

        private void VehicleDeath(IntPtr vehiclepointer, uint reason, IntPtr killerplayerpointer)
        {
            var vehicle = Collections.VehicleCollection[vehiclepointer];
            var player = Collections.PlayerCollection[killerplayerpointer];

            _eventManager.CallAsync(new VehicleDeathEvent(vehicle, player, (DeathReason)reason));
        }

        private void VehicleDamage(IntPtr vehiclepointer, float bodyhealthloss, float enginehealthloss)
        {
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new VehicleDamageEvent(vehicle, bodyhealthloss, enginehealthloss));
        }

        private void PlayerStreamOut(IntPtr playerpointer, IntPtr forplayerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var forPlayer = Collections.PlayerCollection[forplayerpointer];

            _eventManager.CallAsync(new PlayerStreamOutEvent(player, forPlayer));
        }

        private void PlayerStreamIn(IntPtr playerpointer, IntPtr forplayerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var forPlayer = Collections.PlayerCollection[forplayerpointer];

            _eventManager.CallAsync(new PlayerStreamInEvent(player, forPlayer));
        }

        private void PlayerReachWaypoint(IntPtr playerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerReachWaypointEvent(player));
        }

        private void PlayerCreateWaypoint(IntPtr playerpointer, Vector3 position)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerCreateWaypointEvent(player, position));
        }

        private void PlayerExitCheckpoint(IntPtr playerpointer, IntPtr checkpointpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var checkpoint = Collections.CheckpointCollection[checkpointpointer];

            _eventManager.CallAsync(new PlayerExitCheckpointEvent(player, checkpoint));
        }

        private void PlayerEnterCheckpoint(IntPtr playerpointer, IntPtr checkpointpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var checkpoint = Collections.CheckpointCollection[checkpointpointer];

            _eventManager.CallAsync(new PlayerEnterCheckpointEvent(player, checkpoint));
        }

        private void PlayerExitColshape(IntPtr playerpointer, IntPtr colshapepointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var colshape = Collections.ColshapeCollection[colshapepointer];

            _eventManager.CallAsync(new PlayerExitColshapeEvent(player, colshape));
        }

        private void PlayerEnterColshape(IntPtr playerpointer, IntPtr colshapepointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var colshape = Collections.ColshapeCollection[colshapepointer];

            _eventManager.CallAsync(new PlayerEnterColshapeEvent(player, colshape));
        }

        private void PlayerExitVehicle(IntPtr playerpointer, IntPtr vehiclepointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new PlayerExitVehicleEvent(player, vehicle));
        }

        private void PlayerStartExitVehicle(IntPtr playerpointer, IntPtr vehiclepointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new PlayerStartExitVehicleEvent(player, vehicle));
        }

        private void PlayerEnterVehicle(IntPtr playerpointer, IntPtr vehiclepointer, int seat)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new PlayerEnterVehicleEvent(player, vehicle, seat));
        }

        private void PlayerStartEnterVehicle(IntPtr playerpointer, IntPtr vehiclepointer, int seat)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var vehicle = Collections.VehicleCollection[vehiclepointer];

            _eventManager.CallAsync(new PlayerStartEnterVehicle(player, vehicle, seat));
        }

        private void PlayerRemote(IntPtr playerpointer, uint eventNameHash, ArgumentsData data)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var arguments = ArgumentConverter.ConvertToObjects(data);

            _eventManager.CallAsync(new PlayerRemoteEvent(player, eventNameHash, arguments));
        }

        private void PlayerWeaponChange(IntPtr playerpointer, uint oldweapon, uint newweapon)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerWeaponChangeEvent(player, oldweapon, newweapon));
        }

        private void PlayerCommand(IntPtr playerpointer, IntPtr text)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var message = Marshal.PtrToStringUni(text);

            _eventManager.CallAsync(new PlayerCommandEvent(player, message));
        }

        private void PlayerDamage(IntPtr playerpointer, float healthloss, float armorloss)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerDamageEvent(player, healthloss, armorloss));
        }

        private void PlayerSpawn(IntPtr playerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerSpawnEvent(player));
        }

        private void PlayerReady(IntPtr playerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerReadyEvent(player));
        }

        private void PlayerDeath(IntPtr playerpointer, uint reason, IntPtr killerplayerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var killer = Collections.PlayerCollection[killerplayerpointer];

            _eventManager.CallAsync(new PlayerDeathEvent(player, killer, (DeathReason) reason));
        }

        private void PlayerChat(IntPtr playerpointer, IntPtr text)
        {
            var player = Collections.PlayerCollection[playerpointer];
            var message = Marshal.PtrToStringUni(text);

            _eventManager.CallAsync(new PlayerChatEvent(player, message));
        }

        private void EntityModelChange(IntPtr entitypointer, uint oldmodel)
        {
            if (!TryGetEntity(entitypointer, out var entity))
            {
                return;
            }

            _eventManager.CallAsync(new EntityModelChangedEvent(entity, oldmodel));

            if (entity is IPlayer player)
            {
                _eventManager.CallAsync(new PlayerModelChangeEvent(player, (PedHash)oldmodel));
            }
        }

        private void PlayerJoin(IntPtr playerpointer)
        {
            var player = Collections.PlayerCollection[playerpointer];

            _eventManager.CallAsync(new PlayerJoinEvent(player));
        }

        private void PlayerQuit(IntPtr playerpointer, uint type, IntPtr messagepointer)
        {
            var player = (Player) Collections.PlayerCollection[playerpointer];
            var message = StringConverter.PointerToString(messagepointer);
            var reason = (DisconnectReason) type;

            _eventManager.CallAsync(new PlayerQuitEvent(player, message, reason));
            player.MarkDisconnected();
        }

        private void Tick()
        {
            _stopwatch.Restart();
            _tickEvent.TickCount++;
            TickScheduler.Instance.Tick();
            _tickEvent.IsPropagationStopped = false;
            _eventManager.CallAsync(_tickEvent);
            _stopwatch.Stop();

            if (_stopwatch.ElapsedMilliseconds > 50)
            {
                _logger.LogWarning($"The last tick toke {_stopwatch.ElapsedMilliseconds}ms");
            }
        }

        private void EntityDestroyed(IntPtr entitypointer)
        {
            if (TryRemoveEntity(entitypointer, out var createdEntity))
            {
                _entityCache.TryRemove(entitypointer, out _);
                _eventManager.CallAsync(new EntityDestroyedEvent(createdEntity));
            }
        }

        private void EntityCreated(IntPtr entitypointer)
        {
            if (TryBuildEntity(entitypointer, out var createdEntity))
            {
                _entityCache.TryAdd(entitypointer, createdEntity);
                _eventManager.CallAsync(new EntityCreatedEvent(createdEntity));
            }
        }

        private bool GetPoolFromPointer(IntPtr entityPointer, out IInternalCollection collection)
        {
            return Collections.TryGetPool(Rage.Entity.Entity_GetType(entityPointer), out collection);
        }

        private bool TryBuildEntity(IntPtr entityPointer, out IEntity entity)
        {
            if (_entityCache.TryGetValue(entityPointer, out entity))
            {
                return true;
            }

            if (!GetPoolFromPointer(entityPointer, out var pool))
            {
                entity = null;
                return false;
            }

            return pool.CreateAndSaveEntity(entityPointer, out entity);
        }

        private bool TryRemoveEntity(IntPtr entityPointer, out IEntity entity)
        {
            if (!GetPoolFromPointer(entityPointer, out IInternalCollection pool))
            {
                entity = null;
                return false;
            }

            return pool.RemoveEntity(entityPointer, out entity);
        }

        private bool TryGetEntity(IntPtr entityPointer, out IEntity entity)
        {
            if (_entityCache.TryGetValue(entityPointer, out entity))
            {
                return true;
            }

            if (Collections.TryGetPool(Rage.Entity.Entity_GetType(entityPointer), out var pool))
            {
                entity = pool.GetEntity(entityPointer);
                return true;
            }

            entity = null;
            return false;
        }
    }
}
