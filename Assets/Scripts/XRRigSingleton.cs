using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRRigSingleton : MonoBehaviour
{
    private static XRRigSingleton _instance;

    public static XRRigSingleton Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
