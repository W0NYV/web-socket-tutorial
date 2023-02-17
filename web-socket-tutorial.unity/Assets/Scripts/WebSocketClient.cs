using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;

public class WebSocketClient : MonoBehaviour
{

    [SerializeField] private string _msg;
    private SocketIOUnity _socket;

    private void Start() {
        
        var uri = new Uri("http://localhost:3000");

        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        _socket.OnConnected += (sender, e) => {
            Debug.Log("Connected");
        };

        _socket.Connect();
        
        _socket.On("receiveMessage", (msg) => {
            Debug.Log(msg);
        });

    }

    private void Update() {
        
        if(Input.GetKeyDown("s"))
        {
            Debug.Log("Send !");
            _socket.Emit("sendMessage", _msg);
        }

    }
}
