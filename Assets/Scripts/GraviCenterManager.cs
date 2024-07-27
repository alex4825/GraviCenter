using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenterManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;
    public float gravityPower = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerBall");
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rbPlayer.AddForce(direction * gravityPower);
    }
}
