using UnityEngine;
using System;
using System.Collections;

public class DestroyOnSoundEnd : MonoBehaviour
{
    public Action callback;

    void Update()
    {
        if (callback != null && !GetComponent<AudioSource>().isPlaying)
        {
            callback();
            Destroy(gameObject);
        }
    }
}
