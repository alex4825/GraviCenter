using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private List<GameObject> obstacles = new List<GameObject>();
    private Transform startPoint;

    public delegate void BallConditionAction(Transform ballTransform);
    public static event BallConditionAction OnBallCreated;
    public static event BallConditionAction OnBallDead;

    public delegate void EnergyChangeAction(int energyValue);
    public static event EnergyChangeAction OnChangeEnergy;

    void Start()
    {
        startPoint = Searcher.FindChildWithTag(GameManager.Instance.CurrentLevel.transform, "StartPoint");
        transform.position = startPoint.position;
        OnBallCreated?.Invoke(transform);
    }

    void Update()
    {
        UpdateXrayObstacles();

        if (transform.position.y < -0.75f)
        {
            Depth();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string collisionTag = other.gameObject.tag;

        if (collisionTag == "Energy")
        {
            OnChangeEnergy?.Invoke(other.gameObject.GetComponent<Energy>().EnergyValue);

            GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(other.transform.position));

            Destroyer.DeleteEnergy(other.transform);
        }
    }

    private void UpdateXrayObstacles()
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

    private void Depth()
    {
        OnBallDead?.Invoke(transform);
    }
}
