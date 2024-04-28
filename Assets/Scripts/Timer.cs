using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime;
    private bool isTiming;
    bool paused = false;

    public void StartTimer()
    {
        isTiming = true;

    }

    public void StopTimer()
    {
        ChangeTimeScale(1);
        paused = false;
        isTiming = false;
    }

    public void PauseTimer(bool _paused)
    {
        paused = _paused;
    }

    public void ChangeTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
    }
    public float GetTime()
    {
        return currentTime;
    }
    
    void Update()
    {
        if(isTiming && !paused)
        {
            currentTime += Time.deltaTime;
        }
      

    }
}
