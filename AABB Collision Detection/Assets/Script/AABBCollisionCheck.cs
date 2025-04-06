using UnityEngine;

public class AABBCollisionCheck : MonoBehaviour {
    public GameObject object1;
    public GameObject object2;

    private SpriteRenderer sr1;
    private SpriteRenderer sr2;

    // 오브젝트를 찾고 SpriteRenderer 컴포넌트 캐싱
    private void Start() {
        if (object1 == null) object1 = GameObject.Find("GreenBox");
        if (object2 == null) object2 = GameObject.Find("RedBox");

        sr1 = object1.GetComponent<SpriteRenderer>();
        sr2 = object2.GetComponent<SpriteRenderer>();
    }

    // 매 프레임마다 두 오브젝트의 충돌을 체크하고 투명도 설정
    private void Update() {
        bool isCollided = CheckBoundingBoxOverlap(sr1.bounds, sr2.bounds);

        if (isCollided) {
            Debug.Log("Overlapped");
            SetAlpha(0.2f);  // 충돌 시 투명도 0.2
        } else {
            SetAlpha(1.0f);  // 비충돌 시 투명도 복구
        }
    }

    // 두 바운딩 박스가 겹치는지 AABB 방식으로 판별
    private bool CheckBoundingBoxOverlap(Bounds boundsA, Bounds boundsB) {

        // 여기에 AABB 알고리즘 작성
        if (boundsA.min.x < boundsB.max.x &&
           boundsA.max.x > boundsB.min.x &&
           boundsA.min.y < boundsB.max.y &&
           boundsA.max.y > boundsB.min.y) return true;

        return false;
    }

    // 두 SpriteRenderer의 알파 값을 변경 (이미 같은 값이면 생략)
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