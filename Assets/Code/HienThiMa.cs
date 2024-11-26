using UnityEngine;
using TMPro;
using System;

public class DisplayCaseByTime : MonoBehaviour
{
    public TMP_Text caseText; // Text Mesh để hiển thị mã và mô tả
    public TMP_Text codeText;

    void Start()
    {
        // Load các case từ CaseData
        CaseData.LoadCases();

        // Lấy giờ hiện tại để xác định caseIndex
        int currentHour = DateTime.Now.Hour; // Giờ hiện tại (0-23)
        int caseIndex = currentHour % CaseData.Cases.Count; // Chỉ số case (vòng lặp nếu lớn hơn số lượng cases)

        // Kiểm tra kết nối mạng
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        // Lấy mã và mô tả của caseIndex
        if (CaseData.Cases.ContainsKey(caseIndex))
        {
            var (code, description) = CaseData.GetCaseCodeAndDescription(caseIndex);

            // Hiển thị thông tin case lên TextMeshPro dựa trên trạng thái mạng
            if (caseText != null)
            {
                if (isConnected)
                {
                    caseText.text = $"{code}:{description}";
                }
                else
                {
                    caseText.text = $"{code}.{description}";
                }
            }

            if (codeText != null)
            {
                codeText.text = $"{code}"; // Code luôn hiển thị
            }
        }
        else
        {
            // Nếu caseIndex không tồn tại, thông báo lỗi
            Debug.LogError($"CaseIndex {caseIndex} không tồn tại trong CaseData!");
            if (caseText != null)
            {
                caseText.text = $"Case {caseIndex} không tồn tại!";
            }
        }
    }
}
