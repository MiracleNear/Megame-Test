using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        Time.timeScale = IsPaused ? 0f : 1f;
    }
}
