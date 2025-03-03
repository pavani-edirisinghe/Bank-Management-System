using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class Save_Data : Project
    {
        public void SaveDataToCsv(string filePath, Dictionary<string, Account> accounts, List<Loan> loans)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header
                writer.WriteLine("Type,AccountNumber,Name,Address,Balance,TransactionType,Amount,TransactionDate,LoanAmount,InterestRate,LoanEndDate");

                // Write customer data
                foreach (var account in accounts.Values)
                {
                    writer.WriteLine($"Customer,{account.AccountNumber},{account.Name},{account.Address},{account.Balance},,,,");
                }

                // Write transaction data
                foreach (var account in accounts.Values)
                {
                    foreach (var transaction in account.TransactionHistory)
                    {
                        writer.WriteLine($"Transaction,{account.AccountNumber},,,,{transaction.Type},{transaction.Amount},{transaction.Date},,");
                    }
                }

                // Write loan data
                foreach (var loan in loans)
                {
                    writer.WriteLine($"Loan,{loan.AccountNumber},,,,,,,{loan.Amount},{loan.InterestRate},{loan.EndDate}");
                }
            }
            Console.WriteLine("Data saved to CSV.");
        }
    }
}
