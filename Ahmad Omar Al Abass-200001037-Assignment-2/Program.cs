using Ahmad_Omar_Al_Abass_200001037_Assignment_2;

namespace Ahmad_Omar_Al_Abass_200001037_Assignment_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankDatabase bankDatabase = new BankDatabase();

            // إنشاء حساب مدير رئيسي عند بداية التشغيل
            bankDatabase.Accounts.Add(new Account
            {
                Id = 1,
                Username = "admin",
                Password = "admin123",
                Role = UserRole.Admin,
                AccountType = AccountType.Checking,
                IsActive = true
            });

            while (true)
            {
                Console.WriteLine("\n--- Welcome to the Bank System ---");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Employee Login");
                Console.WriteLine("3. Customer Login");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "4")
                {
                    Console.WriteLine("Exiting system...");
                    break;
                }

                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Admin admin = new Admin(bankDatabase);
                        admin.Login(username, password);
                        AdminMenu(admin);
                        break;

                    case "2":
                        Employee employee = new Employee(bankDatabase);
                        employee.Login(username, password);
                        EmployeeMenu(employee);
                        break;

                    case "3":
                        Customer customer = new Customer(bankDatabase);
                        customer.Login(username, password);
                        CustomerMenu(customer);
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static void AdminMenu(Admin admin)
        {
            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Create Account (Admin/Employee/Customer)");
                Console.WriteLine("2. View Statistics");
                Console.WriteLine("3. Logout");
                Console.Write("Choose an option: ");
                string adminChoice = Console.ReadLine();

                if (adminChoice == "3")
                    break;

                switch (adminChoice)
                {
                    case "1":
                        Console.Write("Enter Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string password = Console.ReadLine();
                        Console.WriteLine("Select Role: 1. Admin 2. Employee 3. Customer");
                        int roleChoice = int.Parse(Console.ReadLine());
                        UserRole role = roleChoice == 1 ? UserRole.Admin :
                                        roleChoice == 2 ? UserRole.Employee : UserRole.Customer;
                        Console.WriteLine("Select Account Type: 1. Savings 2. Checking");
                        int accTypeChoice = int.Parse(Console.ReadLine());
                        AccountType accountType = accTypeChoice == 1 ? AccountType.Savings : AccountType.Checking;

                        admin.CreateAccount(username, password, role, accountType);
                        break;

                    case "2":
                        admin.ViewStatistics();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void EmployeeMenu(Employee employee)
        {
            while (true)
            {
                Console.WriteLine("\n--- Employee Menu ---");
                Console.WriteLine("1. Create Customer Account");
                Console.WriteLine("2. View Accounts");
                Console.WriteLine("3. Update Account");
                Console.WriteLine("4. Close Account");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");
                string empChoice = Console.ReadLine();

                if (empChoice == "5")
                    break;

                switch (empChoice)
                {
                    case "1":
                        Console.Write("Enter Customer Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string password = Console.ReadLine();
                        Console.WriteLine("Select Account Type: 1. Savings 2. Checking");
                        int accTypeChoice = int.Parse(Console.ReadLine());
                        AccountType accountType = accTypeChoice == 1 ? AccountType.Savings : AccountType.Checking;

                        employee.CreateCustomerAccount(username, password, accountType);
                        break;

                    case "2":
                        employee.ViewAllAccounts();
                        break;

                    case "3":
                        Console.Write("Enter Account ID to update: ");
                        int accountId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Username: ");
                        string newUsername = Console.ReadLine();
                        Console.Write("Enter New Balance: ");
                        decimal newBalance = decimal.Parse(Console.ReadLine());

                        employee.UpdateCustomerAccount(accountId, newUsername, newBalance);  // تم التغيير هنا
                        break;

                    case "4":
                        Console.Write("Enter Account ID to close: ");
                        int closeAccountId = int.Parse(Console.ReadLine());
                        employee.CloseCustomerAccount(closeAccountId);  // إضافة إغلاق الحساب
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void CustomerMenu(Customer customer)
        {
            while (true)
            {
                Console.WriteLine("\n--- Customer Menu ---");
                Console.WriteLine("1. Deposit Money");
                Console.WriteLine("2. Withdraw Money");
                Console.WriteLine("3. View Account Details");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");
                string custChoice = Console.ReadLine();

                if (custChoice == "5")
                    break;

                switch (custChoice)
                {
                    case "1":
                        Console.Write("Enter Amount to Deposit: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        customer.Deposit(depositAmount);
                        break;

                    case "2":
                        Console.Write("Enter Amount to Withdraw: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        customer.Withdraw(withdrawAmount);
                        break;

                    case "3":
                        customer.ViewAccountDetails();
                        break;

                    case "4":
                        customer.ViewTransactionHistory();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
