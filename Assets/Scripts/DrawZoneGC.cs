using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawZoneGC : MonoBehaviour
{
    private float radius;

    private void Awake()
    {
        radius = gameObject.GetComponent<GraviCenterManager>().DistanceToGC();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
