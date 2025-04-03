using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInteraction : MonoBehaviour
{
    public static ServerInteraction self;
    public PieceController[] pices;
    public BoxController[] boxes;
    public Server server;

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
