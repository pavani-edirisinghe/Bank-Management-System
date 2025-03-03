using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class LoanManagement : Project
    {
        public static void loanMng(List<Account> accounts, string filePath)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------Loan Management-------------");

                string[] items = new string[]
                {
                    "1. Apply Loan",
                    "2. Repayments",
                    "3. Calculate Interest",
                    "4. Back"
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
                            ApplyLoan(accounts, filePath);
                            break;
                        case 2:
                            MakeRepayment(accounts, filePath);
                            break;
                        case 3:
                            CalculateInterest(accounts);
                            break;
                        case 4:
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

        // Apply for a loan
        static void ApplyLoan(List<Account> accounts, string filePath)
        {
            Console.Write("Enter Account Number: ");
            string accNum = Console.ReadLine();
            Console.Write("Enter Loan Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Interest Rate (%): ");
            decimal interestRate = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Loan End Date (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            // Pass accountNumber when creating the Loan object
            account.Loans.Add(new Loan(accNum, amount, interestRate, endDate));
            SaveDataToCsv(filePath, accounts);
            Console.WriteLine("Loan applied successfully and data saved to CSV.");
        }


        // Make a loan repayment
        static void MakeRepayment(List<Account> accounts, string filePath)
        {
            Console.Write("Enter Account Number: ");
            string accNum = Console.ReadLine();
            Console.Write("Enter Repayment Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            if (account.Loans.Count == 0)
            {
                Console.WriteLine("No loans found for this account.");
                return;
            }

            // Deduct the repayment amount from the loan balance
            var loan = account.Loans[0]; // Assuming only one loan per account for simplicity
            if (loan.Amount >= amount)
            {
                loan.Amount -= amount;
                account.TransactionHistory.Add(new Transaction("Loan Repayment", amount));
                SaveDataToCsv(filePath, accounts);
                Console.WriteLine("Repayment successful and data saved to CSV.");
            }
            else
            {
                Console.WriteLine("Repayment amount exceeds the loan balance.");
            }
        }

        // Calculate interest for a loan
        static void CalculateInterest(List<Account> accounts)
        {
            Console.Write("Enter Account Number: ");
            string accNum = Console.ReadLine();

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            if (account.Loans.Count == 0)
            {
                Console.WriteLine("No loans found for this account.");
                return;
            }

            var loan = account.Loans[0]; // Assuming only one loan per account for simplicity
            decimal interest = loan.CalculateInterest();
            Console.WriteLine($"Interest for the loan: {interest:C}");
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