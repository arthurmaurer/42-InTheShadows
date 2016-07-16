using UnityEngine;
using UnityEngine.SceneManagement;

public struct Level
{
    public string name;
    public string sceneName;
    public GameObject puzzlePiece;

    public Level(string name, GameObject puzzlePiece, string sceneName)
    {
        this.name = name;
        this.puzzlePiece = puzzlePiece;
        this.sceneName = sceneName;
    }
}
