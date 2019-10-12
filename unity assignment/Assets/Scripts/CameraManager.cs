using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera MainCamera;
    public Camera FlagCamera;
    void Start()
    {
        mainCameraOn();
    }

    // Update is called once per frame
    public void mainCameraOn()
    {
        MainCamera.enabled = true;
        FlagCamera.enabled = false;
    }
    public void flagCameraOn()
    {
        Debug.Log("flag!");
        FlagCamera.enabled = true;
        MainCamera.enabled = false;
        
    }
}
