using UnityEngine;

public class OBBDrawing : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // LineRenderer ������Ʈ �߰� �� ����
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 4; // �簢���� 4�� ������
    }

    private void Update() {
        RenderOBBBoundingBox();
    }

    private void RenderOBBBoundingBox() {
        if (spriteRenderer == null) return;

        // ��������Ʈ�� ���� ��� �ڽ� ũ�� ���
        Vector2 size = spriteRenderer.sprite.bounds.size; // �������� ���� �� �� ������

        // ���� ���������� ������ ���
        Vector3[] corners = new Vector3[4];
        // Code Here

        // ���� ���������� ������ ��� (transform.TransformPoint ����)
        // Code here

        // LineRenderer�� ������ ����
        for (int i = 0; i < corners.Length; i++) {
            lineRenderer.SetPosition(i, corners[i]);
        }
    }
}