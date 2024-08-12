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
        floorTransforms = FindTransforms();
    }

    private List<Transform> FindTransforms()
    {
        List<Transform> transforms = new List<Transform>();

        string floorTag = "Floor";

        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.CompareTag(floorTag))
            {
                transforms.Add(child.gameObject.transform);
            }
        }
        return transforms;
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
