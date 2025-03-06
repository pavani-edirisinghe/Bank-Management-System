using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DSA_Project
{
    class TransactionManagement : Project
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

        static void CenteredTextMultiColor(string part1, ConsoleColor color1, string part2, ConsoleColor color2, string part3, ConsoleColor color3)
        {
            string combinedText = part1 + part2 + part3;

            int windowWidth = Console.WindowWidth;
            int textWidth = combinedText.Length;
            int spaces = (windowWidth - textWidth) / 2;

            Console.SetCursorPosition(spaces, Console.CursorTop);

            ColorHelper.SetColor(color1);
            Console.Write(part1);

            ColorHelper.SetColor(color2);
            Console.Write(part2);

            ColorHelper.SetColor(color3);
            Console.Write(part3);

            ColorHelper.ResetColor();

            Console.WriteLine();
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

        public static void transMng(List<Account> accounts, string filePath)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                CenteredTextMultiColor("********* ", ConsoleColor.Green, "Transaction Management", ConsoleColor.Yellow, " ********", ConsoleColor.Green);


                string[] items = new string[]
                {
                    "Press 1 to Money Deposits",
                    "Press 2 to Money Withdrawal      ",
                    "Press 3 to Money Transfer",
                    "Press 4 to Transfer History",
                    "Press 5 to Back"
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
                            Deposit(accounts, filePath);
                            break;
                        case 2:
                            Withdraw(accounts, filePath);
                            break;
                        case 3:
                            Transfer(accounts, filePath);
                            break;
                        case 4:
                            ShowTransactionHistory(accounts);
                            break;
                        case 5:
                            Console.Clear();
                            return;
                        default:
                            CenteredText("Invalid choice! Please try again.", ConsoleColor.Red);
                            break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                CenteredText("Press 3 to go back to the Transaction Management Menu or Press 7 to go back Main Menu..!", ConsoleColor.Blue);

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

        static void Deposit(List<Account> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Money Deposit *********", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = { "Enter Account Number: ", "Enter Amount to Deposit: " };
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

            account.Balance += amount;
            account.TransactionHistory.Add(new Transaction("Deposit", amount));

            SaveDataToCsv(filePath, accounts);

            Console.WriteLine();
            CenteredText("Deposit successful.", ConsoleColor.Green);
            Console.WriteLine();
        }

        static void Withdraw(List<Account> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Money Withdraw **********", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = { "Enter Account Number: ", "Enter Amount to Withdraw: " };
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

            if (account.Balance >= amount)
            {
                account.Balance -= amount;
                account.TransactionHistory.Add(new Transaction("Withdrawal", amount));
                SaveDataToCsv(filePath, accounts);

                Console.WriteLine();
                CenteredText("Withdrawal successful.", ConsoleColor.Green);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                CenteredText("Insufficient balance.", ConsoleColor.Red);
                Console.WriteLine();
            }
        }

        static void Transfer(List<Account> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Money Transfer **********", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = {
                "Enter Source Account Number: ",
                "Enter Target Account Number: ",
                "Enter Amount to Transfer: "
            };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i], ConsoleColor.Cyan);
            }

            string sourceAccNum = values[0];
            string targetAccNum = values[1];
            decimal amount = decimal.Parse(values[2]);

            var sourceAccount = accounts.Find(a => a.AccountNumber == sourceAccNum);
            var targetAccount = accounts.Find(a => a.AccountNumber == targetAccNum);

            if (sourceAccount == null || targetAccount == null)
            {
                Console.WriteLine();
                CenteredText("One or both accounts not found!", ConsoleColor.Red);
                Console.WriteLine();
                return;
            }

            if (sourceAccount.Balance >= amount)
            {
                sourceAccount.Balance -= amount;
                targetAccount.Balance += amount;

                sourceAccount.TransactionHistory.Add(new Transaction($"Transfer to {targetAccNum}", amount));
                targetAccount.TransactionHistory.Add(new Transaction($"Transfer from {sourceAccNum}", amount));

                SaveDataToCsv(filePath, accounts);

                Console.WriteLine();
                CenteredText("Transfer successful.", ConsoleColor.Green);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                CenteredText("Insufficient balance.", ConsoleColor.Red);
                Console.WriteLine();
            }
        }

        static void ShowTransactionHistory(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Transaction History **********", ConsoleColor.Yellow);

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

            Console.WriteLine();
            CenteredText($"Transaction History for Account {accNum}:", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine();

            CenteredText("###########################################################################", ConsoleColor.Cyan);
            int dateWidth = 25;
            int typeWidth = 25;
            int amountWidth = 10;

            string header = $"{"Date".PadRight(dateWidth)} {"Type".PadRight(typeWidth)} {"Amount".PadRight(amountWidth)}";
            CenteredText(header, ConsoleColor.Cyan);
            CenteredText("###########################################################################", ConsoleColor.Cyan);

            foreach (var transaction in account.TransactionHistory)
            {
                string transactionDetails = $"{transaction.Date.ToString().PadRight(dateWidth)} {transaction.Type.PadRight(typeWidth)} Rs.{transaction.Amount.ToString("N2")}";
                CenteredText(transactionDetails, ConsoleColor.White);
            }

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