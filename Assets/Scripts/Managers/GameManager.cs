using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public Level CurrentLevel { get; set; }
    private void OnEnable()
    {
        SetCurrentLevel(1);
    }

    private void Update()
    {

    }

    private Level GetCurrentLevel()
    {
        List<Level> levels = new List<Level>();

        levels.AddRange(FindObjectsByType<Level>(FindObjectsSortMode.None));

        Level currentLevel = levels.Find(level => level.IsActive == true);

        return currentLevel;
    }
    private void SetCurrentLevel(int number)
    {
        List<Level> levels = new List<Level>();

        levels.AddRange(FindObjectsByType<Level>(FindObjectsSortMode.None));

        Level currentLevel = levels.Find(level => level.Number == number);
        currentLevel.IsActive = true;
        CurrentLevel = currentLevel;
    }
}
