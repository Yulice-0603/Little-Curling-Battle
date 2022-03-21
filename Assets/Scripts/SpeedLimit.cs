using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    public PhysicMaterial move;
    Rigidbody rb;
    
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude < 0.00001f)
        {
            //Debug.Log(this.gameObject.name+"zeroになった");
            rb.velocity = Vector3.zero;
            GetComponent<CapsuleCollider>().material = move;
        }
    }
}
