using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    public bool rush;
    void Start()
    {
        rush =  false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rush)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            //GetComponent<CapsuleCollider>().radius = 0.3f;
        }
        if (!rush)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            //GetComponent<CapsuleCollider>().radius = 0152f;
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        rush =false;
    }

}
