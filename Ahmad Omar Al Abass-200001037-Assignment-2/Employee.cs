public class Employee : IUserActions
{
    private BankDatabase _bankDatabase;
    private Account _loggedInAccount;

    public Employee(BankDatabase bankDatabase)
    {
        _bankDatabase = bankDatabase;
    }

    // تسجيل الدخول
    public void Login(string username, string password)
    {
        _loggedInAccount = _bankDatabase.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password && a.Role == UserRole.Employee);
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Invalid login credentials.");
            return;
        }
        Console.WriteLine("Login successful as Employee.");
    }

    // إضافة حساب جديد للعملاء
    public void CreateCustomerAccount(string username, string password, AccountType accountType)
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Access denied. Please login first.");
            return;
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username or password cannot be empty.");
            return;
        }

        var newAccount = new Account
        {
            Id = _bankDatabase.Accounts.Count + 1,
            Username = username,
            Password = password,
            Role = UserRole.Customer,
            AccountType = accountType,
            IsActive = true,
            Balance = 0 // Assuming new accounts start with zero balance
        };
        _bankDatabase.Accounts.Add(newAccount);
        Console.WriteLine("Customer account created successfully.");
    }

    // عرض جميع الحسابات
    public void ViewAllAccounts()
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Access denied. Please login first.");
            return;
        }

        Console.WriteLine("\n--- All Accounts ---");
        foreach (var account in _bankDatabase.Accounts)
        {
            Console.WriteLine($"ID: {account.Id}, Username: {account.Username}, Role: {account.Role}, Balance: {account.Balance}, Active: {account.IsActive}");
        }
    }

    // إغلاق حساب العميل
    public void CloseCustomerAccount(int accountId)
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Access denied. Please login first.");
            return;
        }

        var customer = _bankDatabase.Accounts.FirstOrDefault(a => a.Id == accountId && a.Role == UserRole.Customer);
        if (customer != null)
        {
            customer.IsActive = false;
            Console.WriteLine("Customer account closed successfully.");
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }

    // حذف الحساب المغلق
    public void DeleteClosedAccount(int accountId)
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Access denied. Please login first.");
            return;
        }

        var customer = _bankDatabase.Accounts.FirstOrDefault(a => a.Id == accountId && !a.IsActive && a.Role == UserRole.Customer);
        if (customer != null)
        {
            _bankDatabase.Accounts.Remove(customer);
            Console.WriteLine("Closed account deleted successfully.");
        }
        else
        {
            Console.WriteLine("Closed account not found or still active.");
        }
    }

    // تحديث معلومات الحساب
    public void UpdateCustomerAccount(int accountId, string newUsername, decimal newBalance)
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Access denied. Please login first.");
            return;
        }

        var account = _bankDatabase.Accounts.FirstOrDefault(a => a.Id == accountId && a.Role == UserRole.Customer);
        if (account != null)
        {
            account.Username = newUsername;
            account.Balance = newBalance;
            Console.WriteLine("Customer account updated successfully.");
        }
        else
        {
            Console.WriteLine("Customer account not found.");
        }
    }
}
