﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DSA_Project
{
    class LoanManagement : Project
    {
        static void CenteredText(string text, ConsoleColor color = ConsoleColor.White)
        {
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = (windowWidth - textWidth) / 2;
            Console.SetCursorPosition(spaces, Console.CursorTop);
            ColorHelper.SetColor(color);
            Console.WriteLine(text);
            ColorHelper.ResetColor();
        }

        static void DisplayCenteredWithBorder(string[] items, ConsoleColor color = ConsoleColor.Cyan)
        {
            int consoleWidth = Console.WindowWidth;
            int maxItemLength = items.Max(item => item.Length);
            int boxWidth = maxItemLength + 6;
            int leadingSpaces = (consoleWidth - boxWidth) / 2;

            ColorHelper.SetColor(color);
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

        static void CenteredInputPrompt(string prompt, int maxPromptLength, out string value, ConsoleColor color = ConsoleColor.White)
        {
            int windowWidth = Console.WindowWidth;
            int totalTextWidth = maxPromptLength + 1;
            int spaces = (windowWidth - totalTextWidth - 8) / 2;

            Console.SetCursorPosition(spaces, Console.CursorTop);
            ColorHelper.SetColor(color);
            Console.Write(prompt);

            Console.SetCursorPosition(spaces + prompt.Length, Console.CursorTop);
            value = Console.ReadLine();
            ColorHelper.ResetColor();
        }

        public static void loanMng(List<Account> accounts, string filePath)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                CenteredText("********** Loan Management ***********", ConsoleColor.Yellow);

                string[] items = new string[]
                {
                    "Press 1 to Apply Loan",
                    "Press 2 to Loan Repayments",
                    "Press 3 to Calculate Interest   ",
                    "Press 4 to Back"
                };

                DisplayCenteredWithBorder(items, ConsoleColor.Cyan);
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
                            ApplyLoan(accounts, filePath);
                            break;
                        case 2:
                            MakeRepayment(accounts, filePath);
                            break;
                        case 3:
                            CalculateInterest(accounts);
                            break;
                        case 4:
                            Console.Clear();
                            return;
                        default:
                            CenteredText("Invalid choice! Please try again.", ConsoleColor.Red);
                            break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                CenteredText("Press 3 to go back to the Loan Management Menu or Press 7 to go back Main Menu..!", ConsoleColor.Blue);

                while (true)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.D3)
                    {
                        break;
                    }
                    else if (key == ConsoleKey.D7)
                    {
                        Console.Clear();
                        Main();
                        return;
                    }
                    else
                    {
                        CenteredText("Invalid choice! Enter Again.", ConsoleColor.Red);
                    }
                }
            }
        }

        static void ApplyLoan(List<Account> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("************ Apply for a Loan **************", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = {
                "Enter Account Number: ",
                "Enter Loan Amount: ",
                "Enter Interest Rate (%): ",
                "Enter Loan End Date (yyyy-mm-dd): "
            };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i], ConsoleColor.Cyan);
            }

            string accNum = values[0];
            decimal amount = decimal.Parse(values[1]);
            decimal interestRate = decimal.Parse(values[2]);
            DateTime endDate = DateTime.Parse(values[3]);

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine();
                CenteredText("Account not found!", ConsoleColor.Red);
                Console.WriteLine();
                return;
            }

            account.Loans.Add(new Loan(accNum, amount, interestRate, endDate));
            SaveDataToCsv(filePath, accounts);

            Console.WriteLine();
            CenteredText("Loan applied successfully ..!", ConsoleColor.Green);
            Console.WriteLine();
        }

        static void MakeRepayment(List<Account> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("****** Make a Loan Repayment ******", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = {
                "Enter Account Number: ",
                "Enter Repayment Amount: "
            };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i], ConsoleColor.Cyan);
            }

            string accNum = values[0];
            decimal amount = decimal.Parse(values[1]);

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine();
                CenteredText("Account not found!", ConsoleColor.Red);
                Console.WriteLine();
                return;
            }

            if (account.Loans.Count == 0)
            {
                Console.WriteLine();
                CenteredText("No loans found for this account.", ConsoleColor.Red);
                Console.WriteLine();
                return;
            }

            var loan = account.Loans[0];
            if (loan.Amount >= amount)
            {
                loan.Amount -= amount;
                account.TransactionHistory.Add(new Transaction("Loan Repayment", amount));
                SaveDataToCsv(filePath, accounts);

                Console.WriteLine();
                CenteredText("Repayment successful.", ConsoleColor.Green);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                CenteredText("Repayment amount exceeds the loan balance.", ConsoleColor.Red);
                Console.WriteLine();
            }
        }

        static void CalculateInterest(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("******* Calculate Loan Interest *******", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = { "Enter Account Number: " };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i], ConsoleColor.Cyan);
            }

            string accNum = values[0];

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine();
                CenteredText("Account not found!", ConsoleColor.Red);
                Console.WriteLine();
                return;
            }

            if (account.Loans.Count == 0)
            {
                Console.WriteLine();
                CenteredText("No loans found for this account.", ConsoleColor.Red);
                Console.WriteLine();
                return;
            }

            var loan = account.Loans[0];
            decimal interest = loan.CalculateInterest();

            Console.WriteLine();
            CenteredText($"Interest for the loan: Rs.{interest:N2}", ConsoleColor.Green);
            Console.WriteLine();
        }

        static void SaveDataToCsv(string filePath, List<Account> accounts)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Type,AccountNumber,Name,Address,Gender,AccountType,Balance,TransactionType,Amount,TransactionDate,LoanAmount,InterestRate,LoanEndDate");

                foreach (var account in accounts)
                {
                    writer.WriteLine($"Customer,{account.AccountNumber},{account.Name},{account.Address},{account.Gender},{account.Type},{account.Balance},,,,");

                    foreach (var transaction in account.TransactionHistory)
                    {
                        writer.WriteLine($"Transaction,{account.AccountNumber},,,,,,{transaction.Type},{transaction.Amount},{transaction.Date},,,,");
                    }

                    foreach (var loan in account.Loans)
                    {
                        writer.WriteLine($"Loan,{account.AccountNumber},,,,,,,,,{loan.Amount},{loan.InterestRate},{loan.EndDate}");
                    }
                }
            }
        }
    }
}