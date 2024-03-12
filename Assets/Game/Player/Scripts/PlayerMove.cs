using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] CharacterController _charController;
    [SerializeField] Animator _animator;

    [SerializeField] float _minMove = 0.2f;
    [SerializeField] float _velocity = 2f;
    [SerializeField] float _angleSpeed;
    [SerializeField] float _angleTime = 0.1f;

    Vector3 frontMove, sideMove;

    Camera _mainCam;


    Vector2 _physicalImput;
    Vector2 movementVector;


    private void Start()
    {
        _mainCam = Camera.main;
        frontMove = Vector3.ProjectOnPlane(_mainCam.transform.forward, Vector3.up).normalized;
        sideMove = Vector3.ProjectOnPlane(_mainCam.transform.right, Vector3.up).normalized;
    }



    void OnMove(InputValue movementValue)
    {
        _physicalImput = movementValue.Get<Vector2>();
    }



    void UpdateMovementVariables()
    {
        movementVector = (_physicalImput.magnitude > UIJoystick._Sgt.GetPointedVector().magnitude) ? _physicalImput : UIJoystick._Sgt.GetPointedVector();
    }



    void FixedUpdate()
    {
        UpdateMovementVariables();

        Vector3 finalMove = frontMove * movementVector.y + sideMove * movementVector.x;
        float selectedVelocity = finalMove.magnitude < 0.8f ? finalMove.magnitude : 1;

        if (selectedVelocity > _minMove)
        {
            Vector3 movement = finalMove.normalized * selectedVelocity * _velocity * Time.deltaTime;

            _charController.Move(movement);

            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg + _mainCam.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _angleSpeed, _angleTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        _charController.Move(Vector3.down * 0.1f);

        _animator.SetFloat("Speed", selectedVelocity);
    }



    internal void PunchNPC(CharNpc npc)
    {
        _animator.SetTrigger("Punch");

        Vector3 LookDirection = npc.transform.position - this.transform.position;

        transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
    }
}
