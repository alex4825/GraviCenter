using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorSelector : MonoBehaviour
{
    private GameObject hoveredFloor;
    private GameObject lastHoveredFloor;
    private float originalMetallicValue;
    private float hoverMetallicValue;

    public GameObject HoveredFloor { get { return hoveredFloor; } set { hoveredFloor = value; } }

    private void Start()
    {
        Material floorMaterial = GameObject.FindWithTag("Floor").GetComponent<Renderer>().material;

        HoveredFloor = null;
        lastHoveredFloor = null;
        hoverMetallicValue = 0f;
        originalMetallicValue = floorMaterial.GetFloat("_Metallic");
    }

    private void Update()
    {
        hoveredFloor = GetComponent<RaycastManager>().GetRaycastObject("Floor");

        //floor was found
        if (hoveredFloor != null)
        {
            if (lastHoveredFloor != null)
            {
                DrawFloor(ref lastHoveredFloor, originalMetallicValue);
            }
            DrawFloor(ref hoveredFloor, hoverMetallicValue);
            lastHoveredFloor = hoveredFloor;
        }
        else if (lastHoveredFloor != null)
        {
            DrawFloor(ref lastHoveredFloor, originalMetallicValue);
            lastHoveredFloor = null;
        }
    }
    private void DrawFloor(ref GameObject floor, float newMetallicValue)
    {
        Renderer renderer = floor.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.SetFloat("_Metallic", newMetallicValue);
        }
    }
}
