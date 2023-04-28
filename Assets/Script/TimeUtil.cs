using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeUtil
{
    public static float UpdateTimer(float timer, float updateTime)
    {
        return timer + updateTime;
    }
    public static float UpdateTimer(float timer)
    {
        return timer + Time.deltaTime;
    }

    public static bool IsTimerDone(float timer, float checkTimer)
    {
        return timer >= checkTimer;
    }
}