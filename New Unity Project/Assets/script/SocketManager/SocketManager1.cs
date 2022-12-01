using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;
public class SocketManager1 : MonoBehaviour
{
    WebSocket websocket;

    private void Awake()
    {
        Event.register(Events.socketSendMessage, SendWebSocketMessage);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        websocket = new WebSocket("wss://devmini.com");
        //wss://devmini.com
        //ws://localhost:3000

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += OnMessage;
        StartCoroutine(enumerator());

        // waiting for messages
        websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    IEnumerator enumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);            
        }
    }

    private void SendWebSocketMessage(object context)
    {
        if (websocket.State == WebSocketState.Open)
        {
            websocket.SendText(JsonUtility.ToJson(context));
        }
    }

    private void OnMessage(byte[] bytes)
    {
        try
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("message: " + message);
            Json.SocketResponse socketResponse = JsonUtility.FromJson<Json.SocketResponse>(message);
            Event.emit(socketResponse.type, socketResponse.data);
        }
        catch { }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
