using UnityEngine;

public class AABBDrawing : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true; // 월드 좌표계
        lineRenderer.positionCount = 4; // 꼭지점 4개로 설정
    }

    private void Update() {
        RenderBoundingBox();
    }

    private void RenderBoundingBox() {
        if (spriteRenderer == null) return;

        Bounds bounds = spriteRenderer.bounds;

        // 꼭지점 계산 (월드 좌표 기준)
        Vector3 bottomLeft = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 bottomRight = new Vector3(bounds.max.x, bounds.min.y, 0);
        Vector3 topRight = new Vector3(bounds.max.x, bounds.max.y, 0);
        Vector3 topLeft = new Vector3(bounds.min.x, bounds.max.y, 0);

        // 꼭지점 설정
        lineRenderer.SetPosition(0, bottomLeft);
        lineRenderer.SetPosition(1, bottomRight);
        lineRenderer.SetPosition(2, topRight);
        lineRenderer.SetPosition(3, topLeft);
    }
}