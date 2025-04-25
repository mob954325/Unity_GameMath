using UnityEngine;

public class Bullet_Eight : MonoBehaviour
{
    public float size = 2f;     // 진폭
    public float period = 4f;   // 1회전 (2pi)하는 데 걸리는 시간 

    private float radian = 0f;  // 현재 라디안 값
    private float speed;        // 라디안 / 초
    private Vector3 center;

    private void OnEnable()
    {
        Destroy(this.gameObject, 5f);
    }

    private void Start()
    {
        center = transform.position;

        // 속도 = 1회전 / 주기
        speed = Mathf.PI * 2f / period;
    }

    private void Update()
    {
        // 매 프레임마다 라디안 값 갱신
        radian += speed * Time.deltaTime;

        // 8자 곡선: x = sin(t), y = sin(t) * cos(t)
        float x = size * Mathf.Sin(radian);
        float y = size * Mathf.Sin(radian) * Mathf.Cos(radian);

        // 이동 방향 회전 

        // 접선 벡터 = 도함수 값
        float dx = Mathf.Cos(radian);
        float dy = Mathf.Cos(radian) * Mathf.Cos(radian) - Mathf.Sin(radian) * Mathf.Sin(radian);

        // 방향 벡터 정규화
        Vector3 dir = new Vector3(dx, dy, 0).normalized;

        // 총알 회전
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 총알 위치
        transform.position = center + new Vector3(x, y, 0);
    }
}
