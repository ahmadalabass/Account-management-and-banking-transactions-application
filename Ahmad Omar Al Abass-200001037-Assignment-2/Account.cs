public class Account
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public decimal Balance { get; set; }
    public AccountType AccountType { get; set; }
    public bool IsActive { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}
