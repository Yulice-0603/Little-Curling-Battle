using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAngle : MonoBehaviour
{
    public GameObject Arrow;
    [SerializeField] GameObject gameManager;
    int PlayerNum;
    int StoneNum;
    
    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
        ArrowPos();
        Vector3 rotation = new Vector3(-90,0,0);
        rotation.y = 180f - Arrow.GetComponent<Arrow>().angle;
        this.gameObject.transform.localEulerAngles = rotation;
    }

    public void inactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void activate()
    {
        this.gameObject.SetActive(true);
    }
    private void ArrowPos()
    {
        Vector3 arrowPos = new Vector3();
        arrowPos = GameObject.Find("Player"+PlayerNum+"Stone"+StoneNum).transform.position;
        arrowPos.y = -0.04f;
        transform.position = arrowPos;
    }
}
