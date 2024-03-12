using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerPos : MonoBehaviour
{
    [SerializeField] Transform _playerT;

    [SerializeField] float _followDistance = 0;
    [SerializeField] float _followVelocity = 0;



    void Start()
    {
        _playerT = Player._Sgt.transform;
    }



    void Update()
    {
        Vector3 dir = _playerT.position - this.transform.position;

        if (dir.magnitude > _followDistance)
        {
            this.transform.position += (_playerT.position - this.transform.position).normalized * _followVelocity * Time.deltaTime * dir.magnitude;
        }
    }
}
