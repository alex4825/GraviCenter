using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneResizer : MonoBehaviour
{
    [SerializeField] float zoneRadius = 5;
    void Start()
    {
        Transform zoneTransform = Searcher.FindChildWithTag(transform, "Zone");

        const float DEFAULT_SCALE = 5; //5 units for radius

        float scale = zoneRadius / DEFAULT_SCALE;

        GraviCenter GC = new GraviCenter(); //if this file attached to GC
        if (TryGetComponent<GraviCenter>(out GC))
        {
            scale = GC.GravityZoneRadius / DEFAULT_SCALE;
        }

        zoneTransform.localScale = new Vector3(scale, scale, scale);
    }
}
