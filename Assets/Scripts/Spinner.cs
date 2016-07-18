using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour
{
    public Vector3 speed;

    void Update()
    {
        transform.rotation *= Quaternion.Euler(speed * Time.deltaTime);
    }
}
