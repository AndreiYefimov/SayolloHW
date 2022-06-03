using System;
using System.Collections;
using UnityEngine;

namespace AndriiYefimov.SayolloHW.TimeManagement
{
    public class Timer
    {
        public event Action<TimerState> TimerStateChanged;
        
        public IEnumerator StartTimer(float seconds)
        {
            TimerStateChanged?.Invoke(TimerState.Started);
            yield return new WaitForSecondsRealtime(seconds);
            TimerStateChanged?.Invoke(TimerState.Finished);
        }
    }
}