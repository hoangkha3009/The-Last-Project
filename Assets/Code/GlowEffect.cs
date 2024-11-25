using UnityEngine;
using UnityEngine.UI;

public class ImageGlowController : MonoBehaviour
{
    public GameObject subImage; // Đối tượng SubImage
    public Image mainImage;     // Image chính cần đổi màu
    public Color glowColor = new Color(0.678f, 0.678f, 0.678f, 1f); // Màu tối hơn (ví dụ: #ADADAD)
    public Color defaultColor = Color.white; // Màu mặc định

    private bool isGlowActive = false; // Trạng thái bật/tắt hiệu ứng

    public void ToggleGlow()
    {
        // Đảo ngược trạng thái bật/tắt
        isGlowActive = !isGlowActive;

        // Bật/tắt SubImage
        subImage.SetActive(isGlowActive);

        // Đổi màu của MainImage
        mainImage.color = isGlowActive ? glowColor : defaultColor;
    }
}
