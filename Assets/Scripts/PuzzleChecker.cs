
using UnityEngine;

public class PuzzleChecker
{
    public static bool Check42(GameObject[] pieces)
    {
        if (!CheckDefault(pieces))
            return false;

        Transform transformA = pieces[0].transform;
        Transform transformB = pieces[1].transform;
        Vector3 delta = transformA.position - transformB.position;

        if (delta.z > -1f)
            return false;

        return true;
    }

    public static bool CheckDefault(GameObject[] pieces)
    {
        foreach (GameObject piece in pieces)
        {
            bool isValid = IsPuzzlePieceTransformValid(piece);

            if (!isValid)
                return false;
        }

        return true;
    }

    static bool IsPuzzlePieceTransformValid(GameObject puzzlePiece)
    {
        Vector3 rotation = puzzlePiece.transform.localRotation.eulerAngles;

        if (rotation.x > 180f)
            rotation.x -= 360f;

        if (rotation.y > 180f)
            rotation.y -= 360f;

        float tolerance = puzzlePiece.GetComponent<PuzzlePiece>().validationTolerance;

        bool xValid = (rotation.x >= -tolerance && rotation.x <= tolerance) ||
            (rotation.x - 180f >= -tolerance && rotation.x - 180f <= tolerance);

        bool yValid = (rotation.y >= -tolerance && rotation.y <= tolerance) ||
            (rotation.y - 180f >= -tolerance && rotation.y - 180f <= tolerance);

        return (xValid && yValid);
    }
}
