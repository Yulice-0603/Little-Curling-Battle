using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePosition : MonoBehaviour
{
    void Update()
    {
        if (transform.parent.gameObject.tag == "Blue")
        {
            transform.localScale = new Vector3(0.32f,0.32f,0.32f);
        }
        transform.position = new Vector3(transform.parent.position.x,0.27f,transform.parent.position.z);
    }
}
