using UnityEngine;
using UnityEngine.UI;

public class AnimationControl : MonoBehaviour
{
    public Animator animator; // Animator của "Cái Nắp"
    public Button openButton; // Nút "Mở"
    public Button shakeButton; // Nút "Xóc"
	public DiceController diceController;

    private bool isAnimating = false; // Kiểm tra Animation đang chạy
    private bool isPaused = false; // Kiểm tra trạng thái tạm dừng

    void Start()
    {
        // Thiết lập trạng thái ban đầu
        openButton.gameObject.SetActive(true);
        shakeButton.gameObject.SetActive(false);

        // Gán sự kiện cho nút "Mở"
        openButton.onClick.AddListener(StartAnimation);

        // Gán sự kiện cho nút "Xóc"
        shakeButton.onClick.AddListener(ResumeAnimation);

        if (animator != null)
        {
            animator.ResetTrigger("PlayAnimation");
            animator.ResetTrigger("ResetAnimation");
        }
    }

    // Bắt đầu Animation khi nhấn "Mở"
    public void StartAnimation()
    {
        if (animator != null && !isAnimating)
        {
            isAnimating = true; // Đánh dấu Animation đang chạy
            isPaused = false;

            animator.SetTrigger("PlayAnimation"); // Kích hoạt Animation
            openButton.interactable = false; // Vô hiệu hóa nút "Mở" trong khi Animation đang chạy

            Debug.Log("Animation bắt đầu!");
        }
        else
        {
            Debug.LogWarning("Animator không được gắn hoặc Animation đã chạy!");
        }
    }

    // Tạm dừng Animation tại keyframe (kích hoạt bởi Animation Event)
    public void PauseAnimation()
    {
        if (animator != null && isAnimating && !isPaused)
        {
            animator.speed = 0; // Tạm dừng Animation
            isPaused = true;

            // Chuyển đổi nút: Ẩn "Mở", hiển thị "Xóc"
            openButton.gameObject.SetActive(false);
            shakeButton.gameObject.SetActive(true);

            Debug.Log("Animation đã tạm dừng!");
        }
        else
        {
            Debug.LogWarning("Animation đã tạm dừng hoặc Animator không được gắn!");
        }
    }

    // Tiếp tục Animation khi nhấn "Xóc"
    public void ResumeAnimation()
{
    if (animator != null && isAnimating && isPaused)
    {
        animator.speed = 1; // Tiếp tục Animation
        isPaused = false;


        // Kích hoạt Trigger để reset về trạng thái ban đầu
        animator.SetTrigger("ResetAnimation");

        // Chuyển trạng thái nút sau khi Animation hoàn tất
        Invoke(nameof(ResetButtons), 0.1f);

        Debug.Log("Animation đã tiếp tục và reset về trạng thái ban đầu!");
    }
    else
    {
        Debug.LogWarning("Animation đang chạy hoặc Animator không được gắn!");
    }
}

    // Reset trạng thái nút sau khi Animation hoàn tất
    private void ResetButtons()
    {
        isAnimating = false; // Đánh dấu Animation đã kết thúc
        shakeButton.gameObject.SetActive(false);
        openButton.gameObject.SetActive(true);
        openButton.interactable = true; // Bật lại nút "Mở"

        Debug.Log("Animation đã reset về trạng thái ban đầu!");
    }
}
