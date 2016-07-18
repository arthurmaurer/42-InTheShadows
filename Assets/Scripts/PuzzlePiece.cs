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

        ApplyConstrains(ref randomRotation);

        transform.localRotation = Quaternion.Euler(randomRotation);
    }

    void ApplyConstrains(ref Vector3 rotation)
    {
        Level level = GameManager.currentLevel;

        if (level.HasConstrain(Level.Constrain.NoXRotation))
            rotation.x = 0f;

        if (level.HasConstrain(Level.Constrain.NoYRotation))
            rotation.y = 0f;
    }
}
