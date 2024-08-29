using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FloorChecker
{
    static public List<Transform> FindFloors()
    {
        List<Transform> transforms = new List<Transform>();

        string floorTag = "Floor";

        Transform[] allChildren = GameManager.CurrentLevel.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.CompareTag(floorTag))
            {
                transforms.Add(child.gameObject.transform);
            }
        }
        return transforms;
    }

}
