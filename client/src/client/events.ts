const browsers = new Set<BrowserMp>();
let rpcCommandsString: string;

mp.events.add("nf_rpcList", function(json) {
    rpcCommandsString = json;

    for (let browser of browsers) {
        browser.execute(`if(events){events.call("rpcList", ${rpcCommandsString})}else{__rpc=${rpcCommandsString}}`);
    }
});

mp.events.add('browserCreated', (browser: BrowserMp) => {
    browsers.add(browser);

    if(rpcCommandsString) {
        browser.execute(`if(events){events.call("rpcList", ${rpcCommandsString})}else{__rpc=${rpcCommandsString}}`);
    }
});

mp.events.add("nf_browserEvent", function(...args: any[]) {
    for (const browser of browsers) {
        browser.execute(`events.call(${args.join(", ")})`);
    }
});

events = {
    call(name: string, ...args: any[]) {
        for (const browser of browsers) {
            browser.execute(`if(events) { events.call("${name}", ${args.map(a => JSON.stringify(a)).join(", ")}); }`);
        }
    },

    on(name: string, callback: Function) {
        throw new Error("Cannot register events on the client-side.");
    }
}