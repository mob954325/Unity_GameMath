﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Sin = 0,
    Lerp,
    Spiral,
    Eight,
}

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance = null;
    public static ObjectManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ObjectManager>();
                if(instance == null)
                {
                    GameObject obj = new GameObject("ColliderManager_Created");
                    instance = obj.AddComponent<ObjectManager>();
                }
            }

            return instance;
        }
    }

    private Player player = null;
    private List<GameObject> objects;

    [Tooltip("플레이 중에 사용하지 말 것")]
    public bool useQuadTree = false;
    private Quadtree quadtree;

    public GameObject[] projectile;

    public bool isDebug = false;

    private void Awake()
    {
        objects = new List<GameObject>();

        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Vector2 center = transform.TransformPoint(Camera.main.rect.center);
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        center = new Vector2(center.x - screenWidth / 2, center.y - screenHeight / 2);
        Rect worldRect = new Rect(center.x, center.y, screenWidth, screenHeight);

        quadtree = new Quadtree(0, worldRect);

        if(player == null)
        {
            Debug.LogWarning("CollierManager의 Player가 없습니다.");
        }
        else
        {
            objects.Add(player.gameObject);
        }
    }

    private void Update()
    {
        QudeTreeCheck();
    }

    void QudeTreeCheck()
    {
        // quadtree 초기화
        quadtree.Clear();

        objects.RemoveAll(obj => obj == null);
        foreach (GameObject obj in objects)
        {
            quadtree.Insert(obj);
        }

        // 충돌 오브젝트 찾기

        player.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Bounds playerBound = player.gameObject.GetComponent<SpriteRenderer>().bounds;

        List<GameObject> list = new List<GameObject>();
        foreach (GameObject obj in objects)
        {
            if (obj == player.gameObject) continue;

            obj.GetComponent<SpriteRenderer>().color = Color.blue;
            list.Clear();
            list = quadtree.Retrieve(list, obj);

            Bounds boundsA = obj.GetComponent<SpriteRenderer>().bounds;
            if (OBBCollisionCheck(boundsA, playerBound, obj.transform, player.transform))
            {
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                if(!isDebug)
                {
                    // Player Hit
                    if(player.hitDelay <= 0f)
                    {
                        player.hp--;
                        player.hitDelay = player.maxHitDelay;
                    }
                }
            }
        }
    }

    bool OBBCollisionCheck(Bounds boundsA, Bounds boundsB, Transform objA, Transform objB)
    {
        Vector2 sizeA = objA.localScale;
        Vector2 sizeB = objB.localScale;

        Vector2 rightA = objA.right * sizeA.x / 2f;
        Vector2 upA = objA.up * sizeA.y / 2f;
        Vector2 rightB = objB.right * sizeB.x / 2f;
        Vector2 upB = objB.up * sizeB.y / 2f;

        Vector2 centerA = objA.transform.position; // 현재 도형의 월드 중심 포지션이 필요함
        Vector2 centerB = objB.transform.position;

        // 도형 a의 꼭짓점들 좌표
        Vector2[] localACorners = new Vector2[4];
        localACorners[0] = centerA - rightA - upA;  // bottomLeft
        localACorners[1] = centerA + rightA - upA;  // bottomRight
        localACorners[2] = centerA + rightA + upA;  // topRight
        localACorners[3] = centerA - rightA + upA;  // topLeft

        // 도형 b의 꼭짓점들 죄표
        Vector2[] localBCorners = new Vector2[4];
        localBCorners[0] = centerB - rightB - upB;  // bottomLeft
        localBCorners[1] = centerB + rightB - upB;  // bottomRight
        localBCorners[2] = centerB + rightB + upB;  // topRight
        localBCorners[3] = centerB - rightB + upB;  // topLeft

        // 1. 두 도형의 수직인 축들을 모두 구한다.
        // A
        Vector2[] axes = new Vector2[4];
        axes[0] = GetNormal(GetEdge(localACorners[0], localACorners[1]));
        axes[1] = GetNormal(GetEdge(localACorners[1], localACorners[2]));
        // B
        axes[2] = GetNormal(GetEdge(localBCorners[0], localBCorners[1]));
        axes[3] = GetNormal(GetEdge(localBCorners[1], localBCorners[2]));

        // 최대 최소 값
        float[] pointsA = new float[2];
        float[] pointsB = new float[2];

        for (int i = 0; i < 4; i++)
        {
            // 2. 투영
            pointsA = Projection(localACorners, axes[i]);
            pointsB = Projection(localBCorners, axes[i]);

            // 3. 겹치는지 확인
            if (!CheckOverlap(pointsA, pointsB)) return false;
        }

        return true;
    }

    float[] Projection(Vector2[] localCorners, Vector2 axis)
    {
        float[] result = new float[2];
        float min = Vector2.Dot(axis, localCorners[0]);
        float max = min;

        foreach (Vector2 corner in localCorners)
        {
            float p = Vector2.Dot(corner, axis); // 축에 꼭짓점 투영하기
            if (p < min)
            {
                min = p;
            }
            else if (p > max)
            {
                max = p;
            }
        }

        result[0] = min;
        result[1] = max;
        return result;
    }

    bool CheckOverlap(float[] a, float[] b)
    {
        return a[1] > b[0] && b[1] > a[0]; // a.max > b.min && b.max > a.min
    }

    Vector2 GetEdge(Vector2 from, Vector2 to)
    {
        Vector2 edgeVec = to - from;
        return edgeVec.normalized;
    }

    Vector2 GetNormal(Vector2 edge)
    {
        return new Vector2(-edge.y, edge.x);
    }

    public GameObject SpawnProjectile(ProjectileType type)
    {
        GameObject obj = Instantiate(projectile[(int)type]);
        objects.Add(obj);

        return obj;
    }

    public void AddObjectList(GameObject obj)
    {
        objects.Add(obj);
    }
}
