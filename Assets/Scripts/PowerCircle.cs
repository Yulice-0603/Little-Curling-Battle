using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCircle : MonoBehaviour
{
    [SerializeField] GameObject powerCircle;
    public bool circleactivate;
    float angle;

    void Start()
    {
    
        circleactivate =false;
        angle = 24.00f;
        powerCircle.transform.rotation = Quaternion.Euler(0,0,angle);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            angle = angle - 43.5f*Time.deltaTime;
            
            if (angle < -63.00f)
            {
                angle = -63.00f;
            }
        }
        powerCircle.transform.rotation = Quaternion.Euler(0,0,angle);
        if(Input.GetMouseButtonUp(0))
        {
            angle = 24.00f;
        }
        
    }
    public void toactive()
    {
        circleactivate =true;
    }
}
