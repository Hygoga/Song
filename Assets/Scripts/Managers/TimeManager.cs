using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public float timeMultiplier = 60f; // 1 giây thực = 1 phút game
    private float currentTime; 

    public static event Action OnHourPassed; // Sự kiện mỗi khi qua 1 giờ
    public static event Action OnNewDay;    // Sự kiện ngày mới

    void Awake() { Instance = this; }

    void Update()
    {
        currentTime += Time.deltaTime * timeMultiplier;
        if (currentTime >= 1440) // 1440 phút = 24 giờ
        {
            currentTime = 0;
            OnNewDay?.Invoke();
        }
        
        // Cứ mỗi 60 phút game thì gửi thông báo một lần
        if (Mathf.FloorToInt(currentTime) % 60 == 0) 
        {
            OnHourPassed?.Invoke();
        }
    }

    public float GetCurrentTime() => currentTime;
}