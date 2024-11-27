using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    // Các đối tượng UI của xúc xắc (Image)
    public Image dice1Image;
    public Image dice2Image;
    public Image dice3Image;

    // Danh sách tên hình ảnh xúc xắc (1 đến 6)
    private string[] diceFaces = { "1", "2", "3", "4", "5", "6" };

    // Thư mục Resources chứa ảnh xúc xắc
    private string diceFaceFolder = "Ảnh Xúc Xắc";

    // Biến lưu trữ kết quả xúc xắc
    private int nextDice = 0; // Giá trị tiếp theo của xúc xắc

    void Start()
    {
        // Gọi hàm lắc xúc xắc khi bắt đầu
        RollDice();
    }

    // Hàm lắc xúc xắc và tính toán kết quả
    public void RollDice()
    {
        // Tạo mảng kết quả cho 3 viên xúc xắc
        int[] diceResults = new int[3];
        for (int i = 0; i < 3; i++)
        {
            diceResults[i] = Random.Range(0, 6); // Lắc xúc xắc ngẫu nhiên từ 0 đến 5
        }

        // Lấy kết quả xúc xắc từ mảng diceFaces
        string dice1Face = diceFaces[diceResults[0]];
        string dice2Face = diceFaces[diceResults[1]];
        string dice3Face = diceFaces[diceResults[2]];

        // Cập nhật hình ảnh các xúc xắc trong UI
        dice1Image.sprite = Resources.Load<Sprite>($"{diceFaceFolder}/{dice1Face}");
        dice2Image.sprite = Resources.Load<Sprite>($"{diceFaceFolder}/{dice2Face}");
        dice3Image.sprite = Resources.Load<Sprite>($"{diceFaceFolder}/{dice3Face}");

        // Tính toán nextDice theo công thức từ CaseData
        CalculateNextDice(diceResults[0], diceResults[1], diceResults[2]);

        // Hiển thị kết quả
        Debug.Log($"Xúc xắc 1: {dice1Face}, Xúc xắc 2: {dice2Face}, Xúc xắc 3: {dice3Face}");
        Debug.Log($"Kết quả chẵn/lẻ: {DetermineEvenOdd(nextDice)}");
    }

    // Tính toán nextDice dựa trên công thức X + Y + Z (có thể thay đổi tùy theo case)
    private void CalculateNextDice(int x, int y, int z)
    {
        // Công thức tính nextDice có thể thay đổi, ví dụ: X + Y + Z
        nextDice = x + y + z; // Đây chỉ là một công thức ví dụ
    }

    // Hàm kiểm tra chẵn/lẻ
    private string DetermineEvenOdd(int result)
    {
        if (result % 2 == 0)
        {
            return "Chẵn";
        }
        else
        {
            return "Lẻ";
        }
    }
}
