using System;
using UnityEngine;
using UnityWebSocket;

namespace Sxer.WWW.WebSocket
{
    public class UnityWebSocketConnector : MonoBehaviour
    {
        public string address = "wss://echo.websocket.events";
        public string sendText = "Hello UnityWebSocket!";
        public string receiveText;

        Action<string> OnReceiveMessageDo;
        public IWebSocket socket;

     



        public void InitConnector(string ip, Action<string> onMsgDo)
        {
            // ip+port    ws://192.168.8.228:5566
            address = ip;
            OnReceiveMessageDo = onMsgDo;

            socket = new UnityWebSocket.WebSocket(address);
            socket.OnOpen += Socket_OnOpen;
            socket.OnMessage += Socket_OnMessage;
            socket.OnClose += Socket_OnClose;
            socket.OnError += Socket_OnError;
            Debug.Log(string.Format("Connecting..."));
            socket.ConnectAsync();

        }


        public void SendWebSocketMessage(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                socket.SendAsync(msg);
                //sendCount += 1;
            }
        }

        private void Socket_OnOpen(object sender, OpenEventArgs e)
        {
            Debug.Log(string.Format("Connected: {0}", address));
            //AddLog();
        }



      //  MainProcess.DataJsonDT dt = new MainProcess.DataJsonDT();
        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            //获取接收数据
            receiveText = e.Data.ToString();
            if (OnReceiveMessageDo != null)
                OnReceiveMessageDo(receiveText);

           // receiveCount += 1;
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log(string.Format("Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            Debug.Log(string.Format("Error: {0}", e.Message));
        }


        private void OnApplicationQuit()
        {
            if (socket != null && socket.ReadyState != WebSocketState.Closed)
            {
                socket.CloseAsync();
            }
        }

       
    }
}
