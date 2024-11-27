using UnityEngine;
using System;
using System.Collections.Generic;

public static class CaseDatas
{
    public static Dictionary<int, (string[], Func<int, int, int, int>, string, string)> Cases =
        new Dictionary<int, (string[], Func<int, int, int, int>, string, string)>();

    public static void LoadCases()
    {
        bool hasNetwork = IsNetworkAvailable();

        foreach (var key in Cases.Keys)
        {
            var currentCase = Cases[key];

            string item3 = hasNetwork ? GenerateRandomNumbers() : currentCase.Item3;
            string item4 = hasNetwork ? GenerateRandomLetters() : currentCase.Item4;

            Cases[key] = (
                currentCase.Item1, // Danh sách biểu tượng
                currentCase.Item2, // Logic
                item3,             // Item3
                item4              // Item4
            );
        }
    }

    // Kiểm tra mạng
    static bool IsNetworkAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    // Tạo 3 số ngẫu nhiên
    static string GenerateRandomNumbers()
    {
        System.Random rand = new System.Random();
        return $"{rand.Next(0, 10)}{rand.Next(0, 10)}{rand.Next(0, 10)}";
    }

    // Tạo 3 chữ cái ngẫu nhiên
    static string GenerateRandomLetters()
    {
        System.Random rand = new System.Random();
        char RandomLetter() => (char)rand.Next('A', 'Z' + 1);
        return $"{RandomLetter()}{RandomLetter()}{RandomLetter()}";
    }
}
