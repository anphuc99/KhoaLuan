using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketManager : MonoBehaviour
{
    public string webSocketURL;
    private WebSocket ws;
    private string msg = null;
    public bool IsConnected = false;

    private void Awake()
    {
        Event.register(Events.connected, Connected);
        Event.register(Events.closeSocket, Close);
        Event.register(Events.socketSendMessage, SendMessage);
    }

    private void Connected(object context)
    {
        ws = new WebSocket(webSocketURL);
        ws.Connect();
        IsConnected = true;
        Debug.Log("Connected socket");
        ws.OnClose += OnClose;
        ws.OnMessage += OnMessage;
    }

    private void Update()
    {
        if (msg != null)
        {
            string json = msg;
            Debug.Log(json);
            Json.SocketResponse response = JsonUtility.FromJson<Json.SocketResponse>(json);
            Event.emit(response.type, response.data);
            msg = null;
        }
    }

    private void OnMessage(object sender,MessageEventArgs e)
    {
        msg = e.Data;
    }
    
    private void OnClose(object sender, EventArgs e)
    {
        Debug.Log("Close connect socket");
        IsConnected = false;
    }

    private void Close(object context){
        if (!IsConnected) return;
        ws.Close();
    }

    private void SendMessage(object context)
    {
        if (!IsConnected) return;
        string json = JsonUtility.ToJson(context);
        ws.Send(json);
    }

    private void OnApplicationQuit()
    {
        ws.Close();
    }
}
