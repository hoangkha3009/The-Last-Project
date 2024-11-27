﻿using System;

class Program
{
    static void Main(string[] args)
    {
        // Khởi tạo dữ liệu từ CaseData
        CaseData.LoadCases();

        Console.WriteLine("Chao mung ban den voi game Bau Tom Cua Ca");

        // Xác định Case hiện tại dựa trên giờ
        int currentCase = DateTime.Now.Hour % 24;
        if (!CaseData.Cases.ContainsKey(currentCase))
        {
            Console.WriteLine("Case " + currentCase + " khong ton tai. Dat ve Case mac dinh 0");
            currentCase = 0;
        }

        // Lấy danh sách biểu tượng cho Case hiện tại
        string[] diceNames = CaseData.Cases[currentCase].Item1;
        Console.WriteLine("Case " + currentCase + ": " + string.Join(", ", diceNames));

        // Giá trị Next Dice ban đầu
        int nextDice = new Random().Next(0, diceNames.Length);
        Console.WriteLine("Next Dice ban dau: " + diceNames[nextDice] + " (" + nextDice + ")");

        // Vòng lặp chính của game
        while (true)
        {
            Console.WriteLine("\n--- Lua chon cua ban ---");
            Console.WriteLine("Nhan Enter de xoc xuc xac hoac q de thoat");
            string input = Console.ReadLine();

            if (input.ToLower() == "q")
            {
                Console.WriteLine("Cam on ban da choi game! Hen gap lai");
                break;
            }

            // Sinh ngẫu nhiên giá trị 3 xúc xắc
            int dice1 = new Random().Next(0, diceNames.Length);
            int dice2 = new Random().Next(0, diceNames.Length);
            int dice3 = new Random().Next(0, diceNames.Length);

            // Gán Next Dice vào một xúc xắc ngẫu nhiên
            int guaranteedIndex = new Random().Next(0, 3);
            if (guaranteedIndex == 0) dice1 = nextDice;
            if (guaranteedIndex == 1) dice2 = nextDice;
            if (guaranteedIndex == 2) dice3 = nextDice;

            // Hiển thị biểu tượng ABC của xúc xắc
            Console.WriteLine("Vien 1: " + diceNames[dice1] + " (" + dice1 + ")");
            Console.WriteLine("Vien 2: " + diceNames[dice2] + " (" + dice2 + ")");
            Console.WriteLine("Vien 3: " + diceNames[dice3] + " (" + dice3 + ")");

            // Tính toán Next Dice mới
            var nextDiceFormula = CaseData.Cases[currentCase].Item2;
            nextDice = nextDiceFormula(dice1, dice2, dice3);

            // Hiển thị công thức
            string formulaDescription = CaseData.GetFormulaDescription(currentCase, dice1, dice2, dice3);
            Console.WriteLine("Cong thuc su dung: " + formulaDescription);

            // Hiển thị ABC
            var caseCodeAndDescription = CaseData.GetCaseCodeAndDescription(currentCase);
            Console.WriteLine("Case ABC: " + caseCodeAndDescription.code);

            // Xử lý chẵn lẻ và hiển thị kết quả cuối
            Console.WriteLine("Lac xuc xac X Y Z");
            EvenAndOdd.RollDiceWithParity(nextDice);
        }
    }
}
