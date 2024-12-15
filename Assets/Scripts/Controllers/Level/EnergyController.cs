using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    /*private int amount;
    public int Amount { get { return amount; } }
    private void OnEnable()
    {
        //GameManager.OnGameStateChanged +=
        BallController.OnBallCatchSpark += AddAmount;
        GraviCenter.OnChangeEnergy += ChangeEnergyAmount;
        //BallController.OnBallFell += Restart;
    }
    private void OnDisable()
    {
        BallController.OnBallCatchSpark -= AddAmount;
        GraviCenter.OnChangeEnergy -= ChangeEnergyAmount;
        //BallController.OnBallFell -= Restart;
    }

    public void AddAmount(int value)
    {
        amount += Mathf.Max(0, value);
    }

    public void SetAmount(int value)
    {
        amount = Mathf.Max(0, value);
    }
    /*private int amount;

     public int Amount { get { return amount; } }

     public EnergyController(int value)
     {
         amount = Mathf.Max(0, value);
     }

     public void AddAmount(int value)
     {
         amount += Mathf.Max(0, value);
     }

     public void SetAmount(int value)
     {
         amount = Mathf.Max(0, value);
     }
     */
}
