using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NextFramework.Data.Collections;
using NextFramework.Enums;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Entities
{
    internal partial class Player : Entity, IPlayer
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CancellationToken DisconnectToken => _cancellationTokenSource.Token;

        public string Serial { get; }

        public string Name
        {
            get
            {
                CheckExistence();

                return StringConverter.PointerToString(Rage.Player.Player_GetName(NativePointer));
            }
            set
            {
                Contract.NotEmpty(value, nameof(value));
                CheckExistence();
                
                using (var converter = new StringConverter())
                {
                    Rage.Player.Player_SetName(NativePointer, converter.StringToPointer(value));
                }
            }
        }

        public string SocialClubName
        {
            get
            {
                CheckExistence();

                return StringConverter.PointerToString(Rage.Player.Player_GetSocialClubName(NativePointer));
            }
        }

        public float Heading
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetHeading(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Player.Player_SetHeading(NativePointer, value);
            }
        }

        public override Vector3 Rotation
        {
            get
            {
                CheckExistence();

                var vehicle = Vehicle;

                if (vehicle != null)
                {
                    return vehicle.Rotation;
                }

                return new Vector3(0, 0, Heading);
            }
            set
            {
                CheckExistence();

                Heading = value.Z;
            }
        }

        public float MoveSpeed
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetMoveSpeed(NativePointer);
            }
        }

        public float Health
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetHealth(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Player.Player_SetHealth(NativePointer, value);
            }
        }

        public float Armor
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetArmor(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Player.Player_SetArmor(NativePointer, value);
            }
        }

        public Vector3 AimingAt
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<Vector3>(Rage.Player.Player_GetAminingAt(NativePointer));
            }
        }

        public string Ip
        {
            get
            {
                CheckExistence();

                return StringConverter.PointerToString(Rage.Player.Player_GetIp(NativePointer));
            }
        }

        public int Ping
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetPing(NativePointer);
            }
        }

        public float PacketLoss
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetPacketLoss(NativePointer);
            }
        }

        public string KickReason
        {
            get
            {
                CheckExistence();

                return StringConverter.PointerToString(Rage.Player.Player_GetKickReason(NativePointer));
            }
        }

        public bool IsJumping
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsJumping(NativePointer);
            }
        }

        public bool IsInCover
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsInCover(NativePointer);
            }
        }

        public bool IsEnteringVehicle
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsEnteringVehicle(NativePointer);
            }
        }

        public bool IsLeavingVehicle
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsLeavingVehicle(NativePointer);
            }
        }

        public bool IsClimbing
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsClimbing(NativePointer);
            }
        }

        public bool IsOnLadder
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsOnLadder(NativePointer);
            }
        }

        public bool IsReloading
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsReloading(NativePointer);
            }
        }

        public bool IsInMelee
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsInMelee(NativePointer);
            }
        }

        public bool IsAiming
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_IsAiming(NativePointer);
            }
        }

        public string ActionString
        {
            get
            {
                CheckExistence();

                return StringConverter.PointerToString(Rage.Player.Player_GetActionString(NativePointer));
            }
        }

        internal Player(IntPtr playerPointer) : base(playerPointer, EntityType.Player)
        {
            Serial = StringConverter.PointerToString(Rage.Player.Player_GetSerial(NativePointer));
        }

        public async Task KickAsync(string reason = null)
        {
            CheckExistence();

            using (var converter = new StringConverter())
            {
                var reasonPointer = converter.StringToPointer(reason);

                await TickScheduler.Instance
                    .Schedule(() => Rage.Player.Player_Kick(NativePointer, reasonPointer))
                    .ConfigureAwait(false);
            }
        }

        public async Task BanAsync(string reason = null)
        {
            CheckExistence();

            using (var converter = new StringConverter())
            {
                var reasonPointer = converter.StringToPointer(reason);

                await TickScheduler.Instance
                    .Schedule(() => Rage.Player.Player_Ban(NativePointer, reasonPointer))
                    .ConfigureAwait(false);
            }
        }

        public async Task OutputChatBoxAsync(string text)
        {
            CheckExistence();

            using (var converter = new StringConverter())
            {
                var textPointer = converter.StringToPointer(text);

                await TickScheduler.Instance
                    .Schedule(() => Rage.Player.Player_OutputChatBox(NativePointer, textPointer))
                    .ConfigureAwait(false);
            }
        }

        public async Task NotifyAsync(string text)
        {
            CheckExistence();

            using (var converter = new StringConverter())
            {
                var textPointer = converter.StringToPointer(text);

                await TickScheduler.Instance
                    .Schedule(() => Rage.Player.Player_Notify(NativePointer, textPointer))
                    .ConfigureAwait(false);
            }
        }

        public Task CallClientAsync(string eventName, params object[] arguments)
        {
            return CallClientAsync(eventName, (IEnumerable<object>) arguments);
        }

        public async Task CallClientAsync(string eventName, IEnumerable<object> arguments)
        {
            Contract.NotEmpty(eventName, nameof(eventName));
            Contract.NotNull(arguments, nameof(arguments));
            CheckExistence();

            var data = ArgumentConverter.ConvertFromObjects(arguments);

            using (var converter = new StringConverter())
            {
                var eventNamePointer = converter.StringToPointer(eventName);

                await TickScheduler.Instance
                    .Schedule(() => Rage.Player.Player__Call(NativePointer, eventNamePointer, data, (ulong) data.Length))
                    .ConfigureAwait(false);
            }

            ArgumentData.Dispose(data);
        }

        public Task CallBrowserAsync(string eventName, params object[] arguments)
        {
            var args = new object[arguments.Length + 1];
            args[0] = JsonConvert.SerializeObject(eventName);

            for (var i = 0; i < arguments.Length; i++)
            {
                args[i + 1] = JsonConvert.SerializeObject(arguments[i]);
            }

            return CallClientAsync("nf_browserEvent", args);
        }

        public Task CallBrowserAsync(string eventName, IEnumerable<object> arguments)
        {
            return CallBrowserAsync(eventName, arguments.ToArray());
        }

        public Task CallHashAsync(ulong eventHash, params object[] arguments)
        {
            return CallHashAsync(eventHash, (IEnumerable<object>) arguments);
        }

        public async Task CallHashAsync(ulong eventHash, IEnumerable<object> arguments)
        {
            Contract.NotNull(arguments, nameof(arguments));
            CheckExistence();

            var data = ArgumentConverter.ConvertFromObjects(arguments);

            await TickScheduler.Instance
                .Schedule(() => Rage.Player.Player__CallHash(NativePointer, eventHash, data, (ulong) data.Length))
                .ConfigureAwait(false);

            ArgumentData.Dispose(data);
        }

        public Task InvokeAsync(ulong nativeHash, params object[] arguments)
        {
            return InvokeAsync(nativeHash, (IEnumerable<object>) arguments);
        }

        public async Task InvokeAsync(ulong nativeHash, IEnumerable<object> arguments)
        {
            Contract.NotNull(arguments, nameof(arguments));
            CheckExistence();

            var data = ArgumentConverter.ConvertFromObjects(arguments);

            await TickScheduler.Instance
                .Schedule(() => Rage.Player.Player__Invoke(NativePointer, nativeHash, data, (ulong) data.Length))
                .ConfigureAwait(false);

            ArgumentData.Dispose(data);
        }

        public void Spawn(Vector3 position, float heading)
        {
            CheckExistence();

            Rage.Player.Player_Spawn(NativePointer, position, heading);
        }

        public void PlayAnimation(string dictionary, string name, float speed = 8, AnimationFlags flags = (AnimationFlags) 0)
        {
            Contract.NotEmpty(dictionary, nameof(dictionary));
            Contract.NotEmpty(name, nameof(name));
            CheckExistence();

            using (var converter = new StringConverter())
            {
                Rage.Player.Player_PlayAnimation(NativePointer, converter.StringToPointer(dictionary), converter.StringToPointer(name), speed, (int) flags);
            }
        }

        public void StopAnimation()
        {
            CheckExistence();

            Rage.Player.Player_StopAnimation(NativePointer);
        }

        public void PlayScenario(string scenario)
        {
            Contract.NotEmpty(scenario, nameof(scenario));
            CheckExistence();

            using (var converter = new StringConverter())
            {
                Rage.Player.Player_PlayScenario(NativePointer, converter.StringToPointer(scenario));
            }
        }

        public bool IsStreamed(IPlayer player)
        {
            Contract.NotNull(player, nameof(player));
            CheckExistence();

            return Rage.Player.Player_IsStreamed(NativePointer, player.NativePointer);
        }

        public void RemoveObject(uint model, Vector3 position, float radius)
        {
            CheckExistence();

            Rage.Player.Player_RemoveObject(NativePointer, model, position, radius);
        }

        public void RemoveObject(int model, Vector3 position, float radius)
        {
            RemoveObject((uint) model, position, radius);
        }

        public void Eval(string code)
        {
            Contract.NotEmpty(code, nameof(code));
            CheckExistence();

            using (var converter = new StringConverter())
            {
                Rage.Player.Player_Eval(NativePointer, converter.StringToPointer(code));
            }
        }

        public async Task<IReadOnlyCollection<IPlayer>> GetStreamedPlayersAsync()
        {
            CheckExistence();

            IntPtr playerPointers = IntPtr.Zero;
            ulong size = 0;

            await TickScheduler.Instance
                .Schedule(() => Rage.Player.Player_GetStreamed(NativePointer, out playerPointers, out size))
                .ConfigureAwait(false);

            return ArrayHelper.ConvertFromIntPtr(playerPointers, size, x => Collections.Collections.PlayerCollection[x]);
        }

        public void MarkDisconnected()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
