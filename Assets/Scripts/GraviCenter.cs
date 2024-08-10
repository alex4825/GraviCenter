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
    private float transparency = 0.1f;

    [SerializeField] float gravityPower = 20f;
    [SerializeField] float gravityZone = 10f;
    [SerializeField] float distanceFromCamera = 10f;

    public float GravityZone { get { return gravityZone; } set { gravityZone = value; } }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        rbPlayer = player.GetComponent<Rigidbody>();
        GetComponent<MaterialChanger>().SetTransparency(transparency);
        isSearchingPlace = true;
        isPlaceFound = false;
    }

    private void Update()
    { 
        if (isSearchingPlace)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject floor = GetComponent<RaycastManager>().GetRaycastObject("Floor");

                if (floor != null)
                {
                    SetTransformGC(floor.transform);
                    GetComponent<MaterialChanger>().SetTransparency(1);
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

        //Alt + left mouse click => destroy this GC
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetMouseButton(0))
            {
               if (GetComponent<RaycastManager>().GetRaycastObject("GC") == gameObject)
                {
                    Destroy(gameObject);
                }
            }
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
    private void SetTransformGC(Transform floorTransform) => transform.position = floorTransform.position;

}
