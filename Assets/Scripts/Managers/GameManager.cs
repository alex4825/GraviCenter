using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject levelSelectInterface;
    [SerializeField] GameObject inPlayInterface;
    [SerializeField] List<GameObject> parts;
    [SerializeField] float restartTimeInSec = 1f;

    private Level currentLevel;
    private GameObject currentPart;

    public static GameManager Instance { get; private set; }
    public Level CurrentLevel { get { return currentLevel; } }
    public GameObject CurrentPart { get { return currentPart; } }
    public GameStates GameState { get; private set; }

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
        MenuShortcutHandler.OnMenuEnabled += PauseGame;
        BallController.OnBallFell += RestartingGame;
        Level.OnBallMovedToStart += RestartGame;
    }

    private void OnDisable()
    {
        LevelSelector.OnLevelSelected -= StartGame;
        MenuShortcutHandler.OnMenuEnabled -= PauseGame;
        BallController.OnBallFell += RestartingGame;
        Level.OnBallMovedToStart -= RestartGame;
    }

    void OnApplicationQuit()
    {
        DOTween.Clear();
        DOTween.KillAll();
    }

    private void StartGame(int partNumber, int levelNumber)
    {
        GameState = GameStates.Started;

        currentPart = parts[partNumber - 1];

        List<Level> levels = new List<Level>();
        levels.AddRange(currentPart.GetComponentsInChildren<Level>(true));

        currentLevel = levels.Find(level => level.Number == levelNumber);
        currentLevel.gameObject.SetActive(true);
        currentLevel.Initiate();

        levelSelectInterface.SetActive(false);

        inPlayInterface.SetActive(true);

        GameState = GameStates.Playing;
    }

    private void PauseGame()
    {
        currentLevel.gameObject.SetActive(false);
        levelSelectInterface.SetActive(true);

        inPlayInterface.SetActive(false);

        GameState = GameStates.Paused;
    }

    private void RestartingGame()
    {
        GameState = GameStates.Restarting;
        currentLevel.Restart(restartTimeInSec);
    }

    private void RestartGame()
    {
        GameState = GameStates.Playing;
    }
}
