using UnityEngine;
using TMPro;

public class DisplayCaseOffline : MonoBehaviour
{
    public TMP_Text offlineCaseText;  // Text để hiển thị mã và mô tả
    public TMP_Text offlineCodeText;  // Text để hiển thị mã code

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

        // Cập nhật giao diện ban đầu
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
            // Xử lý nếu cần (deserialize dữ liệu)
        }
        else
        {
            Debug.Log("No saved case data found.");
        }
    }

    void UpdateDisplay()
    {
        string code = "", description = "";

        // Lấy ID từ PlayerPrefs và hiển thị
        if (PlayerPrefs.HasKey("PrefPlayerID"))
        {
            code = PlayerPrefs.GetString("PrefPlayerID");
        }
        else
        {
            code = "No ID";  // Nếu không tìm thấy ID, hiển thị thông báo này
        }

        // Lấy idShort (description) từ PlayerPrefs
        if (PlayerPrefs.HasKey("PrefPlayerShortID"))
        {
            description = PlayerPrefs.GetString("PrefPlayerShortID");
        }
        else
        {
            description = "No idShort";  // Nếu không tìm thấy idShort, hiển thị thông báo này
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
        // Kiểm tra kết nối mạng
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
