using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Transactions;

namespace DSA_Project
{
    class TransactionManagement : Project
    {
        public static void transMng(List<Account> accounts, string filePath)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------Transaction Management-------------");

                string[] items = new string[]
                {
                    "1. Money Deposits",
                    "2. Money Withdrawal",
                    "3. Money Transfer",
                    "4. Transfer History",
                    "5. Back"
                };

                // Display menu items
                int consoleWidth = Console.WindowWidth;
                int maxItemLength = items.Max(item => item.Length);
                int leadingSpaces = (consoleWidth - maxItemLength) / 2;

                foreach (var item in items)
                {
                    Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                Console.Write("Choose an option: ");
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
                            return; // Go back to the main menu
                        default:
                            Console.WriteLine("Invalid choice! Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        // Deposit money into an account
        static void Deposit(List<Account> accounts, string filePath)
        {
            Console.Write("Enter Account Number: ");
            string accNum = Console.ReadLine();
            Console.Write("Enter Amount to Deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            account.Balance += amount;
            account.TransactionHistory.Add(new Transaction("Deposit", amount));
            SaveDataToCsv(filePath, accounts);
            Console.WriteLine("Deposit successful and data saved to CSV.");
        }

        // Withdraw money from an account
        static void Withdraw(List<Account> accounts, string filePath)
        {
            Console.Write("Enter Account Number: ");
            string accNum = Console.ReadLine();
            Console.Write("Enter Amount to Withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            if (account.Balance >= amount)
            {
                account.Balance -= amount;
                account.TransactionHistory.Add(new Transaction("Withdrawal", amount));
                SaveDataToCsv(filePath, accounts);
                Console.WriteLine("Withdrawal successful and data saved to CSV.");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        // Transfer money between accounts
        static void Transfer(List<Account> accounts, string filePath)
        {
            Console.Write("Enter Source Account Number: ");
            string sourceAccNum = Console.ReadLine();
            Console.Write("Enter Target Account Number: ");
            string targetAccNum = Console.ReadLine();
            Console.Write("Enter Amount to Transfer: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var sourceAccount = accounts.Find(a => a.AccountNumber == sourceAccNum);
            var targetAccount = accounts.Find(a => a.AccountNumber == targetAccNum);

            if (sourceAccount == null || targetAccount == null)
            {
                Console.WriteLine("One or both accounts not found!");
                return;
            }

            if (sourceAccount.Balance >= amount)
            {
                sourceAccount.Balance -= amount;
                targetAccount.Balance += amount;

                // Add transaction history
                sourceAccount.TransactionHistory.Add(new Transaction($"Transfer to {targetAccNum}", amount));
                targetAccount.TransactionHistory.Add(new Transaction($"Transfer from {sourceAccNum}", amount));

                SaveDataToCsv(filePath, accounts);
                Console.WriteLine("Transfer successful and data saved to CSV.");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        // Show transaction history for an account
        static void ShowTransactionHistory(List<Account> accounts)
        {
            Console.Write("Enter Account Number: ");
            string accNum = Console.ReadLine();

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            Console.WriteLine($"Transaction History for Account {accNum}:");
            foreach (var transaction in account.TransactionHistory)
            {
                Console.WriteLine($"{transaction.Date}: {transaction.Type} of {transaction.Amount:C}");
            }
        }

        // Save data to CSV
        static void SaveDataToCsv(string filePath, List<Account> accounts)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header
                writer.WriteLine("Type,AccountNumber,Name,Address,Balance,TransactionType,Amount,TransactionDate,LoanAmount,InterestRate,LoanEndDate");

                // Write customer data
                foreach (var account in accounts)
                {
                    writer.WriteLine($"Customer,{account.AccountNumber},{account.Name},{account.Address},{account.Balance},,,,");

                    // Write transaction data
                    foreach (var transaction in account.TransactionHistory)
                    {
                        writer.WriteLine($"Transaction,{account.AccountNumber},,,,{transaction.Type},{transaction.Amount},{transaction.Date},,");
                    }

                    // Write loan data
                    foreach (var loan in account.Loans)
                    {
                        writer.WriteLine($"Loan,{account.AccountNumber},,,,,,,{loan.Amount},{loan.InterestRate},{loan.EndDate}");
                    }
                }
            }
            Console.WriteLine("Data saved to CSV.");
        }
    }
}