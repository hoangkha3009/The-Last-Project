using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator animator; // Gắn Animator vào đây
    private bool isPaused = false; // Trạng thái Animation (tạm dừng hay đang chạy)

    public void PauseAnimation()
    {
        if (animator != null && !isPaused)
        {
            // Tạm dừng Animation
            animator.speed = 0;
            isPaused = true;
            Debug.Log("Animation đã tạm dừng!");
        }
        else
        {
            Debug.LogWarning("Animation đã tạm dừng hoặc Animator không được gắn!");
        }
    }

    public void ResumeAnimation()
    {
        if (animator != null && isPaused)
        {
            // Tiếp tục chạy Animation
            animator.speed = 1;
            isPaused = false;
            Debug.Log("Animation đã tiếp tục!");
        }
        else
        {
            Debug.LogWarning("Animation đang chạy hoặc Animator không được gắn!");
        }
    }
}
