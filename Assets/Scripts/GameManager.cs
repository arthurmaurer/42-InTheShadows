using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager
{
    public static Level currentLevel;
    public static Level[] levels;
    
    public static void Configure()
    {
        levels = new Level[2];

        levels[0] = new Level(
            "Puzzle 1",
            Resources.Load<GameObject>("Prefabs/LevelItem"),
            "Scenes/SolvingRooms/Theatre"
        );

        levels[1] = new Level(
            "Puzzle 2",
            Resources.Load<GameObject>("Prefabs/LevelItem"),
            "Scenes/SolvingRooms/Desert"
        );
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Scenes/LevelSelection");
    }

    public static void LoadLevel(Level level)
    {
        currentLevel = level;
        SceneManager.LoadScene(level.sceneName);
    }
    
    public static void EndLevel()
    {

    }
}
