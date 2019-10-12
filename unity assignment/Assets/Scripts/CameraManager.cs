using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour // just simple switching camera
{
    public Camera MainCamera;
    public Camera FlagCamera;
    void Start()
    {
        mainCameraOn();
    }

    public void mainCameraOn()
    {
        MainCamera.enabled = true;
        FlagCamera.enabled = false;
    }
    public void flagCameraOn()
    {
        FlagCamera.enabled = true;
        MainCamera.enabled = false;
        
    }
}
