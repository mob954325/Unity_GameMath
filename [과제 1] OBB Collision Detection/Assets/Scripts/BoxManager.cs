using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private Quadtree quadtree;
    public GameObject boxPrefab;

    //public GameObject[] boxLists;
    List<GameObject> objects;
    public int boxCount = 100;

    [Tooltip("플레이 중에 사용하지 말 것")]
    public bool useQuadTree = false;


    // 박스 생성
    // 박스 충돌 확인
    private void Start()
    {
        objects = new List<GameObject>();

        Vector2 center = transform.TransformPoint(Camera.main.rect.center);
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        center = new Vector2(center.x - screenWidth / 2, center.y - screenHeight / 2);
        Rect worldRect = new Rect(center.x, center.y, screenWidth, screenHeight);

        quadtree = new Quadtree(0, worldRect);
        
        for(int i = 0; i < boxCount; i++)
        {
            GameObject obj = Instantiate(boxPrefab);
            objects.Add(obj);
        }
    }

    private void Update()
    {
        if(!useQuadTree)
        {
            DoubleForLoopCheck();
        }
        else
        {
            QudeTreeCheck();
        }
    }

    void DoubleForLoopCheck()
    {
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        for (int i = 0; i < boxCount; i++)
        {
            for(int j = 0; j < boxCount; j++)
            {
                if (i == j) continue; // 본인 제외

                Bounds boundsA = objects[i].GetComponent<SpriteRenderer>().sprite.bounds;
                Bounds boundsB = objects[j].GetComponent<SpriteRenderer>().sprite.bounds;
                if (CheckOverlapped(boundsA, boundsB, objects[i].transform, objects[j].transform))
                {
                    objects[i].GetComponent<SpriteRenderer>().color = Color.red;
                    objects[j].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    void QudeTreeCheck()
    {
        // quadtree 초기화
        quadtree.Clear();
        foreach (GameObject obj in objects)
        {
            quadtree.Insert(obj);
        }

        // 충돌 오브젝트 찾기
        List<GameObject> list = new List<GameObject>();
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<SpriteRenderer>().color = Color.blue;
            list.Clear();
            list = quadtree.Retrieve(list, obj);

            foreach (GameObject listObj in list)
            {
                if (listObj == obj) continue; // 본인 제외

                Bounds boundsA = obj.GetComponent<SpriteRenderer>().sprite.bounds;
                Bounds boundsB = listObj.GetComponent<SpriteRenderer>().sprite.bounds;
                if (CheckOverlapped(boundsA, boundsB, obj.transform, listObj.transform))
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.red;
                    listObj.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    bool CheckOverlapped(Bounds boundsA, Bounds boundsB, Transform objA, Transform objB)
    {
        Vector2 sizeA = boundsA.size;
        Vector2 sizeB = boundsB.size;

        Vector3 centerA = objA.transform.position; // 현재 도형의 월드 중심 포지션이 필요함
        Vector3 centerB = objB.transform.position;

        // 도형 a의 꼭짓점들 좌표
        Vector3[] localACorners = new Vector3[4];
        localACorners[0] = centerA + new Vector3(-sizeA.x / 2, -sizeA.y / 2, 0);  // bottomLeft
        localACorners[1] = centerA + new Vector3(sizeA.x / 2, -sizeA.y / 2, 0);   // bottomRight
        localACorners[2] = centerA + new Vector3(sizeA.x / 2, sizeA.y / 2, 0);    // topRight
        localACorners[3] = centerA + new Vector3(-sizeA.x / 2, sizeA.y / 2, 0);   // topLeft

        // 도형 b의 꼭짓점들 죄표
        Vector3[] localBCorners = new Vector3[4];
        localBCorners[0] = centerB + new Vector3(-sizeB.x / 2, -sizeB.y / 2, 0);  // bottomLeft
        localBCorners[1] = centerB + new Vector3(sizeB.x / 2, -sizeB.y / 2, 0);   // bottomRight
        localBCorners[2] = centerB + new Vector3(sizeB.x / 2, sizeB.y / 2, 0);    // topRight
        localBCorners[3] = centerB + new Vector3(-sizeB.x / 2, sizeB.y / 2, 0);   // topLeft

        // 1. 두 도형의 수직인 축들을 모두 구한다.
        float a = localACorners[0].x - localACorners[1].x;
        float b = localACorners[0].y - localACorners[1].y;
        Vector2 parellelAxisY = new Vector2(b, -a); // x축의 노말 벡터

        a = localACorners[1].x - localACorners[2].x;
        b = localACorners[1].y - localACorners[2].y;
        Vector2 parellelAxisX = new Vector2(b, -a); // y축의 노말 벡터

        return IsOverlapping(localACorners, localBCorners, parellelAxisX) && 
               IsOverlapping(localACorners, localBCorners, parellelAxisY);
    }

    bool IsOverlapping(Vector3[] localACorners, Vector3[] localBCorners, Vector2 axis)
    {
        float aMin = float.MaxValue;
        float aMax = float.MinValue;
        float bMin = float.MaxValue;
        float bMax = float.MinValue;
        for (int i = 0; i < 4; i++)
        {
            aMin = Mathf.Min(Vector2.Dot(axis, localACorners[i]), aMin);
            aMax = Mathf.Max(Vector2.Dot(axis, localACorners[i]), aMax);
            bMin = Mathf.Min(Vector2.Dot(axis, localBCorners[i]), bMin);
            bMax = Mathf.Max(Vector2.Dot(axis, localBCorners[i]), bMax);
        }

        return !(aMax < bMin || aMin > bMax);
    }
}