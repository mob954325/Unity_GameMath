using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public GameObject boxPrefab;
    public int boxCount = 100;
    // 박스 생성
    // 박스 충돌 확인

    private void Start()
    {
        for(int i = 0; i < boxCount; i++)
        {
            Instantiate(boxPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}