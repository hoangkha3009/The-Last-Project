using UnityEngine;
using TMPro;
using System;

public class DisplayCaseOnline : MonoBehaviour
{
    public TMP_Text onlineCaseText;  // Text để hiển thị mã và mô tả khi có mạng
    public TMP_Text onlineCodeText;  // Text để hiển thị mã code khi có mạng

    private int previousHour = -1;  // Biến lưu trữ giờ trước đó
    private System.Random random = new System.Random();  // Tạo random để sinh số và chữ cái ngẫu nhiên

    void Start()
    {
        // Ban đầu ẩn tất cả
        if (onlineCaseText != null) onlineCaseText.gameObject.SetActive(false);
        if (onlineCodeText != null) onlineCodeText.gameObject.SetActive(false);

        // Cập nhật giao diện ngay từ đầu
        UpdateDisplay();
    }

    void Update()
    {
        // Lấy giờ hiện tại
        int currentHour = DateTime.Now.Hour;

        // Kiểm tra xem giờ có thay đổi không
        if (currentHour != previousHour)
        {
            // Nếu giờ thay đổi, cập nhật giao diện
            UpdateDisplay();

            // Cập nhật lại giờ trước đó
            previousHour = currentHour;
        }
    }

    void UpdateDisplay()
    {
        // Sinh mã code và mô tả ngẫu nhiên
        string code = GenerateRandomCode();
        string description = GenerateRandomDescription();

        // Hiển thị "code:description"
        if (onlineCaseText != null)
        {
            onlineCaseText.text = $"{code}:{description}";
            onlineCaseText.gameObject.SetActive(true);  // Kích hoạt text online
        }

        // Hiển thị mã code
        if (onlineCodeText != null)
        {
            onlineCodeText.text = $"{code}";
            onlineCodeText.gameObject.SetActive(true);  // Kích hoạt mã code online
        }
    }

    // Hàm tạo mã ngẫu nhiên gồm 3 số
    private string GenerateRandomCode()
    {
        return random.Next(100, 1000).ToString();  // Sinh số ngẫu nhiên từ 100 đến 999
    }

    // Hàm tạo mô tả ngẫu nhiên gồm 3 chữ cái
    private string GenerateRandomDescription()
    {
        char letter1 = (char)random.Next('A', 'Z' + 1);  // Sinh chữ cái ngẫu nhiên từ A đến Z
        char letter2 = (char)random.Next('A', 'Z' + 1);
        char letter3 = (char)random.Next('A', 'Z' + 1);
        return $"{letter1}{letter2}{letter3}";  // Ghép thành chuỗi 3 chữ cái
    }
}
