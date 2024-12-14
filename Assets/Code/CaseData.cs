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
    new string[] { "Ca", "Bau", "Ga", "Nai", "Tom", "Cua" },
    (a, b, c) => (b + 2) % 6, // Next_dice= (B + 2) % 6
    "8A1",
    "XYZ"
);

Cases[1] = (
    new string[] { "Tom", "Ca", "Nai", "Cua", "Bau", "Ga" },
    (a, b, c) => (a + b + 2) % 6, // Next_dice= (A + B + 2) % 6
    "6B5",
    "PQR"
);

Cases[2] = (
    new string[] { "Nai", "Tom", "Bau", "Ca", "Cua", "Ga" },
    (a, b, c) => (a + b + 1) % 6, // Next_dice= (A + B + 1) % 6
    "3C5",
    "EFG"
);

Cases[3] = (
    new string[] { "Nai", "Cua", "Ca", "Tom", "Bau", "Ga" },
    (a, b, c) => (b + 1) % 6, // Next_dice= (B + 1) % 6
    "1D4",
    "ABC"
);

Cases[4] = (
    new string[] { "Ga", "Ca", "Nai", "Tom", "Bau", "Cua" },
    (a, b, c) => (a + 1) % 6, // Next_dice= (A + 1) % 6
    "2E1",
    "LMN"
);

Cases[5] = (
    new string[] { "Cua", "Bau", "Ga", "Tom", "Ca", "Nai" },
    (a, b, c) => (c + 1) % 6, // Next_dice= (C + 1) % 6
    "5F7",
    "UVW"
);

Cases[6] = (
    new string[] { "Tom", "Ga", "Nai", "Ca", "Bau", "Cua" },
    (a, b, c) => (b + 2) % 6, // Next_dice= (B + 2) % 6
    "3G9",
    "HIJ"
);

Cases[7] = (
    new string[] { "Nai", "Ga", "Ca", "Cua", "Bau", "Tom" },
    (a, b, c) => (a + c + 1) % 6, // Next_dice= (A + C + 1) % 6
    "7H2",
    "TUV"
);

Cases[8] = (
    new string[] { "Cua", "Ga", "Ca", "Tom", "Nai", "Bau" },
    (a, b, c) => (a + b + 2) % 6, // Next_dice= (A + B + 2) % 6
    "6I4",
    "JKL"
);

Cases[9] = (
    new string[] { "Cua", "Tom", "Ga", "Ca", "Nai", "Bau" },
    (a, b, c) => (a + c + 2) % 6, // Next_dice= (A + C + 2) % 6
    "4J8",
    "RST"
);

Cases[10] = (
    new string[] { "Ga", "Ca", "Cua", "Nai", "Tom", "Bau" },
    (a, b, c) => (b + c + 1) % 6, // Next_dice= (B + C + 1) % 6
    "3K8",
    "OPQ"
);

Cases[11] = (
    new string[] { "Ga", "Tom", "Ca", "Cua", "Bau", "Nai" },
    (a, b, c) => (b + c + 1) % 6, // Next_dice= (B + C + 1) % 6
    "5L3",
    "DEF"
);

Cases[12] = (
    new string[] { "Ca", "Bau", "Tom", "Ga", "Cua", "Nai" },
    (a, b, c) => (b + c + 2) % 6, // Next_dice= (B + C + 2) % 6
    "5M6",
    "FGH"
);

Cases[13] = (
    new string[] { "Ca", "Bau", "Nai", "Tom", "Cua", "Ga" },
    (a, b, c) => (a + b + 1) % 6, // Next_dice= (A + B + 1) % 6
    "1N3",
    "KLM"
);

Cases[14] = (
    new string[] { "Cua", "Ca", "Bau", "Nai", "Tom", "Ga" },
    (a, b, c) => (c + 1) % 6, // Next_dice= (C + 1) % 6
    "5O1",
    "YZA"
);

Cases[15] = (
    new string[] { "Cua", "Tom", "Ga", "Nai", "Ca", "Bau" },
    (a, b, c) => (b + 2) % 6, // Next_dice= (B + 2) % 6
    "2P7",
    "WXY"
);

Cases[16] = (
    new string[] { "Ga", "Cua", "Nai", "Bau", "Tom", "Ca" },
    (a, b, c) => (b + c + 2) % 6, // Next_dice= (B + C + 2) % 6
    "9Q8",
    "NOP"
);

Cases[17] = (
    new string[] { "Nai", "Cua", "Ca", "Tom", "Ga", "Bau" },
    (a, b, c) => (a + c + 2) % 6, // Next_dice= (A + C + 2) % 6
    "7R0",
    "GHI"
);

Cases[18] = (
    new string[] { "Tom", "Ga", "Cua", "Ca", "Nai", "Bau" },
    (a, b, c) => (b + 1) % 6, // Next_dice= (B + 1) % 6
    "4S9",
    "STU"
);

Cases[19] = (
    new string[] { "Nai", "Ga", "Cua", "Tom", "Bau", "Ca" },
    (a, b, c) => (a + 1) % 6, // Next_dice= (A + 1) % 6
    "8T7",
    "ZAB"
);

Cases[20] = (
    new string[] { "Ca", "Cua", "Tom", "Ga", "Nai", "Bau" },
    (a, b, c) => (a + 2) % 6, // Next_dice= (A + 2) % 6
    "6U0",
    "ABC"
);

Cases[21] = (
    new string[] { "Ga", "Nai", "Tom", "Cua", "Bau", "Ca" },
    (a, b, c) => (c + 1) % 6, // Next_dice= (C + 1) % 6
    "2V9",
    "PQR"
);

Cases[22] = (
    new string[] { "Tom", "Ca", "Bau", "Ga", "Nai", "Cua" },
    (a, b, c) => (a + b + 2) % 6, // Next_dice= (A + B + 2) % 6
    "3W0",
    "EFG"
);

Cases[23] = (
    new string[] { "Tom", "Nai", "Ca", "Ga", "Cua", "Bau" },
    (a, b, c) => (a + c + 2) % 6, // Next_dice= (A + C + 2) % 6
    "1X7",
    "RST"
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
    return "Công thức: B + 2\nThứ tự: Ca, Bau, Ga, Nai, Tom, Cua";
case 1:
    return "Công thức: A + B + 2\nThứ tự: Tom, Ca, Nai, Cua, Bau, Ga";
case 2:
    return "Công thức: A + B + 1\nThứ tự: Nai, Tom, Bau, Ca, Cua, Ga";
case 3:
    return "Công thức: B + 1\nThứ tự: Nai, Cua, Ca, Tom, Bau, Ga";
case 4:
    return "Công thức: A + 1\nThứ tự: Ga, Ca, Nai, Tom, Bau, Cua";
case 5:
    return "Công thức: C + 1\nThứ tự: Cua, Bau, Ga, Tom, Ca, Nai";
case 6:
    return "Công thức: B + 2\nThứ tự: Tom, Ga, Nai, Ca, Bau, Cua";
case 7:
    return "Công thức: A + C + 1\nThứ tự: Nai, Ga, Ca, Cua, Bau, Tom";
case 8:
    return "Công thức: A + B + 2\nThứ tự: Cua, Ga, Ca, Tom, Nai, Bau";
case 9:
    return "Công thức: A + C + 2\nThứ tự: Cua, Tom, Ga, Ca, Nai, Bau";
case 10:
    return "Công thức: B + C + 1\nThứ tự: Ga, Ca, Cua, Nai, Tom, Bau";
case 11:
    return "Công thức: B + C + 1\nThứ tự: Ga, Tom, Ca, Cua, Bau, Nai";
case 12:
    return "Công thức: B + C + 2\nThứ tự: Nai, Bau, Cua, Ca, Tom, Ga";
case 13:
    return "Công thức: A + B + 1\nThứ tự: Ga, Nai, Ca, Tom, Bau, Cua";
case 14:
    return "Công thức: C + 1\nThứ tự: Cua, Ca, Bau, Nai, Tom, Ga";
case 15:
    return "Công thức: B + 2\nThứ tự: Ga, Tom, Nai, Bau, Cua, Ca";
case 16:
    return "Công thức: B + C + 2\nThứ tự: Ga, Nai, Bau, Ca, Tom, Cua";
case 17:
    return "Công thức: A + C + 2\nThứ tự: Nai, Cua, Ca, Tom, Ga, Bau";
case 18:
    return "Công thức: B + 1\nThứ tự: Bau, Ga, Tom, Cua, Nai, Ca";
case 19:
    return "Công thức: A + 1\nThứ tự: Ga, Nai, Ca, Tom, Bau, Cua";
case 20:
    return "Công thức: A + 2\nThứ tự: Nai, Bau, Cua, Ca, Tom, Ga";
case 21:
    return "Công thức: C + 1\nThứ tự: Cua, Tom, Nai, Ga, Bau, Ca";
case 22:
    return "Công thức: A + B + 2\nThứ tự: Bau, Nai, Tom, Ca, Ga, Cua";
case 23:
    return "Công thức: A + C + 2\nThứ tự: Tom, Nai, Ca, Ga, Cua, Bau";
default:
    return "Công thức không tồn tại.";

}

}



	
}