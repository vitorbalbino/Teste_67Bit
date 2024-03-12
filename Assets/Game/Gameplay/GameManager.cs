using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal void Pause(bool v)
    {
        Time.timeScale = v ? 0 : 1;
    }

    private void Awake()
    {
        SetSingleton(this);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}