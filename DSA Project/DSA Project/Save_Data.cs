using System;
using System.Collections.Generic;
using System.IO;

namespace DSA_Project
{
    class Save_Data : Project
    {
        public static void SaveDataToCsv(string filePath, List<Account> accounts, List<Loan> loans)
        {
            
            bool fileExists = File.Exists(filePath);

            using (StreamWriter writer = new StreamWriter(filePath, append: false)) 
            {
               
                if (!fileExists)
                {
                    writer.WriteLine("Type,AccountNumber,Name,Address,Gender,AccountType,Balance,TransactionType,Amount,TransactionDate,LoanAmount,InterestRate,LoanEndDate");
                }

                foreach (var account in accounts)
                {
                   
                    writer.WriteLine($"Customer,{account.AccountNumber},{account.Name},{account.Address},{account.Gender},{account.Type},{account.Balance},,,,");

                    
                    foreach (var transaction in account.TransactionHistory)
                    {
                        writer.WriteLine($"Transaction,{account.AccountNumber},,,,{transaction.Type},{transaction.Amount},{transaction.Date},,");
                    }

                    foreach (var loan in account.Loans)
                    {
                        writer.WriteLine($"Loan,{account.AccountNumber},,,,,,,{loan.Amount},{loan.InterestRate},{loan.EndDate}");
                    }
                }

                foreach (var loan in loans)
                {
                    writer.WriteLine($"Loan,{loan.AccountNumber},,,,,,,{loan.Amount},{loan.InterestRate},{loan.EndDate}");
                }
            }
        }
    }
}