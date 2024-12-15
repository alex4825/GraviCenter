using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicList : MonoBehaviour
{
    private List<GameObject> items;

    public DynamicList()
    {
        items = new List<GameObject>();
    }

    public List<GameObject> Items { get { return items; } }

    public void Add(GameObject obj)
    {
        items.Add(obj);
    }

    public void ClearAll()
    {
        foreach (var obj in items)
        {
            if (obj != null)
            {
                Object.Destroy(obj); // ??????????? ??????? ? ?????
            }
        }
        items.Clear();
    }
}
