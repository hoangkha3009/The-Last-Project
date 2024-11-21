using System;

public class RandomManager
{
    private string[] cases;

    public RandomManager()
    {
        // Khởi tạo các case
        cases = new string[]
        {
            "Bầu5 Cua4 Cá3 Gà2 Tôm1 Nai0",
            "Cá5 Nai4 Gà3 Bầu2 Tôm1 Cua0",
            "Cua5 Nai4 Bầu3 Cá2 Tôm1 Gà0",
            "Cua5 Cá4 Gà3 Tôm2 Nai1 Bầu0",
            "Nai5 Cá4 Tôm3 Gà2 Cua1 Bầu0",
            "Nai5 Gà4 Cua3 Tôm2 Bầu1 Cá0",
            "Cá5 Gà4 Tôm3 Nai2 Bầu1 Cua0",
            "Tôm5 Nai4 Bầu3 Cua2 Gà1 Cá0",
            "Tôm5 Cá4 Nai3 Bầu2 Gà1 Cua0",
            "Tôm5 Nai4 Bầu3 Cua2 Gà1 Cá0",
            "Bầu5 Gà4 Cua3 Cá2 Nai1 Tôm0",
            "Bầu5 Tôm4 Gà3 Cua2 Nai1 Cá0",
            "Nai5 Bầu4 Cua3 Cá2 Tôm1 Gà0",
            "Gà5 Nai4 Cá3 Tôm2 Bầu1 Cua0",
            "Gà5 Cá4 Bầu3 Nai2 Cua1 Tôm0",
            "Gà5 Tôm4 Nai3 Bầu2 Cua1 Cá0",
            "Cua5 Bầu4 Nai3 Gà2 Cá1 Tôm0",
            "Cá5 Tôm4 Gà3 Nai2 Bầu1 Cua0",
            "Bầu5 Gà4 Tôm3 Cua2 Nai1 Cá0",
            "Tôm5 Gà4 Cá3 Nai2 Bầu1 Cua0",
            "Nai5 Bầu4 Cua3 Cá2 Tôm1 Gà0",
            "Cua5 Tôm4 Nai3 Gà2 Bầu1 Cá0",
            "Bầu5 Nai4 Tôm3 Cá2 Gà1 Cua0",
            "Tôm5 Nai4 Cá3 Gà2 Cua1 Bầu0"
        };
    }

    public string GetCaseByHour(int hour)
    {
        return cases[hour % cases.Length]; // Lấy case theo giờ
    }
}
