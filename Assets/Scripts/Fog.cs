using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] ParticleSystem fog;
    GameObject gameManager;
    ParticleSystem newFog;
    public int PlayerNum;
    public int StoneNum;
    public int playernum;
    public int stonenum;
    public bool activate;
    public int time = 1;
    void Start()
    {
        activate =false;
        gameManager = GameObject.Find("GameManager");
    }

    
    void Update()
    {
        PlayerNum = gameManager.GetComponent<GameManager>().player;
        StoneNum = gameManager.GetComponent<GameManager>().round;
        if (activate && time == 1 )
        {
            Invoke("FogInstantiate",1.0f);
        }
        if (PlayerNum == playernum && StoneNum == stonenum+1)
        {
            GameObject[] fogs = GameObject.FindGameObjectsWithTag("fog");
            foreach (GameObject Fog1 in fogs)
            {
                Destroy(Fog1);
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Red" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "White" || other.gameObject.tag == "Ao")
        {
            activate = true;
        }
    }

    void FogInstantiate()
    {
        newFog = Instantiate(fog);
        newFog.gameObject.tag = "fog";
        newFog.transform.position = this.transform.position;
        newFog.Play();
        playernum = gameManager.GetComponent<GameManager>().player;
        stonenum = gameManager.GetComponent<GameManager>().round;
        activate=false;
        time = 0;
    }
}
