﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenter : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;
    private bool isSearchingPlace;
    private bool isPlaceFound;
    private float transparency = 0.1f;
    private int energyExplosion;

    [SerializeField] float gravityPower = 20f;
    [SerializeField] float gravityZone = 10f;
    [SerializeField] float distanceFromCamera = 10f;
    [SerializeField] int energyCost = 100;

    public float GravityZone { get { return gravityZone; } set { gravityZone = value; } }

    public delegate void GraviCenterAction(int energyValue);
    public static event GraviCenterAction OnGraviCenterDestroyed;
    public static event GraviCenterAction OnGraviCenterCreated;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        rbPlayer = player.GetComponent<Rigidbody>();
        MaterialChanger.SetTransparency(gameObject, transparency);
        isSearchingPlace = true;
        isPlaceFound = false;
        energyExplosion = energyCost / 2;
    }

    private void Update()
    { 
        if (isSearchingPlace)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject floor = GetComponent<RaycastTracker>().GetRaycastObject("Floor");

                LevelManager currentLevel = FindFirstObjectByType<LevelManager>();

                List<Transform> avalibleTransforms = currentLevel.GetComponent<FloorChecker>().FloorTransforms;

                if (floor != null && currentLevel.EnergyAmount >= energyCost
                    && avalibleTransforms.Contains(floor.transform))
                {
                    SetTransformGC(floor.transform);
                    avalibleTransforms.Remove(floor.transform);
                    OnGraviCenterCreated?.Invoke(-energyCost);
                    MaterialChanger.SetTransparency(gameObject, 1);
                }
                else
                {
                    //cancel GC selecting 
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
               if (GetComponent<RaycastTracker>().GetRaycastObject("GC") == gameObject)
                {
                    OnGraviCenterDestroyed?.Invoke(energyExplosion);
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
