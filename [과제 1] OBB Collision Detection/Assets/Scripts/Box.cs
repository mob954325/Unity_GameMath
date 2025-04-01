using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Box : MonoBehaviour
{
    LineRenderer lineRenderer;
    SpriteRenderer spriteRenderer;

    Vector2 dirVec = Vector2.zero;
    public float speed = 20f;
    public float rotSpeed = 30f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dirVec = Random.insideUnitCircle;

        // lineRenderer
        lineRenderer.loop = true;
        lineRenderer.positionCount = 4;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        CheckBoarder();

        transform.Translate(dirVec * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

        Bounds bounds = spriteRenderer.bounds;

        Vector3 bottomLeft = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 bottomRight = new Vector3(bounds.max.x, bounds.min.y, 0);
        Vector3 topRight = new Vector3(bounds.max.x, bounds.max.y, 0);
        Vector3 topLeft = new Vector3(bounds.min.x, bounds.max.y, 0);

        lineRenderer.SetPosition(0, transform.rotation * bottomLeft);
        lineRenderer.SetPosition(1, transform.rotation * bottomRight);
        lineRenderer.SetPosition(2, transform.rotation * topRight);
        lineRenderer.SetPosition(3, transform.rotation * topLeft);
    }

    void CheckBoarder()
    {
        Vector3 viewportVec = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportVec.x < 0 || viewportVec.x > 1) dirVec.x *= -1;
        if (viewportVec.y < 0 || viewportVec.y > 1) dirVec.y *= -1;
    }
}