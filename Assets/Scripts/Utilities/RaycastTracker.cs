using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastTracker
{
    public static GameObject GetRaycastObject(string objectTag)
    {
        float rayDistance = 100f;
        // Raycast from camera position to cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            // Choose first object which the ray crossed with        
            if (hit.collider.gameObject.tag == objectTag)
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    public static List<GameObject> GetRaycastObjects(Transform target)
    {
        List<GameObject> obstacles = new List<GameObject>();

        float rayDistance = (Camera.main.transform.position - target.position).magnitude;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.transform == target.transform) //exclude the object itself
                continue;

            obstacles.Add(hit.collider.gameObject);
        }
        return obstacles;
    }
}
