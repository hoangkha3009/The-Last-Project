using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserManager : MonoBehaviour
{
    private const string ApiAddUserUrl = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/addUser";
    private string userId; // ID của User
    private string userShortId; // IDShort của User

    void Start()
    {
        // Kiểm tra nếu UserID đã tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey("PrefPlayerID"))
        {
            userId = PlayerPrefs.GetString("PrefPlayerID");
            Debug.Log($"UserID đã tồn tại trong PrefPlayer: {userId}");

            // Kiểm tra nếu IDShort đã được lưu
            if (!PlayerPrefs.HasKey("PrefPlayerShortID"))
            {
                Debug.Log("Chưa có IDShort trong PrefPlayer. Tải từ API...");
                StartCoroutine(GetUserById(userId));
            }
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
               "\"ID\": \"000\", " +
               "\"CurrentABC\": [\"Cua\", \"Tom\", \"Ga\"], " +
               "\"NewABC\": [\"Nai\", \"Tom\", \"Tom\"], " +
               "\"ChangeABC\": \"NO\", " +
               "\"LogTime\": \"2024-12-25T00:00:00.0000000Z\", " +
               "\"CreateIDTime\": \"2024-12-25T00:00:00.0000000Z\", " +
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
                    userShortId = response.newUser.idShort;

                    // Lưu ID và IDShort vào PlayerPrefs
                    PlayerPrefs.SetString("PrefPlayerID", userId);
                    PlayerPrefs.SetString("PrefPlayerShortID", userShortId);
                    PlayerPrefs.Save();

                    Debug.Log($"UserID mới đã được lưu trong PrefPlayer: {userId}");
                    Debug.Log($"UserShortID mới đã được lưu trong PrefPlayer: {userShortId}");
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

    private IEnumerator GetUserById(string userId)
    {
        string url = $"https://bautomcua-a8b183da803d.herokuapp.com/api/config/id/{userId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("X-API-KEY", "f47ac10b-58cc-4372-a567-0e02b2c3d479");

        Debug.Log("Đang gọi API để lấy thông tin user...");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Phản hồi từ server: {request.downloadHandler.text}");

            try
            {
                UserResponse response = JsonUtility.FromJson<UserResponse>(request.downloadHandler.text);

                if (response != null && response.success && response.user != null)
                {
                    userShortId = response.user.idShort;

                    // Lưu IDShort vào PlayerPrefs
                    PlayerPrefs.SetString("PrefPlayerShortID", userShortId);
                    PlayerPrefs.Save();

                    Debug.Log($"IDShort đã được lưu trong PrefPlayer: {userShortId}");
                }
                else
                {
                    Debug.LogWarning("Phản hồi từ server không hợp lệ hoặc thiếu IDShort.");
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
        public NewUser newUser; // Phản hồi từ POST /addUser
        public User user;      // Phản hồi từ GET /id/{id}
    }

    [System.Serializable]
    public class NewUser
    {
        public string id;
        public string idShort; // Thêm idShort vào phản hồi
    }

    [System.Serializable]
    public class User
    {
        public string id;
        public string idShort; // idShort từ GET API
    }
}
