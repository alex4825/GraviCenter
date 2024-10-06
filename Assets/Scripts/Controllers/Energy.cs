using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Energy : MonoBehaviour
{
    [SerializeField] int energyValue = 100;
    public int EnergyValue { get { return energyValue; } set { energyValue = value; } }

    public delegate void EnergyConditionAction(Transform energyTransform);
    public static event EnergyConditionAction OnEnergyCreated;

    private void Start()
    {
        OnEnergyCreated?.Invoke(transform);
    }
}
