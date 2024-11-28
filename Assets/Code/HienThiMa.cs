using UnityEngine;
using TMPro;
using System;

public class DisplayCaseByTime : MonoBehaviour
{
    public TMP_Text caseText; // Text để hiển thị mã và mô tả
    public TMP_Text codeText; // Text để hiển thị mã code riêng

    private int caseIndex;
    private bool lastNetworkStatus;

    void Start()
    {
        // Lấy giờ hiện tại để xác định caseIndex
        int currentHour = DateTime.Now.Hour; // Giờ hiện tại (0-23)
        caseIndex = currentHour % CaseData.Cases.Count; // Chỉ số case (vòng lặp nếu lớn hơn số lượng cases)

        // Kiểm tra kết nối mạng ban đầu
        lastNetworkStatus = Application.internetReachability != NetworkReachability.NotReachable;

        // Load các case từ CaseData
        CaseData.LoadCases();

        // Cập nhật giao diện ngay từ đầu
        UpdateDisplay();
    }

    void Update()
    {
        // Kiểm tra trạng thái mạng mỗi khung hình
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        // Nếu trạng thái mạng thay đổi, cập nhật lại giao diện
        if (isConnected != lastNetworkStatus)
        {
            lastNetworkStatus = isConnected;
            Debug.Log("Trạng thái mạng thay đổi: " + (isConnected ? "Có mạng" : "Không có mạng"));
            UpdateDisplay(); // Cập nhật giao diện khi trạng thái mạng thay đổi
        }
    }

    void UpdateDisplay()
    {
        string code = "", description = "";

        // Tính lại caseIndex theo giờ hiện tại
        int currentHour = DateTime.Now.Hour;
        caseIndex = currentHour % CaseData.Cases.Count;

        if (lastNetworkStatus) // Có mạng
        {
            // Cập nhật giá trị ngẫu nhiên cho caseIndex
            CaseData.UpdateCaseWithRandomValues(caseIndex);

            // Lấy mã và mô tả từ caseIndex sau khi cập nhật
            (code, description) = CaseData.GetCaseCodeAndDescription(caseIndex);

            // Hiển thị "code:description" khi có mạng
            if (caseText != null)
            {
                caseText.text = $"{code}:{description}";
            }
        }
        else // Không có mạng
        {
            // Lấy lại giá trị mặc định từ CaseData
            var currentCase = CaseData.Cases[caseIndex];
            code = currentCase.Item3; // Lấy mã mặc định từ gốc
            description = currentCase.Item4; // Lấy mô tả mặc định từ gốc

            // Hiển thị "code.description" khi không có mạng
            if (caseText != null)
            {
                caseText.text = $"{code}.{description}";
            }
        }

        // Luôn hiển thị mã code
        if (codeText != null)
        {
            codeText.text = $"{code}";
        }
    }
}
