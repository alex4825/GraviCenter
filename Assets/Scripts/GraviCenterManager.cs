using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenterManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;

    public float gravityPower = 25f;
    public float gravityZone = 10f;

    void Start()
    {
        player = GameObject.Find("playerBall");
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (DistanceToGC() < 10)
        {
            MoveBall();
        }
    }

    private void MoveBall()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rbPlayer.AddForce(direction * gravityPower);
    }
    public float DistanceToGC() => (transform.position - player.transform.position).magnitude;

}
