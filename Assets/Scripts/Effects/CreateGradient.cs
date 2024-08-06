using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateGradient : MonoBehaviour
{
    [SerializeField] int textureSize = 256;
    [SerializeField] Gradient gradient;

    private void Start()
    {
        Texture2D texture = new Texture2D(textureSize, textureSize);

        Vector2 center = new Vector2(textureSize / 2f, textureSize / 2f);

        // Maximum distance from the center (radius of the circle)
        float maxDist = Vector2.Distance(Vector2.zero, center);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // The distance from the current pixel to the center
                float dist = Vector2.Distance(new Vector2(x, y), center);

                // Normalization of the distance in the range [0, 1]
                float t = Mathf.InverseLerp(0, maxDist, dist);

                // Getting a color from a gradient based on a normalized distance
                Color color = gradient.Evaluate(t);

                // Setting the pixel color in the texture
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Textures/GradientTexture.png", bytes);

        // Applying texture to  material
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
