using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDistributer : MonoBehaviour
{
    [SerializeField] GameObject energyPrefab;
    [SerializeField] int energyNums = 5;

    private void OnEnable()
    {
        SetRandomPositions(energyPrefab, energyNums);
    }

    private void SetRandomPositions(GameObject prefab, int nums)
    {
        for (int i = 0; i < nums; i++)
        {
            List<Vector3> floors = GameManager.CurrentLevel.Floors;

            int randIndex = Random.Range(0, floors.Count);

            Vector3 pos = floors[randIndex];
            pos.y = pos.y - 0.5f * prefab.transform.localScale.y;

            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

            //delete the element to avoid repetitions
            floors.RemoveAt(randIndex);
        }
    }
}
