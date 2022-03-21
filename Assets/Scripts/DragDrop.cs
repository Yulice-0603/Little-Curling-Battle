using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    bool isDraging;
    Transform DragObj;
    private Camera mainCamera;
    float cameraFarClipPlane;
    RaycastHit hitPoint;
    private Vector3 objectPosition;
    GameObject obj;
    [SerializeField] GameObject gameManager;
    int PlayerNum;
    int StoneNum;
    void Start(){
        mainCamera = Camera.main;
        cameraFarClipPlane = Camera.main.farClipPlane;
    }

    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
        obj = GameObject.Find("Player"+PlayerNum+"Stone"+StoneNum);
        if(Input.GetMouseButton(0)){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hitPoint,Mathf.Infinity)){
                objectPosition = hitPoint.point;
                objectPosition.y = obj.transform.position.y;
                if (objectPosition.x < -1.35f)
                {
                    objectPosition.x = -1.35f;
                }
                if (objectPosition.x > 1.35f)
                {
                    objectPosition.x = 1.35f;
                }
                if (objectPosition.z > -4.4f)
                {
                    objectPosition.z = -4.4f;
                }
                if (objectPosition.z < -5.6f)
                {
                    objectPosition.z = -5.6f;
                }
                GameObject.Find("Player"+PlayerNum+"Stone"+StoneNum).transform.position = objectPosition;
            }
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
}
