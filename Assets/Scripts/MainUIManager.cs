using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    Animator myAnimator;
    [SerializeField] GameObject gameManager;
    int PlayerNum;
    void Start()
    {
        myAnimator =  GetComponent<Animator>();
    }
    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
    }
    public void PlayertoGo()
    {
        if (PlayerNum == 1)
        {
            Player1toGo();
        }
        else
        {
            Player2toGo();
        }
    }

    public void Player1toGo()
    {
        myAnimator.SetBool("Player1toGo",true);
        myAnimator.SetBool("Player2toGo",false);
        myAnimator.SetBool("GotoPlayer2",false);
        myAnimator.SetBool("GotoPlayer1",false);
    }
    public void Player2toGo()
    {
        myAnimator.SetBool("Player2toGo",true);
        myAnimator.SetBool("Player1toGo",false);
        myAnimator.SetBool("GotoPlayer2",false);
        myAnimator.SetBool("GotoPlayer1",false);
    }
    public void GotoPlayer2()
    {
        myAnimator.SetBool("GotoPlayer2",true);
        myAnimator.SetBool("Player1toGo",false);
        myAnimator.SetBool("Player2toGo",false);
        myAnimator.SetBool("GotoPlayer1",false);
    }
    public void GotoPlayer1()
    {
        myAnimator.SetBool("GotoPlayer1",true);
        myAnimator.SetBool("Player1toGo",false);
        myAnimator.SetBool("Player2toGo",false);
        myAnimator.SetBool("GotoPlayer2",false);
    }
    
}
