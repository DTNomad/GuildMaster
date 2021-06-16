using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController TC;

    [SerializeField]
    public TextMeshProUGUI timeCounter;
    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        if (TimeController.TC != null && TimeController.TC != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            TimeController.TC = this;
        }
        DontDestroyOnLoad(this.gameObject);

        //timeCounter.text = "00:00.00";
        timerGoing = false;
    }

    public DateTime GetDateTimeNow()
    {
        return DateTime.Now;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void PauseTimer()
    {
        timerGoing = false;
    }

    public void SwitchTimer()
    {

        if (timerGoing)
        {
            timerGoing = false;
        }
        else
        {
            timerGoing = true;
            StartCoroutine(UpdateTimer());
        }
        Debug.Log("SWITCH");
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    public float GetTimer()
    {
        return (float)timePlaying.TotalSeconds;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
