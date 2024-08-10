using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    private List<Transform> floorTransforms = new List<Transform>();
    public List<Transform>  FloorTransforms { get { return floorTransforms; } }

    private void OnEnable()
    {
        //all floors are free after game starting
        floorTransforms = FindPositions();
    }

    private List<Transform> FindPositions()
    {
        string floorTag = "Floor";

        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.CompareTag(floorTag))
            {
                floorTransforms.Add(child.gameObject.transform);
            }
        }
        return floorTransforms;
    }

    public void Draw(ref GameObject floor, float newMetallicValue)
    {
        Renderer renderer = floor.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.SetFloat("_Metallic", newMetallicValue);
        }
    }
}
