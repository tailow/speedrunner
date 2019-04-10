using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using TMPro;

public class GameManagement : MonoBehaviour
{
    Stopwatch stopwatch;

    public TMP_Text stopwatchText;

    void Start()
    {
        stopwatch = new Stopwatch();

        stopwatch.Start();
    }

    
    void Update()
    {
        string timePassed = string.Format("{0:D2}:{1:D2}.{2:D3}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);

        stopwatchText.text = timePassed;
    }
}
