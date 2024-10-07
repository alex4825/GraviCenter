using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private List<GameObject> obstacles = new List<GameObject>();
    private Transform startPoint;

    public bool IsAbove { get; set; }

    public delegate void BallConditionAction(Transform ball);
    public static event BallConditionAction OnBallCreated;
    public static event BallConditionAction OnBallFell;

    public delegate void EnergyChangeAction(int energyValue);
    public static event EnergyChangeAction OnChangeEnergy;

    void Start()
    {
        startPoint = Searcher.FindChildWithTag(GameManager.Instance.CurrentLevel.transform, "StartPoint");
        transform.position = startPoint.position;
        IsAbove = true;
        OnBallCreated?.Invoke(transform);
    }

    void Update()
    {
        UpdateXrayObstacles();

        if (IsAbove && transform.position.y < -0.75f)
        {
            OnBallFell?.Invoke(transform);
            IsAbove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string collisionTag = other.gameObject.tag;

        if (collisionTag == "Energy")
        {
            OnChangeEnergy?.Invoke(other.gameObject.GetComponent<Energy>().EnergyValue);

            GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(other.transform.position));

            Destroyer.DeleteObject(other.transform);
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
}
