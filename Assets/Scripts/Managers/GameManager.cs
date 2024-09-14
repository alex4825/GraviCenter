using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGamePaused = false;

    static public Level CurrentLevel { get; set; }
    private void OnEnable()
    {
        SetCurrentLevel(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        // Если игра на паузе, возобновляем её
        if (isGamePaused)
        {
            Time.timeScale = 1f; // Нормальная скорость времени
            isGamePaused = false;
            Debug.Log("Игра продолжена");
        }
        // Если игра не на паузе, ставим её на паузу
        else
        {
            Time.timeScale = 0f; // Останавливаем время
            isGamePaused = true;
            Debug.Log("Игра на паузе");
        }
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
