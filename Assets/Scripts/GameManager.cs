using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GameManager
{
    public static Level currentLevel;
    public static Level[] levels;

    public static void Configure()
    {
        levels = new Level[] {
            new Level(
                "SLURPP...",
                Resources.Load<GameObject>("Prefabs/LevelItems/1"),
                new GameObject[] {
                    Resources.Load<GameObject>("Prefabs/Puzzles/Teapot")
                },
                "Scenes/SolvingRooms/Theatre",
                Level.Constrain.NoXTranslation | Level.Constrain.NoYTranslation | Level.Constrain.NoXRotation,
                PuzzleChecker.CheckDefault,
                true
            ),
            new Level(
                "NOT   IN   THE   DESERT",
                Resources.Load<GameObject>("Prefabs/LevelItems/2"),
                new GameObject[] {
                    Resources.Load<GameObject>("Prefabs/Puzzles/Elephant")
                },
                "Scenes/SolvingRooms/Desert",
                Level.Constrain.NoXTranslation | Level.Constrain.NoYTranslation,
                PuzzleChecker.CheckDefault,
                false
            ),
            new Level(
                "BORN2CODE", 
                Resources.Load<GameObject>("Prefabs/LevelItems/3"),
                new GameObject[] {
                    Resources.Load<GameObject>("Prefabs/Puzzles/2"),
                    Resources.Load<GameObject>("Prefabs/Puzzles/4")
                },
                "Scenes/SolvingRooms/Theatre",
                Level.Constrain.None,
                PuzzleChecker.Check42,
                false
            )
        };
    }

    public static void LoadLevel(Level level)
    {
        currentLevel = level;
        GameObject.FindObjectOfType<Fader>().FadeIn(() => SceneManager.LoadScene(level.sceneName));
    }
    
    public static void EndLevel()
    {
        GameObject soundPrefab = Resources.Load<GameObject>("Prefabs/BellSound");
        GameObject flashPrefab = Resources.Load<GameObject>("Prefabs/SuccessFader");
        GameObject canvas = GameObject.FindWithTag("Canvas");

        GameObject flash = UnityEngine.Object.Instantiate<GameObject>(flashPrefab);
        flash.transform.SetParent(canvas.transform, false);

        GameObject bellSound = UnityEngine.Object.Instantiate<GameObject>(soundPrefab);

        bellSound.GetComponent<DestroyOnSoundEnd>().callback = () =>
            GameObject.FindObjectOfType<Fader>().FadeIn(() =>
                SceneManager.LoadScene("Scenes/VictoryScreen"));
    }

    public static void LoadNextLevel()
    {
        int id = Array.IndexOf(levels, currentLevel);

        if (id >= 0 && id < levels.Length)
            LoadLevel(levels[id + 1]);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/LevelSelection");
    }

    public static void UnlockNextLevel()
    {
        int id = GetCurrentLevelID() + 1;

        if (id < levels.Length)
            levels[id].Unlock();
    }

    static int GetCurrentLevelID()
    {
        return Array.IndexOf(levels, currentLevel);
    }
}
