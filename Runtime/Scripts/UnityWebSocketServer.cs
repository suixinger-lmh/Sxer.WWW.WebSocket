using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fleck;

namespace Sxer.WWW.WebSocket
{
    public class UnityWebSocketServer
    {
        public static WebSocketServer server;
        public static List<IWebSocketConnection> allSockets;
        
        public static void OpenWebSocketServer(string address)
        {
            //FleckLog.Level = LogLevel.Debug;
            allSockets = new List<IWebSocketConnection>();
            server = new WebSocketServer(address);
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Debug.Log("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Debug.Log("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Debug.Log(message);
                    for (int i = 0; i < allSockets.Count; i++)
                    {
                        allSockets[i].Send("Echo: " + message);
                    }
                };
                socket.OnBinary = message =>
                {
                    Debug.Log(message);
                    Debug.Log(System.Text.Encoding.UTF8.GetString(message));
                    for (int i = 0; i < allSockets.Count; i++)
                    {
                        allSockets[i].Send(message);
                    }
                };
                socket.OnError = message =>
                {
                    Debug.Log("OnError!");
                };
            });
        }

        public static void CloseWebSocketServer()
        {
            if(allSockets!=null && allSockets.Count > 0)
            {
                foreach (var xi in allSockets)
                {
                    xi.Close();
                }
                allSockets.Clear();
            }
           
            if(server!=null)
                server.Dispose();
        }



    }

}
