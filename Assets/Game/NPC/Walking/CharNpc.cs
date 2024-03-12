using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharNpc : MonoBehaviour
{
    [SerializeField] GameObject _collectablePrefab;
    [SerializeField] Animator _animator;

    [SerializeField] float _WalkVelocity = 1f;
    [SerializeField] Vector3 _endPoint = Vector3.zero;

    [SerializeField] float _CollectableThrowForce = 0.1f;

    [SerializeField] float _DistanceForColisionWithPlayer = 1.5f;



    void EndNPC()
    {
        this.gameObject.SetActive(false);

        Destroy(this.gameObject);
    }



    private void MoveTillEnd()
    {
        Vector3 direction = _endPoint - this.transform.position;
        Vector3 step = (direction).normalized * _WalkVelocity * Time.fixedDeltaTime;

        if (direction.magnitude < step.magnitude)
        {
            EndNPC();
        }

        this.transform.position += (_endPoint - this.transform.position).normalized * _WalkVelocity * Time.fixedDeltaTime;
    }



    private void CheckPlayerCollision()
    {
        if (Vector3.Distance(Player._Sgt.transform.position, this.transform.position) < _DistanceForColisionWithPlayer)
        {
            Player._Sgt.CollisionWithNPC(this);
        }
    }



    private void Start()
    {
        this.transform.LookAt(this._endPoint);
    }



    private void FixedUpdate()
    {
        MoveTillEnd();

        CheckPlayerCollision();

    }


    public void SetEndPoint(Vector3 point)
    {
        _endPoint = point;
    }



    public void OnKill()
    {
        GameObject collectableInstance = GameObject.Instantiate(_collectablePrefab, this.transform.position + Vector3.up, this.transform.rotation, null);

        if (collectableInstance.TryGetComponent(out Rigidbody rigd))
        {
            Vector3 OpositeDirection = (this.transform.position - Player._Sgt.transform.position).normalized;

            rigd.AddForce((OpositeDirection + Vector3.up) * _CollectableThrowForce);
        }

        EndNPC();
    }
}
