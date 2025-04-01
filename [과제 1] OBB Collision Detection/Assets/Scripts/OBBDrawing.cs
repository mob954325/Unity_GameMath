using UnityEngine;

public class OBBDrawing : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // LineRenderer 컴포넌트 추가 및 설정
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 4; // 사각형의 4개 꼭지점
    }

    private void Update() {
        RenderOBBBoundingBox();
    }

    private void RenderOBBBoundingBox() {
        if (spriteRenderer == null) return;

        // 스프라이트의 로컬 경계 박스 크기 계산
        Vector2 size = spriteRenderer.sprite.bounds.size; // 스케일이 적용 안 된 사이즈

        // 로컬 공간에서의 꼭지점 계산
        Vector3[] corners = new Vector3[4];
        // Code Here

        // 월드 공간에서의 꼭지점 계산 (transform.TransformPoint 적용)
        // Code here

        // LineRenderer에 꼭지점 설정
        for (int i = 0; i < corners.Length; i++) {
            lineRenderer.SetPosition(i, corners[i]);
        }
    }
}