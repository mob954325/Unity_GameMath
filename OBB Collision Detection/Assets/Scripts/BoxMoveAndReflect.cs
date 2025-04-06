using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxMoveAndReflect : MonoBehaviour
{
    Vector2 moveDirection = Vector2.zero;

    float speed = 20f;
    float rotSpeed = 180f;

    void Start()
    {
        moveDirection = Random.insideUnitCircle;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

        CheckBoarder();
    }

    void CheckBoarder()
    {
        bool isReflected = false;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0f || viewportPos.x > 1f) // 좌우 경계 확인
        {
            moveDirection.x *= -1.0f;
            isReflected = true;
        }
        if (viewportPos.y < 0f || viewportPos.y > 1f) // 상하 경계 확인
        {
            moveDirection.y *= -1.0f;
            isReflected = true;
        }

        if (isReflected)
        {
            viewportPos.x = Mathf.Clamp(viewportPos.x, 0.01f, 0.99f);
            viewportPos.y = Mathf.Clamp(viewportPos.y, 0.01f, 0.99f);

            transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
        }
    }
}