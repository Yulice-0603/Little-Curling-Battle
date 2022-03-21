using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text Player;
    public Text Point;
    void Update()
    {
        Player.text = Rule.winner;
        if (Rule.winner == "Player1")
        {
            Player.color = new Color(103f,204f,123f,1.0f);
        }
        if (Rule.winner == "Player2")
        {
            Player.color = new Color(255f,77f,66f,1.0f);
        }
        
        Point.text = Rule.score.ToString();
    }
}
