using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryButton : MonoBehaviour
{
    public bool isActive;
    public float maxValue = 1f;
    public float minValue = 0f;

    Text _text;

    void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        Color color = _text.color;
        float delta = 0.05f;

        color.a += (isActive)
            ? delta
            : -delta;

        color.a = Mathf.Clamp(color.a, minValue, maxValue);

        _text.color = color;
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }
}
