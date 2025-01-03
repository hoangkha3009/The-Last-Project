using System;
using UnityEngine;

public class EvenAndOdd
{
    public static void RollDiceWithParity(int nextDice)
    {
        int x = 0, y = 0, z = 0;

        // Chọn ngẫu nhiên 1 trong 4 trường hợp
        int caseType = new System.Random().Next(1, 5); // Sử dụng System.Random

        switch (caseType)
        {
            case 1:
                // Trường hợp 1: X và Y chẵn, Next_Dice ở vị trí ngẫu nhiên
                x = GenerateEvenNumber();
                y = GenerateEvenNumber();
                z = nextDice;
                SwapRandom(ref x, ref y, ref z);
                break;

            case 2:
                // Trường hợp 2: X và Y lẻ, Next_Dice ở vị trí ngẫu nhiên
                x = GenerateOddNumber();
                y = GenerateOddNumber();
                z = nextDice;
                SwapRandom(ref x, ref y, ref z);
                break;

            case 3:
                // Trường hợp 3: X chẵn, Y lẻ, tổng X + Y là lẻ, Z = Next_Dice + 1
                x = GenerateEvenNumber();
                y = GenerateOddNumber();
                if ((x + y) % 2 == 0)
                {
                    y = GenerateOddNumber();
                }
                z = nextDice + 1;
                break;

            case 4:
                // Trường hợp 4: X lẻ, Y chẵn, tổng X + Y là lẻ, Z = Next_Dice + 1
                x = GenerateOddNumber();
                y = GenerateEvenNumber();
                if ((x + y) % 2 == 0)
                {
                    y = GenerateEvenNumber();
                }
                z = nextDice + 1;
                break;
        }

        // Tính tổng
        int total = x + y + z;
        string parity = (total % 2 == 0) ? "Chan" : "Le";

        // In kết quả
        Console.WriteLine("Ket qua xoc xac:");
        Console.WriteLine($"X = {x}, Y = {y}, Z = {z}");
        Console.WriteLine($"Next_Dice = {nextDice}");
        Console.WriteLine($"Tong = {total} ({parity})");
        Console.WriteLine($"Ben {parity} Thang");
    }

    private static void SwapRandom(ref int x, ref int y, ref int z)
    {
        // Đổi vị trí của Next_Dice (z) ngẫu nhiên với x hoặc y
        int swapChoice = new System.Random().Next(1, 4); // Sử dụng System.Random
        if (swapChoice == 1)
        {
            (x, z) = (z, x);
        }
        else if (swapChoice == 2)
        {
            (y, z) = (z, y);
        }
    }

    private static int GenerateEvenNumber()
    {
        // Sinh số chẵn ngẫu nhiên từ 2 đến 6
        System.Random random = new System.Random(); // Sử dụng System.Random
        return random.Next(1, 4) * 2;
    }

    private static int GenerateOddNumber()
    {
        // Sinh số lẻ ngẫu nhiên từ 1 đến 5
        System.Random random = new System.Random(); // Sử dụng System.Random
        return random.Next(0, 3) * 2 + 1;
    }

    // Bổ sung: Hàm xử lý chẵn/lẻ cho X, Y, Z
    public static void HandleEvenAndOddWithXYZ(int x, int y, int z)
    {
        // Tính tổng và xác định chẵn/lẻ
        int total = x + y + z;
        string parity = (total % 2 == 0) ? "Chan" : "Le";

        // Log kết quả
        Debug.Log($"Ket qua xu ly chan le: X = {x}, Y = {y}, Z = {z}");
        Debug.Log($"Tong = {total} ({parity})");
        Debug.Log($"Ben {parity} Thang");
    }

    // Bổ sung: Hàm mô phỏng xóc xúc xắc
    public static void SimulateDiceRoll()
    {
        // Sinh giá trị ngẫu nhiên cho X, Y, Z
        int x = UnityEngine.Random.Range(1, 7); // Sử dụng UnityEngine.Random
        int y = UnityEngine.Random.Range(1, 7); // Sử dụng UnityEngine.Random
        int z = UnityEngine.Random.Range(1, 7); // Sử dụng UnityEngine.Random

        // Gọi xử lý chẵn/lẻ
        HandleEvenAndOddWithXYZ(x, y, z);
    }
}
