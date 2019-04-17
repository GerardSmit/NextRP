using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NextFramework.Data.Collections;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Entities
{
    internal partial class Player
    {
        public async Task<IReadOnlyCollection<IPlayer>> GetVoiceListenersAsync()
        {
            CheckExistence();

            IntPtr players = IntPtr.Zero;
            ulong count = 0;

            await TickScheduler.Instance
                .Schedule(() => Rage.Player.Player_GetVoiceListeners(NativePointer, out players, out count));

            return ArrayHelper.ConvertFromIntPtr(players, count, x => Collections.Collections.PlayerCollection[x]);
        }

        public async Task EnableVoiceToAsync(IPlayer target)
        {
            Contract.NotNull(target, nameof(target));
            Contract.EntityValid(target, nameof(target));
            CheckExistence();

            await TickScheduler.Instance
                .Schedule(() => Rage.Player.Player_EnableVoiceTo(NativePointer, target.NativePointer));
        }

        public async Task DisableVoiceToAsync(IPlayer target)
        {
            Contract.NotNull(target, nameof(target));
            Contract.EntityValid(target, nameof(target));
            CheckExistence();

            await TickScheduler.Instance
                .Schedule(() => Rage.Player.Player_DisableVoiceTo(NativePointer, target.NativePointer));
        }
    }
}
