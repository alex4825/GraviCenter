using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        hoveredFloor = RaycastTracker.GetRaycastObject("Floor");

        //floor was found
        if (hoveredFloor != null && GameManager.CurrentLevel.Floors.Contains(hoveredFloor.transform))
        {
            if (lastHoveredFloor != null)
            {
                MaterialChanger.ChangeMetallic(ref lastHoveredFloor, originalMetallicValue);
            }
            MaterialChanger.ChangeMetallic(ref hoveredFloor, hoverMetallicValue);
            lastHoveredFloor = hoveredFloor;
        }
        else if (lastHoveredFloor != null)
        {
            MaterialChanger.ChangeMetallic(ref lastHoveredFloor, originalMetallicValue);
            lastHoveredFloor = null;
        }
    }
}
