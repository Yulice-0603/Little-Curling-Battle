using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    PlayableDirector _Playable;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject StoneSelect;
    [SerializeField] GameObject main_Animator;
    [SerializeField] GameObject MainUI;
    public GameObject outObject;
    Animator select_Animator;
    public bool nullCollision;
    int PlayerNum;
    int StoneNum;
    public GameObject cmvcam5;
    public GameObject player1;
    public GameObject player2;
    GameObject obj;
    void Start()
    {
        MainUI.SetActive(true);
        cmvcam5.SetActive(false);
        nullCollision = true;
        select_Animator = StoneSelect.GetComponent<Animator>();
        _Playable = GetComponent<PlayableDirector>();
        _Playable.time = 0d;
        Play();
        StoneSelect.GetComponent<StoneSelect>().change();
    }

    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
        if (StoneNum <= 8)
        {
            if (_Playable.time <= 0.84d)
            {
                Pause();
            }
            if(_Playable.time >= 2.98d && _Playable.time <= 3.03)
            {
                Pause();
            }
            if(_Playable.time >= 6.82d && _Playable.time <= 6.87)
            {
                Pause();
            }
            if (nullCollision && _Playable.time >= 6.82d && _Playable.time <= 6.87)
            {
                if (PlayerNum == 1)
                {
                    obj = player1.transform.Find("Player1Stone"+StoneNum).gameObject;
                    if (obj != null)
                    {
                        if (obj.GetComponent<Rigidbody>().velocity.magnitude < 0.00001f)
                        {
                            Destroy(GameObject.Find("Player1Stone"+StoneNum).GetComponent<StoneEffective>());
                            Timereset();
                        }
                    }
                    else
                    {
                        Timereset();
                    }
                }
                if (PlayerNum == 2)
                {
                    obj = player2.transform.Find("Player2Stone"+StoneNum).gameObject;
                    if (obj != null)
                    {
                        if (obj.GetComponent<Rigidbody>().velocity.magnitude < 0.00001f)
                        {
                            Destroy(GameObject.Find("Player2Stone"+StoneNum).GetComponent<StoneEffective>());
                            Timereset();
                        }
                    }
                    else
                    {
                        Timereset();
                    }
                }

            }
            else if (!nullCollision && _Playable.time >= 11.50d)
            {
                if (PlayerNum == 1)
                {
                    obj = player1.transform.Find("Player1Stone"+StoneNum).gameObject;
                    if (obj != null)
                    {
                        if (obj.GetComponent<Rigidbody>().velocity.magnitude < 0.00001f)
                        {
                            Destroy(GameObject.Find("Player1Stone"+StoneNum).GetComponent<StoneEffective>());
                            Timereset();
                        }
                    }
                    else
                    {
                        Timereset();
                    }
                }
                if (PlayerNum == 2)
                {
                    obj = player2.transform.Find("Player2Stone"+StoneNum).gameObject;
                    if (obj != null)
                    {
                        if (obj.GetComponent<Rigidbody>().velocity.magnitude < 0.00001f)
                        {
                            Destroy(GameObject.Find("Player2Stone"+StoneNum).GetComponent<StoneEffective>());
                            Timereset();
                        }
                    }
                    else
                    {
                        Timereset();
                    }
                }
            }
        }
        else
        {
            gameManager.GetComponent<GameManager>().round = 1;
            gameManager.GetComponent<GameManager>().player = 1;
            Timechange(0d);
            _Playable.Stop();
            outObject.SetActive(true);
            cmvcam5.SetActive(true);
            cmvcam5.GetComponent<StaticTimeline>().Play();
            this.gameObject.SetActive(false);
        }      
    }
    public void Timechange(double time)
    {
        _Playable.time = time;
    }

    public void Play()
    {
        _Playable.Play();
    }

    public void Pause()
    {
        _Playable.Pause();
    }

    public void Timereset()
    {
        Timechange(0d);
        Play();
        main_Animator.transform.Find("Frame").gameObject.GetComponent<Image>().raycastTarget = true;
        if (PlayerNum == 2 && StoneNum == 8)
        {
            StoneSelect.SetActive(false);
            MainUI.SetActive(false);

        }
        else
        {
            StoneSelect.SetActive(true);
            
            if (PlayerNum == 1)
            {
                main_Animator.GetComponent<MainUIManager>().GotoPlayer2();
            }
            else if (PlayerNum == 2)
            {
                main_Animator.GetComponent<MainUIManager>().GotoPlayer1();
            }
            select_Animator.SetBool("Select",true);
        }
        outObject.SetActive(false);
        
        if (gameManager.GetComponent<GameManager>().player == 1)
        {
            gameManager.GetComponent<GameManager>().player = 2;
        }
        else if (gameManager.GetComponent<GameManager>().player == 2)
        {
            gameManager.GetComponent<GameManager>().player = 1;
            gameManager.GetComponent<GameManager>().round++;
        }
        StoneSelect.GetComponent<StoneSelect>().change();
        
    }

}
