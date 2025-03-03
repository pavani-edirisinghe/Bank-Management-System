using System;
using System.Collections.Generic;
using System.IO;

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

        static void DisplayCenteredWithBorder(string[] items)
        {
            int consoleWidth = Console.WindowWidth;
            int maxItemLength = 0;
            foreach (var item in items)
            {
                if (item.Length > maxItemLength)
                    maxItemLength = item.Length;
            }
            int boxWidth = maxItemLength + 6;
            int leadingSpaces = (consoleWidth - boxWidth) / 2;

            Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
            Console.WriteLine("╔" + new string('═', boxWidth - 2) + "╗");

            foreach (var item in items)
            {
                Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
                string paddedItem = item.PadRight(maxItemLength);
                Console.WriteLine($"║  {paddedItem}  ║");
            }
            Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
            Console.WriteLine("╚" + new string('═', boxWidth - 2) + "╝");
        }

        public static void accountMng()
        {
            accounts = CsvDataHandler.LoadDataFromCsv("C:\\Users\\PAVANI EDIRISINGHE\\Desktop\\DSA Project\\bank_data.csv");

            while (true) 
            {
                Console.Clear();
                Console.WriteLine();
                string text1 = "********* Account Management ********";
                CenteredText(text1);

                string[] items = new string[]
                {
                   "Press 1 to Create an Account",
                   "Press 2 to Update an Account   ",
                   "Press 3 to Delete an Account",
                   "Press 4 to Back to Main"
                };
                int consoleWidth = Console.WindowWidth;
                int maxItemLength = 0;

                DisplayCenteredWithBorder(items);
                Console.WriteLine();

                string text3 = "Choose an Option: ";
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
                            Console.Clear();
                            return; 
                        default:
                            Console.WriteLine("Invalid choice! Enter Again.");
                            break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                CenteredText("Press 3 to go back to the Account Management Menu or Press 7 to go back Main Menu..!");
                while (true)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.D3) 
                    {
                        break; 
                    }
                    else if (key == ConsoleKey.D7) 
                    {
                        Console.Clear();
                        Main(); 
                        return; 
                    }
                    else
                    {
                        CenteredText("Invalid choice! Enter Again.");
                        
                    }
                }
            }
        }

        static void createAcc()
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Create an Account ***********");

            Console.WriteLine();

            string[] prompts = {
                "Name: ",
                "Account Number (5 digits): ",
                "Account Type (C/S): ",
                "Address: ",
                "Gender (M/F): ",
                "Balance: "
              };

            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i]);
            }

            string name = values[0];
            string accNum = values[1];
            string type = values[2];
            string address = values[3];
            string gender = values[4];

            while (true)
            {
                if (string.IsNullOrEmpty(accNum))
                {
                    CenteredText("Account number cannot be empty. Please enter again:");
                }
                else if (accNum.Length != 5)
                {
                    CenteredText("Account number must be 5 digits. Please enter again:");
                }
                else if (!ulong.TryParse(accNum, out _))
                {
                    CenteredText("Account number must contain only numbers. Please enter again:");
                }
                else if (accounts.Any(acc => acc.AccountNumber == accNum))
                {
                    CenteredText("Account number already exists. Please enter a unique account number:");
                }
                else
                {
                    break;
                }
                CenteredInputPrompt(prompts[1], maxPromptLength, out accNum);
                values[1] = accNum;
            }

            while (true)
            {
                if (string.IsNullOrEmpty(type))
                {
                    CenteredText("Account type cannot be empty. Please enter again:");
                }
                else if (type.ToUpper() != "C" && type.ToUpper() != "S")
                {
                    CenteredText("Account type must be 'C' (Current) or 'S' (Savings). Please enter again:");
                }
                else
                {
                    break;
                }
                CenteredInputPrompt(prompts[2], maxPromptLength, out type);
                values[2] = type;
            }

            while (true)
            {
                if (string.IsNullOrEmpty(gender))
                {
                    CenteredText("Gender cannot be empty. Please enter again:");
                }
                else if (gender.ToUpper() != "M" && gender.ToUpper() != "F")
                {
                    CenteredText("Gender must be 'M' (Male) or 'F' (Female). Please enter again:");
                }
                else
                {
                    break;
                }
                CenteredInputPrompt(prompts[4], maxPromptLength, out gender);
                values[4] = gender;
            }

            decimal balance;
            while (true)
            {
                if (decimal.TryParse(values[5], out balance))
                {
                    if (balance >= 0)
                    {
                        break;
                    }
                    else
                    {
                        CenteredText("Balance cannot be negative. Please enter a valid number.");
                    }
                }
                else
                {
                    CenteredText("Invalid balance. Please enter a valid number.");
                }

                CenteredInputPrompt(prompts[5], maxPromptLength, out values[5]);
            }

            Account newAccount = new Account(name, address, accNum, balance, type, gender);
            accounts.Add(newAccount);

            SaveDataToCsv("C:\\Users\\PAVANI EDIRISINGHE\\Desktop\\DSA Project\\bank_data.csv", accounts);

            Console.WriteLine();
            CenteredText("Account created successfully..!");
            Console.WriteLine();

            CenteredText("Account Details:");
            Console.WriteLine();

            CenteredText("###########################################################################################");
            int accNumberWidth1 = 15;
            int accNumberWidth = 18;
            int nameWidth = 20;
            int addressWidth = 20;
            int genderWidth = 13;
            int typeWidth = 10;
            int balanceWidth = 10;

            Console.WriteLine(
                "          ".PadRight(accNumberWidth1) +
                "ACC_Number".PadRight(accNumberWidth) +
                "Name".PadRight(nameWidth) +
                "Address".PadRight(addressWidth) +
                "Gender".PadRight(genderWidth) +
                "Type".PadRight(typeWidth) +
                "Balance".PadRight(balanceWidth)
            );
            CenteredText("###########################################################################################");

            Console.WriteLine();
            Console.Write("          ".PadRight(accNumberWidth1));
            Console.Write($"{values[1].ToString().PadRight(accNumberWidth)}");
            Console.Write($"{values[0].ToString().PadRight(nameWidth)}");
            Console.Write($"{values[3].ToString().PadRight(addressWidth)}");
            Console.Write($"{values[4].ToString().PadRight(genderWidth)}");
            Console.Write($"{values[2].ToString().PadRight(typeWidth)}");
            Console.Write($"{values[5].ToString().PadRight(balanceWidth)}");
           
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

        }


        static void CenteredInputPrompt(string prompt, int maxPromptLength, out string value)
        {
            int windowWidth = Console.WindowWidth;
            int totalTextWidth = maxPromptLength + 1;
            int spaces = (windowWidth - totalTextWidth - 8) / 2;

            Console.SetCursorPosition(spaces, Console.CursorTop);
            Console.Write(prompt);

            Console.SetCursorPosition(spaces + prompt.Length, Console.CursorTop);
            value = Console.ReadLine();
        }

        static void updateAcc()
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("************ Update an Account *************");

            Console.WriteLine();

            string[] prompts = { "Enter Account Number to update: " };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i]);
            }

            string accNum = values[0].Trim();

            Account accountToUpdate = accounts.Find(acc => acc.AccountNumber == accNum);

            if (accountToUpdate == null)
            {
                Console.WriteLine();
                CenteredText("Account not found!");
                Console.WriteLine();
                return;
            }

            Console.WriteLine();
            CenteredText("Updating Account for: " + accountToUpdate.Name + "             ");

            string[] updatePrompts = {
                "New Name (leave blank to keep current): ",
                "New Address (leave blank to keep current): ",
                "New Balance (leave blank to keep current): "
                 };
            string[] updateValues = new string[updatePrompts.Length];

            for (int i = 0; i < updatePrompts.Length; i++)
            {
                CenteredInputPrompt(updatePrompts[i], maxPromptLength, out updateValues[i]);
            }

            string newName = updateValues[0];
            string newAddress = updateValues[1];
            string newBalanceInput = updateValues[2];

            if (!string.IsNullOrEmpty(newName))
                accountToUpdate.Name = newName;

            if (!string.IsNullOrEmpty(newAddress))
                accountToUpdate.Address = newAddress;

            if (!string.IsNullOrEmpty(newBalanceInput) && decimal.TryParse(newBalanceInput, out decimal newBalance))
                accountToUpdate.Balance = newBalance;

            SaveDataToCsv("C:\\Users\\PAVANI EDIRISINGHE\\Desktop\\DSA Project\\bank_data.csv", accounts);

            Console.WriteLine();
            CenteredText("Account updated successfully!");
            
            Console.WriteLine();

            CenteredText("Upadetd Account Details:");
            Console.WriteLine();

            CenteredText("#############################################################################################");
            int accNumberWidth1 = 15;
            int accNumberWidth = 18;
            int nameWidth = 20;
            int addressWidth = 20;
            int genderWidth = 13;
            int typeWidth = 10;
            int balanceWidth = 10;

            Console.WriteLine(
                "          ".PadRight(accNumberWidth1) +
                "ACC_Number".PadRight(accNumberWidth) +
                "Name".PadRight(nameWidth) +
                "Address".PadRight(addressWidth) +
                "Gender".PadRight(genderWidth) +
                "Type".PadRight(typeWidth) +
                "Balance".PadRight(balanceWidth)
            );
            CenteredText("#############################################################################################");

            Console.WriteLine();
            Console.Write("          ".PadRight(accNumberWidth1));
            Console.Write($"{accountToUpdate.AccountNumber.PadRight(accNumberWidth)}");
            Console.Write($"{accountToUpdate.Name.PadRight(nameWidth)}");
            Console.Write($"{accountToUpdate.Address.PadRight(addressWidth)}");
            Console.Write($"{accountToUpdate.Gender.PadRight(genderWidth)}"); 
            Console.Write($"{accountToUpdate.Type.PadRight(typeWidth)}");     
            Console.Write($"{accountToUpdate.Balance.ToString("N2").PadRight(balanceWidth)}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        static void deleteAcc()
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("************ Delete an Account *************");

            Console.WriteLine();

            string[] prompts = { "Enter Account Number to delete: " };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i]);
            }

            string accNum = values[0];

            Account account = accounts.Find(acc => acc.AccountNumber == accNum);

            if (account == null)
            {
                Console.WriteLine();
                CenteredText("Account not found!");
                Console.WriteLine();
                return;
            }

            accounts.Remove(account);

            SaveDataToCsv("C:\\Users\\PAVANI EDIRISINGHE\\Desktop\\DSA Project\\bank_data.csv", accounts);

            Console.WriteLine();
            CenteredText("Account deleted successfully!");
            Console.WriteLine();
        }

        static void SaveDataToCsv(string filePath, List<Account> accounts)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine("Type,AccountNumber,Name,Address,Gender,AccountType,Balance,TransactionType,Amount,TransactionDate,LoanAmount,InterestRate,LoanEndDate");

                foreach (var account in accounts)
                {
                    
                    writer.WriteLine($"Customer,{account.AccountNumber},{account.Name},{account.Address},{account.Gender},{account.Type},{account.Balance},,,,");

                    foreach (var transaction in account.TransactionHistory)
                    {
                        writer.WriteLine($"Transaction,{account.AccountNumber},,,,,,{transaction.Type},{transaction.Amount},{transaction.Date},,,,");
                    }

                    foreach (var loan in account.Loans)
                    {
                        writer.WriteLine($"Loan,{account.AccountNumber},,,,,,,,,{loan.Amount},{loan.InterestRate},{loan.EndDate}");
                    }
                }
            }
        }
    }
}