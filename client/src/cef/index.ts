let eventsByName: {[name: string]: Set<Function>} = {};
let ready = false;

function trigger(eventName: string, ...args: (string|number)[]) {
    (<any>mp).trigger(eventName, ...args);
}

// Enums
const global = <any>window;

global.SwitchType = {
    OneStepTowards: 0,
    ThreeStepsBackwards: 1,
    OneStepBackwards: 2
};

// Events
events = {
    call(name: string, ...args: any[]) {
        const registered = eventsByName[name];

        if (registered && registered.size > 0) {
            for (const item of registered) {
                item.apply(undefined, args);
            }
        }
    },

    on(name: string, callback: Function) {
        if (name === "initialized" && ready) {
            callback();
        }

        const registered = eventsByName[name] || (eventsByName[name] = new Set());
        registered.add(callback);
        return () => registered.delete(callback);
    }
};

// RPC
const rpcCommands: {[name: string]: Function} = {};
let rpcCounter = 0;

function registerRpc(command: RpcCommand) {
    console.log(command, command.name)
    rpcCommands[command.name] = function(...args: any[]) {
        return new Promise((resolve, reject) => {
            // TODO: Check the arguments.

            const id = rpcCounter++;
            const dispose = events.on("rpcResult", (rpcId, errorCode, result) => {
                if (id === rpcId) {
                    if (errorCode === 0) {
                        resolve(result);
                    } else {
                        reject(result);
                    }
                    dispose();
                }
            });

            trigger("nf_rpcCallRemote", command.name, id, ...args.map(a => JSON.stringify(a)));
        });
    };
}

nf = {
    chat: {
        push(message: string) {
            trigger("nf_chat", message);
        }
    },
    player: {
        switchOut(type: SwitchType = SwitchType.ThreeStepsBackwards, instant: boolean = false) {
            return new Promise(resolve => {
                const dispose = events.on("playerSwitchedOut", () => {
                    resolve();
                    dispose();
                });

                trigger("nf_playerSwitchOut", type, instant ? 1 : 0);
            });
        },

        switchIn() {
            return new Promise(resolve => {
                const dispose = events.on("playerSwitchedIn", () => {
                    resolve();
                    dispose();
                });

                trigger("nf_playerSwitchIn");
            });
        }
    },
    server: rpcCommands
};

if (global.__rpc) {
    for (const command of global.__rpc) {
        registerRpc(command);
    }
    
    delete global.__rpc;
    events.call("initialized");
    ready = true;
} else {
    events.on("rpcList", (commands) => {
        console.log(commands);
        for (const command of commands) {
            registerRpc(command);
        }
    
        events.call("initialized");
        ready = true;
    });
}