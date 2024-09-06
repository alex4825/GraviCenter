using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneResizer : MonoBehaviour
{
    void Start()
    {
        Transform zoneTransform = Searcher.FindChildWithTag(transform, "ZoneGC");

        float zoneSize = GetComponent<GraviCenter>().GravityZone;

        const float DEFAULT_SCALE = 10; //10 units

        float scale = zoneSize / DEFAULT_SCALE;

        zoneTransform.localScale = new Vector3(scale, scale, scale);
    }
}
