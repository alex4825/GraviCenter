using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private int amount;
    private int energyPerSecond = 0;
    private bool isFirstGC = false;

    [SerializeField] int startAmount = 500;
    [SerializeField] TextMeshProUGUI amountTMP;
    public int Amount { get { return amount; } }
    private void Start()
    {
        ResetAmount();
    }
    private void Update()
    {
        if (amount <= 0)
        {
            energyPerSecond = 0;
            amount = 0;
            isFirstGC = false;
            Destroyer.DeleteObjectList(GetComponent<Level>().GCs);
        }
    }
    private void OnEnable()
    {
        BallController.OnBallCatchSpark += AddAmount;
        GraviCenter.OnPlacedGC += ReduceByPlacedGC;
        GraviCenter.OnDeletedGC += AddByDeletedGC;
        Level.OnLevelInitiated += ResetAmount;
    }
    private void OnDisable()
    {
        BallController.OnBallCatchSpark -= AddAmount;
        GraviCenter.OnPlacedGC -= ReduceByPlacedGC;
        GraviCenter.OnDeletedGC -= AddByDeletedGC;
        Level.OnLevelInitiated -= ResetAmount;
    }

    private void ReduceByPlacedGC(GraviCenter gc)
    {
        AddAmount(-gc.EnergyCost);

        if (energyPerSecond == 0) //the most first GC
            isFirstGC = true;

        energyPerSecond += gc.EnergyPerSecondCost;

        if (isFirstGC)
        {
            StartCoroutine(ReduceAmountPerSecond());
            isFirstGC = false;
        }

    }
    private void AddByDeletedGC(GraviCenter gc)
    {
        AddAmount(gc.EnergyCost / 2);
        energyPerSecond -= gc.EnergyPerSecondCost;
    }
    private void AddAmount(int energySummand)
    {
        amount += energySummand;
        amountTMP.text = amount.ToString();
    }

    private void ResetAmount()
    {
        StopCoroutine(ReduceAmountPerSecond());
        amount = startAmount;
        amountTMP.text = amount.ToString();
    }
    private IEnumerator ReduceAmountPerSecond()
    {
        while (true)
        {
            if (amount <= 0)
            {
                yield break;
            }
            AddAmount(-1);

            yield return new WaitForSeconds(1f / (float)energyPerSecond);
        }
    }
}
