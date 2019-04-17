mp.events.add('nf_chat', (message: string) => {
    mp.gui.chat.push(message);
});