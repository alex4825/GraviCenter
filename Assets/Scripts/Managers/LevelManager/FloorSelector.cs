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
        hoveredFloor = GetComponent<RaycastTracker>().GetRaycastObject("Floor");

        //floor was found
        if (hoveredFloor != null && GetComponent<FloorChecker>().FloorTransforms.Contains(hoveredFloor.transform))
        {
            if (lastHoveredFloor != null)
            {
                GetComponent<FloorChecker>().Draw(ref lastHoveredFloor, originalMetallicValue);
            }
            GetComponent<FloorChecker>().Draw(ref hoveredFloor, hoverMetallicValue);
            lastHoveredFloor = hoveredFloor;
        }
        else if (lastHoveredFloor != null)
        {
            GetComponent<FloorChecker>().Draw(ref lastHoveredFloor, originalMetallicValue);
            lastHoveredFloor = null;
        }
    }
}
