using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class MaterialChanger
{
    public static void SetTransparency(GameObject obj, float alpha)
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
            Material mat = renderer.material;

            Color color = mat.color;
            color.a = alpha;
            mat.color = color;
        }
    }
    private static void SetEmission(ref Material material, float alpha)
    {
        if (alpha == 1 && !material.IsKeywordEnabled("_EMISSION"))
            material.EnableKeyword("_EMISSION");
        else
            material.DisableKeyword("_EMISSION");
    }
    public static void ChangeMetallic(ref GameObject floor, float newMetallicValue)
    {
        Renderer renderer = floor.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.SetFloat("_Metallic", newMetallicValue);
        }
    }

}
