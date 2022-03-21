using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisionfx : MonoBehaviour
{
    [SerializeField] ParticleSystem collision;
    ParticleSystem newcollision;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (GetComponent<Stick>().stick == false)
        {
            if (other.gameObject.tag == "Red" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "White" || other.gameObject.tag == "Ao")
            {
                Vector3 hitPos;
                foreach (ContactPoint point in other.contacts)
                {
                    hitPos = point.point;
                    newcollision = Instantiate(collision);
                    newcollision.gameObject.tag = "collision";
                    newcollision.transform.position = hitPos;
                    newcollision.Play();
                }   
            }
        }
        
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Red" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "White" || other.gameObject.tag == "Ao")
        {
            GameObject[] collisions = GameObject.FindGameObjectsWithTag("collision");
            if (this.gameObject.tag == "Yellow")
            {
                foreach (GameObject hit2 in collisions)
                {
                    Destroy(hit2,1f);
                }
            }
            else
            {
                foreach (GameObject hit in collisions)
                {
                    Destroy(hit,1f);
                }
            }  
        }
    }
}
