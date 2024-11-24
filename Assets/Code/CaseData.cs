using System;
using System.Collections.Generic;

class CaseData
{
    public static Dictionary<int, (string[], Func<int, int, int, int>)> Cases = new Dictionary<int, (string[], Func<int, int, int, int>)>();

    public static void LoadCases()
{
    Cases[0] = (
        new string[] { "Bau", "Cua", "Ca", "Ga", "Tom", "Nai" },
        (a, b, c) => (a + 2) % 6 // Next_dice= (A + 2) % 6
    );

    Cases[1] = (
        new string[] { "Ca", "Nai", "Ga", "Bau", "Tom", "Cua" },
        (a, b, c) => (b + 2) % 6 // Next_dice= (B + 2) % 6
    );

    Cases[2] = (
        new string[] { "Cua", "Nai", "Bau", "Ca", "Tom", "Ga" },
        (a, b, c) => (a + b + 1) % 6 // Next_dice= (A + B + 1) % 6
    );

    Cases[3] = (
        new string[] { "Cua", "Ca", "Ga", "Tom", "Nai", "Bau" },
        (a, b, c) => (b + 1) % 6 // Next_dice= (B + 1) % 6
    );

    Cases[4] = (
        new string[] { "Nai", "Ca", "Tom", "Ga", "Cua", "Bau" },
        (a, b, c) => (a + b + 2) % 6 // Next_dice= (A + B + 2) % 6
    );

    Cases[5] = (
        new string[] { "Tom", "Cua", "Bau", "Ga", "Nai", "Ca" },
        (a, b, c) => (b + c + 2) % 6 // Sửa thành Next_dice= (B + C + 2) % 6
    );

    Cases[6] = (
        new string[] { "Ca", "Ga", "Tom", "Nai", "Bau", "Cua" },
        (a, b, c) => (c + 1) % 6 // Next_dice= (C + 1) % 6
    );

    Cases[7] = (
        new string[] { "Tom", "Nai", "Bau", "Cua", "Ga", "Ca" },
        (a, b, c) => (a + 1) % 6 // Next_dice= (A + 1) % 6
    );

    Cases[8] = (
        new string[] { "Tom", "Ca", "Nai", "Bau", "Ga", "Cua" },
        (a, b, c) => (a + c + 1) % 6 // Next_dice= (A + C + 1) % 6
    );

    Cases[9] = (
        new string[] { "Tom", "Nai", "Bau", "Cua", "Ga", "Ca" },
        (a, b, c) => (b + 2) % 6 // Next_dice= (B + 2) % 6
    );

    Cases[10] = (
        new string[] { "Bau", "Ga", "Cua", "Ca", "Nai", "Tom" },
        (a, b, c) => (b + c + 1) % 6 // Next_dice= (B + C + 1) % 6
    );

    Cases[11] = (
        new string[] { "Bau", "Tom", "Ga", "Cua", "Nai", "Ca" },
        (a, b, c) => (a + b + 2) % 6 // Next_dice= (A + B + 2) % 6
    );

    Cases[12] = (
        new string[] { "Nai", "Bau", "Cua", "Ca", "Tom", "Ga" },
        (a, b, c) => (a + 1) % 6 // Next_dice= (A + 1) % 6
    );

    Cases[13] = (
        new string[] { "Ga", "Nai", "Ca", "Tom", "Bau", "Cua" },
        (a, b, c) => (a + c + 2) % 6 // Next_dice= (A + C + 2) % 6
    );

    Cases[14] = (
        new string[] { "Ga", "Ca", "Bau", "Nai", "Cua", "Tom" },
        (a, b, c) => (b + c + 1) % 6 // Next_dice= (B + C + 1) % 6
    );

    Cases[15] = (
        new string[] { "Ga", "Tom", "Nai", "Bau", "Cua", "Ca" },
        (a, b, c) => (c + 1) % 6 // Next_dice= (C + 1) % 6
    );

    Cases[16] = (
        new string[] { "Cua", "Bau", "Nai", "Ga", "Ca", "Tom" },
        (a, b, c) => (b + 2) % 6 // Next_dice= (B + 2) % 6
    );

    Cases[17] = (
        new string[] { "Ca", "Tom", "Ga", "Nai", "Bau", "Cua" },
        (a, b, c) => (a + c + 2) % 6 // Next_dice= (A + C + 2) % 6
    );

    Cases[18] = (
        new string[] { "Bau", "Ga", "Tom", "Cua", "Nai", "Ca" },
        (a, b, c) => (a + 1) % 6 // Next_dice= (A + 1) % 6
    );

    Cases[19] = (
        new string[] { "Tom", "Ga", "Ca", "Nai", "Bau", "Cua" },
        (a, b, c) => (a + b + 2) % 6 // Next_dice= (A + B + 2) % 6
    );

    Cases[20] = (
        new string[] { "Nai", "Bau", "Cua", "Ca", "Tom", "Ga" },
        (a, b, c) => (a + b + 1) % 6 // Next_dice= (A + B + 1) % 6
    );

    Cases[21] = (
        new string[] { "Cua", "Tom", "Nai", "Ga", "Bau", "Ca" },
        (a, b, c) => (b + 1) % 6 // Next_dice= (B + 1) % 6
    );

    Cases[22] = (
        new string[] { "Bau", "Nai", "Tom", "Ca", "Ga", "Cua" },
        (a, b, c) => (c + 1) % 6 // Next_dice= (C + 1) % 6
    );

    Cases[23] = (
        new string[] { "Tom", "Nai", "Ca", "Ga", "Cua", "Bau" },
        (a, b, c) => (b + c + 2) % 6 // Next_dice= (B + C + 2) % 6
    );
}

    public static string GetFormulaDescription(int caseChoice, int a, int b, int c)
    {
        switch (caseChoice)
        {
            case 0:
                return $"Cong thuc: (A + 2) % 6 => ({a} + 2) % 6 = {(a + 2) % 6}";
            case 1:
                return $"Cong thuc: (B + 2) % 6 => ({b} + 2) % 6 = {(b + 2) % 6}";
            case 23:
                return $"Cong thuc: (B + C + 2) % 6 => ({b} + {c} + 2) % 6 = {(b + c + 2) % 6}";
            default:
                return "Cong thuc khong ton tai.";
        }
    }
}