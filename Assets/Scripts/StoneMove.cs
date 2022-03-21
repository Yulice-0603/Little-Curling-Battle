using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 98.2f;
    Vector3 force;
    public GameObject arrow;
    public float vel;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        vel = rb.velocity.magnitude;
        if (this.gameObject.transform.position.x > 2.0f ||this.gameObject.transform.position.x < -2.0f)
        {
            rb.velocity =  Vector3.zero;
            Destroy(this.gameObject,10.0f);
        }
        if (this.gameObject.transform.position.z > 6.0f)
        {
            rb.velocity =  Vector3.zero;
            Destroy(this.gameObject,10.0f);
        }
    }

    public void shot(Vector3 targetPosition)
    {
        
        this.gameObject.GetComponent<Slide>().activate =true;
        Vector3 diff = targetPosition - this.gameObject.transform.position;
        force = diff.normalized;
        float angle;
        angle = Vector3.SignedAngle(force, Vector3.forward,Vector3.up);
        Vector3 rotation = new Vector3(0,0,0);
        rotation.y = 0-angle;
        this.gameObject.transform.localEulerAngles = rotation;
        if (this.gameObject.tag == "Yellow")
        {
            GetComponent<Rush>().rush =true;
            rb.AddForce(force*speed*2, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(force*speed, ForceMode.Impulse);
        }
        
    }

}
