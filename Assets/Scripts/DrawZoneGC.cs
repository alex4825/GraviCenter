using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawZoneGC : MonoBehaviour
{
    private float radius;  // Радиус кольца

    public int segments = 100; // Количество сегментов в кольце    
    public float width = 2f; // Ширина линии
    public Gradient colorGradient;
    public Vector3 rotationEulerAngles = new Vector3(0, 0, 0);

    void Start()
    {
        radius = gameObject.GetComponent<GraviCenterManager>().gravityZone;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; // Один дополнительный сегмент для замыкания кольца
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.useWorldSpace = false; // Локальные координаты относительно объекта

        // Настройка материала и цвета линии (необязательно)
        lineRenderer.colorGradient = colorGradient;
        lineRenderer.colorGradient.mode = GradientMode.Blend;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));


        // Создаем кватернион для вращения
        Quaternion rotation = Quaternion.Euler(rotationEulerAngles);

        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = angleStep * i;
            float radian = Mathf.Deg2Rad * angle;

            // Вычисляем позицию каждой точки по окружности
            float x = Mathf.Cos(radian) * radius;
            float z = Mathf.Sin(radian) * radius;
            Vector3 pointPosition = new Vector3(x, 0.5f, z);

            // Применяем вращение к каждой точке
            pointPosition = rotation * pointPosition;

            lineRenderer.SetPosition(i, pointPosition);
        }
    }
}
