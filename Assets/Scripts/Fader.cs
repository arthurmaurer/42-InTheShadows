using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Fader : MonoBehaviour
{
    public enum State
    {
        In,
        Out
    };

    public float duration;

    State _state;
    float _timer;
    Image _image;
    Action _callback;

    void Start()
    {
        _image = GetComponent<Image>();
        FadeOut();
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            if (_callback != null)
            {
                _callback();
                _callback = null;
            }
            return;
        }

        Color color = _image.color;

        if (_state == State.In)
            color.a = Mathf.Clamp01(1f - _timer / duration);
        else if (_state == State.Out)
            color.a = Mathf.Clamp01(_timer / duration);

        _image.color = color;
    }

    public void FadeIn(Action callback = null)
    {
        _timer = duration;
        _state = State.In;

        if (callback != null)
            _callback = callback;
    }

    public void FadeOut(Action callback = null)
    {
        _timer = duration;
        _state = State.Out;

        if (callback != null)
            _callback = callback;
    }
}
