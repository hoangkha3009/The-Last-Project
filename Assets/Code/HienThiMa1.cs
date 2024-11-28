using UnityEngine;
using TMPro;
using System;

public class DisplayCaseOffline : MonoBehaviour
{
    public TMP_Text offlineCaseText;  // Text để hiển thị mã và mô tả khi không có mạng
    public TMP_Text offlineCodeText;  // Text để hiển thị mã code khi không có mạng

    private int caseIndex;

    void Start()
    {
        // Ban đầu ẩn tất cả
        if (offlineCaseText != null) offlineCaseText.gameObject.SetActive(false);
        if (offlineCodeText != null) offlineCodeText.gameObject.SetActive(false);

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

        // Lấy lại giá trị mặc định từ CaseData khi không có mạng
        var currentCase = CaseData.Cases[caseIndex];
        code = currentCase.Item3;  // Lấy mã mặc định từ gốc
        description = currentCase.Item4;  // Lấy mô tả mặc định từ gốc

        // Hiển thị "code.description" khi không có mạng
        if (offlineCaseText != null)
        {
            offlineCaseText.text = $"{code}.{description}";
            offlineCaseText.gameObject.SetActive(true);  // Kích hoạt text offline
        }

        // Hiển thị mã code khi không có mạng
        if (offlineCodeText != null)
        {
            offlineCodeText.text = $"{code}";
            offlineCodeText.gameObject.SetActive(true);  // Kích hoạt mã code offline
        }
    }
}
