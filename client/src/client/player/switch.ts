const Natives = {
    SWITCH_OUT_PLAYER: '0xAAB3200ED59016BC',
    SWITCH_IN_PLAYER: '0xD8295AF639FD9CB8',
    IS_PLAYER_SWITCH_IN_PROGRESS: '0xD9D2CFFF49FAB35F'
};

mp.events.add('nf_playerSwitchOut', (type: number, instant: number = 0) => {
    mp.game.invoke(Natives.SWITCH_OUT_PLAYER, mp.players.local.handle, instant ? 1 : 0, type);

    setTimeout(() => events.call("playerSwitchedOut"), type === 2 ? 1000 : 2300)
});

mp.events.add('nf_playerSwitchIn', () => {
    mp.game.invoke(Natives.SWITCH_IN_PLAYER, mp.players.local.handle);
    
    const intervalId = setInterval(() => {
        if (!mp.game.invoke(Natives.IS_PLAYER_SWITCH_IN_PROGRESS)) {
            events.call("playerSwitchedIn");
            clearInterval(intervalId);
        }
    }, 100);  
});