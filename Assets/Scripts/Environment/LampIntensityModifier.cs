using UnityEngine;
using System.Collections;

public class LampIntensityModifier : MonoBehaviour
{
    float   _timer = 0f;
    float   _baseIntensity;
    Light   _light;
    bool    _isDisabled;

    void    Start()
    {
        _light = GetComponent<Light>();
        _baseIntensity = _light.intensity;
        _isDisabled = false;
    }

    void    Update()
    {
        _timer -= Time.deltaTime;

        if (_isDisabled)
        {
            if (_timer <= 0f)
                EnableLight();
        }
        else
        {
            if (Random.Range(0, 200) == 0)
            {
                _timer = 0.1f;
                DisableLight();
            }
        }
    }

    void    DisableLight()
    {
        _light.intensity = Random.Range(_baseIntensity / 1.2f, _baseIntensity);
        _isDisabled = true;
    }

    void    EnableLight()
    {
        _light.intensity = _baseIntensity;
        _isDisabled = false;
    }
}
