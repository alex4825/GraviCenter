using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dynamic : MonoBehaviour 
{
    private GameObject parent;
    private List<GameObject> childs;

    private List<GameObject> graviCenters;
    private List<GameObject> powerUps;
    private List<GameObject> blackHoles;
    private List<GameObject> sparks;

    public List<GameObject> GCs { get { return graviCenters; } }
    public List<GameObject> PowerUps { get { return powerUps; } }
    public List<GameObject> BlackHoles { get { return blackHoles; } }
    public List<GameObject> Sparks { get { return sparks; } }
    public Dynamic(GameObject parentObject)
    {
        parent = parentObject;
        childs = new List<GameObject>();

        graviCenters = new List<GameObject>();
        powerUps = new List<GameObject>();
        blackHoles = new List<GameObject>();
        sparks = new List<GameObject>();
    }

    public GameObject Add(GameObject prefab, Vector3 position)
    {
        if(prefab == null)
        {
            Debug.LogError("GameObject " + prefab.name + " is empty!");
            return null;
        }

        GameObject child = Instantiate(prefab, position, Quaternion.identity, parent.transform);

        childs.Add(child);

        switch (child.tag)
        {
            case "Spark": sparks.Add(child); break;
            case "GC": graviCenters.Add(child); break;
            case "PowerUp": powerUps.Add(child); break;
            case "BlackHole": blackHoles.Add(child); break;
        }
        return child;
    }

    public void Remove(GameObject child)
    {
        if (child == null || !childs.Contains(child))
        {
            Debug.LogError(parent.name + " doesn't have a child " + child);
            return;
        }

        string childTag = child.tag;
        Destroyer.DeleteObject(child);

        switch (childTag)
        {
            case "Spark": sparks.RemoveAll(x => x == null); break;
            case "GC": graviCenters.RemoveAll(x => x == null); break;
            case "PowerUp": powerUps.RemoveAll(x => x == null); break;
            case "BlackHole": blackHoles.RemoveAll(x => x == null); break;
        }
        childs.RemoveAll(x => x == null);
    }

    public void ClearAll()
    {
        foreach (GameObject child in childs)
        {
            if (child != null)
            {
                Destroyer.DeleteObject(child);
            }
        }
        childs.Clear();
        graviCenters.Clear();
        powerUps.Clear();
        blackHoles.Clear();
        sparks.Clear();
    }
    public void ClearAllGCs()
    {
        foreach (GameObject gc in graviCenters)
        {
            if (gc != null)
            {
                Destroyer.DeleteObject(gc);
            }
        }
        childs.RemoveAll(x => x == null);
        graviCenters.Clear();
    }
}
