using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DisplayCaseOffline : MonoBehaviour
{
    public TMP_Text offlineCaseText;  // Text để hiển thị mã và mô tả
    public TMP_Text offlineCodeText;  // Text để hiển thị mã code

    private int caseIndex;

    void Start()
    {
        // Ban đầu ẩn tất cả
        if (offlineCaseText != null) offlineCaseText.gameObject.SetActive(false);
        if (offlineCodeText != null) offlineCodeText.gameObject.SetActive(false);

        // Kiểm tra xem có kết nối mạng không
        if (IsNetworkAvailable())
        {
            // Nếu có mạng, tải và lưu dữ liệu
            LoadAndSaveCaseData();
        }
        else
        {
            // Nếu không có mạng, tải dữ liệu đã lưu từ PlayerPrefs
            LoadSavedCaseData();
        }

        // Cập nhật giao diện ngay từ đầu
        UpdateDisplay();
    }

    void Update()
    {
        // Tính lại caseIndex mỗi lần trong vòng lặp cập nhật
        int currentHour = System.DateTime.Now.Hour;
        // Nếu CaseData.Cases không rỗng, tính toán chỉ số caseIndex
        if (CaseData.Cases.Count > 0)
        {
            caseIndex = currentHour % CaseData.Cases.Count; // Tính lại caseIndex theo giờ hiện tại
        }

        // Cập nhật lại giao diện sau khi tính toán caseIndex mới
        UpdateDisplay();
    }

    void LoadAndSaveCaseData()
    {
        // Giả sử bạn tải các case từ một nguồn dữ liệu online
        CaseData.LoadCases();

        // Lưu dữ liệu vào PlayerPrefs
        string caseDataJson = JsonUtility.ToJson(CaseData.Cases);
        PlayerPrefs.SetString("SavedCaseData", caseDataJson);
        PlayerPrefs.Save();
    }

    void LoadSavedCaseData()
    {
        // Kiểm tra xem có dữ liệu case đã lưu trong PlayerPrefs chưa
        if (PlayerPrefs.HasKey("SavedCaseData"))
        {
            string savedCaseData = PlayerPrefs.GetString("SavedCaseData");
            
        }
        else
        {
            Debug.Log("No saved case data found.");
        }
    }

    void UpdateDisplay()
    {
        string code = "", description = "";

        // Kiểm tra xem CaseData.Cases có dữ liệu hay không trước khi truy cập
        if (CaseData.Cases.Count > 0)
        {
            // Lấy giá trị case từ CaseData
            var currentCase = CaseData.Cases[caseIndex];

            // Lấy ID từ PlayerPrefs và hiển thị
            if (PlayerPrefs.HasKey("PrefPlayerID"))
            {
                string userId = PlayerPrefs.GetString("PrefPlayerID");
                Debug.Log($"UserID đã tồn tại trong PrefPlayer: {userId}");
                code = userId;  // Hiển thị Player ID
            }
            else
            {
                code = "No Player ID found";  // Nếu không tìm thấy ID, hiển thị thông báo này
            }

            // Lấy mô tả từ currentCase
            description = currentCase.Item4 ?? "No description available";  // Kiểm tra nếu mô tả trống
        }
        else
        {
            // Nếu không có cases, hiển thị thông báo mặc định
            code = "No Case Data";
            description = "No Description Available";
        }

        // Kiểm tra kết nối mạng và thay đổi định dạng hiển thị
        string displayText = IsNetworkAvailable() 
            ? $"MP:{code}:{description}"  // Khi có mạng
            : $"MP.{code}.{description}";  // Khi không có mạng

        // Hiển thị "code.description"
        if (offlineCaseText != null)
        {
            offlineCaseText.text = displayText;
            offlineCaseText.gameObject.SetActive(true);  // Kích hoạt text để hiển thị
        }

        // Hiển thị mã code (ID người chơi)
        if (offlineCodeText != null)
        {
            offlineCodeText.text = $"{code}";
            offlineCodeText.gameObject.SetActive(true);  // Kích hoạt mã code để hiển thị
        }
    }

    bool IsNetworkAvailable()
    {
        // Kiểm tra kết nối mạng (đơn giản, có thể sử dụng các API khác nếu cần kiểm tra kết nối mạng chính xác hơn)
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
