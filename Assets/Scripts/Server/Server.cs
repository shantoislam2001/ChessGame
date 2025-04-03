using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;   // https://github.com/itisnajim/SocketIOUnity.git
using SocketIOClient.Transport;
//using UnityEditor.VersionControl;
//using Unity.VisualScripting;
//using UnityEditor.PackageManager;

public class Server : MonoBehaviour
{
    private SocketIOUnity socket;
    private string currentRoom;

    private void Start()
    {
        
        var uri = new System.Uri("https://bubbly-lydian-gopher.glitch.me");
       // var uri = new System.Uri("http://localhost:3000");

        var options = new SocketIOOptions
        {
            Transport = TransportProtocol.WebSocket, // Always use WebSocket for Glitch
            Reconnection = true,
            ReconnectionAttempts = 5,
            ReconnectionDelay = 2000,
            ExtraHeaders = new Dictionary<string, string>
            {
                { "user-agent", "unity" }
            }
        };

        socket = new SocketIOUnity(uri, options);

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log(" Connected to server");
            ServerInteraction.self.ServerConnectEvent();
        };


        socket.On("roomCreated", response =>
        {
            string roomName = response.GetValue<string>();
            currentRoom = roomName;
            Debug.Log($"Room {roomName} created and joined");
            ServerInteraction.self.RoomCreateEvent();
        });


        socket.On("roomJoined", response =>
        {
            string roomName = response.GetValue<string>();
            currentRoom = roomName;
            Debug.Log($"Joined room: {roomName}");
            ServerInteraction.self.RoomJoinEvent();
        });


        socket.On("pice", response =>
        {
            int message = response.GetValue<int>();
            Debug.Log($"Message received: {message}");
            ServerInteraction.self.PiceClick( message );
        });


        socket.On("box", response =>
        {
            int message = response.GetValue<int>();
            Debug.Log($"Message received: {message}");
            ServerInteraction.self.BoxClick(message);
        });


        socket.On("roomList", response =>
        {
            List<string> rooms = response.GetValue<List<string>>();
            Debug.Log("Active Rooms: " + string.Join(", ", rooms));
        });

        socket.On("error", response =>
        {
            string errorMessage = response.GetValue<string>();
            Debug.LogError("Error: " + errorMessage);
        });



        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log(" Disconnected from server: " + e);
        };

        socket.Connect();

    }




    public void CreateRoom(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            socket.EmitAsync("createRoom", roomName);
        }
    }

    public void JoinRoom(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            socket.EmitAsync("joinRoom", roomName);
        }
    }

    public void sendPice(int message)
    {
        if (!string.IsNullOrEmpty(currentRoom) && message > -1)
        {
            socket.EmitAsync("piceSelect", new { roomName = currentRoom, message });
        }
    }


    public void sendBox(int message)
    {
        if (!string.IsNullOrEmpty(currentRoom) && message > -1)
        {
            socket.EmitAsync("boxSelect", new { roomName = currentRoom, message });
        }
    }





    private void OnApplicationQuit()
    {
        socket?.Dispose();
    }
}
