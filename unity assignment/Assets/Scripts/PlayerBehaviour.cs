using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerBehaviour : MonoBehaviour
{
    private TextMesh textMain; // text at MainCamera
    private TextMesh textFlag; //  text at flagCamera 
    private GameObject flag;
    private Vector3 offset;
    private Rigidbody rb;
    private Vector3 mousePrevPos;
    private Vector3 mouseCurPos;
    private CameraManager cameraManager; // CameraManager is implemented at /Assets/Scripts  ( CameraManager will switch camera)

    float coordZ;
    float coordY;
    float speed;
    bool isPlayerDrag;


    // Start is called before the first frame update
    void Start()
    {
        textMain = (GameObject.Find("textMain") as GameObject).GetComponent<TextMesh>();
        textFlag = (GameObject.Find("textFlag") as GameObject).GetComponent<TextMesh>();
        flag = GameObject.FindGameObjectWithTag("Flag");
        showDistance();
        rb = GetComponent<Rigidbody>();
        isPlayerDrag = true;
        speed = 0;
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }
    private void FixedUpdate() // 물리효과가 계산될 때 마다 호출
    {
        if (isPlayerDrag && transform.position.x > (float)5.9) // if car is out of main Camera, camera will switched to Flag Camera
        {
            isPlayerDrag = false; // if car is out of main Camera, player can not be dragged
            cameraManager.flagCameraOn();
        }

        showDistance();
        
    }

    private void OnMouseDown() // called if press mouse left button at player
    {
        if (!isPlayerDrag) return;

        /*
         * 플레이어가 드래그 될 수 있도록 하기 위한 사전작업
         * offset을 구해 player가 마우스를 제대로 따라갈수 있도록 함.
         */

        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        coordZ = playerScreenPos.z;
        coordY = playerScreenPos.y;
        
        offset = transform.position - getMouseWorldPos(); 

        /*
         * 마우스 속도를 알기 위한 사전작업
         */

        mousePrevPos = Input.mousePosition;
        mousePrevPos.z = coordZ;
    }
    
    
    private void OnMouseDrag() // called if player is dragging
    {
        if (!isPlayerDrag) return;


        transform.position = getMouseWorldPos() + offset;

        mouseCurPos = Input.mousePosition;
        speed = mouseCurPos.x - mousePrevPos.x;
        mousePrevPos = mouseCurPos;

        
        showDistance();
    }

    private void OnMouseUp() // called if release button at player
    {
        if (!isPlayerDrag) return;
        Debug.Log("speed " + speed);
        rb.AddForce(new Vector3(speed * 100, 0, 0));

    }

    Vector3 getMouseWorldPos() // return mouse position at world point
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = coordZ; // player position.y at screen
        mousePos.y = coordY; // player position.z at screen
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return mouseWorldPos;
    }



    void showDistance() // change 3Dtexts to show distance between player and flag
    {
        Vector3 playerPos = transform.position;
        Vector3 flagPos = flag.transform.position;
        float dis = playerPos.x - flagPos.x;
        dis += (float)0.95;
        if (dis < 20)
        {
            dis = -dis;
            textMain.text = dis.ToString();
            textFlag.text = dis.ToString();
        }
        else
        {
            textFlag.text = "OOPS!";
        }
    }
   
}
