using UnityEngine;

namespace UnityWebSocket.Demo
{
    public class UnityWebSocketTest : MonoBehaviour
    {
        public string address = "ws://127.0.0.1:5566";
        public string sendText = "Hello UnityWebSocket!";
        public string msgText = "";
        public string logText = "";
        public IWebSocket socket;

        public void InitWebSocket(string ip)
        {
            try
            {
                address = ip;
                socket = new WebSocket(address);
                socket.OnOpen += Socket_OnOpen;
                socket.OnMessage += Socket_OnMessage;
                socket.OnClose += Socket_OnClose;
                socket.OnError += Socket_OnError;
                AddLog("建立连接...");
                socket.ConnectAsync();
            }catch(System.Exception ex)
            {
                Debug.LogError("websocket连接失败！ex:"+ex.Message);
            }
        }

        //关闭连接
        public void CloseWebSocket()
        {
            if (socket != null && socket.ReadyState != WebSocketState.Closed)
            {
                socket.CloseAsync();
            }
        }



        public void SendWebSocketMessage(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                sendText = msg;
                socket.SendAsync(sendText);
                AddLog(string.Format("发送信息: {0}", sendText));
            }
        }

        public void SendWebSocketMessage(byte[] msg)
        {
            //var bytes = System.Text.Encoding.UTF8.GetBytes(sendText);
            sendText = System.Text.Encoding.UTF8.GetString(msg);
            socket.SendAsync(msg);
            AddLog(string.Format("发送字节信息: ({1}): {0}", msg, msg.Length));
        }






        //连接上
        private void Socket_OnOpen(object sender, OpenEventArgs e)
        {
            AddLog(string.Format("连接成功: {0}", address));
        }

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsBinary)
            {
                AddLog(string.Format("收到字节数据：({1}): {0}", e.Data, e.RawData.Length));
            }
            else if (e.IsText)
            {
                AddLog(string.Format("收到字符数据: {0}", e.Data));
            }
            msgText = e.Data;
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            AddLog(string.Format("关闭连接: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            AddLog(string.Format("Error!: {0}", e.Message));
        }




        private void AddLog(string str)
        {
            logText = str;
        }

        private void OnApplicationQuit()
        {
            CloseWebSocket();
        }
    }
}
