using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayTarget
{
    private GameObject obj;
    private List<GameObject> obstacles;
    public GameObject Obj { get { return obj; } }
    public XrayTarget(GameObject targetObject)
    {
        obj = targetObject;
        obstacles = new List<GameObject>();
    }

    public void UpdateObstacles()
    {
        List<GameObject> currentObstacles = RaycastTracker.GetRaycastObjects(obj.transform);

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (currentObstacles.Contains(obstacles[i]))
                continue;

            MaterialChanger.SetTransparency(obstacles[i], 1);
            obstacles.RemoveAt(i);
            i--;
        }

        for (int i = 0; i < currentObstacles.Count; i++)
        {
            if (obstacles.Contains(currentObstacles[i]))
                continue;

            MaterialChanger.SetTransparency(currentObstacles[i]);
            obstacles.Add(currentObstacles[i]);
        }
    }

    public void UpdateObstacles(bool isReset)
    {
        if (isReset)
        {
            foreach (var obstacle in obstacles)
            {
                MaterialChanger.SetTransparency(obstacle, 1);
            }
        }
    }
}
