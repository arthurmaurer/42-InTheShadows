using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public struct Level
{
    [Flags]
    public enum Constrain
    {
        None = 0,
        NoXRotation = 1,
        NoYRotation = 2,
        NoXTranslation = 4,
        NoYTranslation = 8
    }

    public string name;
    public string sceneName;
    public GameObject menuObject;
    public GameObject[] puzzlePieces;
    public Constrain constrains;

    public Level(string name, GameObject menuObject, GameObject[] puzzlePieces, string sceneName, Constrain constrains)
    {
        this.name = name;
        this.menuObject = menuObject;
        this.puzzlePieces = puzzlePieces;
        this.sceneName = sceneName;
        this.constrains = constrains;
    }

    public bool HasConstrain(Constrain constrain)
    {
        return ((constrains & constrain) == constrain);
    }
}
