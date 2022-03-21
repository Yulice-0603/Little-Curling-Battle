using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    Rigidbody rb;
    public bool stick;
    public Vector3 dis;
    public string stickStoneName;
    int time = 1;
    GameObject cmvcam4;
    void Start()
    {
        stick = false;
        rb = GetComponent<Rigidbody>();
        cmvcam4 = GameObject.Find("CM vcam4");
    }

    // Update is called once per frame
    void Update()
    {
        if (stick)
        {
            this.gameObject.transform.position = GameObject.Find(stickStoneName).transform.position-dis;
        }
        if (rb.velocity.magnitude < 0.03f && stick)
        {
            Invoke("StickOff",2.0f);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Ao" && this.time>0)
        {
            if (other.gameObject.tag == "Red" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "White" || other.gameObject.tag == "Ao")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                other.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
                other.gameObject.GetComponent<Stick>().stickStoneName = this.gameObject.name;
                other.gameObject.GetComponent<Stick>().dis = this.gameObject.transform.position - other.gameObject.transform.position;
                other.gameObject.GetComponent<Stick>().stick = true;
            }
        }
    }

    void StickOff()
    {
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        stick = false;
        time = 0;
    }
}
