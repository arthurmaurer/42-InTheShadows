﻿using UnityEngine;
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
    public bool locked;
    public GameObject menuObject;
    public GameObject[] puzzlePieces;
    public Constrain constrains;
    public Func<GameObject[], bool> checker;

    public Level(
        string name,
        GameObject menuObject,
        GameObject[] puzzlePieces,
        string sceneName,
        Constrain constrains,
        Func<GameObject[], bool> checker,
        bool alwaysUnlocked = false
    )
    {
        this.name = name;
        this.menuObject = menuObject;
        this.puzzlePieces = puzzlePieces;
        this.sceneName = sceneName;
        this.constrains = constrains;
        this.checker = checker;
        this.locked = true;

        if (alwaysUnlocked)
            this.locked = false;
        else
            locked = IsLocked();
    }

    public bool HasConstrain(Constrain constrain)
    {
        return ((constrains & constrain) == constrain);
    }

    public bool IsLocked()
    {
        return (PlayerPrefs.GetInt("locked." + name, 1) == 1);
    }

    public void Unlock()
    {
        PlayerPrefs.SetInt("locked." + name, 0);
        locked = false;
    }
}
