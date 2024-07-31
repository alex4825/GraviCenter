using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        Transform startPoint = GameObject.Find("Start Point").transform;
        transform.position = startPoint.position;
    }

    void Update()
    {
        
    }
}
