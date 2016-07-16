using UnityEngine;
using System.Collections;

public class ProjectorLight : MonoBehaviour
{
    public enum Type
    {
        Light,
        Texture
    }

    public enum State
    {
        On,
        Off
    }

    public Type type;
    public float frequency;

    float _timer;
    State _state = State.On;
    Color _normalColor;
    Material _material;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _normalColor = _material.color;

        SwitchOn();
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            switch (_state)
            {
                case State.On:
                    SwitchOff();
                    break;
                case State.Off:
                    SwitchOn();
                    break;
            }
        }
    }

    void SwitchOn()
    {
        _timer = 1f / frequency;
        _state = State.On;

        _material.color = _normalColor;
    }

    void SwitchOff()
    {
        _timer = 0.1f;
        _state = State.Off;

        _material.color = _material.color - new Color(0, 0, 0, 0.1f);
    }
}
