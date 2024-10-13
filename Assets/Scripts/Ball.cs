using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Managers.Game.balls.Add(this.gameObject);
    }

    protected virtual void Update()
    {
        if (Managers.Game.isFinish)
            return;
    }

    protected virtual void FixedUpdate()
    {

    }
}
