using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (collisionTag == "Energy")
        {
            OnEnergyPickedUp?.Invoke(collision.gameObject.GetComponent<Energy>().EnergyValue);
            Destroy(collision.gameObject);            
        }

        if (collisionTag == "Finish")
        {
            Debug.Log("Level complete");
        }
    }
}
