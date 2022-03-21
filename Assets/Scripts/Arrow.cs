using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Camera mainCamera;
    float cameraFarClipPlane;
    public Vector3 cursorPosition;
    RaycastHit hitPoint;
    RaycastHit hit;
    GameObject obj;
    //public GameObject Shot;
    public GameObject arrow;
    public GameObject cmvcam4;
    public GameObject outObject;
    public GameObject mainUI;
    public float angle;
    public float truespeed;
    public Vector2 dis;
    [SerializeField] GameObject gameManager;
    int PlayerNum;
    int StoneNum;
    bool nullCollision = true;
    public float speed;
    void Start()
    {
        mainCamera = Camera.main;
        cameraFarClipPlane = Camera.main.farClipPlane;
        truespeed = 70;
    }

    void Update()
    {
        
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
        obj = GameObject.Find("Player"+PlayerNum+"Stone"+StoneNum);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray,out hitPoint,Mathf.Infinity);
        cursorPosition = hitPoint.point;
        cursorPosition.y = obj.transform.position.y;
        
        Vector3 diff = cursorPosition - obj.transform.position;
        Vector3 dir = diff.normalized;
        Ray obray = new Ray(obj.transform.position,dir);
        if (Physics.Raycast(obray.origin,obray.direction,out hit,Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Red" || hit.collider.gameObject.tag == "Blue" || hit.collider.gameObject.tag == "Green" || hit.collider.gameObject.tag == "White" || hit.collider.gameObject.tag == "Yellow" || hit.collider.gameObject.tag == "Ao")
            {
                nullCollision = false;
                //Debug.Log(hit.collider.gameObject.tag);
                //dis = new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.z) - new Vector2(obj.transform.position.x,obj.transform.position.z);
                //truespeed = 3.16f*dis.magnitude+60.18f+12.50f;
                if(Input.GetMouseButton(0))
                {
                    truespeed = truespeed+16.5f*Time.deltaTime;
                    if (truespeed>108f)
                    {
                        truespeed = 103f;
                    }
                }
            }
        }
        else
        {
            if(Input.GetMouseButton(0))
            {
                truespeed = truespeed+14*Time.deltaTime;
                if (truespeed>98f)
                {
                    truespeed = 98f;
                }
            }
            //dis = new Vector2(cursorPosition.x, 1.86f) - new Vector2(obj.transform.position.x,obj.transform.position.z);
            //truespeed = 4.54f*dis.magnitude+45.55f+Random.Range(1.00f,10.00f);
            nullCollision = true;
        }
        obj.GetComponent<StoneMove>().speed = truespeed;
        angle = Vector3.SignedAngle(dir, Vector3.forward,Vector3.up);
        if(Input.GetMouseButtonUp(0))
        {
            cmvcam4.GetComponent<TimelineController>().nullCollision = nullCollision;
            outObject.SetActive(true);
            obj.GetComponent<StoneMove>().shot(cursorPosition);
            cmvcam4.GetComponent<TimelineController>().Timechange(3.04d);
            cmvcam4.GetComponent<TimelineController>().Play();
            inactivate();
            //Shot.gameObject.SetActive(false);
            arrow.gameObject.SetActive(false);
            truespeed = 70f;
            mainUI.GetComponent<MainUIManager>().PlayertoGo();
        }
    }
    public void inactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Timechange085()
    {
        cmvcam4.GetComponent<TimelineController>().Timechange(0.85d);
        cmvcam4.GetComponent<TimelineController>().Play();
    }
}
