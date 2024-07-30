using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenterManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;

    public float gravityPower = 20f;
    public float gravityZone = 10f;

    void Start()
    {
        player = GameObject.Find("playerBall");
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (DistanceToGC() < gravityZone)
        {
            MoveBall();
        }
    }

    private void MoveBall()
    {
        //the closer from the ball to GC, the stronger gravitation 
        float powerDivider = 1 - DistanceToGC() / gravityZone;
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rbPlayer.AddForce(direction * gravityPower * powerDivider);
    }
    public float DistanceToGC() => (transform.position - player.transform.position).magnitude * 2;

}
