using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public LevelManager CurrentLevel { get; set; }
    void OnEnable()
    {
        CurrentLevel = FindFirstObjectByType<LevelManager>();
    }

    void Update()
    {
        
    }
}
