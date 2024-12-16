using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class Distributer
{
    public static List<GameObject> RandomizePositions(Level level, GameObject prefab, int amount)
    {
        List<GameObject> initiatedObjList = new List<GameObject>();
        List<Vector3> floors = level.Floors;

        Vector3[] randPositions = GetRandPositions(new List<Vector3>(floors), amount);

        for (int i = 0; i < amount; i++)
        {
            initiatedObjList.Add(Object.Instantiate(prefab, randPositions[i], Quaternion.identity, level.transform));
        }
        floors.RemoveAll(x => randPositions.Contains(x));
        return initiatedObjList;
    }
    private static Vector3[] GetRandPositions(List<Vector3> floorsCopy, int amount)
    {
        float divider = 0.7f;
        int sectionCellsAmount = (int)((floorsCopy.Count / amount) * divider); //57

        float sectionRadius = Mathf.Sqrt(sectionCellsAmount);

        Vector3[] randCells = new Vector3[amount];

        for (int i = 0; i < amount; i++)
        {
            Vector3 randCell;
            List<Vector3> nearbyCells;

            randCell = floorsCopy[Random.Range(0, floorsCopy.Count)];

            nearbyCells = floorsCopy.Where(cell => Vector3.Distance(randCell, cell) < sectionRadius).ToList();

            int sizeSection = nearbyCells.Count;
            floorsCopy.RemoveAll(cell => nearbyCells.Contains(cell));
            randCells[i] = randCell;
        }
        return randCells;
    }
}
