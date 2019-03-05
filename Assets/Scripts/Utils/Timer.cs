using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float currentSeconds;

    [SerializeField]
    float maxSeconds;


    public delegate void _Func();
    public delegate void _FuncValueFloat(float value);
    public delegate void _FuncValueBool(bool value);

    public event _Func OnStart;
    public event _Func OnStop;
    public event _FuncValueFloat OnTick;
    public event _FuncValueBool OnPause;

    public float Current => currentSeconds;
    public float Max => maxSeconds;

    public bool IsStart { get; private set; }
    public bool IsPause { get; private set; }


    void Update()
    {
        TickHandler();
        Debug.Log(currentSeconds);
    }

    void OnDestroy()
    {
        OnStart = null;
        OnStop = null;
        OnTick = null;
    }

    void TickHandler()
    {
        if (!IsStart || IsPause)
            return;

        currentSeconds -= 1.0f * Time.deltaTime;
        OnTick?.Invoke(currentSeconds);

        if (currentSeconds <= 0.0f) {
            currentSeconds = 0.0f;
            Stop();
        }
    }

    public void Pause()
    {
        Pause(true);
    }

    public void Pause(bool value)
    {
        if (IsPause == value)
            return;

        IsPause = value;
        OnPause?.Invoke(value);
    }

    public void Countdown()
    {
        if (IsStart)
            return;

        IsStart = true;
        IsPause = false;

        OnStart?.Invoke();
    }

    public void Stop()
    {
        if (!IsStart)
            return;

        IsStart = false;
        IsPause = false;

        OnStop?.Invoke();
    }

    public void Reset()
    {
        IsStart = false;
        IsPause = false;
        currentSeconds = maxSeconds;
    }
}

