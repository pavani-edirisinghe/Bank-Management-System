using System;
using System.Collections.Generic;
using System.IO;

namespace DSA_Project
{
    class CsvDataHandler
    {
        public static List<Account> LoadDataFromCsv(string filePath)
        {
            List<Account> accounts = new List<Account>();

            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("CSV file not found. Starting with an empty list.");
                    return accounts;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine();
                    int lineNumber = 1; 

                    while (!reader.EndOfStream)
                    {
                        lineNumber++;
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (string.IsNullOrWhiteSpace(line))
                        {
                            Console.WriteLine($"Empty line at line {lineNumber}. Skipping this line.");
                            continue;
                        }

                        string type = values[0];
                        string accountNumber = values[1];

                        if (type == "Customer")
                        {
                            if (values.Length < 7)
                            {
                                Console.WriteLine($"Missing required columns for Customer at line {lineNumber}. Skipping this line.");
                                continue;
                            }

                            string name = values[2];
                            string address = values[3];
                            string gender = values[4];
                            string balanceStr = values[6];
                            string accountType = values[5];

                            if (!decimal.TryParse(balanceStr, out decimal balance))
                            {
                                Console.WriteLine($"Invalid balance '{balanceStr}' at line {lineNumber}. Skipping this account.");
                                continue;
                            }

                            var account = new Account(name, address, accountNumber, balance, accountType, gender);
                            accounts.Add(account);
                        }
                        else if (type == "Transaction")
                        {
                            if (values.Length < 13)
                            {
                                Console.WriteLine($"Missing required columns for Transaction at line {lineNumber}. Skipping this line.");
                                continue;
                            }

                            string transactionType = values[7];
                            string amountStr = values[8];
                            string transactionDateStr = values[9];

                            if (!decimal.TryParse(amountStr, out decimal amount))
                            {
                                Console.WriteLine($"Invalid amount '{amountStr}' at line {lineNumber}. Skipping this transaction.");
                                continue;
                            }

                            if (!DateTime.TryParse(transactionDateStr, out DateTime transactionDate))
                            {
                                Console.WriteLine($"Invalid date '{transactionDateStr}' at line {lineNumber}. Skipping this transaction.");
                                continue;
                            }

                            var account = accounts.Find(a => a.AccountNumber == accountNumber);
                            if (account != null)
                            {
                                account.TransactionHistory.Add(new Transaction(transactionType, amount) { Date = transactionDate });
                            }
                            else
                            {
                                Console.WriteLine($"Account {accountNumber} not found for transaction at line {lineNumber}. Skipping this transaction.");
                            }
                        }
                        else if (type == "Loan")
                        {
                            
                            if (values.Length < 13)
                            {
                                Console.WriteLine($"Missing required columns for Loan at line {lineNumber}. Skipping this line.");
                                continue;
                            }

                            string loanAmountStr = values[10];
                            string interestRateStr = values[11];
                            string loanEndDateStr = values[12];

                            if (!decimal.TryParse(loanAmountStr, out decimal loanAmount))
                            {
                                Console.WriteLine($"Invalid loan amount '{loanAmountStr}' at line {lineNumber}. Skipping this loan.");
                                continue;
                            }

                            if (!decimal.TryParse(interestRateStr, out decimal interestRate))
                            {
                                Console.WriteLine($"Invalid interest rate '{interestRateStr}' at line {lineNumber}. Skipping this loan.");
                                continue;
                            }

                            if (!DateTime.TryParse(loanEndDateStr, out DateTime loanEndDate))
                            {
                                Console.WriteLine($"Invalid loan end date '{loanEndDateStr}' at line {lineNumber}. Skipping this loan.");
                                continue;
                            }

                            var account = accounts.Find(a => a.AccountNumber == accountNumber);
                            if (account != null)
                            {
                                account.Loans.Add(new Loan(accountNumber, loanAmount, interestRate, loanEndDate));
                            }
                            else
                            {
                                Console.WriteLine($"Account {accountNumber} not found for loan at line {lineNumber}. Skipping this loan.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Unknown type '{type}' at line {lineNumber}. Skipping this line.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return accounts;
        }
    }
}