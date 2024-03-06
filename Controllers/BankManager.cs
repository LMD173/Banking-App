using BankingApp.Models;
using BankingApp.Utils;

namespace BankingApp.Controllers;

/// <summary>
/// Allows the management of a bank account for a given customer.
/// </summary>
/// <param name="account">The account to manage.</param>
public class BankManager(Account account)
{
    private readonly Account _account = account;

    public void Withdraw(decimal amount)
    {
        if (amount > _account.Balance)
        {
            Logger.Error("Insufficient funds to withdraw from your account. Please try again.");
        }
        else
        {
            _account.Balance -= amount;
            Logger.Info($"You have successfully withdrawn {amount.ToString("F2")} from your account. Your new balance is £{_account.Balance.ToString("F2")}");
        }
    }

    public void Deposit(decimal amount)
    {
        _account.Balance += amount;
        Logger.Info($"You have successfully deposited £{amount.ToString("F2")} to your account. Your new balance is £{_account.Balance.ToString("F2")}");
    }

    public void ViewBalance()
    {
        Logger.Info($"Your current balance is £{_account.Balance.ToString("F2")}");
    }
}