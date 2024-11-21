using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationController : MonoBehaviour
{
    public Button button; // Tham chiếu đến Button UI
    public Animator bowlAnimator; // Tham chiếu đến Animator
    public string bowlAnimationStateName = "BowlAnimation"; // Tên của state trong Animator

    private bool isAnimationPlaying = false; // Trạng thái kiểm tra animation đang chạy

    void Start()
    {
        button.onClick.AddListener(OnButtonClick); // Gán sự kiện click cho button
    }

    void OnButtonClick()
    {
        if (!isAnimationPlaying) // Chỉ chạy nếu animation chưa chạy
        {
            isAnimationPlaying = true;
            button.interactable = false; // Khóa button

            // Kích hoạt logic random
            FindObjectOfType<OnClickEffect>().OnButtonClicked();

            // Phát trực tiếp animation bằng tên state
            bowlAnimator.Play(bowlAnimationStateName);

            // Tính toán thời gian animation dựa trên AnimationClip
            float animationLength = GetAnimationClipLength(bowlAnimationStateName);
            Invoke("OnAnimationComplete", animationLength); // Gọi khi animation kết thúc
        }
    }

    float GetAnimationClipLength(string animStateName)
    {
        // Lấy thông tin Animation Clip trong Animator Controller
        AnimationClip[] clips = bowlAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animStateName)
            {
                return clip.length;
            }
        }
        Debug.LogWarning($"Không tìm thấy Animation Clip: {animStateName}");
        return 0f;
    }

    void OnAnimationComplete()
    {
        isAnimationPlaying = false; // Reset trạng thái
        button.interactable = true; // Mở khóa button

        // Animation sẽ tự động trả về Idle nếu đã thiết lập transition trong Animator
    }
}
