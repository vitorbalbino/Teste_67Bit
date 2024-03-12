using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIJoystick : Singleton<UIJoystick>
{
    [SerializeField] GameObject _dragIconImg, _backgroundImg;

    Vector2 _originalPos;
    Vector2 _currentPos;
    Vector2 _pointedVector;
    
    float _radius;



    Vector3 ClickPos()
    {
        return Input.mousePosition;
    }



    void SetImagesActiveState(bool state)
    {
        _dragIconImg.SetActive(state);
        _backgroundImg.SetActive(state);
    }



    private void Awake()
    {
        SetSingleton(this);
    }



    void Start()
    {
        _originalPos = _backgroundImg.transform.position;
        _radius = _backgroundImg.GetComponent<RectTransform>().sizeDelta.y / 4;
        SetImagesActiveState(false);
    }



    public void OnPointerDown()
    {
        _dragIconImg.transform.position = _backgroundImg.transform.position = _currentPos = ClickPos();

        SetImagesActiveState(true);
    }



    public void OnPointerUp()
    {
        _pointedVector = Vector2.zero;
        _dragIconImg.transform.position = _originalPos;
        _backgroundImg.transform.position = _originalPos;

        SetImagesActiveState(false);
    }



    public void OnDrag(BaseEventData baseEventData)
    {
        SetImagesActiveState(true);

        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        _pointedVector = (dragPos - _currentPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, _currentPos);

        if (joystickDist < _radius)
        {
            _dragIconImg.transform.position = _currentPos + _pointedVector * joystickDist;
        }

        else
        {
            _dragIconImg.transform.position = _currentPos + _pointedVector * _radius;
        }
    }



    public Vector2 GetPointedVector()
    {
        return _pointedVector;
    }
}