using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(SpriteRenderer))]
public class BoxDrawLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    SpriteRenderer spriteRenderer;

    Vector3[] localCorners = new Vector3[4];
    Vector2 size = Vector2.zero;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
        size = spriteRenderer.sprite.bounds.size;

        // lineRenderer
        lineRenderer.loop = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 4;
    }

    void Update()
    {
        localCorners[0] = new Vector3(-size.x / 2, -size.y / 2, 0);
        localCorners[1] = new Vector3(size.x / 2, -size.y / 2, 0);
        localCorners[2] = new Vector3(size.x / 2, size.y / 2, 0);
        localCorners[3] = new Vector3(-size.x / 2, size.y / 2, 0);

        DrawLines();
    }

    void DrawLines()
    {
        for (int i = 0; i < 4; i++)
        {
            localCorners[i] = transform.TransformPoint(localCorners[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            lineRenderer.SetPosition(i, localCorners[i]);
        }
    }
}
