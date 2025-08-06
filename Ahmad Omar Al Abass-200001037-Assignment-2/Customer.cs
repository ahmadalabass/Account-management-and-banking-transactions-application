public class Customer : IUserActions
{
    private BankDatabase _bankDatabase;
    private Account _loggedInAccount;

    public Customer(BankDatabase bankDatabase)
    {
        _bankDatabase = bankDatabase;
    }

    // تسجيل الدخول
    public void Login(string username, string password)
    {
        _loggedInAccount = _bankDatabase.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password && a.Role == UserRole.Customer);
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Invalid login credentials.");
            return;
        }
        Console.WriteLine("Login successful as Customer.");
    }

    // إنشاء حساب جديد
    public void CreateAccount()
    {
        Console.WriteLine("\n--- Create a New Customer Account ---");
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();
        Console.Write("Enter Password: ");
        string password = Console.ReadLine();
        Console.WriteLine("Select Account Type: 1. Savings 2. Checking");
        int accTypeChoice = int.Parse(Console.ReadLine());
        AccountType accountType = accTypeChoice == 1 ? AccountType.Savings : AccountType.Checking;

        Account newAccount = new Account
        {
            Id = _bankDatabase.Accounts.Count + 1,
            Username = username,
            Password = password,
            Role = UserRole.Customer,
            AccountType = accountType,
            Balance = 0,
            IsActive = true
        };

        _bankDatabase.Accounts.Add(newAccount);
        Console.WriteLine("Account created successfully.");
    }

    // إيداع مبلغ في الحساب
    public void Deposit(decimal amount)
    {
        if (_loggedInAccount == null || !_loggedInAccount.IsActive)
        {
            Console.WriteLine("Account is not active.");
            return;
        }
        _loggedInAccount.Balance += amount;
        _loggedInAccount.Transactions.Add(new Transaction
        {
            Id = _loggedInAccount.Transactions.Count + 1,
            AccountId = _loggedInAccount.Id,
            Amount = amount,
            TransactionType = TransactionType.Deposit,
            TransactionDate = DateTime.Now
        });
        Console.WriteLine("Deposit successful.");
    }

    // سحب مبلغ من الحساب
    public void Withdraw(decimal amount)
    {
        if (_loggedInAccount == null || !_loggedInAccount.IsActive)
        {
            Console.WriteLine("Account is not active.");
            return;
        }
        if (_loggedInAccount.Balance < amount)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }
        _loggedInAccount.Balance -= amount;
        _loggedInAccount.Transactions.Add(new Transaction
        {
            Id = _loggedInAccount.Transactions.Count + 1,
            AccountId = _loggedInAccount.Id,
            Amount = amount,
            TransactionType = TransactionType.Withdrawal,
            TransactionDate = DateTime.Now
        });
        Console.WriteLine("Withdrawal successful.");
    }

    // عرض تفاصيل الحساب
    public void ViewAccountDetails()
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Please login first.");
            return;
        }
        Console.WriteLine($"ID: {_loggedInAccount.Id}, Username: {_loggedInAccount.Username}, Balance: {_loggedInAccount.Balance}, Status: {(_loggedInAccount.IsActive ? "Active" : "Closed")}");
    }

    // عرض سجل المعاملات
    public void ViewTransactionHistory()
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Please login first.");
            return;
        }
        foreach (var transaction in _loggedInAccount.Transactions)
        {
            Console.WriteLine($"{transaction.TransactionDate}: {transaction.TransactionType} {transaction.Amount}");
        }
    }

    // إغلاق الحساب
    public void CloseAccount()
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Please login first.");
            return;
        }
        _loggedInAccount.IsActive = false;
        Console.WriteLine("Account closed successfully.");
    }

    // عرض قائمة الوظائف المتاحة للعميل
    public void DisplayCustomerMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Customer Menu ---");
            Console.WriteLine("1. Deposit Money");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. View Account Details");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Close Account");
            Console.WriteLine("6. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Amount to Deposit: ");
                    decimal depositAmount = decimal.Parse(Console.ReadLine());
                    Deposit(depositAmount);
                    break;
                case "2":
                    Console.Write("Enter Amount to Withdraw: ");
                    decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                    Withdraw(withdrawAmount);
                    break;
                case "3":
                    ViewAccountDetails();
                    break;
                case "4":
                    ViewTransactionHistory();
                    break;
                case "5":
                    CloseAccount();
                    break;
                case "6":
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
