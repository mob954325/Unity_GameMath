using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSin : MonoBehaviour
{
    Camera mainCam;

    float horizontalSpeed = 5f;         // x축 이동 속도
    float waveSpeed = Mathf.PI * 1f;    // 주기 속도 
    float radius = 2f;                  // 진폭
    float radian = 0f;                  // sin 각도

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if(!IsGameObjectInViewport())
        {
            Destroy(this.gameObject);
        }

        radian += Time.deltaTime;
        float y = radius * Mathf.Sin(radian);
        transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime, y);

        radian += waveSpeed * Time.deltaTime;
    }

    private bool IsGameObjectInViewport()
    {
        Vector2 viewportPosition = mainCam.WorldToViewportPoint(transform.position);

        return viewportPosition.x >= 0f && viewportPosition.x <= 1f && viewportPosition.y >= 0f && viewportPosition.y <= 1f;
    }
}