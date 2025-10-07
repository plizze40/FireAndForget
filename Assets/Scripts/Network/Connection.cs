using System;
using NativeWebSocket;
using Newtonsoft.Json;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private WebSocket _websocket;

    public string Host = "127.0.0.1";
    public ushort Port = 3000;
    
    public event Action Connected;
    public event Action Disconnected;
    public event Action<string> MessageReceived; 
    
    public static Connection Singleton { get; private set; }

    private void Awake()
    {
        _websocket = new WebSocket($"ws://{Host}:{Port}");

        _websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            Connected?.Invoke();
        };

        _websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        _websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
            Disconnected?.Invoke();
        };

        _websocket.OnMessage += (bytes) =>
        {
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
                
            MessageReceived?.Invoke(message);
        };
        
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public async void ConnectToHost()
    {
        try
        { 
            await _websocket.Connect();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    } 
    
    public async void DisconnectFromHost()
    {
        try
        {
            if (_websocket.State == WebSocketState.Open)
            {
                await _websocket.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void SendData(string data)
    {
        if (_websocket is { State: WebSocketState.Open })
        {
            _websocket.SendText(data);
        }
    }

    public void SendData<T>(T data) where T : MessageBase
    {
        SendData(JsonConvert.SerializeObject(data));
    }
    

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _websocket.DispatchMessageQueue();
#endif
    }

    private async void SendWebSocketMessage()
    {
        try
        {
            if (_websocket.State == WebSocketState.Open)
            {
                // Sending bytes
                await _websocket.Send(new byte[] { 10, 20, 30 });

                // Sending plain text
                await _websocket.SendText("plain text message");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private async void OnApplicationQuit()
    {
        try
        {
            await _websocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}