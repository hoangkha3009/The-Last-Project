using UnityEngine;
using TMPro;
using System;

public class DisplayCaseOnline : MonoBehaviour
{
    public TMP_Text onlineCaseText;  // Text để hiển thị mã và mô tả khi có mạng
    public TMP_Text onlineCodeText;  // Text để hiển thị mã code khi có mạng

    private int caseIndex;

    void Start()
    {
        // Ban đầu ẩn tất cả
        if (onlineCaseText != null) onlineCaseText.gameObject.SetActive(false);
        if (onlineCodeText != null) onlineCodeText.gameObject.SetActive(false);

        // Lấy giờ hiện tại để xác định caseIndex
        int currentHour = DateTime.Now.Hour; // Giờ hiện tại (0-23)
        caseIndex = currentHour % CaseData.Cases.Count; // Chỉ số case (vòng lặp nếu lớn hơn số lượng cases)

        // Load các case từ CaseData
        CaseData.LoadCases();

        // Cập nhật giao diện ngay từ đầu
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        string code = "", description = "";

        // Tính lại caseIndex theo giờ hiện tại
        int currentHour = DateTime.Now.Hour;
        caseIndex = currentHour % CaseData.Cases.Count;

        // Cập nhật giá trị ngẫu nhiên cho caseIndex
        CaseData.UpdateCaseWithRandomValues(caseIndex);

        // Lấy mã và mô tả từ caseIndex sau khi cập nhật
        (code, description) = CaseData.GetCaseCodeAndDescription(caseIndex);

        // Hiển thị "code:description" khi có mạng
        if (onlineCaseText != null)
        {
            onlineCaseText.text = $"{code}:{description}";
            onlineCaseText.gameObject.SetActive(true);  // Kích hoạt text online
        }

        // Hiển thị mã code khi có mạng
        if (onlineCodeText != null)
        {
            onlineCodeText.text = $"{code}";
            onlineCodeText.gameObject.SetActive(true);  // Kích hoạt mã code online
        }
    }
}
