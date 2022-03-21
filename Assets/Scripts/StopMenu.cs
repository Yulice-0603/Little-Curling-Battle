using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMenu : MonoBehaviour
{
    Animator stopAnimator;
    bool a;
    void Start()
    {
        a = false;
        stopAnimator =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (a == false)
            {
                stopAnimator.SetBool("Open",true);
                a = true;
                if (transform.position.x == 0)
                {
                    Time.timeScale = 0f;
                }
            }
            else
            {
                stopAnimator.SetBool("Open",false);
                a = false;
                if (transform.position.x == -1920)
                {
                    Time.timeScale = 1f;
                }
            }
            
        }
    }

    public void Continue()
    {
        stopAnimator.SetBool("Open",false);
        a = false;
        if (transform.position.x == -1920)
        {
            Time.timeScale = 1f;
        }
    }
    
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameExit()
    {
        Application.Quit();
    }

}
