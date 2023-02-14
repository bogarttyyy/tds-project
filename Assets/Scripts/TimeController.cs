using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    private DateTime currentTime;

    private TimeSpan dayTime;
    private TimeSpan afternoonTime;
    private TimeSpan nightTime;

    [SerializeField]
    private ETimeOfDay timeOfDay;

    public GameEvent onTimePhaseChanged;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        dayTime = new TimeSpan(5,0,0);
        afternoonTime = new TimeSpan(12,0,0);
        nightTime = new TimeSpan(18, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        SendTimeOfDaySignal();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null && (currentTime.Minute % 10 == 0))
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void SendTimeOfDaySignal()
    {
        if (currentTime.TimeOfDay >= nightTime)
        {
            TimePhaseChanged(ETimeOfDay.Evening);
        }
        else if (currentTime.TimeOfDay >= afternoonTime)
        {
            TimePhaseChanged(ETimeOfDay.Afternoon);
        }
        else if (currentTime.TimeOfDay >= dayTime)
        {
            TimePhaseChanged(ETimeOfDay.Morning);
        }
    }

    private void TimePhaseChanged(ETimeOfDay phase)
    {
        if (phase != timeOfDay)
        {
            timeOfDay = phase;
            onTimePhaseChanged.Raise(this, timeOfDay);
        }
    }

    public enum ETimeOfDay
    {
        Morning = 0,
        Afternoon,
        Evening,
    }
}
