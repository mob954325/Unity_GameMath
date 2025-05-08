using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpiral : MonoBehaviour
{
    float speed = Mathf.PI * 2f;
    float radius = 1f;
    float waveSpeed = 1f; 
    float radian = 0f;
    Vector3 centerVec = Vector3.zero;

    private void Start()
    {
        centerVec = transform.position;
        waveSpeed = UnityEngine.Random.Range(1f, 3f);
        speed = Mathf.PI * Random.Range(1f, 2f);

        Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        radian += speed * Time.deltaTime;
        float x = radius * Mathf.Cos(radian);
        float y = radius * Mathf.Sin(radian);

        transform.position = centerVec + new Vector3(x, y, 0);

        radius += waveSpeed * Time.deltaTime;
    }
}