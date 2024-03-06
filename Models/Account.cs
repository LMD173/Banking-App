namespace BankingApp.Models;

/// <summary>
/// Represents a bank account.
/// Simulates a real bank account with a unique account number, balance, and customer details.
/// </summary>
public class Account
{
    public required string AccountNumber { get; set; }
    public required decimal Balance { get; set; } // Using decimal to represent currency because it is more precise than float or double
    public required Customer Customer { get; set; }
}
