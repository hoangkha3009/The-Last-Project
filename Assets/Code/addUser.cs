using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserManager : MonoBehaviour
{
    private const string ApiUrl = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/addUser";

    void Start()
    {
        if (PlayerPrefs.HasKey("UserID"))
        {
            string userId = PlayerPrefs.GetString("UserID");
            Debug.Log($"ID đã tồn tại: {userId}");
        }
        else
        {
            Debug.Log("Chưa có ID, tạo mới...");
            CreateNewUser();
        }
    }

    public void CreateNewUser()
    {
        string jsonPayload = 
            "{ \"ID\": \"BC0000\", " +
            "\"CurrentABC\": [\"Ga\", \"Tom\", \"Cua\"], " +
            "\"NewABC\": [\"Nai\"], " +
            "\"ChangeABC\": \"NO\", " +
            "\"LogTime\": \"" + System.DateTime.UtcNow.ToString("o") + "\" }";

        Debug.Log("Chuỗi JSON gửi lên: " + jsonPayload);
        StartCoroutine(PostNewUser(jsonPayload));
    }

    private IEnumerator PostNewUser(string jsonPayload)
    {
        UnityWebRequest request = new UnityWebRequest(ApiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log("Gửi payload: " + jsonPayload);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Kết nối thành công! Phản hồi từ server: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Lỗi kết nối API: {request.error}, Phản hồi từ server: {request.downloadHandler.text}");
        }
    }



    [System.Serializable]
    public class Response
    {
        public bool Success;
        public UserData NewUser;
        public string Message;
    }

    [System.Serializable]
    public class UserData
    {
        public string ID;
        public string[] CurrentABC;
        public string[] NewABC;
        public string ChangeABC;
        public string LogTime;
    }
}
