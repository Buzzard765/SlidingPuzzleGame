using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton ThisObject;

    private void Awake()
    {
        if (ThisObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ThisObject = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
