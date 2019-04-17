mp.events.add("entityStreamIn", (entity: EntityMp) => {
    entity.setVisible(!entity.getVariable('invisible'), true);
    entity.setInvincible(!!entity.getVariable('invincible'));
});

mp.events.addDataHandler("invisible", (entity: EntityMp, value: boolean) => entity.setVisible(!value, true));
mp.events.addDataHandler("invincible", (entity: EntityMp, value: boolean) => entity.setInvincible(value));