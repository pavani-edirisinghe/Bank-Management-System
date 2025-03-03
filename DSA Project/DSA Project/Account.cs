using DSA_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; } 
        public string Type { get; set; } 
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
        public DateTime CreationDate { get; set; }

        public Account(string name, string address, string accountNumber, decimal balance, string type, string gender)
        {
            Name = name;
            Address = address;
            AccountNumber = accountNumber;
            Balance = balance;
            Type = type;
            Gender = gender;
        }
    }

}

