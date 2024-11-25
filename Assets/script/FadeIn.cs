using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public Image fadeImage; // Hình ảnh dùng để fade (ví dụ: hình màu đen)
    public float fadeDuration = 1.5f; // Thời gian để fade hoàn tất (giây)

    void Start()
    {
        if (fadeImage != null)
        {
            // Đảm bảo hình ảnh bắt đầu ở màu đen
            fadeImage.color = new Color(0, 0, 0, 1); // Đen, alpha = 1 (hoàn toàn mờ)
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.LogError("Chưa gắn hình ảnh để fade.");
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Tính alpha từ 1 -> 0
            fadeImage.color = new Color(0, 0, 0, alpha); // Cập nhật màu với alpha mới
            yield return null;
        }

        // Đảm bảo fadeImage hoàn toàn trong suốt
        fadeImage.color = new Color(0, 0, 0, 0);
    }
}
