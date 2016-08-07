using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Victory : MonoBehaviour
{
    int _choiceID = 1;
    VictoryButton[] _buttons;

    void Start()
    {
        _buttons = GameObject.FindObjectsOfType<VictoryButton>();

        SpawnPuzzlePieces();

        GameManager.UnlockNextLevel();
    }

    void Update()
    {
        if (Input.GetButton("Submit"))
        {
            if (_choiceID == 0)
                GameManager.LoadMenu();
            else if (_choiceID == 1)
                GameManager.LoadNextLevel();
        }
        else
        {
            if (Input.GetKeyDown("d"))
                --_choiceID;

            if (Input.GetKeyDown("q"))
                ++_choiceID;

            _choiceID = Mathf.Clamp(_choiceID, 0, _buttons.Length - 1);

            foreach (VictoryButton button in _buttons)
                button.SetActive(false);

            _buttons[_choiceID].SetActive(true);
        }
    }

    void SpawnPuzzlePieces()
    {
        Transform spinner = FindObjectOfType<Spinner>().transform;

        foreach (GameObject puzzlePiece in GameManager.currentLevel.puzzlePieces)
        {
            SpawnPuzzlePiece(puzzlePiece, spinner);
        }
    }

    void SpawnPuzzlePiece(GameObject puzzlePiece, Transform parent)
    {
        GameObject instance = (GameObject)Instantiate(puzzlePiece);

        instance.transform.SetParent(parent, false);
        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;
    }

    public void LoadNextLevel()
    {
        GameManager.LoadNextLevel();
    }

    public void LoadMenu()
    {
        GameManager.LoadMenu();
    }
}
