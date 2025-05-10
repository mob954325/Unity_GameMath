using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawnManager : MonoBehaviour
{
    Player2 player;

    private GameObject[] inner = new GameObject[6];
    private GameObject[] outter = new GameObject[6];

    public GameObject linePrefab;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 6;
        lineRenderer.loop = true;

        player = FindAnyObjectByType<Player2>();

        float radius = 0.8f; // 원하는 반지름 설정
        Vector3 center = transform.position; // 중심 좌표

        for (int i = 0; i < 6; i++)
        {
            float angle = 2 * Mathf.PI / 6 * i; // i번째 꼭짓점 각도
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 point = new Vector3(center.x + x, center.y + y, 0f); // z=
            GameObject obj = new GameObject(); // 여기에 원하는 오브젝트를 Instantiate
            obj.name = $"Inner {i}";
            obj.transform.position = point;

            lineRenderer.SetPosition(i, point);

            inner[i] = obj;
        }

        for (int i = 0; i < 6; i++)
        {
            float angle = 2 * Mathf.PI / 6 * i; // i번째 꼭짓점 각도
            float x = Mathf.Cos(angle) * radius * 5;
            float y = Mathf.Sin(angle) * radius * 5;

            Vector3 point = new Vector3(center.x + x, center.y + y, 0f); // z=
            GameObject obj = new GameObject(); // 여기에 원하는 오브젝트를 Instantiate
            obj.transform.position = point;
            obj.name = $"outter {i}";

            outter[i] = obj;
        }
    }

    float timer = 0f;
    float maxTimer = 1f;

    private void Update()
    {
        if(timer <= 0.0f)
        {
            timer = maxTimer;
            int randIndex = UnityEngine.Random.Range(0, 6);

            LineCollsionCheck line = Instantiate(linePrefab).GetComponent<LineCollsionCheck>();

            line.SpawnLine(outter[randIndex].transform, outter[(randIndex + 1) % 6].transform, 
                inner[randIndex].transform, inner[(randIndex + 1) % 6].transform, 
                player);
        }

        timer -= Time.deltaTime;
    }
}
