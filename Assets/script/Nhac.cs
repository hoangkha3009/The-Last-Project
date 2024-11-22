using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public AudioSource audioSource; // Gắn AudioSource trong Inspector
    public AudioClip soundEffect; // Gắn tệp âm thanh trong Inspector

    public void PlaySoundEffect()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
        else
        {
            Debug.LogWarning("AudioSource hoặc AudioClip chưa được gắn!");
        }
    }
}
