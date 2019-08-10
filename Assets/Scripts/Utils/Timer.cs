using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float currentSeconds;

    [SerializeField]
    float maxSeconds;


    public event Action OnStart;
    public event Action OnStop;
    public event Action<float> OnTick;
    public event Action<bool> OnPause;

    public float Current => currentSeconds;
    public float Max => maxSeconds;

    public bool IsStart { get; private set; }
    public bool IsPause { get; private set; }


    void Update()
    {
        TickHandler();
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

    internal void Pause()
    {
        Pause(true);
    }

    internal void Pause(bool value)
    {
        if (IsPause == value)
            return;

        IsPause = value;
        OnPause?.Invoke(value);
    }

    internal void Countdown()
    {
        if (IsStart)
            return;

        IsStart = true;
        IsPause = false;

        OnStart?.Invoke();
    }

    internal void Stop()
    {
        if (!IsStart)
            return;

        IsStart = false;
        IsPause = false;

        OnStop?.Invoke();
    }

    internal void Reset()
    {
        IsStart = false;
        IsPause = false;
        currentSeconds = maxSeconds;
    }
}

