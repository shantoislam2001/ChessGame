using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = 1280f / 1520f; // Your reference aspect ratio

        Camera.main.orthographicSize *= targetRatio / screenRatio;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
