using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] ParticleSystem slide;
    ParticleSystem newslide;
    public bool activate;
    Rigidbody rb;
    void Start()
    {
        activate =false;
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (activate && rb.velocity.magnitude > 0)
        {
            newslide = Instantiate(slide);
            newslide.gameObject.tag = "slide";
            newslide.transform.position = new Vector3(this.transform.position.x, 0.25f,this.transform.position.z-0.2f);
            newslide.Play();
        }
        if (rb.velocity.magnitude < 0.0000001f && activate)
        {
            GameObject[] slides = GameObject.FindGameObjectsWithTag("slide");
            foreach (GameObject Snow in slides)
            {
                Destroy(Snow);
            }
            activate = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Red" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Yellow" || other.gameObject.tag == "White" || other.gameObject.tag == "Ao")
        {
            GameObject[] slides = GameObject.FindGameObjectsWithTag("slide");
            foreach (GameObject Snow in slides)
            {
                Destroy(Snow);
            }
            activate = false;
        }
    }
}
