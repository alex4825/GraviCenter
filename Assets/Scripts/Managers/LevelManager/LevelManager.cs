using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject energyPrefab;
    [SerializeField] TextMeshProUGUI energyAmountTMP;
    [SerializeField] int energyAmount = 150;
    [SerializeField] int energyNums = 5;

    public List<Vector3> Floors { get; set; }
    public int EnergyAmount { get { return energyAmount; } set { energyAmount = value; } }

    private void OnEnable()
    {
        Floors = FloorChecker.FindFloors();

        energyAmountTMP.text = energyAmount.ToString();
        SetRandomPositions(energyPrefab, energyNums);

        BallController.OnEnergyPickedUp += ChangeEnergyAmount;
        GraviCenter.OnGraviCenterDestroyed += ChangeEnergyAmount;
        GraviCenter.OnGraviCenterCreated += ChangeEnergyAmount;
    }

    private void OnDisable()
    {
        BallController.OnEnergyPickedUp -= ChangeEnergyAmount;
        GraviCenter.OnGraviCenterDestroyed -= ChangeEnergyAmount;
        GraviCenter.OnGraviCenterCreated -= ChangeEnergyAmount;
    }

    private void ChangeEnergyAmount(int energySummand)
    {
        energyAmount += energySummand;
        energyAmountTMP.text = energyAmount.ToString();
    }

    private void SetRandomPositions(GameObject prefab, int nums)
    {
        for (int i = 0; i < nums; i++)
        {
            int randIndex = Random.Range(0, Floors.Count);

            Vector3 pos = Floors[randIndex];
            pos.y = pos.y - 0.5f * prefab.transform.localScale.y;

            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

            //delete the element to avoid repetitions
            Floors.RemoveAt(randIndex);
        }
    }

    
}
