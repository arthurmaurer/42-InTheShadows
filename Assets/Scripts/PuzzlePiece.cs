using UnityEngine;
using System.Collections;

public class PuzzlePiece : MonoBehaviour
{
    public float validationTolerance;

    void Start()
    {
        Vector3 randomRotation = new Vector3(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            0f
        );
        transform.localRotation = Quaternion.Euler(randomRotation);
    }
}
