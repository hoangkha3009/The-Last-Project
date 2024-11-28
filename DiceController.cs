using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    public GameObject[] diceObjects; // Các đối tượng xúc xắc (UI Image)
    public string[] diceNames; // Tên biểu tượng xúc xắc
    private int nextDice = -1; // Giá trị Next Dice (ban đầu là -1 để kiểm tra)
    private int currentCase; // Case hiện tại
	public Image resultImage1; // UI Image cho Dice 1
    public Image resultImage2; // UI Image cho Dice 2
    public Image resultImage3; // UI Image cho Dice 3

    int[] diceResults = new int[3];
    int checkCase = 2;
    void Start()
    {
        // Load các case từ CaseData
        CaseData.LoadCases();

        // Thiết lập case hiện tại
        currentCase = DateTime.Now.Hour % 24; // Chọn case dựa trên giờ hiện tại
        if (!CaseData.Cases.TryGetValue(currentCase, out var caseData))
        {
            Debug.LogError("Case không tồn tại. Đặt về case mặc định (0).");
            currentCase = 0; // Đặt case mặc định
        }

        // Lấy danh sách tên biểu tượng cho case hiện tại
        diceNames = CaseData.Cases[currentCase].Item1;
    }

    public void TriggerDiceRoll()
{
        bool isPauseCase = false;
        if(nextDice != -1)
            foreach (var dice in diceResults)
            {
                if (checkCase == dice)
                {
                    isPauseCase = true;
                    break;
                }
            }
    // Đặt giá trị xúc xắc ngẫu nhiên
    for (int i = 0; i < 3; i++)
    {
        diceResults[i] = UnityEngine.Random.Range(0, 6);
    }
    // Gán Next Dice vào một trong các xúc xắc (nếu có)
    if (nextDice != -1 && !isPauseCase)
    {
        int guaranteedIndex = UnityEngine.Random.Range(0, 3);
        diceResults[guaranteedIndex] = nextDice;
    } 

    // Hiển thị kết quả xúc xắc lên giao diện Dice UI
    for (int i = 0; i < diceObjects.Length; i++)
    {
        Image diceImage = diceObjects[i].GetComponent<Image>();
        if (diceImage != null)
        {
            diceImage.sprite = Resources.Load<Sprite>($"Ảnh Bầu Cua/{diceNames[diceResults[i]]}");
        }
    }

    // Tính toán Next Dice mới
    CalculateNextDice(diceResults);
}

    public void UpdateImage()
    {

        // Hiển thị kết quả lên các Image UI được chỉ định (Dice1, Dice2, Dice3)
        resultImage1.sprite = Resources.Load<Sprite>($"Ảnh Bầu Cua/{diceNames[diceResults[0]]}");
        resultImage2.sprite = Resources.Load<Sprite>($"Ảnh Bầu Cua/{diceNames[diceResults[1]]}");
        resultImage3.sprite = Resources.Load<Sprite>($"Ảnh Bầu Cua/{diceNames[diceResults[2]]}");

        // Debug thông tin xúc xắc đã roll
        Debug.Log($"Dice Results: {diceResults[0]} ({diceNames[diceResults[0]]}), " +
                  $"{diceResults[1]} ({diceNames[diceResults[1]]}), " +
                  $"{diceResults[2]} ({diceNames[diceResults[2]]})");

    }


    private void CalculateNextDice(int[] diceResults)
    {
        // Lấy công thức tính toán Next_dice từ case hiện tại
        var calculateNextDice = CaseData.Cases[currentCase].Item2;

        // Tính giá trị Next_dice
        nextDice = calculateNextDice(diceResults[0], diceResults[1], diceResults[2]);

        // Hiển thị công thức tính toán Next_dice
        string formulaDescription = CaseData.GetFormulaDescription(currentCase, diceResults[0], diceResults[1], diceResults[2]);
        Debug.Log($"Công thức sử dụng cho Case {currentCase}: {formulaDescription}");
        Debug.Log($"Next Dice tiếp theo: {nextDice} ({diceNames[nextDice]})");
    }
}
