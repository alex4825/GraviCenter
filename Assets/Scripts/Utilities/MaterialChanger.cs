using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public void SetTransparency(float alpha)
    {
        GameObject obj = this.gameObject;

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

    private void SetEmission(ref Material material, float alpha)
    {
        if (alpha == 1 && !material.IsKeywordEnabled("_EMISSION"))
            material.EnableKeyword("_EMISSION");
        else
            material.DisableKeyword("_EMISSION");

    }
}
