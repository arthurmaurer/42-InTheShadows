using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving
    };
    
    public Vector3      itemOffset;
    public float        movementDuration;

    uint                _currentLevelItemID = 0;
    State               _state = State.Idle;
    Vector3             _moveFrom;
    Vector3             _moveTo;
    float               _timer = 0f;

    void        Start()
    {
        _moveTo = transform.localPosition;
        SpawnLevelItems();
        ShowCurrentLevelItem();
    }

    void        Update()
    {
        if (Input.GetButtonDown("Submit"))
            GameManager.LoadLevel(GameManager.levels[_currentLevelItemID]);
        else if (Input.GetKeyDown("d") && CanMoveRight())
            StartMovement(true);
        else if (Input.GetKeyDown("q") && CanMoveLeft())
            StartMovement(false);

        if (_state == State.Moving)
            Move();
    }

    void        StartMovement(bool moveRight)
    {
        _state = State.Moving;
        _timer = 0f;
        _moveFrom = transform.localPosition;

        if (moveRight)
        {
            HideCurrentLevelItem();
            ++_currentLevelItemID;
            ShowCurrentLevelItem();
            _moveTo -= itemOffset;
        }
        else
        {
            HideCurrentLevelItem();
            --_currentLevelItemID;
            ShowCurrentLevelItem();
            _moveTo += itemOffset;
        }
    }

    void ShowCurrentLevelItem()
    {
        GameObject entity = GetCurrentLevelItemGameObject();
        Animator animator = entity.GetComponent<Animator>();

        animator.SetBool("Selected", true);
    }

    void HideCurrentLevelItem()
    {
        GameObject entity = GetCurrentLevelItemGameObject();
        Animator animator = entity.GetComponent<Animator>();

        animator.SetBool("Selected", false);
    }

    GameObject  GetCurrentLevelItemGameObject()
    {
        return transform.GetChild((int)_currentLevelItemID).gameObject;
    }

    bool        CanMoveRight()
    {
        return (_currentLevelItemID < GameManager.levels.Length - 1);
    }

    bool        CanMoveLeft()
    {
        return (_currentLevelItemID > 0);
    }

    void        Move()
    {
        _timer += Time.deltaTime;

        if (_timer >= movementDuration)
            _state = State.Idle;
        else
        {
            float   percentage = Mathf.SmoothStep(0f, 1f, _timer / movementDuration);
            transform.localPosition = Vector3.Lerp(_moveFrom, _moveTo, percentage);
        }
    }

    void        SpawnLevelItems()
    {
        uint    i = 0;

        foreach (Level level in GameManager.levels)
        {
            GameObject  levelItem = SpawnLevelItem(level.puzzlePiece);
            ConfigureLevelItem(levelItem, i);
            ++i;
        }
    }

    GameObject  SpawnLevelItem(GameObject prefab)
    {
        GameObject  levelItem = (GameObject)Instantiate(prefab);
        levelItem.transform.SetParent(transform, false);
        levelItem.transform.localPosition = Vector3.zero;

        return levelItem;
    }

    void ConfigureLevelItem(GameObject levelItem, uint id)
    {
        levelItem.transform.localPosition += itemOffset * id;
        levelItem.GetComponentInChildren<TextMesh>().text = GameManager.levels[id].name;
        levelItem.GetComponent<Animator>().speed = 1f / movementDuration;
    }
}
