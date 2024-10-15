using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isMoving;
    protected Rigidbody rb;
    public bool isHit;

    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Managers.Game.balls.Add(this.gameObject);
    }

    protected virtual void Update()
    {
        if (Managers.Game.isFinish)
            return;

        if (rb.velocity.magnitude < 0.05f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isMoving = false;
            Managers.Game.EndTurn();
        }
        else
        {
            isMoving = true;
        }
    }

    public void SetRandomVelocity()
    {
        rb.velocity = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        string targetLayer = LayerMask.LayerToName(collision.gameObject.layer);

        switch (targetLayer)
        {
            case "Ball":
                Managers.Sound.Play("Sounds/BallHitSound");
                break;
            default:
                return;
        }
    }
}
