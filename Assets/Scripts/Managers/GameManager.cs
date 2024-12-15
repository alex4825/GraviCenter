using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject levelSelectInterface;
    [SerializeField] GameObject inPlayInterface;
    [SerializeField] GameObject ball;
    [SerializeField] List<GameObject> parts;

    private Level currentLevel;
    private GameObject currentPart;

    public static GameManager Instance { get; private set; }
    public Level CurrentLevel { get { return currentLevel; } }
    public GameObject CurrentPart { get { return currentPart; } }
    public GameStates GameState { get; private set; }

    //public delegate void GameStateAction(GameStates gameState);
    //public static event GameStateAction OnGameStateChanged;

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
        GameState = GameStates.MainMenu;
    }

    private void OnEnable()
    {
        LevelSelector.OnLevelSelected += StartGame;
        MenuShortcutHandler.OnMenuEnable += PauseGame;
    }

    private void OnDisable()
    {
        LevelSelector.OnLevelSelected -= StartGame;
        MenuShortcutHandler.OnMenuEnable -= PauseGame;
    }

    void OnApplicationQuit()
    {
        DOTween.Clear();
        DOTween.KillAll();
    }

    private void StartGame(int partNumber, int levelNumber)
    {
        currentPart = parts[partNumber - 1];

        List<Level> levels = new List<Level>();
        levels.AddRange(currentPart.GetComponentsInChildren<Level>(true));

        //currentLevel = null;
        currentLevel = levels.Find(level => level.Number == levelNumber);
        currentLevel.gameObject.SetActive(true);
        currentLevel.Initiate(ball);

        levelSelectInterface.SetActive(false);

        inPlayInterface.SetActive(true);

        GameState = GameStates.Playing;
        //OnGameStateChanged?.Invoke(GameState);
    }

    private void PauseGame()
    {
        currentLevel.gameObject.SetActive(false);
        levelSelectInterface.SetActive(true);

        inPlayInterface.SetActive(false);

        GameState = GameStates.Paused;
        //OnGameStateChanged?.Invoke(GameState);
    }
}
