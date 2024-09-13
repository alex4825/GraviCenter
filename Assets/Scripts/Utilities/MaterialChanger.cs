using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class MaterialChanger
{
    public static void SetTransparency(GameObject obj, float alpha = 0.5f)
    {
        if (obj == null)
            return;

        List<Renderer> renderers = new List<Renderer>();

        if (obj.GetComponents<Renderer>() != null)
            renderers.AddRange(obj.GetComponents<Renderer>());

        if (obj.transform.childCount > 0 && obj.GetComponentsInChildren<Renderer>() != null)
            renderers.AddRange(obj.GetComponentsInChildren<Renderer>());

        foreach (Renderer renderer in renderers)
        {
            Material material = renderer.material;

            Color color = material.color;
            color.a = alpha;
            material.color = color;

            SetEmission(ref material, alpha, "_EmissionColor");

        }
    }
    private static void SetEmission(ref Material material, float alpha, string property)
    {
        if (material.HasProperty(property))
        {
            if (alpha == 1)
                material.SetColor(property, Color.white);
            else
                material.SetColor(property, Color.black);
        }
    }
    public static void ChangeMetallic(ref GameObject floor, float newMetallicValue)
    {
        Renderer renderer = floor.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.SetFloat("_Metallic", newMetallicValue);
        }
    }

    public static void InvertZoneDirection(GameObject objGC)
    {
        if (objGC == null)
            return;

        Renderer[] renderers = objGC.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.HasFloat("_Speed"))
            {
                renderer.material.SetFloat("_Speed", renderer.material.GetFloat("_Speed") * -1);
            }
        }
    }
}
