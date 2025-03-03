using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class CsvDataHandler
    {
        // Load data from CSV file
        public static List<Account> LoadDataFromCsv(string filePath)
        {
            List<Account> accounts = new List<Account>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("CSV file not found. Starting with an empty list.");
                return accounts;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip the header
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    string type = values[0];
                    string accountNumber = values[1];

                    if (type == "Customer")
                    {
                        string name = values[2];
                        string address = values[3];
                        decimal balance = decimal.Parse(values[4]);

                        var account = new Account(name, address, accountNumber, balance);
                        accounts.Add(account);
                    }
                    else if (type == "Transaction")
                    {
                        string transactionType = values[5];
                        decimal amount = decimal.Parse(values[6]);
                        DateTime transactionDate = DateTime.Parse(values[7]);

                        var account = accounts.Find(a => a.AccountNumber == accountNumber);
                        if (account != null)
                        {
                            account.TransactionHistory.Add(new Transaction(transactionType, amount) { Date = transactionDate });
                        }
                    }
                    else if (type == "Loan")
                    {
                        decimal loanAmount = decimal.Parse(values[8]);
                        decimal interestRate = decimal.Parse(values[9]);
                        DateTime loanEndDate = DateTime.Parse(values[10]);

                        var account = accounts.Find(a => a.AccountNumber == accountNumber);
                        if (account != null)
                        {
                            account.Loans.Add(new Loan(accountNumber, loanAmount, interestRate, loanEndDate));
                        }
                    }
                }
            }
            Console.WriteLine("Data loaded from CSV.");
            return accounts;
        }
    }
}