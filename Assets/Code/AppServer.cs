using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameStartup : MonoBehaviour
{
    private string apiUrl = "http://localhost:8080/";  // URL server để nhận mã
    private string generatedCode;  // Mã 4 chữ số

    void Start()
    {
        // Tạo mã 4 chữ số ngẫu nhiên
        generatedCode = Random.Range(1000, 9999).ToString();
        Debug.Log("Generated Code: " + generatedCode);

        // Gửi mã lên server
        StartCoroutine(SendCodeToServer(generatedCode));
    }

    private IEnumerator SendCodeToServer(string code)
{
    // Tạo dữ liệu JSON
    Dictionary<string, string> data = new Dictionary<string, string>
    {
        { "code", code }
    };
    string json = JsonUtility.ToJson(data);

    // Tạo POST request để gửi lên server
    UnityWebRequest www = new UnityWebRequest(apiUrl, "POST");
    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    www.uploadHandler = new UploadHandlerRaw(jsonToSend);
    www.downloadHandler = new DownloadHandlerBuffer();
    www.SetRequestHeader("Content-Type", "application/json");

    // Debug log để kiểm tra dữ liệu gửi đi
    Debug.Log("Sending code to server: " + json);

    // Gửi request và đợi phản hồi
    yield return www.SendWebRequest();

    // Kiểm tra kết quả của request
    if (www.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("Server received code: " + www.downloadHandler.text);
    }
    else
    {
        Debug.Log("Error sending code to server: " + www.error);
    }
}
}
