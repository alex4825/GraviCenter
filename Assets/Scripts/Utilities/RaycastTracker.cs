using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTracker : MonoBehaviour
{
    [SerializeField] float rayDistance = 100f;
    public GameObject GetRaycastObject(string objectTag)
    {
        // Raycast from camera position to cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            // Choose floor object which the ray crossed with        
            if (hit.collider.gameObject.tag == objectTag)
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
