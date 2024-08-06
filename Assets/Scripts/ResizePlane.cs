using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePlane : MonoBehaviour
{
    void Start()
    {
        
        float zoneSize = GetComponentInParent<GraviCenter>().GravityZone;

        const float DEFAULT_SCALE = 10; //10 units

        float scale = zoneSize / DEFAULT_SCALE;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}
