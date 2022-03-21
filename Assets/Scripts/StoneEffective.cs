using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEffective : MonoBehaviour
{
    public PhysicMaterial stop;
    public PhysicMaterial strong;
    public PhysicMaterial week;
    public PhysicMaterial normal;
    public PhysicMaterial rushstop;
    GameObject cmvcam4;

    void FixedUpdate()
    {
        cmvcam4 = GameObject.Find("CM vcam4");
        
    }

    private void OnCollisionEnter(Collision other) 
    {

        if (this.gameObject.tag == "Red")
        {
            if (other.gameObject.tag == "Blue")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = week;
            }
            if (other.gameObject.tag == "Green")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = strong;
            }
            if (other.gameObject.tag == "Red")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
            if (other.gameObject.tag == "White" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "Ao")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
        }
        if (this.gameObject.tag == "Green")
        {
            if (other.gameObject.tag == "Blue")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = strong;
                
            }
            if (other.gameObject.tag == "Green")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
            if (other.gameObject.tag == "Red")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = week;
            }
            if (other.gameObject.tag == "White" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "Ao")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
        }
        if (this.gameObject.tag == "Blue")
        {
            if (other.gameObject.tag == "Blue")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
            if (other.gameObject.tag == "Green")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = week;
            }
            if (other.gameObject.tag == "Red")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = strong;
            }
            if (other.gameObject.tag == "White" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "Ao")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
        }
        if (this.gameObject.tag == "White")
        {
            if (other.gameObject.tag == "White" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "Ao" || other.gameObject.tag == "Red" || other.gameObject.tag == "Green" || other.gameObject.tag == "Blue")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = stop;
                other.gameObject.GetComponent<CapsuleCollider>().material = normal;
            }
        }
        if (this.gameObject.tag == "Yellow")
        {
            if (other.gameObject.tag == "White" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "Ao" || other.gameObject.tag == "Red" || other.gameObject.tag == "Green" || other.gameObject.tag == "Blue")
            {
                cmvcam4.GetComponent<TimelineController>().nullCollision = false;
                cmvcam4.GetComponent<TimelineController>().Timechange(8.25d);
                cmvcam4.GetComponent<TimelineController>().Play();
                this.gameObject.GetComponent<CapsuleCollider>().material = rushstop;
                other.gameObject.GetComponent<CapsuleCollider>().material = strong;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Red"||other.gameObject.tag == "Green"||other.gameObject.tag == "Blue"||other.gameObject.tag == "White" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "Ao")
        {
            Destroy(this.gameObject.GetComponent<StoneEffective>());
        }
        
    }

}
