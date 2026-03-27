using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Chỉ số di chuyển")]
    public float moveSpeed = 5f;
    public bl_Joystick joystick; 

    [Header("Cài đặt Joystick 4 hướng")]
    public float deadZone = 0.3f; 

    [Header("Tương tác")]
    public float interactRange = 1.5f;
    public LayerMask interactableLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float rawX = joystick.Horizontal;
        float rawY = joystick.Vertical;

        moveInput = Vector2.zero;

        // Xử lý 4 hướng dứt khoát
        if (Mathf.Abs(rawX) > deadZone || Mathf.Abs(rawY) > deadZone)
        {
            if (Mathf.Abs(rawX) > Mathf.Abs(rawY))
            {
                moveInput.x = Mathf.Sign(rawX);
            }
            else
            {
                moveInput.y = Mathf.Sign(rawY);
            }
        }

        // Xoay mặt nhân vật
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }

    void FixedUpdate()
    {
        // Ép vận tốc về 0 ngay lập tức khi thả tay (Không trượt)
        rb.linearVelocity = moveInput * moveSpeed;
    }

 
    public void OnActionButtonPressed()
    {
        // Bắn một vòng tròn kiểm tra xem có vật thể nào tương tác được không
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, interactableLayer);

        if (hit != null)
        {
            // Nếu chạm vào Ruộng Muối
            if (hit.CompareTag("SaltField"))
            {
                // Lấy script SaltField từ đối tượng vừa chạm vào
                SaltField field = hit.GetComponent<SaltField>();
                if (field != null)
                {
                    // Gọi hàm Tương tác của ruộng muối
                    field.OnInteract();
                }
            }
            // Nếu chạm vào NPC
            else if (hit.CompareTag("NPC"))
            {
                Debug.Log("Bắt đầu trò chuyện với NPC!");
                // Hệ thống Hội thoại sẽ nằm ở đây
            }
        }
    }

    // Vẽ vòng tròn tầm nhìn màu vàng trong Scene View để bạn dễ canh chỉnh kích thước
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}