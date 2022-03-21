using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadOver()
    {
        SceneManager.LoadScene("Over", LoadSceneMode.Additive);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
