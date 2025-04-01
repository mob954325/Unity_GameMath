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
        lineRenderer.useWorldSpace = true; // ���� ��ǥ��
        lineRenderer.positionCount = 4; // ������ 4���� ����
    }

    private void Update() {
        RenderBoundingBox();
    }

    private void RenderBoundingBox() {
        if (spriteRenderer == null) return;

        Bounds bounds = spriteRenderer.bounds;

        // ������ ��� (���� ��ǥ ����)
        Vector3 bottomLeft = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 bottomRight = new Vector3(bounds.max.x, bounds.min.y, 0);
        Vector3 topRight = new Vector3(bounds.max.x, bounds.max.y, 0);
        Vector3 topLeft = new Vector3(bounds.min.x, bounds.max.y, 0);

        // ������ ����
        lineRenderer.SetPosition(0, bottomLeft);
        lineRenderer.SetPosition(1, bottomRight);
        lineRenderer.SetPosition(2, topRight);
        lineRenderer.SetPosition(3, topLeft);
    }
}