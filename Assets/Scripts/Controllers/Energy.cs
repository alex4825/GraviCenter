using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] int energyValue = 100;

    public int EnergyValue { get { return energyValue; } set { energyValue = value; } }
}
