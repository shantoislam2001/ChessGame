using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public GameObject game;
    public GameObject canvas;
    public Server server;
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
      
        

        
    }

    

    public void createRoom()
    {
        server.CreateRoom(input.text);
    }

    public void joinRoom()
    {
        server.JoinRoom(input.text);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
