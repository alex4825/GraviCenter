﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCamera : MonoBehaviour
{
    public class XrayTarget
    {
        private GameObject obj;
        private List<GameObject> obstacles;
        private float alpha = 0.1f;
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

                MaterialChanger.SetTransparency(currentObstacles[i], alpha);
                obstacles.Add(currentObstacles[i]);                
            }
        }
    }


    [SerializeField] GameObject ball;
    [SerializeField] Vector3 offset;

    private List<XrayTarget> targets;

    private void Start()
    {
        targets = new List<XrayTarget>();
        targets.Add(new XrayTarget(ball));

        transform.position = ball.transform.position;
    }

    private void LateUpdate()
    {
        UpdateTargets(null);
        transform.position = ball.transform.position + offset;
    }

    public void UpdateTargets(GameObject newTarget, bool shouldDestroy = false)
    {
        if (shouldDestroy)
        {

        }

        if (newTarget != null)
        {
            targets.Add(new XrayTarget(newTarget));
        }

        foreach (var target in targets)
        {
            target.UpdateObstacles();
        }
    }
}
