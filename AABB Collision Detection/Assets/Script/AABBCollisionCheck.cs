using UnityEngine;

public class AABBCollisionCheck : MonoBehaviour {
    public GameObject object1;
    public GameObject object2;

    private SpriteRenderer sr1;
    private SpriteRenderer sr2;

    // ������Ʈ�� ã�� SpriteRenderer ������Ʈ ĳ��
    private void Start() {
        if (object1 == null) object1 = GameObject.Find("GreenBox");
        if (object2 == null) object2 = GameObject.Find("RedBox");

        sr1 = object1.GetComponent<SpriteRenderer>();
        sr2 = object2.GetComponent<SpriteRenderer>();
    }

    // �� �����Ӹ��� �� ������Ʈ�� �浹�� üũ�ϰ� ���� ����
    private void Update() {
        bool isCollided = CheckBoundingBoxOverlap(sr1.bounds, sr2.bounds);

        if (isCollided) {
            Debug.Log("Overlapped");
            SetAlpha(0.2f);  // �浹 �� ���� 0.2
        } else {
            SetAlpha(1.0f);  // ���浹 �� ���� ����
        }
    }

    // �� �ٿ�� �ڽ��� ��ġ���� AABB ������� �Ǻ�
    private bool CheckBoundingBoxOverlap(Bounds boundsA, Bounds boundsB) {

        // ���⿡ AABB �˰��� �ۼ�
        if (boundsA.min.x < boundsB.max.x &&
           boundsA.max.x > boundsB.min.x &&
           boundsA.min.y < boundsB.max.y &&
           boundsA.max.y > boundsB.min.y) return true;

        return false;
    }

    // �� SpriteRenderer�� ���� ���� ���� (�̹� ���� ���̸� ����)
    private void SetAlpha(float alpha) {
        Color color1 = sr1.color;
        if (!Mathf.Approximately(color1.a, alpha)) {
            color1.a = alpha;
            sr1.color = color1;
        }

        Color color2 = sr2.color;
        if (!Mathf.Approximately(color2.a, alpha)) {
            color2.a = alpha;
            sr2.color = color2;
        }
    }
}