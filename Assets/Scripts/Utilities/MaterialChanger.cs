using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialChanger
{
    public static void SetTransparency(GameObject obj, float alpha)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;

            if (mat.shader.name == "Standard")
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
                SetEmission(ref mat, alpha);
            }
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
