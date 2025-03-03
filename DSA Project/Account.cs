using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class Account
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
        public List<Loan> Loans { get; set; } = new List<Loan>();

        public Account() { }

        // Constructor to initialize non-nullable properties
        public Account(string name, string address, string accountNumber, decimal balance)
        {
            Name = name;
            Address = address;
            AccountNumber = accountNumber;
            Balance = balance;
        }
    }
}
