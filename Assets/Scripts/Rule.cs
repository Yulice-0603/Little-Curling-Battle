using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    int childQuantity;
    public static string winner;
    public static int score = 0;
    Transform player1;
    Transform player2;
    Transform house;
    public void win()
    {
        player1 = GameObject.Find("Player1").transform;
        player2 = GameObject.Find("Player2").transform;
        house = GameObject.Find("Out").transform;
        int Player1ChildCount = player1.childCount;
        int Player2ChildCount = player2.childCount;
        float min1 = Mathf.Infinity;
        float min2 = Mathf.Infinity;
        for (int i = 0; i < Player1ChildCount; i++)
        {
            Vector3 dis = player1.GetChild(i).gameObject.transform.position - house.position;

            if (dis.magnitude < min1)
            {
                min1 = dis.magnitude;
            }
        }
        for (int j = 0; j < Player2ChildCount; j++)
        {
            Vector3 dis = player2.GetChild(j).gameObject.transform.position - house.position;

            if (dis.magnitude < min2)
            {
                min2 = dis.magnitude;
            }
        }
        if (min1 < min2)
        {
            winner = "Player1";
            Scoring(1,min2);
        }
        else if(min1 > min2)
        {
            winner = "Player2";
            Scoring(2,min1);
        }
        else
        {
            winner = "Draw";
        }
    }
    private void Scoring(int player,float min)
    {
        int Player1ChildCount = player1.childCount;
        int Player2ChildCount = player2.childCount;
        if (player == 1)
        {
            for (int i = 0; i < Player1ChildCount; i++)
            {
                Vector3 dis = player1.GetChild(i).gameObject.transform.position - house.position;

                if (dis.magnitude < min)
                {
                    score++;
                }
            }
        }
        else if (player == 2)
        {
            for (int i = 0; i < Player2ChildCount; i++)
            {
                Vector3 dis = player2.GetChild(i).gameObject.transform.position - house.position;

                if (dis.magnitude < min)
                {
                    score++;
                }
            }
        }
    }
}
