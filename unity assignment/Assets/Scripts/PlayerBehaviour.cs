using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private TextMesh textDistance;
    private GameObject flag;
    private Vector3 offset;
    float coordZ;
    float coordY;

    // Start is called before the first frame update
    void Start()
    {
        textDistance = (GameObject.Find("textDistance") as GameObject).GetComponent<TextMesh>();
        flag = GameObject.FindGameObjectWithTag("Flag");
        showDistance();
    }
    private void OnMouseDown()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        coordZ = playerScreenPos.z;
        coordY = playerScreenPos.y;
        

        offset = transform.position - getMouseWorldPos();
        
    }
    private Vector3 getMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = coordZ;
        mousePos.y = coordY;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return mouseWorldPos;
    }
    
    // Update is called once per frame
    private void OnMouseDrag()
    {
        transform.position = getMouseWorldPos() + offset;
        
    }



    void showDistance()
    {
        Vector3 playerPos = transform.position;
        Vector3 flagPos = flag.transform.position;
        float text = playerPos.x - flagPos.x;
        textDistance.text = text.ToString();
    }
   
}
