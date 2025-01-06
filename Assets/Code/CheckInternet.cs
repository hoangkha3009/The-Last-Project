using UnityEngine;

public class NetworkImageController : MonoBehaviour
{
    // Tham chiếu đến các hình ảnh
    public GameObject image1; // Hình ảnh khi có mạng
	public GameObject image2; // Hình ảnh khi có mạng
	public GameObject image3; // Hình ảnh khi có mạng
    public GameObject image4; // Hình ảnh khi không có mạng

    // Tần suất kiểm tra kết nối mạng (giây)
    public float checkInterval = 2f;

    private void Start()
    {
        // Kiểm tra kết nối mạng ngay khi bắt đầu
        CheckNetworkStatus();

        // Lặp lại kiểm tra kết nối mỗi khoảng thời gian đã định
        InvokeRepeating(nameof(CheckNetworkStatus), checkInterval, checkInterval);
    }

    private void CheckNetworkStatus()
    {
        // Cập nhật trạng thái hình ảnh
        if (APIHander.Instance.isNetwork)
        {
            image1.SetActive(true);
            image2.SetActive(false);
			image3.SetActive(true);
            image4.SetActive(false);
        }
        else
        {
            image1.SetActive(false);
            image2.SetActive(true);
			image3.SetActive(false);
            image4.SetActive(true);
        }
    }
}
