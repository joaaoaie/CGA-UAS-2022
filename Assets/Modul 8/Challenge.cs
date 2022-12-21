using UnityEngine;
using System;

public class Challenge : MonoBehaviour {
    public Transform hoursTransform, minutesTransform, secondsTransform;
    
    [Space]
    public bool continuous = false;

    [Space]
    public bool stopWatch = false;
    public bool stopWatchPlay = false;
    public bool stopWatchReset = false;

    const float degreesPerHour = 30f, degreesPerMinute = 6f, degreesPerSecond = 6f;
    
    float stopWatchHour, stopWatchMinute, stopWatchSecond, stopWatchTime;

    void Awake() {
        hoursTransform = transform.Find("Hours Arm");
        minutesTransform = transform.Find("Minutes Arm");
        secondsTransform = transform.Find("Seconds Arm");

        if (stopWatch) stopWatchReset = true;
    }
    void Update() {
        if (stopWatch) StopWatchMode();
        else ClockMode();
        
        if (stopWatchPlay) {
            stopWatchTime += Time.deltaTime;
            stopWatchHour = (stopWatchTime / 3600f) % 12f;
            stopWatchMinute = (stopWatchTime / 60f) % 60f;
            stopWatchSecond = stopWatchTime % 60f;
            if (!continuous) {
                stopWatchHour = (int) stopWatchHour;
                stopWatchMinute = (int) stopWatchMinute;
                stopWatchSecond = (int) stopWatchSecond;
            }
        }
    }
    
    void StopWatchMode() {
        if (stopWatchReset) {
            stopWatchPlay = false;
            stopWatchReset = false;
            stopWatchTime = 0;
            stopWatchHour = 0;
            stopWatchMinute = 0;
            stopWatchSecond = 0;
        }
        // if (stopWatchPlay) {
        //     stopWatchTime += Time.deltaTime;
        //     stopWatchHour = (stopWatchTime / 3600f) % 12f;
        //     stopWatchMinute = (stopWatchTime / 60f) % 60f;
        //     stopWatchSecond = stopWatchTime % 60f;
        //     if (!continuous) {
        //         stopWatchHour = (int) stopWatchHour;
        //         stopWatchMinute = (int) stopWatchMinute;
        //         stopWatchSecond = (int) stopWatchSecond;
        //     }
        // }
        moveArm(stopWatchHour, stopWatchMinute, stopWatchSecond);
    }

    void ClockMode() {
        float clockHour, clockMinute, clockSecond;
        TimeSpan time = DateTime.Now.TimeOfDay;
        clockHour = (float) time.TotalHours;
        clockMinute = (float) time.TotalMinutes;
        clockSecond = (float) time.TotalSeconds;
        if (!continuous) {
            clockHour = (int) clockHour;
            clockMinute = (int) clockMinute;
            clockSecond = (int) clockSecond;
        }
        moveArm(clockHour, clockMinute, clockSecond);
    }

    void moveArm(float hour, float minute, float second) {
        hoursTransform.localRotation = Quaternion.Euler(0f, hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, second * degreesPerSecond, 0f);
    }
}
