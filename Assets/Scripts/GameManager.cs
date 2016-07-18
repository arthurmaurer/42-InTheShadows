using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager
{
    public static Level currentLevel;
    public static Level[] levels;

    public static void Configure()
    {
        levels = new Level[] {
            new Level(
                "Puzzle 1",
                Resources.Load<GameObject>("Prefabs/LevelItem"),
                new GameObject[] {
                    Resources.Load<GameObject>("Prefabs/Puzzles/Teapot")
                },
                "Scenes/SolvingRooms/Theatre",
                Level.Constrain.NoXTranslation | Level.Constrain.NoYTranslation | Level.Constrain.NoXRotation
            ),
            new Level(
                "Puzzle 2",
                Resources.Load<GameObject>("Prefabs/LevelItem"),
                new GameObject[] {
                    Resources.Load<GameObject>("Prefabs/Puzzles/Elephant")
                },
                "Scenes/SolvingRooms/Desert",
                Level.Constrain.NoXTranslation | Level.Constrain.NoYTranslation
            ),
            new Level(
                "Puzzle 3",
                Resources.Load<GameObject>("Prefabs/LevelItem"),
                new GameObject[] {
                    Resources.Load<GameObject>("Prefabs/Puzzles/2"),
                    Resources.Load<GameObject>("Prefabs/Puzzles/4")
                },
                "Scenes/SolvingRooms/Theatre",
                Level.Constrain.None
            )
        };
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Scenes/LevelSelection");
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

        GameObject flash = Object.Instantiate<GameObject>(flashPrefab);
        flash.transform.SetParent(canvas.transform, false);

        GameObject bellSound = Object.Instantiate<GameObject>(soundPrefab);

        bellSound.GetComponent<DestroyOnSoundEnd>().callback = () =>
            GameObject.FindObjectOfType<Fader>().FadeIn(() =>
                SceneManager.LoadScene("Scenes/VictoryScreen"));
    }
}
