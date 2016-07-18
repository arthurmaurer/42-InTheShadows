using UnityEngine;
using System;
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
    public Vector3 translationModifier;
    public Vector3 rotationModifier;

    public static PuzzlePieceHandler instance;

    RotationType rotationType;
    MovementType movementType;
    GameObject targetedPuzzlePiece;
    bool levelCompleted = false;

    void Start()
    {
        instance = this;
    }

    void FixedUpdate ()
    {
        if (levelCompleted)
            return;

        Vector3 mouseDelta = GetMouseDelta() * Time.fixedDeltaTime;

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
                    rotationType = GetRotationType(mouseDelta);

                Vector3 rotationDelta = CorrectRotationDelta(mouseDelta);
                RotatePuzzlePiece(rotationDelta);
            }
        }

        if (targetedPuzzlePiece != null && IsPuzzlePieceTransformValid(targetedPuzzlePiece))
        {
            levelCompleted = true;
            GameManager.EndLevel();
        }
    }

    bool IsPuzzlePieceTransformValid(GameObject puzzlePiece)
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
        delta.Scale(translationModifier * 2.5f);

        ApplyTranslationConstrains(ref delta);

        targetedPuzzlePiece.transform.localPosition += delta;
    }

    void    ApplyTranslationConstrains(ref Vector3 delta)
    {
        Level level = GameManager.currentLevel;

        if (level.HasConstrain(Level.Constrain.NoXTranslation))
            delta.x = 0;

        if (level.HasConstrain(Level.Constrain.NoYTranslation))
            delta.y = 0;
    }

    void    RotatePuzzlePiece(Vector3 delta)
    {
        delta = new Vector3(delta.y, -delta.x, delta.z);
        delta.Scale(rotationModifier * 50);

        ApplyRotationConstrains(ref delta);

        // Rotation on X is a bit unaligned because we don't look straight.
        targetedPuzzlePiece.transform.Rotate(delta);
    }

    void    ApplyRotationConstrains(ref Vector3 delta)
    {
        Level level = GameManager.currentLevel;

        if (level.HasConstrain(Level.Constrain.NoXRotation))
            delta.x = 0;

        if (level.HasConstrain(Level.Constrain.NoYRotation))
            delta.y = 0;
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
