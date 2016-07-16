using UnityEngine;
using System.Collections;

public class MovieScreen : MonoBehaviour
{
    void Start()
    {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
    }
}
