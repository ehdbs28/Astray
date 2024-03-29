using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    public static TimeManager Instance;

    public float TimeScale{
        get{
            return Time.timeScale;
        }
        set{
            Time.timeScale = value;
        }
    }

    public void TimeLag(float dest, float duration, Action Callback = null){
        GameManager.Instance.RunCoroutine(TimeLagCoroutine(dest, duration, Callback));
    }

    private IEnumerator TimeLagCoroutine(float dest, float duration, Action Callback = null){
        float last = TimeScale;
        TimeScale = dest;
        yield return new WaitForSecondsRealtime(duration);
        TimeScale = last;
        Callback?.Invoke();
    }
}
