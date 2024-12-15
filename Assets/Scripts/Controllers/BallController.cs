using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private List<GameObject> obstacles = new List<GameObject>();

    public delegate void BallConditionAction(Transform ball);
    public static event BallConditionAction OnBallCreated;
    //public static event BallConditionAction OnBallFell;

    public delegate void BallChangeEnergyAction(int energyValue);
    public static event BallChangeEnergyAction OnBallCatchSpark;
    public bool IsAbove { get; set; }

    void Start()
    {
        IsAbove = true;
        Animator.ScaleAppear(transform);
        OnBallCreated?.Invoke(transform);
    }

    void Update()
    {
        UpdateXrayObstacles();

        if (IsAbove && transform.position.y < -0.75f)
        {
            Animator.ScaleDisappear(transform);
            //OnBallFell?.Invoke(transform);
            IsAbove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string collisionTag = other.gameObject.tag;

        if (collisionTag == "Spark")
        {
            OnBallCatchSpark?.Invoke(other.gameObject.GetComponent<Spark>().Value);

            Destroyer.DeleteObject(other.gameObject);
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
