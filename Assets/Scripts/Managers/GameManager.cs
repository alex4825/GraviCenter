using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGamePaused = false;
    private Level currentLevel;

    public Level CurrentLevel { get { return currentLevel; } }
    private void OnEnable()
    {
        SetCurrentLevel(1);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SetCurrentLevel(int number)
    {
        List<Level> levels = new List<Level>();

        levels.AddRange(FindObjectsByType<Level>(FindObjectsSortMode.None));

        Level level = levels.Find(level => level.Number == number);
        level.IsActive = true;
        currentLevel = level;
    }
}
