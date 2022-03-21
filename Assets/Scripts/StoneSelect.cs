using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneSelect : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    public int PlayerNum;
    public int StoneNum;
    void Start()
    {
        gameObject.SetActive(true);
    }
    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
    }
    public void inactivate()
    {
        gameObject.SetActive(false);
    }
    public void activate()
    {
        gameObject.SetActive(true);
    }

    public void change()
    {   
        if (PlayerNum == 1 && StoneNum == 4)
        {
            for (int i = 0; i < 3; i++)
            {
                int random = Random.Range(1,4);
                if (random == 1)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "rush";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(106.975f,102.3f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("rush");
                }
                else if (random == 2)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "slime";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,86.4f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("slime");
                }
                else if (random == 3)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "smoke";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,114.0f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("smoke");
                }
            }
        }
        else if (PlayerNum == 1 && StoneNum == 6)
        {
            for (int i = 0; i < 3; i++)
            {
                int random = Random.Range(1,4);
                if (random == 1)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "rush";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(106.975f,102.3f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("rush");
                }
                else if (random == 2)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "slime";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,86.4f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("slime");
                }
                else if (random == 3)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "smoke";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,114.0f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("smoke");
                }
            }
        }
        else if (PlayerNum == 2 && StoneNum == 4)
        {
            for (int i = 0; i < 3; i++)
            {
                int random = Random.Range(1,4);
                if (random == 1)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "rush";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(106.975f,102.3f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("rush");
                }
                else if (random == 2)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "slime";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,86.4f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("slime");
                }
                else if (random == 3)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "smoke";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,114.0f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("smoke");
                }
            }
        }
        else if (PlayerNum == 2 && StoneNum == 6)
        {
            for (int i = 0; i < 3; i++)
            {
                int random = Random.Range(1,4);
                if (random == 1)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "rush";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(106.975f,102.3f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("rush");
                }
                else if (random == 2)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "slime";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,86.4f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("slime");
                }
                else if (random == 3)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "smoke";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(108.3f,114.0f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("smoke");
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                int random = Random.Range(1,4);
                if (random == 1)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "cat";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(90.25f,91.75f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("cat");
                }
                else if (random == 2)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "frog";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(90.25f,107.25f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("frog");
                }
                else if (random == 3)
                {
                    transform.GetChild(i).GetChild(0).GetChild(0).gameObject.tag = "dog";
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(112.25f,92.5f);
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("dog");
                }
            }
        }
        
    }
    public void SelectSetFalse()
    {
        gameObject.GetComponent<Animator>().SetBool("Select", false);
    }
}
