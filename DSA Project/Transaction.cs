using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class Transaction
    {
        public string Type { get; set; } // Deposit, Withdrawal, Transfer
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }
    }
}
