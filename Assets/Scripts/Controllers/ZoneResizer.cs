using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneResizer : MonoBehaviour
{
    [SerializeField] float zoneRadius = 10;
    void Start()
    {
        Transform zoneTransform = Searcher.FindChildWithTag(transform, "Zone");

        const float DEFAULT_SCALE = 10; //10 units

        float scale = zoneRadius / DEFAULT_SCALE;

        GraviCenter GC = new GraviCenter(); //if this file attached to GC
        if (TryGetComponent<GraviCenter>(out GC))
        {
            scale = GC.GravityZone / DEFAULT_SCALE;
        }

        zoneTransform.localScale = new Vector3(scale, scale, scale);
    }
}
