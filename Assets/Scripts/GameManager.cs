using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int round;
    public int player;
    [SerializeField] Text Blackround;
    void Start()
    {
        round = 1;
        player = 1;
        
    }
    void Update()
    {
        Blackround.text = round.ToString();
    }

}
