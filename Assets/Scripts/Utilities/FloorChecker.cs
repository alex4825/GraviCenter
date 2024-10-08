using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FloorChecker
{
    static public List<Vector3> FindFloors(Level level)
    {
        List<Vector3> positions = new List<Vector3>();

        string floorTag = "Floor";

        Transform[] allChildren = level.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.CompareTag(floorTag))
            {
                positions.Add(child.gameObject.transform.position);
            }
        }
        return positions;
    }

}
