// ------------ Admin ------------ 
public class Admin : IUserActions
{
    private BankDatabase _bankDatabase;
    private Account _loggedInAccount;

    public Admin(BankDatabase bankDatabase)
    {
        _bankDatabase = bankDatabase;
    }

    // تسجيل الدخول
    public void Login(string username, string password)
    {
        _loggedInAccount = _bankDatabase.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password && a.Role == UserRole.Admin);
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Invalid login credentials.");
            return;
        }
        Console.WriteLine("Login successful as Admin.");
    }

    // إنشاء حساب جديد (للأدوار المختلفة)
    public void CreateAccount(string username, string password, UserRole role, AccountType accountType)
    {
        if (_loggedInAccount == null)
        {
            Console.WriteLine("Access denied. Please login first.");
            return;
        }

        var newAccount = new Account
        {
            Id = _bankDatabase.Accounts.Count + 1,
            Username = username,
            Password = password,
            Role = role,
            AccountType = accountType,
            IsActive = true
        };
        _bankDatabase.Accounts.Add(newAccount);
        Console.WriteLine($"{role} account created successfully.");
    }

    // عرض الإحصائيات العامة
    public void ViewStatistics()
    {
        var totalAccounts = _bankDatabase.Accounts.Count;
        var activeAccounts = _bankDatabase.Accounts.Count(a => a.IsActive);
        var closedAccounts = totalAccounts - activeAccounts;

        Console.WriteLine($"Total Accounts: {totalAccounts}");
        Console.WriteLine($"Active Accounts: {activeAccounts}");
        Console.WriteLine($"Closed Accounts: {closedAccounts}");
    }

    // تحديث حساب الموظف
    public void UpdateEmployeeAccount(int employeeId, string newUsername, string newPassword)
    {
        var employee = _bankDatabase.Accounts.FirstOrDefault(a => a.Id == employeeId && a.Role == UserRole.Employee);
        if (employee != null)
        {
            employee.Username = newUsername;
            employee.Password = newPassword;
            Console.WriteLine("Employee account updated successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }

    // حذف حساب الموظف
    public void DeleteEmployeeAccount(int employeeId)
    {
        var employee = _bankDatabase.Accounts.FirstOrDefault(a => a.Id == employeeId && a.Role == UserRole.Employee);
        if (employee != null)
        {
            _bankDatabase.Accounts.Remove(employee);
            Console.WriteLine("Employee account deleted successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }
}
