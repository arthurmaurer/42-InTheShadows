using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour
{

    void Start()
    {
        SpawnPuzzlePieces();
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
}
