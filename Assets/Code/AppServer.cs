using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameApp : MonoBehaviour
{
    // Mã game được tạo ngẫu nhiên
    private string gameCode;

    void Start()
    {
        // Tạo mã ngẫu nhiên khi game bắt đầu
        gameCode = GenerateRandomCode();
        Debug.Log("Mã game ngẫu nhiên được tạo: " + gameCode);  // In ra mã ngẫu nhiên

        // Gửi mã game lên server
        SendCodeToServer(gameCode);
    }

    string GenerateRandomCode()
    {
        // Tạo một mã ngẫu nhiên 4 chữ số
        int code = Random.Range(1000, 9999);  // Mã luôn nằm trong khoảng từ 1000 đến 9998
        string codeStr = code.ToString();
        Debug.Log("Generated Random Code: " + codeStr);  // Kiểm tra mã ngẫu nhiên
        return codeStr;
    }

    void SendCodeToServer(string code)
    {
        // Kiểm tra mã trước khi gửi
        if (string.IsNullOrEmpty(code))
        {
            Debug.LogError("Mã ngẫu nhiên không hợp lệ, không thể gửi.");
            return;
        }

        // Gửi mã lên server qua HTTP POST
        StartCoroutine(SendCodeCoroutine(code));
    }

    IEnumerator SendCodeCoroutine(string code)
    {
        // Tạo dữ liệu JSON với mã đã xác thực
        string jsonData = "{\"code\":\"" + code + "\"}";
        Debug.Log("Gửi mã đến server: " + jsonData);  // Kiểm tra mã gửi đi

        // Chuyển dữ liệu JSON thành mảng byte
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Gửi request tới server
        using (UnityWebRequest www = new UnityWebRequest("http://localhost:8080/", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Đợi server phản hồi
            yield return www.SendWebRequest();

            // Kiểm tra kết quả gửi mã
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Mã đã gửi thành công lên server!");
            }
            else
            {
                Debug.LogError("Lỗi gửi mã: " + www.error);
                Debug.LogError("Chi tiết lỗi: " + www.downloadHandler.text);
            }
        }
    }
}
