using System;
using System.Collections.Generic;
using UnityEngine;

public class CaseData
{
    public static Dictionary<int, (string[], Func<int, int, int, int>, string, string)> Cases =
        new Dictionary<int, (string[], Func<int, int, int, int>, string, string)>();

    public static void LoadCases()
    {
        Cases[0] = (
            new string[] { "Bau", "Cua", "Ca", "Ga", "Tom", "Nai" },
            (a, b, c) => (a + 2) % 6, // Next_dice= (A + 2) % 6
            "234",
            "DEF"
        );

        Cases[1] = (
            new string[] { "Ca", "Nai", "Ga", "Bau", "Tom", "Cua" },
            (a, b, c) => (b + 2) % 6, // Next_dice= (B + 2) % 6
            "345",
            "EFG"
        );

        Cases[2] = (
            new string[] { "Cua", "Nai", "Bau", "Ca", "Tom", "Ga" },
            (a, b, c) => (a + b + 1) % 6, // Next_dice= (A + B + 1) % 6
            "456",
            "FGH"
        );
		Cases[3] = (
            new string[] { "Cua", "Ca", "Ga", "Tom", "Nai", "Bau" },
            (a, b, c) => (b + 1) % 6, // Next_dice= (B + 1) % 6
            "567",
            "GHI"
        );

        Cases[4] = (
            new string[] { "Nai", "Ca", "Tom", "Ga", "Cua", "Bau" },
            (a, b, c) => (a + b + 2) % 6, // Next_dice= (A + B + 2) % 6
            "678",
            "HIJ"
        );

        Cases[5] = (
            new string[] { "Tom", "Cua", "Bau", "Ga", "Nai", "Ca" },
            (a, b, c) => (b + c + 2) % 6, // Next_dice= (B + C + 2) % 6
            "789",
            "IJK"
        );

        Cases[6] = (
            new string[] { "Ca", "Ga", "Tom", "Nai", "Bau", "Cua" },
            (a, b, c) => (c + 1) % 6, // Next_dice= (C + 1) % 6
            "890",
            "JKL"
        );

        Cases[7] = (
            new string[] { "Tom", "Nai", "Bau", "Cua", "Ga", "Ca" },
            (a, b, c) => (a + 1) % 6, // Next_dice= (A + 1) % 6
            "901",
            "KLM"
        );

        Cases[8] = (
            new string[] { "Tom", "Ca", "Nai", "Bau", "Ga", "Cua" },
            (a, b, c) => (a + c + 1) % 6, // Next_dice= (A + C + 1) % 6
            "012",
            "LMN"
        );

        Cases[9] = (
            new string[] { "Tom", "Nai", "Bau", "Cua", "Ga", "Ca" },
            (a, b, c) => (b + 2) % 6, // Next_dice= (B + 2) % 6
            "123",
            "MNO"
        );

        Cases[10] = (
            new string[] { "Bau", "Ga", "Cua", "Ca", "Nai", "Tom" },
            (a, b, c) => (b + c + 1) % 6, // Next_dice= (B + C + 1) % 6
            "234",
            "NOP"
        );

        Cases[11] = (
            new string[] { "Bau", "Tom", "Ga", "Cua", "Nai", "Ca" },
            (a, b, c) => (a + b + 2) % 6, // Next_dice= (A + B + 2) % 6
            "345",
            "OPQ"
        );

        Cases[12] = (
            new string[] { "Nai", "Bau", "Cua", "Ca", "Tom", "Ga" },
            (a, b, c) => (a + 1) % 6, // Next_dice= (A + 1) % 6
            "456",
            "PQR"
        );

        Cases[13] = (
            new string[] { "Ga", "Nai", "Ca", "Tom", "Bau", "Cua" },
            (a, b, c) => (a + c + 2) % 6, // Next_dice= (A + C + 2) % 6
            "567",
            "QRS"
        );

        Cases[14] = (
            new string[] { "Ga", "Ca", "Bau", "Nai", "Cua", "Tom" },
            (a, b, c) => (b + c + 1) % 6, // Next_dice= (B + C + 1) % 6
            "678",
            "RST"
        );

        Cases[15] = (
            new string[] { "Ga", "Tom", "Nai", "Bau", "Cua", "Ca" },
            (a, b, c) => (c + 1) % 6, // Next_dice= (C + 1) % 6
            "789",
            "STU"
        );

        Cases[16] = (
            new string[] { "Cua", "Bau", "Nai", "Ga", "Ca", "Tom" },
            (a, b, c) => (b + 2) % 6, // Next_dice= (B + 2) % 6
            "890",
            "TUV"
        );

        Cases[17] = (
            new string[] { "Ca", "Tom", "Ga", "Nai", "Bau", "Cua" },
            (a, b, c) => (a + c + 2) % 6, // Next_dice= (A + C + 2) % 6
            "901",
            "UVW"
        );

        Cases[18] = (
            new string[] { "Bau", "Ga", "Tom", "Cua", "Nai", "Ca" },
            (a, b, c) => (a + 1) % 6, // Next_dice= (A + 1) % 6
            "012",
            "VWX"
        );

        Cases[19] = (
            new string[] { "Tom", "Ga", "Ca", "Nai", "Bau", "Cua" },
            (a, b, c) => (a + b + 2) % 6, // Next_dice= (A + B + 2) % 6
            "123",
            "WXY"
        );

        Cases[20] = (
            new string[] { "Nai", "Bau", "Cua", "Ca", "Tom", "Ga" },
            (a, b, c) => (a + b + 1) % 6, // Next_dice= (A + B + 1) % 6
            "234",
            "XYZ"
        );

        Cases[21] = (
            new string[] { "Cua", "Tom", "Nai", "Ga", "Bau", "Ca" },
            (a, b, c) => (b + 1) % 6, // Next_dice= (B + 1) % 6
            "345",
            "YZA"
        );

        Cases[22] = (
            new string[] { "Bau", "Nai", "Tom", "Ca", "Ga", "Cua" },
            (a, b, c) => (c + 1) % 6, // Next_dice= (C + 1) % 6
            "456",
            "ZAB"
        );

        // Các case khác giữ nguyên...
        Cases[23] = (
            new string[] { "Tom", "Nai", "Ca", "Ga", "Cua", "Bau" },
            (a, b, c) => (b + c + 2) % 6, // Next_dice= (B + C + 2) % 6
            "567",
            "ABC"
        );
    }
	public static (string code, string description) GetCaseCodeAndDescription(int caseChoice)
    {
        if (Cases.ContainsKey(caseChoice))
        {
            var caseData = Cases[caseChoice];
            return (caseData.Item3, caseData.Item4);
        }
        return ("Unknown", "Case not found.");
    }
	public static void UpdateCaseWithRandomValues(int caseIndex)
{
    if (Cases.ContainsKey(caseIndex))
    {
        var currentCase = Cases[caseIndex];

        // Tạo giá trị ngẫu nhiên cho Item3 và Item4
        string randomItem3 = GenerateRandomNumbers();
        string randomItem4 = GenerateRandomLetters();

        // Cập nhật lại case
        Cases[caseIndex] = (
            currentCase.Item1, // Danh sách biểu tượng
            currentCase.Item2, // Logic
            randomItem3,       // Item3 (Số ngẫu nhiên)
            randomItem4        // Item4 (Chữ ngẫu nhiên)
        );
    }
    else
    {
        Debug.LogError($"CaseIndex {caseIndex} không tồn tại trong CaseData!");
    }
    }

    private static string GenerateRandomNumbers()
    {
        System.Random rand = new System.Random();
        return $"{rand.Next(0, 10)}{rand.Next(0, 10)}{rand.Next(0, 10)}";
    }

    private static string GenerateRandomLetters()
    {
        System.Random rand = new System.Random();
        char RandomLetter() => (char)rand.Next('A', 'Z' + 1);
        return $"{RandomLetter()}{RandomLetter()}{RandomLetter()}";
    }


   public static string GetFormulaDescription(int caseChoice, int a, int b, int c)
{
    switch (caseChoice)
{
    case 0:
        return "Công thức: A + 2\nThứ tự: Bau, Cua, Ca, Ga, Tom, Nai";
    case 1:
        return "Công thức: B + 2\nThứ tự: Ca, Nai, Ga, Bau, Tom, Cua";
    case 2:
        return "Công thức: A + B + 1\nThứ tự: Cua, Nai, Bau, Ca, Tom, Ga";
    case 3:
        return "Công thức: B + 1\nThứ tự: Cua, Ca, Ga, Tom, Nai, Bau";
    case 4:
        return "Công thức: A + B + 2\nThứ tự: Nai, Ca, Tom, Ga, Cua, Bau";
    case 5:
        return "Công thức: B + C + 2\nThứ tự: Tom, Cua, Bau, Ga, Nai, Ca";
    case 6:
        return "Công thức: C + 1\nThứ tự: Ca, Ga, Tom, Nai, Bau, Cua";
    case 7:
        return "Công thức: A + 1\nThứ tự: Tom, Nai, Bau, Cua, Ga, Ca";
    case 8:
        return "Công thức: A + C + 1\nThứ tự: Tom, Ca, Nai, Bau, Ga, Cua";
    case 9:
        return "Công thức: B + 2\nThứ tự: Tom, Nai, Bau, Cua, Ga, Ca";
    case 10:
        return "Công thức: B + C + 1\nThứ tự: Bau, Ga, Cua, Ca, Nai, Tom";
    case 11:
        return "Công thức: A + B + 2\nThứ tự: Bau, Tom, Ga, Cua, Nai, Ca";
    case 12:
        return "Công thức: A + 1\nThứ tự: Nai, Bau, Cua, Ca, Tom, Ga";
    case 13:
        return "Công thức: A + C + 2\nThứ tự: Ga, Nai, Ca, Tom, Bau, Cua";
    case 14:
        return "Công thức: B + C + 1\nThứ tự: Ga, Ca, Bau, Nai, Cua, Tom";
    case 15:
        return "Công thức: C + 1\nThứ tự: Ga, Tom, Nai, Bau, Cua, Ca";
    case 16:
        return "Công thức: B + 2\nThứ tự: Cua, Bau, Nai, Ga, Ca, Tom";
    case 17:
        return "Công thức: A + C + 2\nThứ tự: Ca, Tom, Ga, Nai, Bau, Cua";
    case 18:
        return "Công thức: A + 1\nThứ tự: Bau, Ga, Tom, Cua, Nai, Ca";
    case 19:
        return "Công thức: A + B + 2\nThứ tự: Tom, Ga, Ca, Nai, Bau, Cua";
    case 20:
        return "Công thức: A + B + 1\nThứ tự: Nai, Bau, Cua, Ca, Tom, Ga";
    case 21:
        return "Công thức: B + 1\nThứ tự: Cua, Tom, Nai, Ga, Bau, Ca";
    case 22:
        return "Công thức: C + 1\nThứ tự: Bau, Nai, Tom, Ca, Ga, Cua";
    case 23:
        return "Công thức: B + C + 2\nThứ tự: Tom, Nai, Ca, Ga, Cua, Bau";
    default:
        return "Công thức không tồn tại.";
}

}



	
}