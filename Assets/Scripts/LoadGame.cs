using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour
{
    void Start()
    {
        GameManager.Configure();
        GameManager.LoadMenu();
    }
}
