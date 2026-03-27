using UnityEngine;
using UnityEngine.Rendering.Universal; // Bắt buộc phải có để dùng URP 2D Light

public class LightingManager : MonoBehaviour
{
    [Header("Cài đặt Ánh sáng")]
    public Light2D globalLight;
    
    // Gradient cho phép bạn pha màu theo mốc thời gian (Ví dụ: 0.5 là giữa trưa, 0.8 là chiều tối)
    public Gradient lightColorGradient; 

    void Update()
    {
        if (TimeManager.Instance == null) return;

        // Lấy thời gian hiện tại (từ 0 đến 1440 phút)
        float currentTime = TimeManager.Instance.GetCurrentTime();
        
        // Tính phần trăm thời gian trong ngày (từ 0.0 đến 1.0)
        float timePercent = currentTime / 1440f;
        
        // Cập nhật màu của Global Light dựa trên bảng màu Gradient
        globalLight.color = lightColorGradient.Evaluate(timePercent);
    }
}