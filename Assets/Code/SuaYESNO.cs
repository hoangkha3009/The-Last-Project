using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class StatusPrefPlayerManager : MonoBehaviour
{
    private const string ApiPatchUrl = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/UpdateStatusPrefPlayer/ID/";
    private string prefPlayerID; // ID của PrefPlayer

    void Start()
    {
        // Kiểm tra nếu PrefPlayerID đã tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey("PrefPlayerID"))
        {
            prefPlayerID = PlayerPrefs.GetString("PrefPlayerID");
            Debug.Log($"PrefPlayerID đã tồn tại: {prefPlayerID}");
            PatchStatus("YES"); // Gửi PATCH ban đầu
        }
        else
        {
            Debug.LogError("PrefPlayerID chưa được lưu trong PlayerPrefs. Không thể gửi PATCH.");
        }
    }

    /// <summary>
    /// Gửi yêu cầu PATCH để thay đổi trạng thái StatusPrefPlayer
    /// </summary>
    /// <param name="status">Trạng thái cần cập nhật ("YES" hoặc "NO")</param>
    public void PatchStatus(string status)
    {
        if (string.IsNullOrEmpty(prefPlayerID))
        {
            Debug.LogError("PrefPlayerID không hợp lệ.");
            return;
        }

        string url = $"{ApiPatchUrl}{prefPlayerID}";

        // Tạo payload đơn giản chỉ là chuỗi YES hoặc NO
        string jsonPayload = $"\"{status}\"";
        Debug.Log($"URL API PATCH: {url}");
        Debug.Log($"Payload JSON gửi lên: {jsonPayload}");

        StartCoroutine(SendPatchRequest(url, jsonPayload, status));
    }

    private IEnumerator SendPatchRequest(string url, string jsonPayload, string status)
{
    UnityWebRequest request = new UnityWebRequest(url, "PATCH");
    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    request.SetRequestHeader("X-API-KEY", "f47ac10b-58cc-4372-a567-0e02b2c3d479"); // Đúng key và value

    Debug.Log("Gửi PATCH request...");
    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        Debug.Log($"Cập nhật trạng thái thành công: {status}");
        Debug.Log($"Phản hồi từ server: {request.downloadHandler.text}");
    }
    else
    {
        Debug.LogError($"Lỗi khi gửi PATCH: {request.error}");
        Debug.Log($"HTTP Status Code: {request.responseCode}");
        if (request.downloadHandler != null)
        {
            Debug.Log($"Phản hồi từ server: {request.downloadHandler.text}");
        }
    }
}


    /// <summary>
    /// Thay đổi trạng thái thủ công về YES
    /// </summary>
    public void ManuallyResetStatus()
    {
        Debug.Log("Thay đổi trạng thái thủ công về YES.");
        PatchStatus("YES");
    }
}
