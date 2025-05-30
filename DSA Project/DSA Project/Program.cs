﻿using DSA_Project;
using System;
using System.Collections.Generic;

class Project
{
    static void CenterText()
    {
        string text1 = "****************************************";
        int windowWidth1 = Console.WindowWidth;
        int textWidth1 = text1.Length;
        int spaces1 = (windowWidth1 - textWidth1) / 2;
        Console.SetCursorPosition(spaces1, Console.CursorTop);
        ColorHelper.SetColor(ConsoleColor.Green); 
        Console.WriteLine(text1);
        ColorHelper.ResetColor(); 
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

        ColorHelper.SetColor(ConsoleColor.Cyan); 
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
        ColorHelper.ResetColor(); 
    }

    public static void Main()
    {
        string filePath = "C:\\Users\\PAVANI EDIRISINGHE\\Desktop\\Bank Management System\\bank_data.csv";
        List<Account> accounts = CsvDataHandler.LoadDataFromCsv(filePath);

        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            CenterText();

            string text2 = "Bank Management System";
            int windowWidth2 = Console.WindowWidth;
            int textWidth2 = text2.Length;
            int spaces2 = (windowWidth2 - textWidth2) / 2;
            Console.SetCursorPosition(spaces2, Console.CursorTop);
            ColorHelper.SetColor(ConsoleColor.Yellow); 
            Console.WriteLine(text2);
            ColorHelper.ResetColor(); 
            CenterText();

            string[] items = new string[]
            {
                "Press 1 to Account Management",
                "Press 2 to Transaction Management ",
                "Press 3 to Loan Management",
                "Press 4 to Search & Sort Accounts",
                "Press 5 to Exit"
            };

            DisplayCenteredWithBorder(items);

            Console.WriteLine();

            string text3 = "Choose an option: ";
            int windowWidth3 = Console.WindowWidth;
            int textWidth3 = text3.Length;
            int spaces3 = (windowWidth3 - textWidth3) / 2;
            Console.SetCursorPosition(spaces3, Console.CursorTop);
            ColorHelper.SetColor(ConsoleColor.Magenta); 
            Console.Write(text3);
            ColorHelper.ResetColor(); 
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
                        SearchAccount.searchAcc(accounts);
                        break;
                    case 5:
                        Console.Clear();
                        return;
                    default:
                        ColorHelper.SetColor(ConsoleColor.Red); 
                        Console.WriteLine("Invalid choice! Enter Again.");
                        ColorHelper.ResetColor(); 
                        break;
                }
            }
        }
    }
}