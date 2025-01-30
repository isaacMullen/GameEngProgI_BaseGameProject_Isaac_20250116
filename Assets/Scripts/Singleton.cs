using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    static Singleton instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log($"Destroyed: {gameObject.name}");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
