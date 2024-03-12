using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceChecker : MonoBehaviour
{
    /// <summary>
    /// Minimum distance (m) between this object and player to enable _isClose variable
    /// </summary>
    [SerializeField] float _distanceChecked = 1f;

    bool _isClose = false;

    void FixedUpdate()
    {
        _isClose = Vector3.Distance(this.transform.position, Player._Sgt.transform.position) < _distanceChecked;
    }

    public bool PlayerIsClose()
    {
        return _isClose;
    }
}
