using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenter : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;
    private bool isSearchingPlace;
    private bool isPlaceFound;

    [SerializeField] float gravityPower = 20f;
    [SerializeField] float gravityZone = 10f;
    [SerializeField] float distanceFromCamera = 10f;

    public float GravityZone { get { return gravityZone; } set { gravityZone = value; } }

    private void Start()
    {
        player = GameObject.Find("playerBall");
        rbPlayer = player.GetComponent<Rigidbody>();
        isSearchingPlace = true;
        isPlaceFound = false;
    }

    private void Update()
    {
        if (isSearchingPlace)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject surface = GameObject.FindWithTag("GameManager").GetComponent<SurfaceSelector>().GetSurface();

                if (surface != null)
                {
                    PlaceGC(surface.transform);
                }
                else
                {
                    Destroy(gameObject);
                }
                isSearchingPlace = false;
                return;
            }
            MoveToCursorPosition();
        }
        else if (GetDistanceToGC() < GravityZone)
        {
            MoveBall();
        }
    }

    public float GetDistanceToGC() => (transform.position - player.transform.position).magnitude * 2;
    private void MoveToCursorPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = distanceFromCamera;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = worldPosition;
    }
    private void MoveBall()
    {
        //the closer from the ball to GC, the stronger gravitation 
        float powerDivider = 1 - GetDistanceToGC() / gravityZone;
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rbPlayer.AddForce(direction * gravityPower * powerDivider);
    }
    private void PlaceGC(Transform surfaceTransform) => transform.position = surfaceTransform.position;

}
