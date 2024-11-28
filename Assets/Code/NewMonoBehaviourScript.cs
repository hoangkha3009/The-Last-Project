using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public DisplayCaseOnline onlineScript;  // Reference tới script online
    public DisplayCaseOffline offlineScript;  // Reference tới script offline

    // Các đối tượng Malenh
    public GameObject malenh;   // Malenh (hiển thị khi online)
    public GameObject malenh2;  // Malenh 2 (hiển thị khi online)
    public GameObject malenh3;  // Malenh 3 (hiển thị khi offline)
    public GameObject malenh4;  // Malenh 4 (hiển thị khi offline)

    private bool lastNetworkStatus;

    void Start()
    {
        // Ban đầu ẩn tất cả các phần tử UI
        if (onlineScript != null)
        {
            onlineScript.gameObject.SetActive(false);  // Ẩn phần online khi bắt đầu
        }
        if (offlineScript != null)
        {
            offlineScript.gameObject.SetActive(false);  // Ẩn phần offline khi bắt đầu
        }

        // Ẩn tất cả các Malenh ban đầu
        SetMalenhActive(false);  // Ban đầu ẩn tất cả Malenh

        // Kiểm tra trạng thái mạng ban đầu
        lastNetworkStatus = Application.internetReachability != NetworkReachability.NotReachable;

        Debug.Log("Trạng thái mạng ban đầu: " + (lastNetworkStatus ? "Có mạng" : "Không có mạng"));

        // Cập nhật giao diện ngay từ đầu
        UpdateNetworkStatus();
    }

    void Update()
    {
        // Kiểm tra trạng thái mạng mỗi khung hình
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        // Debug để kiểm tra trạng thái kết nối
        if (isConnected != lastNetworkStatus)
        {
            Debug.Log("Trạng thái mạng thay đổi: " + (isConnected ? "Có mạng" : "Không có mạng"));
            lastNetworkStatus = isConnected;
            UpdateNetworkStatus(); // Cập nhật giao diện khi trạng thái mạng thay đổi
        }
    }

    void UpdateNetworkStatus()
    {
        if (lastNetworkStatus)  // Có mạng (online)
        {
            Debug.Log("Cập nhật trạng thái: Hiển thị online, ẩn offline");
            // Kích hoạt phần online và ẩn phần offline
            if (onlineScript != null)
            {
                onlineScript.gameObject.SetActive(true);  // Kích hoạt phần online
            }

            if (offlineScript != null)
            {
                offlineScript.gameObject.SetActive(false);  // Ẩn phần offline
            }

            // Hiển thị Malenh và Malenh2, ẩn Malenh3 và Malenh4
            SetMalenhActive(true);  // Hiển thị Malenh và Malenh2
        }
        else  // Không có mạng (offline)
        {
            Debug.Log("Cập nhật trạng thái: Hiển thị offline, ẩn online");
            // Kích hoạt phần offline và ẩn phần online
            if (onlineScript != null)
            {
                onlineScript.gameObject.SetActive(false);  // Ẩn phần online
            }

            if (offlineScript != null)
            {
                offlineScript.gameObject.SetActive(true);  // Kích hoạt phần offline
            }

            // Hiển thị Malenh3 và Malenh4, ẩn Malenh và Malenh2
            SetMalenhActive(false);  // Hiển thị Malenh3 và Malenh4
        }
    }

    // Hàm để cập nhật trạng thái hiển thị các Malenh
    void SetMalenhActive(bool isOnline)
    {
        if (malenh != null)
            malenh.SetActive(isOnline);  // Nếu online thì hiện Malenh

        if (malenh2 != null)
            malenh2.SetActive(isOnline);  // Nếu online thì hiện Malenh2

        if (malenh3 != null)
            malenh3.SetActive(!isOnline);  // Nếu offline thì hiện Malenh3

        if (malenh4 != null)
            malenh4.SetActive(!isOnline);  // Nếu offline thì hiện Malenh4
    }
}
