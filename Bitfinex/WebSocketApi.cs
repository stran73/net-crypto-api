﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalServices
{
    public class WebSocketApi
    {
        private static object consoleLock = new object();
        private const int sendChunkSize = 256;
        private const int receiveChunkSize = 256;
        private const bool verbose = true;
        private static readonly TimeSpan delay = TimeSpan.FromMilliseconds(30000);
        protected static string cmd;
       
        
        public static async Task Connect(string uri)
        {
            ClientWebSocket webSocket = null;
            
            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive(webSocket), Send(webSocket));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (webSocket != null)
                    webSocket.Dispose();
                Console.WriteLine();

                lock (consoleLock)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WebSocket closed.");
                    Console.ResetColor();
                }
            }
        }
        static UTF8Encoding encoder = new UTF8Encoding();

        private static async Task Send(ClientWebSocket webSocket)
        {
            
            byte[] buffer = encoder.GetBytes(cmd);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            while (webSocket.State == WebSocketState.Open)
            {
                LogStatus(false, buffer, buffer.Length);
                await Task.Delay(delay);
            }
        }

        private static async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[receiveChunkSize];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    LogStatus(true, buffer, result.Count);
                }
            }
        }

        private static void LogStatus(bool receiving, byte[] buffer, int length)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = receiving ? ConsoleColor.Green : ConsoleColor.Gray;
                //Console.WriteLine("{0} ", receiving ? "Received" : "Sent");

                if (verbose)
                {
                   
                    Console.WriteLine(encoder.GetString(buffer));
                }
                    

                Console.ResetColor();
            }
        }
    }
}
