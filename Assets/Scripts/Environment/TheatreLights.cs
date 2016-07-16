using UnityEngine;
using System.Collections;

public class TheatreLights : MonoBehaviour
{
    public GameObject[] lights;

    float _timer = 0f;
    float[] _baseIntensities;
    Light[] _lights;
    bool _isDisabled;

    void Start()
    {
        _lights = new Light[lights.Length];
        _baseIntensities = new float[lights.Length];

        for (int i = 0; i < lights.Length; ++i)
        {
            Light light = lights[i].GetComponent<Light>();

            _lights[i] = light;
            _baseIntensities[i] = light.intensity;
        }

        _isDisabled = false;
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_isDisabled)
        {
            if (_timer <= 0f)
                EnableLights();
        }
        else
        {
            if (Random.Range(0, 50) == 0)
            {
                _timer = 0.1f;
                DisableLights();
            }
        }
    }

    void DisableLights()
    {
        float factor = Random.Range(1.1f, 1.2f);

        for (int i = 0; i < lights.Length; ++i)
            _lights[i].intensity = _baseIntensities[i] / factor;

        _isDisabled = true;
    }

    void EnableLights()
    {
        for (int i = 0; i < lights.Length; ++i)
            _lights[i].intensity = _baseIntensities[i];

        _isDisabled = false;
    }
}
