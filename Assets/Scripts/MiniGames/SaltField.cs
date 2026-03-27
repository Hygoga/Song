using UnityEngine;

public class SaltField : MonoBehaviour
{
    // 3 Trạng thái của ruộng muối
    public enum SaltState { Empty, Evaporating, ReadyToHarvest }
    public SaltState currentState = SaltState.Empty;

    [Header("Cài đặt")]
    public float timeToEvaporate = 10f; // Để 10 giây test cho nhanh
    private float timer = 0f;

    [Header("Hiển thị (Tạm thời dùng màu sắc để test)")]
    public SpriteRenderer spriteRenderer;
    public Color emptyColor = new Color(0.6f, 0.4f, 0.2f); // Màu đất nâu
    public Color evaporatingColor = new Color(0.4f, 0.8f, 1f); // Màu nước biển xanh
    public Color readyColor = Color.white; // Màu muối trắng tinh

    void Start()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisuals();
    }

    void Update()
    {
        // Nếu ruộng đang chứa nước, đếm ngược thời gian bốc hơi
        if (currentState == SaltState.Evaporating)
        {
            timer += Time.deltaTime;
            if (timer >= timeToEvaporate)
            {
                currentState = SaltState.ReadyToHarvest;
                UpdateVisuals();
                Debug.Log("Muối đã kết tinh! Hãy thu hoạch.");
            }
        }
    }

    // Hàm này sẽ được Player gọi khi bấm nút Hành động
    public void OnInteract()
    {
        switch (currentState)
        {
            case SaltState.Empty:
                Debug.Log("Đã dẫn nước biển vào ruộng!");
                currentState = SaltState.Evaporating;
                timer = 0f;
                UpdateVisuals();
                break;

            case SaltState.Evaporating:
                Debug.Log("Muối vẫn đang kết tinh, vui lòng chờ thêm...");
                break;

            case SaltState.ReadyToHarvest:
                Debug.Log("Thu hoạch thành công! Bạn nhận được +1 Muối Tuyết.");
                // Tương lai: Gọi InventoryManager.Instance.AddItem() ở đây
                
                currentState = SaltState.Empty;
                UpdateVisuals();
                break;
        }
    }

    // Thay đổi màu sắc để dễ quan sát trạng thái
    void UpdateVisuals()
    {
        if (currentState == SaltState.Empty) spriteRenderer.color = emptyColor;
        else if (currentState == SaltState.Evaporating) spriteRenderer.color = evaporatingColor;
        else if (currentState == SaltState.ReadyToHarvest) spriteRenderer.color = readyColor;
    }
}