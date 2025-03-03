using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class Loan
    {
        public string AccountNumber { get; set; } // Add this property
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Loan(string accountNumber, decimal amount, decimal interestRate, DateTime endDate)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            InterestRate = interestRate;
            StartDate = DateTime.Now;
            EndDate = endDate;
        }

        public decimal CalculateInterest()
        {
            return Amount * InterestRate / 100;
        }
    }
}
