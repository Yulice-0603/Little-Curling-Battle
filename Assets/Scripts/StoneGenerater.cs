using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneGenerater : MonoBehaviour
{
    [SerializeField] GameObject red;
    [SerializeField] GameObject green;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject white;
    [SerializeField] GameObject yellow;
    [SerializeField] GameObject ao;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] GameObject redcicle;
    [SerializeField] GameObject greencircle;
    [SerializeField] GameObject main_Animator;
    GameObject Obj;
    GameObject circleObj;
    public GameObject DragDrop;
    //public GameObject Shot;
    public GameObject Arrow;
    [SerializeField] GameObject gameManager;
    int PlayerNum;
    int StoneNum;

    private void Update() 
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
    }
    public void generater()
    {
        if (this.gameObject.tag == "cat")
        {
            Red();
        }
        if (this.gameObject.tag == "frog")
        {
            Green();
        }
        if (this.gameObject.tag == "dog")
        {
            Blue();
        }
        if (this.gameObject.tag == "rush")
        {
            Yellow();
        }
        if (this.gameObject.tag == "slime")
        {
            Ao();
        }
        if (this.gameObject.tag == "smoke")
        {
            White();
        }
    }
    
    private void Red()
    {
        if (PlayerNum == 1)
        {
            Obj = Instantiate (red, new Vector3 (0.0f, 0.323f, -5.5f), Quaternion.identity, player1);
            circleObj = Instantiate(greencircle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        else if (PlayerNum == 2)
        {
            Obj = Instantiate (red, new Vector3 (0.0f, 0.323f, -5.5f), Quaternion.identity, player2);
            circleObj = Instantiate(redcicle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        Obj.name = "Player"+PlayerNum+"Stone"+StoneNum;
        Obj.tag = "Red";
        DragDrop.SetActive(true);
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = false;
        //Shot.SetActive(true);
    }
    private void Green()
    {
        if (PlayerNum == 1)
        {
            Obj = Instantiate (green, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player1);
            circleObj = Instantiate(greencircle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        else if (PlayerNum == 2)
        {
            Obj = Instantiate (green, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player2);
            circleObj = Instantiate(redcicle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        Obj.name = "Player"+PlayerNum+"Stone"+StoneNum;
        Obj.tag = "Green";
        DragDrop.SetActive(true);
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = false;
        //Shot.SetActive(true);
    }
    private void Blue()
    {
        if (PlayerNum == 1)
        {
            Obj = Instantiate (blue, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player1);
            circleObj = Instantiate(greencircle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        else if (PlayerNum == 2)
        {
            Obj = Instantiate (blue, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player2);
            circleObj = Instantiate(redcicle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        Obj.name = "Player"+PlayerNum+"Stone"+StoneNum;
        Obj.tag = "Blue";
        DragDrop.SetActive(true);
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = false;
        //Shot.SetActive(true);
    }
    private void White()
    {
        if (PlayerNum == 1)
        {
            Obj = Instantiate (white, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player1);
            circleObj = Instantiate(greencircle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        else if (PlayerNum == 2)
        {
            Obj = Instantiate (white, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player2);
            circleObj = Instantiate(redcicle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        Obj.name = "Player"+PlayerNum+"Stone"+StoneNum;
        Obj.tag = "White";
        DragDrop.SetActive(true);
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = false;
        //Shot.SetActive(true);
    }
    private void Yellow()
    {
        if (PlayerNum == 1)
        {
            Obj = Instantiate (yellow, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player1);
            circleObj = Instantiate(greencircle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        else if (PlayerNum == 2)
        {
            Obj = Instantiate (yellow, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player2);
            circleObj = Instantiate(redcicle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        Obj.name = "Player"+PlayerNum+"Stone"+StoneNum;
        Obj.tag = "Yellow";
        DragDrop.SetActive(true);
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = false;
        //Shot.SetActive(true);
    }
    private void Ao()
    {
        if (PlayerNum == 1)
        {
            Obj = Instantiate (ao, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player1);
            circleObj = Instantiate(greencircle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        else if (PlayerNum == 2)
        {
            Obj = Instantiate (ao, new Vector3 (0.0f, 0.25f, -5.5f), Quaternion.identity, player2);
            circleObj = Instantiate(redcicle, new Vector3(0.0f, 0.27f, -5.5f),Quaternion.identity, Obj.transform);
        }
        Obj.name = "Player"+PlayerNum+"Stone"+StoneNum;
        Obj.tag = "Ao";
        DragDrop.SetActive(true);
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = false;
        //Shot.SetActive(true);
    }
    
}
