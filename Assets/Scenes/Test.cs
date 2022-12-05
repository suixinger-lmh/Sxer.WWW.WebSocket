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
        connector = gameObject.AddComponent<UnityWebSocketConnector>();
        connector.InitConnector("wss://echo.websocket.events",(str)=> {
            Debug.LogError(str);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            connector.SendWebSocketMessage("111");
        }
    }
}
