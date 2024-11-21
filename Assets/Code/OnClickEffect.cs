using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Đảm bảo namespace này được thêm
using UnityEngine.UI;

public class OnClickEffect : MonoBehaviour
{
    public float pressEffectDuration = 0.1f; // Thời gian hiệu ứng nhấp nhả
    public Image fadeImage; // UI Image dùng để làm hiệu ứng đen
    public float fadeDuration = 0.5f; // Thời gian để màn hình tối dần/sáng dần
    public GameObject lidObject; // Nắp đậy xúc xắc
    public SpriteRenderer[] diceRenderers; // Renderer của 3 xúc xắc
    public Sprite[] diceSprites; // Các hình ảnh xúc xắc (6 hình)
    private RandomManager randomManager; // Đối tượng quản lý random
    private int pressCount = 0; // Biến theo dõi số lần nhấn

    void Start()
    {
        randomManager = new RandomManager(); // Khởi tạo RandomManager
    }

    public void OnButtonClicked()
    {
        if (pressCount == 0)
        {
            StartCoroutine(FirstPressEffect());
        }
        else if (pressCount == 1)
        {
            StartCoroutine(SecondPressEffect());
        }
    }

    IEnumerator FirstPressEffect()
    {
        pressCount++; // Đánh dấu lần nhấn đầu tiên
        Debug.Log($"Press Count: {pressCount}");

        // Hiệu ứng thu nhỏ nút
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 0.99f;
        yield return new WaitForSeconds(pressEffectDuration);
        transform.localScale = originalScale;

        // Đậy nắp
        lidObject.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Chờ hiệu ứng đậy nắp
    }

   IEnumerator SecondPressEffect()
{
    // Random kết quả
    int currentHour = System.DateTime.Now.Hour; // Lấy giờ hiện tại
    string currentCase = randomManager.GetCaseByHour(currentHour);
    Debug.Log($"Case hiện tại: {currentCase}"); // In ra console để kiểm tra

    // Cập nhật hình ảnh xúc xắc theo case
    string[] diceResults = currentCase.Split(' ');
    for (int i = 0; i < diceRenderers.Length; i++)
    {
        if (i >= diceResults.Length)
        {
            Debug.LogWarning("Dữ liệu random không đủ cho xúc xắc!");
            break;
        }
        int spriteIndex = int.Parse(diceResults[i].Substring(diceResults[i].Length - 1)); // Lấy số từ cuối chuỗi
        Debug.Log($"Dice {i}: {spriteIndex}"); // Log giá trị sprite
        if (spriteIndex >= 0 && spriteIndex < diceSprites.Length)
        {
            diceRenderers[i].sprite = diceSprites[spriteIndex];
        }
        else
        {
            Debug.LogError($"Giá trị spriteIndex không hợp lệ: {spriteIndex}");
        }
    }

    yield return new WaitForSeconds(1.0f); // Chờ trước khi mở nắp

    // Mở nắp
    lidObject.SetActive(false);
    Debug.Log("Mở nắp hoàn tất.");

    // Reset trạng thái
    pressCount = 0;
}


    IEnumerator ButtonPressEffect()
    {
        // Hiệu ứng thu nhỏ nút
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 0.99f; // Thu nhỏ nhẹ nút
        yield return new WaitForSeconds(pressEffectDuration); // Chờ hiệu ứng nhấn
        transform.localScale = originalScale; // Trả lại kích thước ban đầu

        // Bắt đầu hiệu ứng tối dần
        yield return StartCoroutine(FadeOut());

        // Chuyển Scene sau khi hoàn thành Fade Out
        StartCoroutine(LoadSceneWithFadeIn("MainGame"));
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

    IEnumerator LoadSceneWithFadeIn(string sceneName)
    {
        // Tải Scene trong nền
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Bắt đầu hiệu ứng Fade In khi Scene đã được tải
        yield return StartCoroutine(FadeIn());
    }
}
