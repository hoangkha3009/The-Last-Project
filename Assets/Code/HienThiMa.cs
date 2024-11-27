using UnityEngine;
using TMPro;
using System;

public class DisplayCaseByTime : MonoBehaviour
{
    public TMP_Text caseText; // Text để hiển thị mã và mô tả
    public TMP_Text codeText; // Text để hiển thị mã code riêng

    void Start()
    {
        // Load các case từ CaseData
        CaseData.LoadCases();

        // Lấy giờ hiện tại để xác định caseIndex
        int currentHour = DateTime.Now.Hour; // Giờ hiện tại (0-23)
        int caseIndex = currentHour % CaseData.Cases.Count; // Chỉ số case (vòng lặp nếu lớn hơn số lượng cases)

        // Kiểm tra kết nối mạng
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        string code, description;

        if (isConnected)
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
        else
        {
            // Lấy giá trị hiện tại (không cập nhật ngẫu nhiên)
            var currentCase = CaseData.Cases[caseIndex];
            code = currentCase.Item3; // Lấy mã mặc định
            description = currentCase.Item4; // Lấy mô tả mặc định

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
