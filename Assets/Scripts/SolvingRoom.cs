using UnityEngine;
using System.Collections;

public class SolvingRoom : MonoBehaviour
{

    void Start()
    {
        SpawnPuzzlePieces();
    }

    void SpawnPuzzlePieces()
    {
        GameObject positionMarker = GameObject.FindWithTag("PuzzlePiecePosition");

        foreach (GameObject puzzlePiece in GameManager.currentLevel.puzzlePieces)
            SpawnPuzzlePiece(puzzlePiece, positionMarker.transform.position);
    }

    void SpawnPuzzlePiece(GameObject puzzlePiece, Vector3 position)
    {
        GameObject instance = (GameObject)Instantiate(puzzlePiece);

        instance.transform.position = position;
    }
}
