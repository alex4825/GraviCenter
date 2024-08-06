using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceSelector : MonoBehaviour
{
    private GameObject hoveredSurface;
    private GameObject lastHoveredSurface;
    private float originalMetallicValue;
    private float hoverMetallicValue;
    private float rayDistance;

    public GameObject HoveredSurface { get { return hoveredSurface; } set { hoveredSurface = value; } }

    private void Start()
    {
        Material surfaceMaterial = GameObject.FindWithTag("Surface").GetComponent<Renderer>().material;

        HoveredSurface = null;
        lastHoveredSurface = null;
        hoverMetallicValue = 0f;
        originalMetallicValue = surfaceMaterial.GetFloat("_Metallic");
        rayDistance = 100f;
    }

    private void Update()
    {
        hoveredSurface = GetSurface();

        //surface was found
        if (hoveredSurface != null)
        {
            if (lastHoveredSurface != null)
            {
                DrawSurface(ref lastHoveredSurface, originalMetallicValue);
            }
            DrawSurface(ref hoveredSurface, hoverMetallicValue);
            lastHoveredSurface = hoveredSurface;
        }
        else if (lastHoveredSurface != null)
        {
            DrawSurface(ref lastHoveredSurface, originalMetallicValue);
            lastHoveredSurface = null;
        }
    }
    public GameObject GetSurface()
    {
        // Raycast from camera position to cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            // Choose surface object which the ray crossed with        
            if (hit.collider.gameObject.tag == "Surface")
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    private void DrawSurface(ref GameObject surface, float newMetallicValue)
    {
        Renderer renderer = surface.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.SetFloat("_Metallic", newMetallicValue);
        }
    }
}
