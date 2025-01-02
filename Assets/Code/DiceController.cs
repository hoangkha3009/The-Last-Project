using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RequestBodyDiceData
{
    public List<string> NewABC = new List<string>();
}

public class ResponseBodyUser
{
    public bool Success { get; set; }
    public User User { get; set; }
}

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

    private List<string> listCurDice;
    private bool isXoc = false;
    private bool isThreePoint = false;

    [SerializeField] private Button btnMo;

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
        listCurDice = new List<string>();

        btnMo.onClick.AddListener(() => { isXoc = false; });
    }

    public bool UpdateTrangThaiThreePoint(bool isThree, (int, List<int>) box, bool onlineRule)
    {
        if(isXoc && onlineRule)
        {
            TriggerDiceRollOnl(box);
        }
        isThreePoint = isThree;
        return isXoc;
    }

    public void TriggerDiceRollOnl((int, List<int>) box)
    {
        isXoc = true;

        diceResults = new int[0];
        diceResults = box.Item2.ToArray();

        PutData(diceResults);

        // Hiển thị kết quả xúc xắc lên giao diện Dice UI
        for (int i = 0; i < diceObjects.Length; i++)
        {
            HienThiXucXac(i, diceNames[diceResults[i]]);
        }
    }

    public void TriggerDiceRoll()
    {
        isXoc = true;
        bool isPauseCase = false;

        GameController.Instance.CheckThreePoint();

        if(nextDice != -1)
            foreach (var dice in diceResults)
            {
                if (checkCase == dice)
                {
					Debug.Log("Vao Case Random");
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

        PutData(diceResults);

        // Hiển thị kết quả xúc xắc lên giao diện Dice UI
        for (int i = 0; i < diceObjects.Length; i++)
        {
            HienThiXucXac(i, diceNames[diceResults[i]]);
        }

        // Tính toán Next Dice mới
        CalculateNextDice(diceResults);
    }

    private void HienThiXucXac(int index, string nameXucXac)
    {
        Image diceImage = diceObjects[index].GetComponent<Image>();
        if (diceImage != null)
        {
            diceImage.sprite = Resources.Load<Sprite>($"Ảnh Bầu Cua/{nameXucXac}");
        }
    }

    private void OpenDice()
    {
        isXoc = false;
    }
    public void OnClickOpen(int index)
    {
        if (isThreePoint)
        {
            diceResults = GameController.Instance.RandomizeNewABC(index).ToArray();
            PutData(diceResults);

            // Hiển thị kết quả xúc xắc lên giao diện Dice UI
            for (int i = 0; i < diceObjects.Length; i++)
            {
                HienThiXucXac(i, diceNames[diceResults[i]]);
            }
        }
    }

    private void PutData(int[] diceResults)
    {
        RequestBodyDiceData requestBodyDiceData = new RequestBodyDiceData();
        foreach (var dice in diceResults)
        {
            requestBodyDiceData.NewABC.Add(diceNames[dice]);
        }
        listCurDice = requestBodyDiceData.NewABC;
        APIHander.Instance.SubmitData(requestBodyDiceData, APIHander.API_PATH_POST_DICE_DATA_BY_ID_USER + PlayerPrefs.GetString("PrefPlayerID"), APIHander.TypeMothod.POST, () => {
            CheckData();
        });
    }

    private async void CheckData(UnityAction action = null)
    {
        ResponseBodyUser responseBodyUser = await APIHander.Instance.GetData<ResponseBodyUser>(APIHander.API_PATH_GET_DATA_BY_ID_USER + PlayerPrefs.GetString("PrefPlayerID"));
        if(responseBodyUser == null)
            return;

        action?.Invoke();
        listCurDice = responseBodyUser.User.CurrentABC;
        if (isXoc) 
        {
            for (int i = 0; i < diceObjects.Length; i++)
            {
                HienThiXucXac(i, listCurDice[i]);
            }
            CheckData();
        }
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
