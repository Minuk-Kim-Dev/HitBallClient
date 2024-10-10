using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : Ball
{
    public Vector3 myVelocity;
    bool isDragging;
    public LayerMask myBallLayer;
    public LayerMask groundLayer;
    public Vector3 dragStartPos;
    public Vector3 dragEndPos;


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
        myVelocity = rb.velocity;

        // 마우스 클릭 시 레이를 발사하여 흰 공 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //흰 공인지 확인
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myBallLayer))
            {
                dragStartPos = hit.point;
                dragStartPos.y = 1f;

                isDragging = true;
            }
        }

        // 마우스 드래그 중
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                // dragStartPos로부터 dragEndPos까지의 벡터 계산
                Vector3 dragDirection = dragEndPos - dragStartPos;

                // 반대 방향으로 힘을 가하기 위해 벡터를 반전
                Vector3 forceDirection = -dragDirection.normalized;

                // 거리 비례하여 힘 크기 설정 (거리 * 힘 계수)
                float forceMagnitude = Mathf.Clamp(dragDirection.magnitude * 10f, 0.1f, 50);

                // myBall에 힘을 가함
                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
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
