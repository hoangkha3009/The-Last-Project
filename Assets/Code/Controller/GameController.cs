using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private DiceController diceController;
    private bool isCheckThreePoint = false;
    public Dictionary<string, int> Mapping = new Dictionary<string, int>();

    private User user = null;
    private int nextDice = 0;

    [SerializeField] private GameObject grBtnThree;

    public bool isFirstDiceOnl = false;
    public int indexCheck = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public async void CheckThreePoint(int indexCheck = 0, UnityAction action = null)
    {
        ResponseBodyUser responseBodyUser = await APIHander.Instance.GetData<ResponseBodyUser>(APIHander.API_PATH_GET_DATA_BY_ID_USER + PlayerPrefs.GetString("PrefPlayerID"));
        if (responseBodyUser != null && this.indexCheck == indexCheck)
        {
            if(!isFirstDiceOnl)
            {
                isFirstDiceOnl = true;
                Mapping.Clear();
                for (int i = 0; i < responseBodyUser.User.Order.Count; i++)
                    Mapping.Add(responseBodyUser.User.Order[i], i);

                bool onlineRule = responseBodyUser.User.onlineRule.ToLower() == "yes";
                grBtnThree.SetActive(responseBodyUser.User.ThreeTouchPoints.ToLower() == "yes");
                if (responseBodyUser.User.ThreeTouchPoints.ToLower() == "yes")
                {
                    isCheckThreePoint = diceController.UpdateTrangThaiThreePoint(true, ProcessData(responseBodyUser.User), onlineRule, user.Order);
                }
                else
                    isCheckThreePoint = diceController.UpdateTrangThaiThreePoint(false, ProcessData(responseBodyUser.User), onlineRule, user.Order);
                action?.Invoke();
            }
        }
        if (isCheckThreePoint)
        {
            CheckThreePoint();
        }
    }

    private int CalculateNextDice(User user)
    {
        int a = Mapping[user.CurrentABC[0]];
        int b = Mapping[user.CurrentABC[1]];
        int c = Mapping[user.CurrentABC[2]];
        int n = user.N;
        int result;

        string str = "";
        foreach (var item in user.Order)
            str += item + "\t";
        Debug.LogError(str);
        Debug.LogError(user.onlineCase + " - " + a + " - " + b + " - " + c);
        switch (user.onlineCase)
        {
            case 1:
                result = a + b + c + n;
                break;
            case 2:
                result = a * 2 + b + c + n;
                break;
            case 3:
                result = b * 2 + c + n;
                break;
            case 4:
                result = c * 2 + a + n;
                break;
            default:
                result = 0;
                break;
        }

        result %= 6;
        while (result > 5)
        {
            result %= 6;
        }
        Debug.LogError(result);
        return result;
    }

    public (int, List<int>) ProcessData(User user)
    {
        nextDice = CalculateNextDice(user);
        this.user = user;
        var newABC = RandomizeNewABC();
        return (nextDice, newABC);
        // Send NewABC to the server
        //UpdateNewABC(user.ID, newABC);
    }

    public List<int> RandomizeNewABC(int indexButton = -1)
    {
        List<int> newABC = new List<int>();

        if (user.ThreeTouchPoints == "NO" || indexButton == -1)
        {
            int position = Random.Range(0, 3); // Random position for NextDice

            for (int i = 0; i < 3; i++)
            {
                if (i == position)
                {
                    newABC.Add(nextDice);
                }
                else
                {
                    newABC.Add(Random.Range(0, 6));
                }
            }
        }
        else
        {
            List<int> restrictedNumbers = GetRestrictedNumbers(user, indexButton);

            for (int i = 0; i < 3; i++)
            {
                int randomValue;
                do
                {
                    randomValue = Random.Range(0, 6);
                } while (restrictedNumbers.Contains(randomValue));

                newABC.Add(randomValue);
            }
        }

        return newABC;
    }

    private List<int> GetRestrictedNumbers(User user, int indexButton)
    {
        List<int> restricted = new List<int>();
        Debug.Log("Button " + indexButton);
        int count = user.Order.Count / 3;
        int start = indexButton * count;
        int end = start + count;

        for (int i = start; i < end; i++)
        {
            restricted.Add(Mapping[user.Order[i]]);
        }

        return restricted;
    }

    [System.Serializable]
    private class UserData
    {
        public string ID;
        public int OnlineCase;
        public List<string> CurrentABC;
        public List<string> Order;
        public string ThreeTouchPoints;
        public int N;
    }
}
