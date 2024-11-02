using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menuBackground;
    [SerializeField] GameObject menuLevelSelect;
    [SerializeField] GameObject inPlayInterface;
    [SerializeField] GameObject ball;
    [SerializeField] List<GameObject> parts;

    private bool isGamePaused = false;
    private Level currentLevel;
    private GameObject currentPart;

    public static GameManager Instance { get; private set; }
    public Level CurrentLevel { get { return currentLevel; } }
    public GameObject CurrentPart { get { return currentPart; } }
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

    private void OnEnable()
    {
        LevelSelector.OnLevelSelected += PlayGame;
    }

    private void OnDisable()
    {
        LevelSelector.OnLevelSelected -= PlayGame;
    }

    void OnApplicationQuit()
    {
        DOTween.Clear();
        DOTween.KillAll();
    }

    private void PlayGame(int partNumber, int levelNumber)
    {
        currentPart = parts[partNumber - 1];

        List<Level> levels = new List<Level>();
        levels.AddRange(currentPart.GetComponentsInChildren<Level>(true));

        currentLevel = levels.Find(level => level.Number == levelNumber);
        currentLevel.gameObject.SetActive(true);
        currentLevel.IsActive = true;

        menuBackground.SetActive(false);
        menuLevelSelect.SetActive(false);

        inPlayInterface.SetActive(true);
        Instantiate(ball);
    }
}
