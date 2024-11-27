using UnityEngine;

public class SetTargetFPS : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60; // Giới hạn FPS ở 60
    }
}
