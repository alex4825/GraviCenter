using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Searcher
{
    public static Transform FindChildWithTag(Transform objTransform, string tag)
    {
        foreach (Transform child in objTransform)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
        }
        return null;
    }

    public static List<Transform> FindChildsWithTag(Transform objTransform, string tag)
    {
        List<Transform> transforms = new List<Transform>();

        foreach (Transform child in objTransform)
        {
            if (child.CompareTag(tag))
            {
                transforms.Add(child);
            }
        }
        return transforms;
    }
}
