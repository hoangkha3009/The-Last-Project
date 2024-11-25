using UnityEngine;

public class LeafFall : MonoBehaviour
{
    public float fallSpeed = 5f; // Tăng tốc độ rơi (giá trị mặc định là 2f)
    public float rotateSpeed = 60f; // Tăng tốc độ xoay (giá trị mặc định là 30f)
    private Vector3 startPosition;

    void Start()
    {
        // Lưu vị trí bắt đầu
        startPosition = transform.position;
    }

    void Update()
    {
        // Rơi xuống với tốc độ nhanh hơn
        transform.position += Vector3.down * fallSpeed * Time.deltaTime * 20f; // Nhân hệ số 1.5 để tăng tốc

        // Xoay nhanh hơn
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Kiểm tra nếu lá rơi khỏi màn hình
        if (transform.position.y < -Screen.height / 100f)
        {
            // Đặt lại vị trí phía trên cùng với độ lệch ngang ngẫu nhiên
            transform.position = startPosition + new Vector3(Random.Range(-2f, 2f), 0, 0);
        }
    }
}
