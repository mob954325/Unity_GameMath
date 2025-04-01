using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Box : MonoBehaviour
{
    LineRenderer lineRenderer;

    Vector2 dirVec = Vector2.zero;
    float speed = 20f;
    float rotSpeed = 180f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        dirVec = Random.insideUnitCircle;

        // lineRenderer
        lineRenderer.loop = true;
        lineRenderer.positionCount = 4;
    }

    void Update()
    {
        CheckBoarder();
        transform.Translate(dirVec * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

        //lineRenderer.SetPosition(0, );
        //lineRenderer.SetPosition(1, );
        //lineRenderer.SetPosition(2, );
        //lineRenderer.SetPosition(3, );
    }

    void CheckBoarder()
    {
        Vector3 viewportVec = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportVec.x < 0 || viewportVec.x > 1) dirVec.x *= -1;
        if (viewportVec.y < 0 || viewportVec.y > 1) dirVec.y *= -1;
    }
}