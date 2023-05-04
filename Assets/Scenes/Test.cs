using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sxer.WWW.WebSocket;
public class Test : MonoBehaviour
{
    UnityWebSocketConnector connector;
    // Start is called before the first frame update
    void Start()
    {
        UnityWebSocketServer.OpenWebSocketServer("ws://127.0.0.1:5566");


        connector = gameObject.AddComponent<UnityWebSocketConnector>();
        connector.InitWebSocket("ws://127.0.0.1:5566");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            connector.SendWebSocketMessage("111");
        }

        if (Input.GetKey(KeyCode.U))
        {
            UnityWebSocketServer.CloseWebSocketServer();
        }
    }

    
}
