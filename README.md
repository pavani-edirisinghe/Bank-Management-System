# 💰 Bank Management System (Console Application)

A **Bank Management System** built using C# Console Application. This system allows users to manage bank accounts, handle transactions, and manage loans.

## 🚀 Features

✅ **Account Management**  
   - Create new accounts  
   - View account details  
   - Delete accounts  

✅ **Transaction Management**  
   - Deposit money  
   - Withdraw money  
   - View transaction history  

✅ **Loan Management**  
   - Apply for a loan  
   - Make loan repayments  
   - Calculate interest  

✅ **Search and Sort Accounts**  
   - Search by Account Number  
   - Search by Name  
   - Sort by Balance  
   - Sort by Account Creation Date  

✅ **Secure & Efficient**  
   - User authentication system  
   - Error handling and validations  

## 🛠️ Technologies Used

- **C# (.NET)**
- **Console Application**
- **File Handling / Database (if applicable)**
- **CSV File Handling** (for data storage)

## 📂 Project Structure

📦 bank-management-system  
├── 📁 .git/                  
├── 📄 bank_data.csv            
├── 📁 DSA Project/           
│   ├── 📁 .vs/               
│   ├── 📁 bin/               
│   ├── 📁 obj/                 
│   ├── 📁 DSA Project/         
│   │   ├── 📁 bin/  
│   │   ├── 📁 obj/  
│   │   ├── 📄 Account.cs  
│   │   ├── 📄 AccountManagement.cs  
│   │   ├── 📄 Bank.cs  
│   │   ├── 📄 CsvDataHandler.cs  
│   │   ├── 📄 DSA Project.csproj  
│   │   ├── 📄 Loan.cs  
│   │   ├── 📄 LoanManagement.cs  
│   │   ├── 📄 Program.cs  
│   │   ├── 📄 Save_Data.cs  
│   │   ├── 📄 SearchAccount.cs  
│   │   ├── 📄 Transaction.cs  
│   │   ├── 📄 TransactionManagement.cs  
│   ├── 📄 DSA Project.sln      


---

## 🎯 How to Run

### 1️⃣ Clone the Repository  
First, download the project to your local machine using Git:  

```sh
git clone https://github.com/pavani-edirisinghe/Bank-Management-System.git

```
## 2️⃣ Save the Project Folder
After cloning, save the project folder in a location of your choice. Ensure that you have all necessary dependencies installed.

## 3️⃣ Update the CSV File Paths
Before running the project, you must update the CSV file paths in the code. There are 5 places where the file path needs to be changed.

📌 **Files to modify:**
- `Program.cs`
- `AccountManagement.cs`

🔹 Change the file path in each of these files to match the actual location of `bank_data.csv` on your system.

**Example:**

```csharp
string filePath = @"C:\your-folder-path\bank_data.csv";
```

Make sure to replace `"C:\your-folder-path\bank_data.csv"` with the correct location where you saved `bank_data.csv`.

## 4️⃣ Run the Project
Once the file paths are updated, you can run the project using Visual Studio:

1. Open the solution file `DSA Project.sln` in Visual Studio.
2. Build the project (`Ctrl + Shift + B`).
3. Run the project (`F5`).

Alternatively, if you are using the command line, navigate to the project folder and run:

```sh
dotnet run
```

## 📝 Notes
- Ensure you have .NET SDK installed on your system to run the project.
- The `bank_data.csv` file is used for storing account, searching accounts, transaction, and loan data. Make sure it is accessible and writable by the application.
- If you encounter any issues, check the file paths and ensure all dependencies are correctly installed.

## 🤝 Contributing
Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. **Fork the repository.**
2. **Create a new branch** for your feature or bugfix.
3. **Commit your changes.**
4. **Push your changes** to the branch.
5. **Submit a pull request.**

## 📄 License
This project is licensed under the MIT License. 




