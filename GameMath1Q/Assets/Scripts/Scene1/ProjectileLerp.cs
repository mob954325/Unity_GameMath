using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLerp : MonoBehaviour
{
    Camera mainCam;

    private Vector3 startVec = Vector3.zero;
    private Vector3 targetVec = Vector3.zero;
    private float duration = 0.5f;
    private float interpolate = 0f;    

    private void Start()
    {
        mainCam = Camera.main;
        startVec = transform.position;
        targetVec = FindObjectOfType<Player>().gameObject.transform.position;
    }

    private void Update()
    {
        interpolate += Time.deltaTime;

        float t = interpolate / duration;
        if (t >= 1f || t <= 0f)
        {
            interpolate = 0f;
            Destroy(this.gameObject);
        }
        Vector3 position = Vector3.Lerp(startVec, targetVec, t);
        transform.position = position;
    }
}
