using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;

public class APIHander : MonoBehaviour
{
    public static string API_PATH_GET_LIST_ID_USER = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/id";
    public static string API_PATH_GET_DATA_BY_ID_USER = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/id/";
    public static string API_PATH_POST_DICE_DATA_BY_ID_USER = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/updateABCUser/ID/";
    public static string API_PATH_PATCH_THREE_TOUCH_POINT_BY_ID_USER = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/ThreeTouchPoints/id/";
    public static string API_PATH_PATCH_THREE_TOUCH_POINT = "https://bautomcua-a8b183da803d.herokuapp.com/api/config/ThreeTouchPoints/";
    public const string API_KEY = "f47ac10b-58cc-4372-a567-0e02b2c3d479";
    public static APIHander Instance;

    public enum TypeMothod
    {
        GET,
        POST, 
        PUT, 
        PATCH,
        DELETE
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public async Task<T> GetData<T>(string apiPath)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiPath);
        request.SetRequestHeader("X-API-KEY", API_KEY);
        await request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Lấy dữ liệu từ server
            Debug.Log("Response: " + request.downloadHandler.text);
            T res = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
            return res;
        }
        return default(T);
    }

    public async void SubmitData<T>(T jsonData, string stringPath, TypeMothod typeMothod, UnityAction action = null)
    {
        string jsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonString);
        UnityWebRequest request = new UnityWebRequest(stringPath, $"{typeMothod}");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("X-API-KEY", API_KEY);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}

public class Config
{
    public string Id { get; set; }
    public List<string> Order { get; set; }
    public Formula Formula { get; set; }
    public List<string> CurrentABC { get; set; }
    public string ChangeABC { get; set; }

    public Config(string id, List<string> order, Formula formula, List<string> currentABC, string changeABC)
    {
        Id = id;
        Order = order;
        Formula = formula;
        CurrentABC = currentABC;
        ChangeABC = changeABC;
    }
}

public class Formula
{
    public string Selected { get; set; }
    public int N { get; set; }
    public List<string> Options { get; set; }

    public Formula(string selected, int n, List<string> options)
    {
        Selected = selected;
        N = n;
        Options = options;
    }
}

public class User
{
    public string Id { get; set; } // "id"
    public string StatusPrefPlayer { get; set; } // "statusPrefPlayer"
    public DateTime LastOnlineTime { get; set; } // "lastOnlineTime"
    public TimeSpan TimeOffline { get; set; } // "timeOffline"
    public List<string> CurrentABC { get; set; } // "currentABC"
    public List<string> NewABC { get; set; } // "newABC"
    public string ChangeABC { get; set; } // "changeABC"
    public DateTime LogTime { get; set; } // "logTime"
    public DateTime CreateIDTime { get; set; } // "createIDTime"
    public string ThreeTouchPoints { get; set; } // "threeTouchPoints"
    public List<string> Order { get; set; } // "order"
    public int onlineCase { get; set; }
    public string case1 { get; set; }
    public string case2 { get; set; }
    public string case3 { get; set; }
    public string case4 { get; set; }
    public int N { get; set; }
    public string onlineRule { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, StatusPrefPlayer: {StatusPrefPlayer}, LastOnlineTime: {LastOnlineTime}, " +
               $"TimeOffline: {TimeOffline}, CurrentABC: [{string.Join(", ", CurrentABC)}], " +
               $"NewABC: [{string.Join(", ", NewABC)}], ChangeABC: {ChangeABC}, LogTime: {LogTime}, " +
               $"CreateIDTime: {CreateIDTime}, ThreeTouchPoints: {ThreeTouchPoints}, " +
               $"Order: [{string.Join(", ", Order)}]";
    }
}
//