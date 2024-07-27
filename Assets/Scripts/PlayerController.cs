using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform startPoint = GameObject.Find("StartPoint").transform;
        transform.position = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
