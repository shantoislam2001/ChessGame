using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerInteraction : MonoBehaviour
{
    public static ServerInteraction self;
    public PieceController[] pices;
    public BoxController[] boxes;
    public BoxCollider2D[] whitePice;
    public BoxCollider2D[] blackPice;
    public Server server;

    public InputField input;
    public GameObject game;
    public GameObject canvas;

    private void Awake()
    {
        self = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void PiceClick(int n)
    {
        Debug.Log("entered");
        if (pices[n] == null)
        {
            Debug.LogError("pices[" + n + "] is null!");
            return;
        }

        UnityMainThreadDispatcher.Enqueue(() =>
        {
            pices[n].click();
        });
    }


    public void BoxClick(int n)
    {
        Debug.Log("entered");
        if (boxes[n] == null)
        {
            Debug.LogError("pices[" + n + "] is null!");
            return;
        }

        UnityMainThreadDispatcher.Enqueue(() =>
        {
            boxes[n].Click();
        });
    }


    public void SendPice(int m)
    {
        server.sendPice(m);
    }

    public void SendBox(int m)
    {
        server.sendBox(m);
    }

    
    public void createRoom()
    {
        server.CreateRoom(input.text);
    }

    public void joinRoom()
    {
        server.JoinRoom(input.text);
    }




    public void ServerConnectEvent()
    {
        UnityMainThreadDispatcher.Enqueue(() =>
        {
            canvas.SetActive(true);
        });
    }

    public void RoomCreateEvent()
    {
        UnityMainThreadDispatcher.Enqueue(() =>
        {
            canvas.SetActive(false);
            game.SetActive(true);

            foreach (var p in blackPice)
            {
                p.enabled = false;
            }

        });
    }

    public void RoomJoinEvent()
    {
        UnityMainThreadDispatcher.Enqueue(() =>
        {
            canvas.SetActive(false);
            game.SetActive(true);

            foreach (var p in whitePice)
            {
                p.enabled = false;
            }

        });
    }

}
