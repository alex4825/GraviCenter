using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public delegate void SurfaceSelectedAction(Transform surfaceObject);
    public static event SurfaceSelectedAction OnSurfaceSelected;

    public GameObject hoveredSurface;

    private GameObject lastHoveredSurface;
    private float originalMetallicValue;
    private float hoverMetallicValue;
    private float rayDistance;



    private void Start()
    {
        Material surfaceMaterial = GameObject.FindWithTag("Surface").GetComponent<Renderer>().material;

        hoveredSurface = null;
        lastHoveredSurface = null;
        hoverMetallicValue = 0f;
        originalMetallicValue = surfaceMaterial.GetFloat("_Metallic");
        rayDistance = 100f;
    }
    private void Update()
    {
        // No one surface has been found
        if (!FindSurfase(ref hoveredSurface) && lastHoveredSurface != null)
        {
            hoveredSurface = null;
            ResetLastHoveredObject();
            return;
        }        

        // hover on new tile
        if (hoveredSurface != lastHoveredSurface)
        {
            // Restoring the last tile
            if (lastHoveredSurface != null)
            {
                ResetLastHoveredObject();
            }

            lastHoveredSurface = hoveredSurface;
            Renderer renderer = hoveredSurface.GetComponent<Renderer>();

            if (renderer != null)
            {
                originalMetallicValue = renderer.material.GetFloat("_Metallic");
                renderer.material.SetFloat("_Metallic", hoverMetallicValue);
            }
        }

        //Signal to place GraviCenter to hovered surface 
        if (Input.GetMouseButtonDown(0) && hoveredSurface != null)
        {
            if (OnSurfaceSelected != null)
            {
                OnSurfaceSelected(hoveredSurface.transform);
            }
        }
    }

    /// <summary>
    /// Assigns hoveredObject object of the found surface
    /// </summary>
    /// <param name="hoveredObject">GameObject that will probably be a surface-tile</param>
    /// <returns>Return true if surface-tile is found, false if is not</returns>
    private bool FindSurfase(ref GameObject hoveredObject)
    {
        // Raycast from camera position to cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            // Choose surface object which the ray crossed with        
            if (hit.collider.gameObject.tag == "Surface")
            {
                hoveredObject = hit.collider.gameObject;
                return true;
            }
        }
        return false;
    }

    private void ResetLastHoveredObject()
    {
        Renderer renderer = lastHoveredSurface.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.SetFloat("_Metallic", originalMetallicValue);
        }
        lastHoveredSurface = null;
    }
}
