using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Button myButton;        // Nút để bật/tắt tiếng
    public AudioSource audioSource; // Nguồn phát âm thanh

    private bool isMuted = false;  // Trạng thái hiện tại của âm thanh

    void Start()
    {
        myButton.onClick.AddListener(ToggleAudio); // Thêm sự kiện vào nút
    }

    void ToggleAudio()
    {
        // Đảo trạng thái âm thanh
        isMuted = !isMuted;

        if (isMuted)
        {
            audioSource.mute = true; // Tắt tiếng
            Debug.Log("Audio Muted!");
        }
        else
        {
            audioSource.mute = false; // Bật tiếng
            Debug.Log("Audio Unmuted!");
        }
    }
}