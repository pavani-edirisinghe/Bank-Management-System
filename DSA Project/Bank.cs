using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class Bank
    {
        public Dictionary<string, Account> Accounts { get; set; } = new Dictionary<string, Account>();
        public List<Loan> Loans { get; set; } = new List<Loan>();

        public void SaveData(string filePath)
        {
            var saveData = new Save_Data();
            saveData.SaveDataToCsv(filePath, Accounts, Loans);
        }

    }
}
