using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;  // Đảm bảo có namespace này nếu bạn dùng List

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

        // Load các case từ CaseData
        CaseData.LoadCases();

        // Cập nhật giao diện ngay từ đầu
        UpdateDisplay();
    }

    void Update()
    {
        // Tính lại caseIndex mỗi lần trong vòng lặp cập nhật
        int currentHour = DateTime.Now.Hour;
        // Nếu CaseData.Cases không rỗng, tính toán chỉ số caseIndex
        if (CaseData.Cases.Count > 0)
        {
            caseIndex = currentHour % CaseData.Cases.Count; // Tính lại caseIndex theo giờ hiện tại
        }

        // Cập nhật lại giao diện sau khi tính toán caseIndex mới
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        string code = "", description = "";

        // Kiểm tra xem CaseData.Cases có dữ liệu hay không trước khi truy cập
        if (CaseData.Cases.Count > 0)
        {
            // Lấy giá trị case từ CaseData
            var currentCase = CaseData.Cases[caseIndex];
            code = currentCase.Item3;  // Lấy mã mặc định từ gốc
            description = currentCase.Item4;  // Lấy mô tả mặc định từ gốc
        }
        else
        {
            // Nếu không có cases, hiển thị thông báo mặc định
            code = "No Case Data";
            description = "No Description Available";
        }

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
