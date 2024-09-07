using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitator : MonoBehaviour
{
    protected GameObject ball;
    protected Rigidbody rbBall;

    [SerializeField] float gravityPower = 20f;
    [SerializeField] float gravityZone = 10f;

    public float GravityPower { get { return gravityPower; } set { gravityPower = value; } }
    public float GravityZone { get { return gravityZone; } set { gravityZone = value; } }
    public bool IsRepeals { get; set; }
    public bool IsGravitate { get; set; }

    protected virtual void Start()
    {
        ball = FindObjectOfType<BallController>().gameObject;
        rbBall = ball.GetComponent<Rigidbody>();

        IsGravitate = false;
    }
    protected virtual void Update()
    {
        if (IsGravitate && GetDistanceToBall() < GravityZone)
        {
            GravitateBall();
        }
    }

    protected void GravitateBall()
    {
        //the closer from the ball to GC, the stronger gravitation 
        float powerDivider = 1 - GetDistanceToBall() / gravityZone;
        Vector3 direction = (transform.position - ball.transform.position).normalized;
        rbBall.AddForce(direction * GravityPower * powerDivider);
    }
    protected float GetDistanceToBall() => (transform.position - ball.transform.position).magnitude * 2;
}
