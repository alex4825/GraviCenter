using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateGradient : MonoBehaviour
{
    public int textureSize = 256;
    public Gradient gradient;

    void Start()
    {
        Texture2D texture = new Texture2D(textureSize, textureSize);
        // Центр текстуры
        Vector2 center = new Vector2(textureSize / 2f, textureSize / 2f);

        // Максимальное расстояние от центра (радиус круга)
        float maxDist = Vector2.Distance(Vector2.zero, center);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // Расстояние от текущего пикселя до центра
                float dist = Vector2.Distance(new Vector2(x, y), center);

                // Нормализация расстояния в диапазоне [0, 1]
                float t = Mathf.InverseLerp(0, maxDist, dist);

                // Получение цвета из градиента на основе нормализованного расстояния
                Color color = gradient.Evaluate(t);

                // Установка цвета пикселя в текстуре
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Textures/GradientTexture.png", bytes);
        
        // Применение текстуры к материалу
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
