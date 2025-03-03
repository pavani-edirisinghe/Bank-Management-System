using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA_Project
{
    class Bank
    {
        public Dictionary<string, Account> Accounts { get; set; } = new Dictionary<string, Account>();
        public List<Loan> Loans { get; set; } = new List<Loan>();

        public void SaveData(string filePath)
        {
            List<Account> accountList = Accounts.Values.ToList();

            Save_Data.SaveDataToCsv(filePath, accountList, Loans);
        }
    }
}