namespace BankingApp.Models;

/// <summary>
/// Represents a customer of the bank.
/// Simulates a real customer with a unique username, address, email, and phone number.
/// </summary>
public class Customer
{
    public required string Username { get; set; }
    public required string Address { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}