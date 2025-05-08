using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEight : MonoBehaviour
{
    public float size = 2f;     // ����
    public float period = 4f;   // 1ȸ�� (2pi)�ϴ� �� �ɸ��� �ð� 

    private float radian = 0f;  // ���� ���� ��
    private float speed;        // ���� / ��
    private Vector3 center;

    private void OnEnable()
    {
        Destroy(this.gameObject, 5f);
    }

    private void Start()
    {
        center = transform.position;

        size = UnityEngine.Random.Range(2f, 5f);
        period = UnityEngine.Random.Range(3f, 5f);

        // �ӵ� = 1ȸ�� / �ֱ�
        speed = Mathf.PI * 2f / period;
    }

    private void Update()
    {
        // �� �����Ӹ��� ���� �� ����
        radian += speed * Time.deltaTime;

        // 8�� �: x = sin(t), y = sin(t) * cos(t)
        float x = size * Mathf.Sin(radian);
        float y = size * Mathf.Sin(radian) * Mathf.Cos(radian);

        // �̵� ���� ȸ�� 

        // ���� ���� = ���Լ� ��
        float dx = Mathf.Cos(radian);
        float dy = Mathf.Cos(radian) * Mathf.Cos(radian) - Mathf.Sin(radian) * Mathf.Sin(radian);

        // ���� ���� ����ȭ
        Vector3 dir = new Vector3(dx, dy, 0).normalized;

        // �Ѿ� ȸ��
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // �Ѿ� ��ġ
        transform.position = center + new Vector3(x, y, 0);
    }
}
