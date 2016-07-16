using UnityEngine;
using System.Collections;

public class Wiggle : MonoBehaviour
{
    public float    unitsPerSecond;
    public float    radius;

    Vector3 _targetPosition;
    Vector3 _basePosition;
    Vector3 _previousPosition;

    float   _timer;
    float   _timerMax;

    void Start()
    {
        _basePosition = transform.localPosition;
        _targetPosition = GetRandomPointOnCircle();
        transform.localPosition = _targetPosition;
        ChangeDirection();
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
            ChangeDirection();
        else
        {
            float   progress = 1f - _timer / _timerMax;
            float   smoothedProgress = Mathf.SmoothStep(0f, 1f, progress);
            transform.localPosition = Vector3.Lerp(_previousPosition, _targetPosition, smoothedProgress);
        }
    }

    void        ChangeDirection()
    {
        _previousPosition = _targetPosition;
        _targetPosition = GetNextPosition();

        UpdateTimer();
    }

    void        UpdateTimer()
    {
        float   distance = Vector3.Distance(transform.localPosition, _targetPosition);
        _timerMax = distance / unitsPerSecond;
        _timer = _timerMax;
    }

    Vector3     GetNextPosition()
    {
        return GetRandomPointOnCircle();
    }

    Vector3 GetRandomPointOnCircle()
    {
        float   angle = Random.Range(0, Mathf.PI * 2);
        Vector3 position = Vector3.zero;

        position.x = _basePosition.x + radius * Mathf.Cos(angle);
        position.y = transform.localPosition.y;
        position.z = _basePosition.z + radius * Mathf.Sin(angle);

        return position;
    }
}
