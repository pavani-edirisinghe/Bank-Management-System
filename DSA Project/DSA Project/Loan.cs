using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    public class Loan
    {
        public string AccountNumber { get; set; }
        
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
            double durationInYears = (EndDate - StartDate).TotalDays / 365;

            decimal totalInterest = Amount * InterestRate / 100 * (decimal)durationInYears;

            return totalInterest;
        }
    }
}

