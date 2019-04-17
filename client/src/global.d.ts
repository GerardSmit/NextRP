
interface Chat {
    push(message: string): void;
}

declare enum SwitchType {
    ThreeStepsBackwards = 1,
    OneStepBackwards = 2
}

interface Player {
    /**
     * Switch out of the player.
     * 
     * @param type The switch type. Default 3 step.
     * @param instant True if the switch should be instant.
     */
    switchOut(type?: SwitchType, instant?: boolean): Promise<void>;
    
    /**
     * Switch into the player.
     */
    switchIn(): Promise<void>
}

interface RpcArgument {
    type: "string"|"number";
    name: string;
}

interface RpcCommand {
    name: string;
    arguments: RpcArgument[]
}

interface NextFramework {
    player: Player;
    chat: Chat;
    server: {[name: string]: Function}
}

interface Events {
    call(name: "playerSwitchedOut"): void;
    call(name: "playerSwitchedIn"): void;
    call(name: string, ...args: any[]): void;

    on(name: "initialized", callback: () => void): Function;
    on(name: "rpcList", callback: (commands: RpcCommand[]) => void): Function;
    on(name: "rpcResult", callback: (id: number, errorCode: number, result: any) => void): Function;
    on(name: "playerSwitchedOut", callback: () => void): Function;
    on(name: "playerSwitchedIn", callback: () => void): Function;
    on(name: string, callback: Function): Function;
}

declare let nf: NextFramework;
declare let events: Events;