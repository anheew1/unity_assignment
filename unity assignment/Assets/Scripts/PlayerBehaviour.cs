using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private TextMesh textDistance;
    private GameObject flag;
    private Vector3 offset;
    private Rigidbody rb;

    private Vector3 mousePrevPos;
    private Vector3 mouseCurPos;
    float coordZ;
    float coordY;
    float speed;
    bool isMouseUp;


    // Start is called before the first frame update
    void Start()
    {
        textDistance = (GameObject.Find("textDistance") as GameObject).GetComponent<TextMesh>();
        flag = GameObject.FindGameObjectWithTag("Flag");
        showDistance();
        rb = GetComponent<Rigidbody>();
        isMouseUp = false;
        speed = 0;
    }
    private void FixedUpdate() // 물리효과가 계산될 때 마다 호출
    {
        if (!isMouseUp) return;

        rb.velocity = new Vector3(speed, 0, 0);
        if(speed >0.1)
        {
            speed -= (float) 0.1;
        }
        else if(speed <-0.1)
        {
            speed += (float)0.1;
        }
        else  // -0.1 < speed <0.1 
        {
            speed = 0;
        }
        
    }

    private void OnMouseDown()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        coordZ = playerScreenPos.z;
        coordY = playerScreenPos.y;
        
        offset = transform.position - getMouseWorldPos();

        mousePrevPos = Input.mousePosition;
        mousePrevPos.z = coordZ;
    }
    
    
    // Update is called once per frame
    private void OnMouseDrag()
    {
        transform.position = getMouseWorldPos() + offset;

        mouseCurPos = Input.mousePosition;
        speed = mouseCurPos.x - mousePrevPos.x;
        mousePrevPos = mouseCurPos;
        
        showDistance();
    }

    private void OnMouseUp()
    {
        isMouseUp = true;
        Debug.Log("speed " + speed);
        
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
        float text = playerPos.x - flagPos.x;
        text += (float)0.95;
        textDistance.text = text.ToString();
    }
   
}
