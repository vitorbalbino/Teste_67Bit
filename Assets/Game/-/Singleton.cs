using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    static public T _Sgt;

    protected void SetSingleton(T newSingleton)
    {
        if (_Sgt != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _Sgt = newSingleton;
    }
}
