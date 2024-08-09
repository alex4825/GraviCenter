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
    private float rayDistance;

    public GameObject HoveredFloor { get { return hoveredFloor; } set { hoveredFloor = value; } }

    private void Start()
    {
        Material floorMaterial = GameObject.FindWithTag("Floor").GetComponent<Renderer>().material;

        HoveredFloor = null;
        lastHoveredFloor = null;
        hoverMetallicValue = 0f;
        originalMetallicValue = floorMaterial.GetFloat("_Metallic");
        rayDistance = 100f;
    }

    private void Update()
    {
        hoveredFloor = GetFloor();

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
    public GameObject GetFloor()
    {
        // Raycast from camera position to cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            // Choose floor object which the ray crossed with        
            if (hit.collider.gameObject.tag == "Floor")
            {
                return hit.collider.gameObject;
            }
        }
        return null;
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
