using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private List<GameObject> obstacles = new List<GameObject>();

    public delegate void EnergyPickedUpAction(int energyValue);
    public static event EnergyPickedUpAction OnEnergyPickedUp;
    void Start()
    {
        Transform startPoint = GameObject.Find("StartPoint").transform;
        transform.position = startPoint.position;
    }

    void Update()
    {
        UpdateXrayObstacles();
    }

    private void OnTriggerEnter(Collider other)
    {
        string collisionTag = other.gameObject.tag;

        if (collisionTag == "Energy")
        {
            OnEnergyPickedUp?.Invoke(other.gameObject.GetComponent<Energy>().EnergyValue);

            GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(other.transform.position));
            
            Destroy(other.gameObject);
        }
    }

    public void UpdateXrayObstacles()
    {
        List<GameObject> currentObstacles = RaycastTracker.GetRaycastObjects(transform);

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
}
