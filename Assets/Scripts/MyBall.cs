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

    [SerializeField] GameObject direction;

    protected override void Start()
    {
        base.Start();
        isDragging = false;
        direction.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if(Managers.Game.isStart)
        {
            Managers.Game.Timer -= Time.deltaTime;
        }

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
                if (hit.collider.gameObject.tag != "MainBall")
                    return;

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

                // 화살표 방향 설정
                Vector3 dragDirection = dragEndPos - dragStartPos;
                direction.SetActive(true); // 드래그 시작할 때 화살표 보이기
                direction.transform.position = transform.position; // 화살표를 공 위치에
                direction.transform.rotation = Quaternion.LookRotation(-dragDirection); // 드래그 반대 방향으로 회전

                // 화살표 길이(크기)를 드래그 길이에 비례하게 설정
                float arrowScale = Mathf.Clamp(dragDirection.magnitude, 0.1f, 10f);
                direction.transform.localScale = new Vector3(1, 1, arrowScale); // Z축 방향으로만 크기 변경
            }
        }

        // 마우스 버튼을 놓았을 때
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            direction.SetActive(false); // 드래그 끝나면 화살표 숨기기

            if (rb != null)
            {
                Vector3 dragDirection = dragEndPos - dragStartPos;

                // 발사 방향 설정(드래그 반대 방향)
                Vector3 forceDirection = -dragDirection.normalized;

                // 발사 힘 설정(드래그 길이 비례)
                float forceMagnitude = Mathf.Clamp(dragDirection.magnitude * 5f, 0.1f, 50);

                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // 공이 매우 느리면 바로 멈추기
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

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (!Managers.Game.isStart)
            return;

        string targetTag = collision.gameObject.tag;

        switch (targetTag)
        {
            case "RedBall":
                Managers.Game.HitRedBall();
                break;
            case "YellowBall":
                Managers.Game.HitYellowBall();
                break;
            default:
                return;
        }
    }
}
