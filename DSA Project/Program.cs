using DSA_Project;
using System.Security.Principal;

class Project
{ 
    static void centerText()
    {
        string text1 = "**************************************************";
        int windowWidth1 = Console.WindowWidth;
        int textWidth1 = text1.Length;
        int spaces1 = (windowWidth1 - textWidth1) / 2;
        Console.SetCursorPosition(spaces1, Console.CursorTop);
        Console.WriteLine(text1);
    }
    static void DisplayCenteredWithBorder(string[] items)
    {
        int consoleWidth = Console.WindowWidth;
        int maxItemLength = 0;
        foreach (var item in items)
        {
            if (item.Length > maxItemLength)
                maxItemLength = item.Length;
        }
        int boxWidth = maxItemLength + 6;
        int leadingSpaces = (consoleWidth - boxWidth) / 2;

        Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
        Console.WriteLine("╔" + new string('═', boxWidth - 2) + "╗");

        foreach (var item in items)
        {
            Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
            string paddedItem = item.PadRight(maxItemLength);
            Console.WriteLine($"║  {paddedItem}  ║");
        }
        Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
        Console.WriteLine("╚" + new string('═', boxWidth - 2) + "╝");
    }


    static void Main(string[] arges)
    {
        string filePath = "bank_data.csv";
        List<Account> accounts = CsvDataHandler.LoadDataFromCsv(filePath);

        while (true)
        {
            centerText();
            string text2 = "Bank Management System";
            int windowWidth2 = Console.WindowWidth;
            int textWidth2 = text2.Length;
            int spaces2 = (windowWidth2 - textWidth2) / 2;
            Console.SetCursorPosition(spaces2, Console.CursorTop);
            Console.WriteLine(text2);
            centerText();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string[] items = new string[]
            {
            "1. Account Management",
            "2. Transaction Management",
            "3. Loan Management",
            "4. Search",
            "5. Exit"
            };

            DisplayCenteredWithBorder(items);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string text3 = "Choose an option: ";
            int windowWidth3 = Console.WindowWidth;
            int textWidth3 = text3.Length;
            int spaces3 = (windowWidth3 - textWidth3) / 2;
            Console.SetCursorPosition(spaces3, Console.CursorTop);
            Console.Write(text3);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        AccountManagement.accountMng();
                        break;
                    case 2:
                        TransactionManagement.transMng(accounts, filePath);
                        break;
                    case 3:
                        LoanManagement.loanMng(accounts, filePath);
                        break;
                    case 4:
                        SearchAccount.searchAcc();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Enter Again.");
                        break;
                }
            }


        }
    }
}
