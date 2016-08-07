using UnityEngine;
using System.Collections;

public class SolvingRoom : MonoBehaviour
{
    public static SolvingRoom instance;
    public GameObject[] pieces;

    void Start()
    {
        instance = this;
        SpawnPuzzlePieces();
    }

    void SpawnPuzzlePieces()
    {
        GameObject positionMarker = GameObject.FindWithTag("PuzzlePiecePosition");

        pieces = new GameObject[GameManager.currentLevel.puzzlePieces.Length];
        int i = 0;

        foreach (GameObject puzzlePiece in GameManager.currentLevel.puzzlePieces)
        {
            GameObject piece = SpawnPuzzlePiece(puzzlePiece, positionMarker.transform.position);
            pieces[i] = piece;
            ++i;
        }
    }

    GameObject SpawnPuzzlePiece(GameObject puzzlePiece, Vector3 position)
    {
        GameObject instance = (GameObject)Instantiate(puzzlePiece);

        instance.transform.position = position;

        return instance;
    }
}
