using System;
using UnityEngine;
using TMPro; // Thêm thư viện TextMeshPro

public class ClockManager : MonoBehaviour
{
    public TextMeshProUGUI clockText; // Sử dụng TextMeshProUGUI
    private string[] caseResults = new string[24];

    void Start()
    {
        InitializeCaseResults(); // Khởi tạo dữ liệu cho từng giờ
        UpdateClock(); // Hiển thị giờ lần đầu tiên
        InvokeRepeating("UpdateClock", 0f, 60f); // Cập nhật mỗi phút
    }

    void InitializeCaseResults()
    {
        // Khởi tạo các chuỗi theo giờ
        caseResults[0] = "Bầu5 Cua4 Cá3 Gà2 Tôm1 Nai0";
        caseResults[1] = "Cá5 Nai4 Gà3 Bầu2 Tôm1 Cua0";
        caseResults[2] = "Cua5 Nai4 Bầu3 Cá2 Tôm1 Gà0";
        caseResults[3] = "Cua5 Cá4 Gà3 Tôm2 Nai1 Bầu0";
        caseResults[4] = "Nai5 Cá4 Tôm3 Gà2 Cua1 Bầu0";
        caseResults[5] = "Nai5 Gà4 Cua3 Tôm2 Bầu1 Cá0";
        caseResults[6] = "Cá5 Gà4 Tôm3 Nai2 Bầu1 Cua0";
        caseResults[7] = "Tôm5 Nai4 Bầu3 Cua2 Gà1 Cá0";
        caseResults[8] = "Tôm5 Cá4 Nai3 Bầu2 Gà1 Cua0";
        caseResults[9] = "Tôm5 Nai4 Bầu3 Cua2 Gà1 Cá0";
        caseResults[10] = "Bầu5 Gà4 Cua3 Cá2 Nai1 Tôm0";
        caseResults[11] = "Bầu5 Tôm4 Gà3 Cua2 Nai1 Cá0";
        caseResults[12] = "Nai5 Bầu4 Cua3 Cá2 Tôm1 Gà0";
        caseResults[13] = "Gà5 Nai4 Cá3 Tôm2 Bầu1 Cua0";
        caseResults[14] = "Gà5 Cá4 Bầu3 Nai2 Cua1 Tôm0";
        caseResults[15] = "Gà5 Tôm4 Nai3 Bầu2 Cua1 Cá0";
        caseResults[16] = "Cua5 Bầu4 Nai3 Gà2 Cá1 Tôm0";
        caseResults[17] = "Cá5 Tôm4 Gà3 Nai2 Bầu1 Cua0";
        caseResults[18] = "Bầu5 Gà4 Tôm3 Cua2 Nai1 Cá0";
        caseResults[19] = "Tôm5 Gà4 Cá3 Nai2 Bầu1 Cua0";
        caseResults[20] = "Nai5 Bầu4 Cua3 Cá2 Tôm1 Gà0";
        caseResults[21] = "Cua5 Tôm4 Nai3 Gà2 Bầu1 Cá0";
        caseResults[22] = "Bầu5 Nai4 Tôm3 Cá2 Gà1 Cua0";
        caseResults[23] = "Tôm5 Nai4 Cá3 Gà2 Cua1 Bầu0";
    }

    void UpdateClock()
    {
        int currentHour = DateTime.Now.Hour; // Lấy giờ hiện tại (0-23)
        string result = caseResults[currentHour]; // Lấy kết quả từ switch-case
        clockText.text = $"Giờ hiện tại: {currentHour}\n{result}"; // Hiển thị trên TextMeshPro
    }
}
