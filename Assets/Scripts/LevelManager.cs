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

    public int EnergyAmount { get { return energyAmount; } set { energyAmount = value; } }

    private void OnEnable()
    {
        energyAmountTMP.text = energyAmount.ToString();
        PlaceRandomEnergy();

        PlayerController.OnEnergyPickedUp += ChangeEnergyAmount;
    }

    private void OnDisable()
    {
        PlayerController.OnEnergyPickedUp -= ChangeEnergyAmount;
    }

    private void ChangeEnergyAmount(int energySummand)
    {
        energyAmount += energySummand;
        energyAmountTMP.text = energyAmount.ToString();
    }

    private void PlaceRandomEnergy()
    {
        List<Transform> floorTransforms = FindFloorPositions();

        for (int i = 0; i < energyNums; i++)
        {
            int randIndex = Random.Range(0, floorTransforms.Count);

            GameObject energyObj = Instantiate(energyPrefab, floorTransforms[randIndex].transform.position, Quaternion.identity);
            energyObj.transform.position.Set(energyObj.transform.position.x, energyObj.transform.position.y - 0.25f, energyObj.transform.position.z);

            //delete the element to avoid repetitions
            floorTransforms.RemoveAt(randIndex);
        }
    }

    private List<Transform> FindFloorPositions()
    {
        string floorTag = "Floor";

        List<Transform> floorTransforms = new List<Transform>();

        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.CompareTag(floorTag))
            {
                floorTransforms.Add(child.gameObject.transform);
            }
        }
        return floorTransforms;
    }
}
