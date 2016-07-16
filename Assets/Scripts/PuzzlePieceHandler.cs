using UnityEngine;
using System.Collections;

public class PuzzlePieceHandler : MonoBehaviour
{
    public enum RotationType
    {
        None,
        Vertical,
        Horizontal,
        Both
    };

    public enum MovementType
    {
        None,
        Rotation,
        Translation
    };

    public float axisTolerance;

    RotationType rotationType;
    MovementType movementType;
    GameObject targetedPuzzlePiece;

    void FixedUpdate ()
    {
        Vector3 mouseDelta = GetMouseDelta();

        movementType = GetMovementType();

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            OnMovementBegin();

        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
            OnMovementEnd();

        if (targetedPuzzlePiece != null)
        {
            if (movementType == MovementType.Translation)
                TranslatePuzzlePiece(mouseDelta);
            else if (movementType == MovementType.Rotation)
            {
                if (rotationType == RotationType.None && mouseDelta != Vector3.zero)
                {
                    rotationType = GetRotationType(mouseDelta);
                }

                Vector3 rotationDelta = CorrectRotationDelta(mouseDelta);
                RotatePuzzlePiece(rotationDelta);
            }
        }

        if (targetedPuzzlePiece != null && IsPuzzlePieceTransformValid(targetedPuzzlePiece))
            GameManager.EndLevel();
    }

    bool IsPuzzlePieceTransformValid(GameObject puzzlePiece)
    {
        Vector3 rotation = puzzlePiece.transform.localRotation.eulerAngles;

        if (rotation.x > 180f)
            rotation.x -= 360f;

        if (rotation.y > 180f)
            rotation.y -= 360f;

        float validationTolerance = puzzlePiece.GetComponent<PuzzlePiece>().validationTolerance;

        bool rotationValidity = (rotation.x >= -validationTolerance && rotation.x <= validationTolerance
            && rotation.y >= -validationTolerance && rotation.y <= validationTolerance);

        return (rotationValidity);
    }

    void OnMovementBegin()
    {
        targetedPuzzlePiece = GetTargetedPuzzlePiece(Input.mousePosition);
    }

    void OnMovementEnd()
    {
        targetedPuzzlePiece = null;
        movementType = MovementType.None;
        rotationType = RotationType.None;
    }

    GameObject GetTargetedPuzzlePiece(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        int layerMask = 1 << 8;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return hit.collider.gameObject;

        return null;
    }

    Vector3 CorrectRotationDelta(Vector3 delta)
    {
        Vector3 finalDelta = delta;

        if (rotationType == RotationType.Horizontal)
            finalDelta.y = 0f;
        else if (rotationType == RotationType.Vertical)
            finalDelta.x = 0f;

        return finalDelta;
    }

    void    TranslatePuzzlePiece(Vector3 delta)
    {
        targetedPuzzlePiece.transform.localPosition += delta;
    }

    void    RotatePuzzlePiece(Vector3 delta)
    {
        delta = new Vector3(delta.y, -delta.x, delta.z);

        // Rotation on X is a bit unaligned because we don't look straight.
        targetedPuzzlePiece.transform.Rotate(delta);
    }

    Vector3 GetMouseDelta()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 delta = new Vector3(mouseX, mouseY, 0f);

        return delta;
    }

    MovementType    GetMovementType()
    {
        if (Input.GetButton("Fire1"))
            return MovementType.Rotation;
        else if (Input.GetButton("Fire2"))
            return MovementType.Translation;
        else
            return MovementType.None;
    }

    RotationType    GetRotationType(Vector3 delta)
    {
        if (delta.x == 0f && delta.y != 0f)
            return RotationType.Vertical;
        else if (delta.x != 0f && delta.y == 0f)
            return RotationType.Horizontal;

        Vector3 direction = delta.normalized;

        if (direction.x > -axisTolerance && direction.x < axisTolerance)
            return RotationType.Vertical;
        else if (direction.y > -axisTolerance && direction.y < axisTolerance)
            return RotationType.Horizontal;

        return RotationType.Both;
    }
}
