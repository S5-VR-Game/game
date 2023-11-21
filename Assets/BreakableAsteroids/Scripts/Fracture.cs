using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    public GameObject fractured;

    public void FractureObject()
    {
        Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        Destroy(gameObject);
    }

    // Gets called at the start of the collision 
    void OnCollisionEnter(Collision collision)
    {
        FractureObject();
    }

    // Gets called during the collision
    void OnCollisionStay(Collision collision)
    {
    }

    // Gets called when the object exits the collision
    void OnCollisionExit(Collision collision)
    {
    }
}