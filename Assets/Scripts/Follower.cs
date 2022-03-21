using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Follower : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCamera;
    [SerializeField] GameObject gameManager;
    int PlayerNum;
    int StoneNum;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
    }

    public void follow()
    {
        _virtualCamera.Follow = GameObject.Find("Player"+PlayerNum+"Stone"+StoneNum).transform;
    }
}
