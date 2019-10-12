using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerBehaviour : MonoBehaviour
{
    private TextMesh textMain; // text at MainCamera
    private TextMesh textFlag; // <-이런식으로 변수 지으면 안됨 / text at flagCamera 
    private GameObject flag;
    private Vector3 offset;
    private Rigidbody rb;
    private Vector3 mousePrevPos;
    private Vector3 mouseCurPos;
    private CameraManager cameraManager; // scripts 안에 구현되어 있음

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
        if (isPlayerDrag && transform.position.x > (float)5.9)
        {
            isPlayerDrag = false;
            cameraManager.flagCameraOn();
        }

        showDistance();
        
    }

    private void OnMouseDown()
    {
        if (!isPlayerDrag) return;

        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        coordZ = playerScreenPos.z;
        coordY = playerScreenPos.y;
        
        offset = transform.position - getMouseWorldPos();

        mousePrevPos = Input.mousePosition;
        mousePrevPos.z = coordZ;
    }
    
    
    private void OnMouseDrag()
    {
        if (!isPlayerDrag) return;


        transform.position = getMouseWorldPos() + offset;

        mouseCurPos = Input.mousePosition;
        speed = mouseCurPos.x - mousePrevPos.x;
        mousePrevPos = mouseCurPos;

        
        showDistance();
    }

    private void OnMouseUp()
    {
        if (!isPlayerDrag) return;
        Debug.Log("speed " + speed);
        rb.AddForce(new Vector3(speed * 100, 0, 0));

    }

    Vector3 getMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = coordZ;
        mousePos.y = coordY;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return mouseWorldPos;
    }



    void showDistance()
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
