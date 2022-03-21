using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject,10f);
    }
}
