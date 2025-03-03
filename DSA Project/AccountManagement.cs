using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DSA_Project
{
    class AccountManagement : Project
    {
        static List<Account> accounts = new List<Account>();
        static void CenteredText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = (windowWidth - textWidth) / 2;
            Console.SetCursorPosition(spaces, Console.CursorTop);
            Console.WriteLine(text);
        }

        public static void accountMng()
        {
            Console.Clear();
            string text1 = "------------- Account Management -------------";
            CenteredText(text1);
            Console.WriteLine();
            Console.WriteLine();

            string[] items = new string[]
            {
                "1. Creat a Account",
                "2. Update a Account",
                "3. Delete a Account",
                "4. Back to Main"
            };
            int consoleWidth = Console.WindowWidth;
            int maxItemLength = 0;

            foreach (var item in items)
            {
                if (item.Length > maxItemLength)
                {
                    maxItemLength = item.Length;
                }
            }
            int leadingSpaces = (consoleWidth - maxItemLength) / 2;

            foreach (var item in items)
            {
                Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string text3 = "What you want to do: ";
            int windowWidth3 = Console.WindowWidth;
            int textWidth3 = text3.Length;
            int spaces3 = (windowWidth3 - textWidth3) / 2;
            Console.SetCursorPosition(spaces3, Console.CursorTop);
            Console.Write(text3);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        createAcc();
                        break;
                    case 2:
                        updateAcc();
                        break;
                    case 3:
                        deleteAcc();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Enter Again.");
                        break;
                }
            }
        }
        static void createAcc()
        {
            Console.Clear();
            string text = "____________Create Account____________";
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = (windowWidth - textWidth) / 2;
            Console.SetCursorPosition(spaces, Console.CursorTop);
            Console.WriteLine(text);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Name : ");
            string name = Console.ReadLine();
            Console.Write("Address : ");
            string address = Console.ReadLine();
            Console.Write("Account Number (Max 10 digits): ");
            string accNum;

            while (true)
            {
                accNum = Console.ReadLine();

                if (string.IsNullOrEmpty(accNum))
                {
                    Console.WriteLine("Account number cannot be empty. Please enter again:");
                }
                else if (accNum.Length > 10 || accNum.Length < 10)
                {
                    Console.WriteLine("Account number is wrong. Please enter again:");
                }
                else if (!ulong.TryParse(accNum, out _))
                {
                    Console.WriteLine("Account number must contain only numbers. Please enter again:");
                }
                else
                {
                    break;
                }
            }

            decimal balance;
            while (true)
            {
                Console.Write("Balance : ");
                string balanceInput = Console.ReadLine();

                if (decimal.TryParse(balanceInput, out balance))
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid balance. Please enter a valid number.");
                    Console.WriteLine();
                }
            }

            accounts.Add(new Account(name, address, accNum, balance));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Account created successfully!");
            Console.WriteLine();
        }

        static void updateAcc()
        {
            Console.WriteLine("Enter Account Number to update: ");
            string accNum = Console.ReadLine();

            Account account = accounts.Find(acc => acc.AccountNumber == accNum);

            if (account == null)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Account not found!");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Updating Account for: " + account.Name);
            Console.Write("New Address (leave blank to keep current): ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAddress))
                account.Address = newAddress;

            Console.Write("New Balance (leave blank to keep current): ");
            string newBalanceInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newBalanceInput) && decimal.TryParse(newBalanceInput, out decimal newBalance))
                account.Balance = newBalance;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Account updated successfully!");
            Console.WriteLine();
        }
        static void deleteAcc()
        {
            Console.WriteLine("Enter Account Number to delete: ");
            string accNum = Console.ReadLine();

            Account account = accounts.Find(acc => acc.AccountNumber == accNum);

            if (account == null)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Account not found!");
                return;
            }
            accounts.Remove(account);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Account deleted successfully!");
            Console.WriteLine();
        }
    }
}