using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : Ball
{
    [SerializeField] bool isDragging;
    [SerializeField] bool isMoving;

    Vector3 dragStartPos;
    Vector3 dragEndPos;

    public LayerMask myBallLayer;
    public LayerMask groundLayer;
    
    public Vector3 velocity;

    protected override void Start()
    {
        base.Start();
        Managers.Game.Init();
        isDragging = false;
    }

    protected override void Update()
    {
        base.Update();
        Managers.Game.Timer -= Time.deltaTime;

        if (isMoving)
            return;

        // 마우스 클릭 시 레이를 발사하여 흰 공 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            RaycastHit hit;

            // 흰 공인지 확인
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myBallLayer))
            {
                dragStartPos = transform.position;
                isDragging = true;
            }
        }

        // 마우스 드래그 중
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 2f);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                dragEndPos = hit.point;
                dragEndPos.y = 1f;
            }
        }

        // 마우스 버튼을 놓았을 때
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            if (rb != null)
            {
                Vector3 dragDirection = dragEndPos - dragStartPos;

                //발사 방향 설정(드래그 반대 방향)
                Vector3 forceDirection = -dragDirection.normalized;

                //발사 힘 설정(드래그 길이 비례)
                float forceMagnitude = Mathf.Clamp(dragDirection.magnitude * 5f, 0.1f, 50);

                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //공이 매우 느리면 바로 멈추기
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

        // 디버깅용 velocity 체크
        if (velocity != rb.velocity)
        {
            velocity = rb.velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string targetLayer = LayerMask.LayerToName(collision.gameObject.layer);

        switch (targetLayer)
        {
            case "RedBall":
                Managers.Game.HitRedBall();
                break;
            case "YellowBall":
                Managers.Game.HitYellowBall();
                break;
        }
    }
}
