using System;
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
    public int EnergyAmount { get { return energyAmount; } set { energyAmount = value; } }
    public List<Vector3> Floors { get; set; }
    public List<GameObject> GCs { get; set; }

    void Update()
    {
        if (EnergyAmount <= 0)
        {
            if (ShortcutManager.SelectedGC != null)
            {
                Destroyer.DeleteGC(ShortcutManager.SelectedGC);
            }

            for (int i = 0; i < GCs.Count; i++)
            {
                Destroyer.DeleteGC(GCs[i]);
                i--;
            }
        }
    }
    private void OnEnable()
    {
        IsActive = true;
        Floors = FloorChecker.FindFloors(this);
        GCs = new List<GameObject>();

        energyAmountTMP.text = energyAmount.ToString();

        BallController.OnChangeEnergy += ChangeEnergyAmount;
        GraviCenter.OnChangeEnergy += ChangeEnergyAmount;
    }
    private void OnDisable()
    {
        BallController.OnChangeEnergy -= ChangeEnergyAmount;
        GraviCenter.OnChangeEnergy -= ChangeEnergyAmount;
    }

    private void ChangeEnergyAmount(int energySummand)
    {
        energyAmount += energySummand;
        energyAmountTMP.text = energyAmount.ToString();
    }

}
