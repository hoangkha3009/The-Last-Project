using System;
using System.Net.NetworkInformation;

class BauCuaGame
{
    static void Main(string[] args)
    {
        // Load case data
        CaseData.LoadCases();

        Console.WriteLine("Chao mung ban den voi game Bau Tom Cua Ca");

        // Check network availability
        bool isNetworkAvailable = IsNetworkAvailable();
        Console.WriteLine($"Trang thai mang: {(isNetworkAvailable ? "Co ket noi" : "Khong co ket noi")}");

        // Determine case based on the current hour
        int caseChoice = DateTime.Now.Hour % 24;
        Console.WriteLine($"Case duoc chon dua tren gio hien tai: {caseChoice}");

        // Retrieve the corresponding animals
        if (!CaseData.Cases.ContainsKey(caseChoice))
        {
            Console.WriteLine($"Case {caseChoice} khong ton tai.");
            return;
        }

        string[] animals = CaseData.Cases[caseChoice].Item1;
        Console.WriteLine($"Case {caseChoice}: {string.Join(", ", animals)}");

        int Next_dice = 0;
        Random random = new Random();

        // Main game loop
        while (true)
        {
            Console.WriteLine("\n--- Lua chon cua ban ---");
            Console.WriteLine("Nhan Enter de xoc xuc xac hoac q de thoat:");
            string input = Console.ReadLine();

            if (input.ToLower() == "q")
            {
                Console.WriteLine("Cam on ban da choi game! Hen gap lai.");
                break;
            }
            else
            {
                int dice1 = random.Next(0, animals.Length);
                int dice2 = random.Next(0, animals.Length);
                int dice3 = random.Next(0, animals.Length);

                // Ensure Next_dice appears
                int guaranteedIndex = random.Next(0, 3);
                if (guaranteedIndex == 0) dice1 = Next_dice;
                if (guaranteedIndex == 1) dice2 = Next_dice;
                if (guaranteedIndex == 2) dice3 = Next_dice;

                Console.WriteLine("Ket qua xoc xuc xac:");
                Console.WriteLine($"Vien 1: {animals[dice1]} ({dice1})");
                Console.WriteLine($"Vien 2: {animals[dice2]} ({dice2})");
                Console.WriteLine($"Vien 3: {animals[dice3]} ({dice3})");

                var nextDiceFormula = CaseData.Cases[caseChoice].Item2;
                Next_dice = nextDiceFormula(dice1, dice2, dice3);

                Console.WriteLine($"Tinh Next_dice su dung cong thuc cua case {caseChoice}:");
                string formulaDescription = CaseData.GetFormulaDescription(caseChoice, dice1, dice2, dice3);
                Console.WriteLine(formulaDescription);
                Console.WriteLine($"Next_dice: {animals[Next_dice]} ({Next_dice})");
            }
        }
    }

    // Function to check network availability
    static bool IsNetworkAvailable()
    {
        try
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        catch
        {
            return false;
        }
    }
}
