using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public GameObject boxPrefab;

    public GameObject[] boxLists;
    public int boxCount = 100;

    // 박스 생성
    // 박스 충돌 확인

    private void Start()
    {
        boxLists = new GameObject[boxCount];

        for (int i = 0; i < boxCount; i++)
        {
            boxLists[i] = Instantiate(boxPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {
        foreach(GameObject obj in boxLists)
        {
            obj.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        for (int i = 0; i < boxCount; i++)
        {
            for(int j = 0; j < boxCount; j++)
            {
                if (i == j) continue; // 본인 제외

                //sat code
                Bounds boundsA = boxLists[i].GetComponent<SpriteRenderer>().bounds;
                Bounds boundsB = boxLists[j].GetComponent<SpriteRenderer>().bounds;
                if(CheckOverlapped(boundsA, boundsB))
                {
                    boxLists[i].GetComponent<SpriteRenderer>().color = Color.red;
                    boxLists[j].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    bool CheckOverlapped(Bounds boundsA, Bounds boundsB)
    {
        if (boundsA.max.x > boundsB.min.x && boundsA.min.x < boundsB.max.x &&
            boundsA.min.y < boundsB.max.y && boundsA.max.y > boundsB.min.y)
        {
            return true;
        }

        return false;
    }
}