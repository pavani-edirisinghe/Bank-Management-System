using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DSA_Project
{
    class SearchAccount : Project
    {
        static void CenteredText(string text, ConsoleColor color = ConsoleColor.White)
        {
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = Math.Max(0, (windowWidth - textWidth) / 2);

            Console.WriteLine();
            Console.SetCursorPosition(spaces, Console.CursorTop);
            ColorHelper.SetColor(color);
            Console.WriteLine(text);
            ColorHelper.ResetColor();
        }

        static void DisplayCenteredWithBorder(string[] items, ConsoleColor color = ConsoleColor.Cyan)
        {
            int consoleWidth = Console.WindowWidth;
            int maxItemLength = items.Max(item => item.Length);
            int boxWidth = maxItemLength + 6;
            int leadingSpaces = (consoleWidth - boxWidth) / 2;

            ColorHelper.SetColor(color);
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
            ColorHelper.ResetColor();
        }

        static void CenteredInputPrompt(string prompt, int maxPromptLength, out string value, ConsoleColor color = ConsoleColor.White)
        {
            int windowWidth = Console.WindowWidth;
            int totalTextWidth = maxPromptLength + 1;
            int spaces = (windowWidth - totalTextWidth - 8) / 2;

            Console.SetCursorPosition(spaces, Console.CursorTop);
            ColorHelper.SetColor(color);
            Console.Write(prompt);

            Console.SetCursorPosition(spaces + prompt.Length, Console.CursorTop);
            value = Console.ReadLine();
            ColorHelper.ResetColor();
        }

        public static void searchAcc(List<Account> accounts)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                CenteredText("********** Search Account ***********", ConsoleColor.Yellow);

                string[] items = new string[]
                {
                    "Press 1 to Search by Account Number",
                    "Press 2 to Search by Name",
                    "Press 3 to Sort by Balance",
                    "Press 4 to Sort by Account Creation Date",
                    "Press 5 to Back"
                };

                DisplayCenteredWithBorder(items, ConsoleColor.Cyan);
                Console.WriteLine();

                string text3 = "Choose an option: ";
                int windowWidth3 = Console.WindowWidth;
                int textWidth3 = text3.Length;
                int spaces3 = (windowWidth3 - textWidth3) / 2;
                Console.SetCursorPosition(spaces3, Console.CursorTop);
                ColorHelper.SetColor(ConsoleColor.Magenta);
                Console.Write(text3);
                ColorHelper.ResetColor();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            SearchByAccountNumber(accounts);
                            break;
                        case 2:
                            SearchByName(accounts);
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine(new string('\n', 100));
                            SortByBalance(accounts);
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine(new string('\n', 100));
                            SortByCreationDate(accounts);
                            break;
                        case 5:
                            Console.Clear();
                            return;
                        default:
                            CenteredText("Invalid choice! Please try again.", ConsoleColor.Red);
                            break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                CenteredText("Press 3 to go back to the Loan Management Menu or Press 7 to go back Main Menu..!", ConsoleColor.Blue);

                while (true)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.D3)
                    {
                        Console.Clear();
                        searchAcc(accounts);
                        return;
                    }
                    else if (key == ConsoleKey.D7)
                    {
                        Console.Clear();
                        Main();
                        return;
                    }
                    else
                    {
                        CenteredText("Invalid choice! Enter Again.", ConsoleColor.Red);
                    }
                }
            }
        }

        static void SearchByAccountNumber(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Search by Account Number **********", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = { "Enter Account Number: " };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i], ConsoleColor.Cyan);
            }

            string accNum = values[0];

            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account != null)
            {
                Console.WriteLine();
                Console.WriteLine();

                CenteredText("###########################################################################################", ConsoleColor.Cyan);
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
                CenteredText("###########################################################################################", ConsoleColor.Cyan);

                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}");
                Console.Write($"{account.Type.PadRight(typeWidth)}");
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                CenteredText("Account not found.", ConsoleColor.Red);
                Console.WriteLine();
            }
        }

        static void SearchByName(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Search by Name **********", ConsoleColor.Yellow);

            Console.WriteLine();

            string[] prompts = { "Enter Name: " };
            string[] values = new string[prompts.Length];

            int maxPromptLength = prompts.Max(p => p.Length);

            for (int i = 0; i < prompts.Length; i++)
            {
                CenteredInputPrompt(prompts[i], maxPromptLength, out values[i], ConsoleColor.Cyan);
            }

            string name = values[0];

            BST bst = new BST();
            foreach (var account in accounts)
            {
                bst.Insert(account);
            }

            var results = bst.Search(name);

            if (results.Count > 0)
            {
                Console.WriteLine();
                CenteredText("Search Results:", ConsoleColor.Yellow);
                Console.WriteLine();

                CenteredText("###########################################################################################", ConsoleColor.Cyan);
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
                CenteredText("###########################################################################################", ConsoleColor.Cyan);

                foreach (var account in results)
                {
                    Console.WriteLine();
                    Console.Write("          ".PadRight(accNumberWidth1));
                    Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                    Console.Write($"{account.Name.PadRight(nameWidth)}");
                    Console.Write($"{account.Address.PadRight(addressWidth)}");
                    Console.Write($"{account.Gender.PadRight(genderWidth)}");
                    Console.Write($"{account.Type.PadRight(typeWidth)}");
                    Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                CenteredText("No accounts found with the given name.", ConsoleColor.Red);
                Console.WriteLine();
            }
        }


        // Use List for Search by Name
        /* static void SearchByName(List<Account> accounts)
         {
             Console.Write("Enter Name: ");
             string name = Console.ReadLine();

             var results = accounts.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

             if (results.Count > 0)
            {
                Console.WriteLine();
                CenteredText("Search Results:");
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
                foreach (var account in results)
                {
                    Console.WriteLine();
                    Console.Write("          ".PadRight(accNumberWidth1));
                    Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                    Console.Write($"{account.Name.PadRight(nameWidth)}");
                    Console.Write($"{account.Address.PadRight(addressWidth)}");
                    Console.Write($"{account.Gender.PadRight(genderWidth)}"); 
                    Console.Write($"{account.Type.PadRight(typeWidth)}");   
                    Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
             else
             {
                 Console.WriteLine("No accounts found with the given name.");
             }
         }*/


        static void SortByBalance(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine();

            Stopwatch t = new Stopwatch();
            t.Start();
            List<Account> sortedAccounts = MergeSort(accounts);
            t.Stop();

            long elapsedTicks = t.ElapsedTicks;
            double elapsedNanoseconds = elapsedTicks * (100000000.0 / Stopwatch.Frequency);

            DisplaySortedAccounts(sortedAccounts);
        }

        static List<Account> MergeSort(List<Account> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }

            int mid = list.Count / 2;
            List<Account> left = list.GetRange(0, mid);
            List<Account> right = list.GetRange(mid, list.Count - mid);

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        static List<Account> Merge(List<Account> left, List<Account> right)
        {
            List<Account> result = new List<Account>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i].Balance <= right[j].Balance)
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }

            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }

            return result;
        }

        static void DisplaySortedAccounts(List<Account> sortedAccounts)
        {
            Console.Clear();
            CenteredText("********** Accounts Sorted by Balance (Ascending) **********", ConsoleColor.Yellow);
            Console.WriteLine();
            CenteredText("Accounts sorted by balance (ascending):", ConsoleColor.Yellow);

            Console.WriteLine();
            CenteredText("###########################################################################################", ConsoleColor.Cyan);
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
            CenteredText("###########################################################################################", ConsoleColor.Cyan);

            foreach (var account in sortedAccounts)
            {
                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}");
                Console.Write($"{account.Type.PadRight(typeWidth)}");
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }


        // Selection Sort Algorithm for Sort by Balance
        /*static void SortByBalance(List<Account> accounts)
        {
            Stopwatch t = new Stopwatch();
            t.Start();
        

            for (int i = 0; i < accounts.Count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < accounts.Count; j++)
                {
                    if (accounts[j].Balance < accounts[minIndex].Balance)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    var temp = accounts[i];
                    accounts[i] = accounts[minIndex];
                    accounts[minIndex] = temp;
                }
            }
            t.Stop();

            long elapsedTicks = t.ElapsedTicks;
            double elapsedNanoseconds = elapsedTicks * (100000000.0 / Stopwatch.Frequency);

            CenteredText("Accounts sorted by balance (ascending):");
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

            foreach (var account in accounts)
            {
                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}"); 
                Console.Write($"{account.Type.PadRight(typeWidth)}");     
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                Console.WriteLine();
            }

            Console.WriteLine($"Time taken: {elapsedNanoseconds}ns");
            Console.WriteLine();
        }*/


        // Bubble Sort Algorithm for Sort by Balance
        /*static void SortByBalance(List<Account> accounts)
        {
            List<Account> sortedAccounts = new List<Account>(accounts);

            int n = sortedAccounts.Count;
           Stopwatch t = new Stopwatch();
            t.Start();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (sortedAccounts[j].Balance > sortedAccounts[j + 1].Balance)
                    {
                        Account temp = sortedAccounts[j];
                        sortedAccounts[j] = sortedAccounts[j + 1];
                        sortedAccounts[j + 1] = temp;
                    }
                }
            }
            t.Stop();

            long elapsedTicks = t.ElapsedTicks;
            double elapsedNanoseconds = elapsedTicks * (100000000.0 / Stopwatch.Frequency);
            Console.WriteLine("Accounts sorted by balance (ascending):");

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

            foreach (var account in sortedAccounts)
            {
                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}"); 
                Console.Write($"{account.Type.PadRight(typeWidth)}");   
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                Console.WriteLine();
            }
            Console.WriteLine($"Time taken: {elapsedNanoseconds}ns");
            Console.WriteLine();
        }*/

        static void SortByCreationDate(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine();
            CenteredText("********** Accounts Sorted by Creation Date (Ascending) **********", ConsoleColor.Yellow);

            Console.WriteLine();
            Stopwatch t = new Stopwatch();
            t.Start();

            void MergeSort(List<Account> accounts, int left, int right)
            {
                if (left < right)
                {
                    int mid = left + (right - left) / 2;

                    MergeSort(accounts, left, mid);
                    MergeSort(accounts, mid + 1, right);

                    Merge(accounts, left, mid, right);
                }
            }

            void Merge(List<Account> accounts, int left, int mid, int right)
            {
                int n1 = mid - left + 1;
                int n2 = right - mid;
                var leftArray = new Account[n1];
                var rightArray = new Account[n2];

                for (int i = 0; i < n1; i++)
                    leftArray[i] = accounts[left + i];
                for (int j = 0; j < n2; j++)
                    rightArray[j] = accounts[mid + 1 + j];

                int k = left;
                int x = 0, y = 0;

                while (x < n1 && y < n2)
                {
                    if (string.Compare(leftArray[x].AccountNumber, rightArray[y].AccountNumber) <= 0)
                    {
                        accounts[k] = leftArray[x];
                        x++;
                    }
                    else
                    {
                        accounts[k] = rightArray[y];
                        y++;
                    }
                    k++;
                }

                while (x < n1)
                {
                    accounts[k] = leftArray[x];
                    x++;
                    k++;
                }

                while (y < n2)
                {
                    accounts[k] = rightArray[y];
                    y++;
                    k++;
                }
            }

            MergeSort(accounts, 0, accounts.Count - 1);

            t.Stop();
            long elapsedTicks = t.ElapsedTicks;
            double elapsedNanoseconds = elapsedTicks * (100000000.0 / Stopwatch.Frequency);

            CenteredText("Accounts sorted by creation date (ascending):", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine();

            CenteredText("###########################################################################################", ConsoleColor.Cyan);
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
            CenteredText("###########################################################################################", ConsoleColor.Cyan);

            foreach (var account in accounts)
            {
                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}");
                Console.Write($"{account.Type.PadRight(typeWidth)}");
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        // Quick Sort Algorithm for Sort by Creation Date
        /*static void SortByCreationDate(List<Account> accounts)
        {
            Stopwatch t = new Stopwatch();
            t.Start();
            void Swap(List<Account> accounts, int i, int j)
            {
                var temp = accounts[i];
                accounts[i] = accounts[j];
                accounts[j] = temp;
            }

            int Partition(List<Account> accounts, int low, int high)
            {

                string pivot = accounts[high].AccountNumber;
                int i = low - 1; 

                for (int j = low; j < high; j++)
                {
                    if (string.Compare(accounts[j].AccountNumber, pivot) <= 0)
                    {
                        i++; 
                        Swap(accounts, i, j); 
                    }
                }

                Swap(accounts, i + 1, high);

                return i + 1;
            }

            void QuickSort(List<Account> accounts, int low, int high)
            {
                if (low < high)
                {
                    int pivotIndex = Partition(accounts, low, high);

                    QuickSort(accounts, low, pivotIndex - 1);  
                    QuickSort(accounts, pivotIndex + 1, high); 
                }
            }

            QuickSort(accounts, 0, accounts.Count - 1);

            t.Stop();
            long elapsedTicks = t.ElapsedTicks;
            double elapsedNanoseconds = elapsedTicks * (100000000.0 / Stopwatch.Frequency);

            CenteredText("Accounts sorted by creation date (ascending):");
            Console.WriteLine();
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

            foreach (var account in accounts)
            {
                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}"); 
                Console.Write($"{account.Type.PadRight(typeWidth)}");    
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                Console.WriteLine();
            }

            Console.WriteLine($"Time taken: {elapsedNanoseconds}ns");
            Console.WriteLine();
        }*/


        // Insertion Sort Algorithm for Sort by Creation Date
        /*static void SortByCreationDate(List<Account> accounts)
        {
            Stopwatch t = new Stopwatch();
            t.Start();

            for (int i = 1; i < accounts.Count; i++)
            {
                var currentAccount = accounts[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(accounts[j].AccountNumber, currentAccount.AccountNumber) > 0)
                {
                    accounts[j + 1] = accounts[j];
                    j--;
                }

                accounts[j + 1] = currentAccount;
            }
            t.Stop();
            long elapsedTicks = t.ElapsedTicks;
            double elapsedNanoseconds = elapsedTicks * (100000000.0 / Stopwatch.Frequency);

            CenteredText("Accounts sorted by creation date (ascending):");
            Console.WriteLine();
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

            foreach (var account in accounts)
            {
                Console.WriteLine();
                Console.Write("          ".PadRight(accNumberWidth1));
                Console.Write($"{account.AccountNumber.PadRight(accNumberWidth)}");
                Console.Write($"{account.Name.PadRight(nameWidth)}");
                Console.Write($"{account.Address.PadRight(addressWidth)}");
                Console.Write($"{account.Gender.PadRight(genderWidth)}"); 
                Console.Write($"{account.Type.PadRight(typeWidth)}");     
                Console.Write($"{account.Balance.ToString("N2").PadRight(balanceWidth)}");
                Console.WriteLine();
            }
            Console.WriteLine($"Time taken: {elapsedNanoseconds}ns");
            Console.WriteLine();
        }*/
    }
}