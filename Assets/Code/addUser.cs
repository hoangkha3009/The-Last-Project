using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserManager : MonoBehaviour
{
    private const string ApiAddUserUrl = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/addUser";
    private string userId; // ID của User

    void Start()
    {
        // Kiểm tra nếu UserID đã tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey("PrefPlayerID"))
        {
            userId = PlayerPrefs.GetString("PrefPlayerID");
            Debug.Log($"UserID đã tồn tại trong PrefPlayer: {userId}");
        }
        else
        {
            Debug.Log("Chưa có UserID trong PrefPlayer. Tiến hành tạo mới...");
            StartCoroutine(CreateNewUser());
        }
    }

    private string CreatePayload()
    {
        return "{ " +
               "\"ID\": \"\", " +
               "\"CurrentABC\": [\"Ga\", \"Tom\", \"Cua\"], " +
               "\"NewABC\": [\"Nai\"], " +
               "\"ChangeABC\": \"NO\", " +
               "\"LogTime\": \"" + System.DateTime.UtcNow.ToString("o") + "\", " +
               "\"CreateIDTime\": \"" + System.DateTime.UtcNow.ToString("o") + "\", " +
               "\"PrefPlayer\": \"\", " +
               "\"ThreeTouchPoints\": \"NO\", " +
               "\"StatusPrefPlayer\": \"NO\", " +
               "\"Order\": [\"Tom\", \"Cua\", \"Nai\", \"Ga\", \"Bau\", \"Ca\"] " +
               "}";
    }

    private IEnumerator CreateNewUser()
    {
        string jsonPayload = CreatePayload();
        Debug.Log($"Payload JSON gửi lên: {jsonPayload}");
        
        UnityWebRequest request = new UnityWebRequest(ApiAddUserUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("X-API-KEY", "f47ac10b-58cc-4372-a567-0e02b2c3d479"); // Thêm API key vào header

        Debug.Log("Đang gửi yêu cầu tạo User...");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Phản hồi từ server: {request.downloadHandler.text}");

            try
            {
                UserResponse response = JsonUtility.FromJson<UserResponse>(request.downloadHandler.text);

                if (response != null && response.success && response.newUser != null && !string.IsNullOrEmpty(response.newUser.id))
                {
                    userId = response.newUser.id;

                    // Lưu ID vào PlayerPrefs
                    PlayerPrefs.SetString("PrefPlayerID", userId);
                    PlayerPrefs.Save();

                    Debug.Log($"UserID mới đã được lưu trong PrefPlayer: {userId}");
                }
                else
                {
                    Debug.LogWarning("Phản hồi từ server không hợp lệ hoặc thiếu ID.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Lỗi khi parse JSON: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Lỗi khi gọi API: {request.error}");
            Debug.Log($"HTTP Status Code: {request.responseCode}");
            if (request.downloadHandler != null)
            {
                Debug.Log($"Phản hồi từ server: {request.downloadHandler.text}");
            }
        }
    }

    [System.Serializable]
    public class UserResponse
    {
        public bool success;
        public NewUser newUser;
    }

    [System.Serializable]
    public class NewUser
    {
        public string id;
        public string[] currentABC;
        public string[] newABC;
        public string changeABC;
        public string logTime;
        public string createIDTime;
        public string prefPlayer;
        public string threeTouchPoints;
        public string statusPrefPlayer;
        public string[] order;
    }
}
