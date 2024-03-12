using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreArea : MonoBehaviour
{
    [SerializeField] FillArea _fillArea;
    [SerializeField] float _delay = 3;

    void Start()
    {
        _fillArea._Filled += OnFill;
    }


    void OnFill()
    {
        StorePanel._Sgt.OnOpenPanel();
        _fillArea.SetDelayNoFill(_delay);
    }
}
