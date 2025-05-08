using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{
    private int MAX_OBJECTS = 10;
    private int MAX_LEVELS = 5;

    private int level;
    private Rect bounds;
    private List<GameObject> objects;
    private Quadtree[] nodes;

    public Quadtree(int level, Rect bounds)
    {
        this.level = level;
        this.bounds = bounds;
        this.objects = new List<GameObject>();
        nodes = new Quadtree[4];
    }
    public void Clear()
    {
        objects.Clear();
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] != null)
            {
                nodes[i].Clear();
                nodes[i] = null;
            }
        }
    }

    private void Split()
    {
        float subWidth = bounds.width / 2f;
        float subHeight = bounds.height / 2f;
        float x = bounds.x;
        float y = bounds.y;

        nodes[0] = new Quadtree(level + 1, new Rect(x + subWidth, y, subWidth, subHeight));
        nodes[1] = new Quadtree(level + 1, new Rect(x, y, subWidth, subHeight));
        nodes[2] = new Quadtree(level + 1, new Rect(x, y + subHeight, subWidth, subHeight));
        nodes[3] = new Quadtree(level + 1, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));
    }

    private int GetIndex(GameObject obj)
    {
        int index = -1;
        Vector3 pos = obj.transform.position;

        bool topQuadrant = (pos.y > bounds.y + bounds.height / 2);
        bool bottomQuadrant = (pos.y < bounds.y + bounds.height / 2 && pos.y > bounds.y);

        if (pos.x > bounds.x + bounds.width / 2)
        {
            if (topQuadrant) index = 3;
            else if (bottomQuadrant) index = 0;
        }
        else if (pos.x < bounds.x + bounds.width / 2 && pos.x > bounds.x)
        {
            if (topQuadrant) index = 2;
            else if (bottomQuadrant) index = 1;
        }

        return index;
    }

    public void Insert(GameObject obj)
    {
        if (nodes[0] != null)
        {
            int index = GetIndex(obj);

            if (index != -1)
            {
                nodes[index].Insert(obj);
                return;
            }
        }

        objects.Add(obj);

        if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
        {
            if (nodes[0] == null) Split();

            int i = 0;

            while (i < objects.Count)
            {
                int index = GetIndex(objects[i]);
                if (index != -1)
                {
                    nodes[index].Insert(objects[i]);
                    objects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public List<GameObject> Retrieve(List<GameObject> returnObjects, GameObject obj)
    {
        int index = GetIndex(obj);
        if (index != -1 && nodes[0] != null)
        {
            nodes[index].Retrieve(returnObjects, obj);
        }

        returnObjects.AddRange(objects);

        return returnObjects;
    }
}
