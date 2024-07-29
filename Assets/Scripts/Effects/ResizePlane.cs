using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float zoneSize = GameObject.Find("GC_little").GetComponent<GraviCenterManager>().gravityZone;

        const float DEFAULT_SCALE = 10; //10 units

        float scale = zoneSize / DEFAULT_SCALE;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}
