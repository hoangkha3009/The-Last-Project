using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonWithEffects : MonoBehaviour
{
    public float pressEffectDuration = 0.1f; // Thời gian hiệu ứng nhấp nhả
    public Image fadeImage; // UI Image dùng để làm hiệu ứng đen
    public float fadeDuration = 0.5f; // Thời gian để màn hình tối dần/sáng dần

    public void OnButtonClicked() // Gọi khi nhấn nút
    {
        StartCoroutine(ButtonPressEffect());
    }

    IEnumerator ButtonPressEffect()
    {
        // Hiệu ứng thu nhỏ nút (giảm xuống 90% kích thước ban đầu)
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 0.99f; // Thu nhỏ nhẹ nút
        yield return new WaitForSeconds(pressEffectDuration); // Chờ hiệu ứng nhấn
        transform.localScale = originalScale; // Trả lại kích thước ban đầu

        // Bắt đầu hiệu ứng tối dần
        yield return StartCoroutine(FadeOut());

        // Chuyển Scene sau khi hoàn thành Fade Out
        StartCoroutine(LoadSceneWithFadeIn("MenuChinh"));
    }

    IEnumerator FadeOut()
    {
        if (fadeImage != null)
        {
            Color fadeColor = fadeImage.color;
            float timer = 0;

            // Dần dần tăng độ đen (Alpha từ 0 đến 1)
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                fadeColor.a = Mathf.Lerp(0, 1, timer / fadeDuration); // Tăng Alpha
                fadeImage.color = fadeColor; // Cập nhật màu
                yield return null;
            }

            // Đảm bảo Alpha là 1 (hoàn toàn đen)
            fadeColor.a = 1;
            fadeImage.color = fadeColor;
        }
        else
        {
            Debug.LogError("FadeImage chưa được gán trong Inspector!");
        }
    }

    IEnumerator FadeIn()
    {
        if (fadeImage != null)
        {
            Color fadeColor = fadeImage.color;
            float timer = 0;

            // Dần dần giảm độ đen (Alpha từ 1 đến 0)
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                fadeColor.a = Mathf.Lerp(1, 0, timer / fadeDuration); // Giảm Alpha
                fadeImage.color = fadeColor; // Cập nhật màu
                yield return null;
            }

            // Đảm bảo Alpha là 0 (hoàn toàn trong suốt)
            fadeColor.a = 0;
            fadeImage.color = fadeColor;
        }
        else
        {
            Debug.LogError("FadeImage chưa được gán trong Inspector!");
        }
    }

    IEnumerator LoadSceneWithFadeIn(string MenuChinh)
    {
        // Tải Scene trong nền
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(MenuChinh);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Bắt đầu hiệu ứng Fade In khi Scene đã được tải
        yield return StartCoroutine(FadeIn());
    }
}