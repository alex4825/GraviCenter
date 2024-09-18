using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public delegate void EnergyPickedUpAction(int energyValue);
    public static event EnergyPickedUpAction OnEnergyPickedUp;
    void Start()
    {
        Transform startPoint = GameObject.Find("StartPoint").transform;
        transform.position = startPoint.position;
    }

    void Update()
    {

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
}
