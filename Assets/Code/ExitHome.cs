using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Hàm được gọi khi nhấn nút Thoát
    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Trong Unity Editor
        Debug.Log("Game exited (Editor).");
        UnityEditor.EditorApplication.isPlaying = false; // Thoát chế độ Play
        #elif UNITY_ANDROID || UNITY_IOS
        // Trên thiết bị di động
        Application.Quit(); // Thoát ứng dụng
        #else
        Application.Quit(); // Trên các nền tảng khác
        #endif
    }
}
