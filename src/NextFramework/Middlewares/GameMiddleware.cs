using System;
using System.Buffers;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NextFramework.Middlewares
{
    public class GameMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Path.Equals("/ws", StringComparison.OrdinalIgnoreCase))
            {
                return next(context);
            }

            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return Task.CompletedTask;
            }

            return AcceptWebSocket(context);
        }

        public async Task AcceptWebSocket(HttpContext context)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            using (var memory = MemoryPool<byte>.Shared.Rent(1024 * 4))
            {
                var buffer = memory.Memory;
                var token = CancellationToken.None;

                var result = await webSocket.ReceiveAsync(buffer, token);

                while (result.MessageType != WebSocketMessageType.Close)
                {
                    if (!result.EndOfMessage)
                    {
                        throw new NotSupportedException();
                    }

                    result = await webSocket.ReceiveAsync(buffer, token);
                }

                await webSocket.CloseAsync(WebSocketCloseStatus.Empty, "Connection closed", token);
            }
        }
    }
}
