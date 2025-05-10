using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollsionCheck : MonoBehaviour
{
    private Camera mainCam;
    public Transform lineStartPoint; // 선분의 시작점
    public Transform lineEndPoint;   // 선분의 끝점
    private LineRenderer lineRenderer;

    private Player2 player;
    private Vector3 lineStartPointStart;
    private Vector3 lineEndPointStart;
    private Vector3 lineStartPointDestination;
    private Vector3 lineEndPointDestination;

    private float interpolate = 0f;
    private float duration = 1f;

    private void Start()
    {
        mainCam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        lineStartPoint = transform.GetChild(0);
        lineEndPoint = transform.GetChild(1);

        duration = UnityEngine.Random.Range(0.5f, 1.0f);
    }

    private void Update()
    {
        if (player == null) return;

        UpdateMove();

        // 점의 현재 위치를 가져옴
        lineRenderer.SetPosition(0, lineStartPoint.position);
        lineRenderer.SetPosition(1, lineEndPoint.position);

        // 점과 선분 사이 수직 거리 계산
        float perpendicularDistance = PerpendicularDistance(lineStartPoint.position, lineEndPoint.position, player.transform.position);

        // 선분 범위 내에서 수직 거리 기준 이하일 때 충돌 처리
        if (perpendicularDistance >= 0 && perpendicularDistance < 0.01f)
        {
            player.hp--;
            Debug.Log("hit");
        }
    }

    // 점과 선분 사이의 수직 거리 반환 (선분 범위 밖이면 -1 반환)
    private float PerpendicularDistance(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 lineDirection = lineEnd - lineStart;
        float lineDirectionSqrMagnitude = lineDirection.sqrMagnitude;

        // 선분이 하나의 점일 경우
        if (lineDirectionSqrMagnitude < Mathf.Epsilon)
        {
            return Vector3.Distance(point, lineStart);
        }

        // 수선의 발을 t로 표현
        float t = Vector3.Dot(point - lineStart, lineDirection) / lineDirectionSqrMagnitude;

        // 선분 범위를 벗어나면 -1 반환
        if (t < 0 || t > 1)
        {
            return -1f;
        }

        // 수선의 발 위치 계산
        Vector3 perpendicularPoint = lineStart + t * lineDirection;

        // 점과 수선의 발 사이의 거리(수직 거리) 반환
        return Vector3.Distance(point, perpendicularPoint);
    }

    private void UpdateMove()
    {
        interpolate += Time.deltaTime;

        float t = interpolate / duration;
        if (t >= 1f || t <= 0f)
        {
            interpolate = 0f;
            Destroy(this.gameObject);
        }
        Vector3 startPosition = Vector3.Lerp(lineStartPointStart, lineStartPointDestination, t);
        Vector3 endPosition = Vector3.Lerp(lineEndPointStart, lineEndPointDestination, t);

        lineStartPoint.position = startPosition;
        lineEndPoint.position = endPosition;
    }

    public void SpawnLine(Transform point1Start, Transform point2End, Transform point1Destination, Transform point2Destination, Player2 player)
    {
        lineStartPoint = transform.GetChild(0);
        lineEndPoint = transform.GetChild(1);

        lineStartPoint.position = point1Start.position;
        lineEndPoint.position = point2End.position;

        lineStartPointStart = lineStartPoint.position;
        lineEndPointStart = lineEndPoint.position;

        lineStartPointDestination = point1Destination.position;
        lineEndPointDestination = point2Destination.position;

        this.player = player;
    }
}
