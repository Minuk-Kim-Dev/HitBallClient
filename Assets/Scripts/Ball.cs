using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {

    }
}
