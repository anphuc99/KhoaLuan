using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class SocketTest : MonoBehaviour
{
    // Start is called before the first frame update
    private TcpListener server = null;
    private Byte[] bytes = new Byte[256];
    void Start()
    {
        try
        {
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);
            server.Start();
        }
        catch(Exception ex)
        {
            Debug.Log("bug tung lon" + ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //TcpClient client = server.AcceptTcpClient();
            //if (client == null)
            //{
            //    return;
            //}
            //string data;
            //NetworkStream stream = client.GetStream();
            //int i;

            //// Loop to receive all the data sent by the client.
            //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            //{
            //    // Translate data bytes to a ASCII string.
            //    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            //    Debug.Log("Received:" + data);
            //}
            //// Shutdown and end connection
            //client.Close();
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
