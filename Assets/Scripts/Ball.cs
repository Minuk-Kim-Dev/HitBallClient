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
            case "Wall":
                Vector3 incomingVelocity = rb.velocity;

                // 충돌 지점에서의 법선 벡터
                Vector3 normal = collision.contacts[0].normal;

                // 반사 벡터 계산: 반사 = 입사 - 2 * (입사 · 법선) * 법선
                Vector3 reflection = Vector3.Reflect(incomingVelocity, normal);

                // 반사 벡터에 0.9배의 감쇠 반발력 적용
                rb.velocity = reflection * 0.9f;
                break;
            default:
                return;
        }
    }
}
