mp.events.add("nf_rpcCallRemote", function(...args: any[]) {
    mp.events.callRemote("nf_rpcCall", ...args);
});

mp.events.add("nf_rpcResult", function(id, errorCode, result) {
    events.call("rpcResult", id, errorCode, JSON.parse(result));
});