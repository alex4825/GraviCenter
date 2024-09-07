using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int number;
    [SerializeField] int energyAmount = 500;
    [SerializeField] TextMeshProUGUI energyAmountTMP;
    public int Number { get { return number; } }
    public bool IsActive { get; set; }
    public List<Vector3> Floors { get; set; }
    public int EnergyAmount { get { return energyAmount; } set { energyAmount = value; } }

    private void OnEnable()
    {
        IsActive = true;
        Floors = FloorChecker.FindFloors(this);

        energyAmountTMP.text = energyAmount.ToString();

        BallController.OnEnergyPickedUp += ChangeEnergyAmount;
        GraviCenter.OnChangeEnergy += ChangeEnergyAmount;
    }
    private void OnDisable()
    {
        BallController.OnEnergyPickedUp -= ChangeEnergyAmount;
        GraviCenter.OnChangeEnergy -= ChangeEnergyAmount;
    }

    private void ChangeEnergyAmount(int energySummand)
    {
        energyAmount += energySummand;
        energyAmountTMP.text = energyAmount.ToString();
    }

    void Update()
    {

    }
}
