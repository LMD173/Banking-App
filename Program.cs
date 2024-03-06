using System.Text.RegularExpressions;
using BankingApp.Controllers;
using BankingApp.Models;
using BankingApp.Utils;

namespace BankingApp;

/// <summary>
/// The starting point for the application. Acts as a View for the user to interact with the application.
/// Partial due to the generated regexes; see more: https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-source-generators
/// </summary>
partial class Program
{

    private static readonly List<Account> _accounts = [];

    static void Main()
    {
        Logger.Info("Welcome to Banking App! Here, you can create a new account and manage your account.");
        Logger.Info("===============================================");
        bool exit = false;
        while (!exit)
        {
            DisplayMainOptions();
            var input = ReadInput();
            switch (input)
            {
                case "1":
                    SetupAccount();
                    break;
                case "2":
                    BankOptions();
                    break;
                case "3":
                    Logger.Info("Goodbye!");
                    exit = true;
                    break;
                default:
                    Logger.Error("Invalid request! Please enter a number from 1-3.");
                    break;
            }
        }
    }

    /// <summary>
    /// Reads input from the user until it is valid (not null or empty).
    /// </summary>
    /// <returns>A trimmed valid input.</returns>
    private static string ReadInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (input == null || input == "")
            {
                Logger.Error("Invalid input, please try again.");
                Logger.Input("");
            }
            else
            {
                return input.Trim();
            }
        }
    }

    /// <summary>
    /// Manages the bank options for the user's account, given it exists.
    /// </summary>
    private static void BankOptions()
    {
        string username = GetUsernameInput();

        var account = GetAccount(username);
        if (account == null) // check if the account doesn't exist
        {
            Logger.Error("Account not found!");
            return;
        }

        BankManager bankManager = new(account);

        bool back = false;
        while (!back)
        {
            DisplayBankOptions();
            var input = ReadInput();
            switch (input)
            {
                case "1":
                    DepositOption(account);
                    break;
                case "2":
                    WithdrawOption(account);
                    break;
                case "3":
                    bankManager.ViewBalance();
                    break;
                case "4":
                    back = true;
                    break;
                default:
                    Logger.Error("Invalid request! Please enter a number from 1-4.");
                    break;
            }
        }
    }

    private static Account? GetAccount(string username)
    {
        return _accounts.FirstOrDefault(account => account.Customer.Username == username);
    }

    private static bool AccountExists(string username)
    {
        return _accounts.Any(account => account.Customer.Username == username);
    }

    /// <summary>
    /// Sets up a new account for the user. Includes multiple validation checks for the user's input.
    /// </summary>
    private static void SetupAccount()
    {
        bool usernameUnique = false;
        string username = "";
        while (!usernameUnique)
        {
            Logger.Input("Enter a unique username");
            username = ReadInput();
            if (AccountExists(username))
            {
                Logger.Error("Username already exists! Please enter a unique one.");
            }
            else
            {
                usernameUnique = true;
            }
        }

        string email = GetEmailInput();
        string phoneNumber = GetPhoneNumberInput();
        string address = GetAddressInput();

        Logger.Input("Enter your initial deposit");
        var initialDeposit = ReadInput();
        bool converted = Decimal.TryParse(initialDeposit, out decimal depositResult);
        if (!converted) Logger.Warn("Invalid deposit amount! Therefore, it was set to 0. You can deposit money later.");

        var account = new Account
        {
            AccountNumber = Guid.NewGuid().ToString(), // Generate a unique account number
            Balance = converted ? depositResult : 0m,
            Customer = new Customer
            {
                Username = username,
                Address = address,
                Email = email,
                PhoneNumber = phoneNumber
            }
        };
        _accounts.Add(account);

        Logger.Success("Your account has been successfully created!");
    }

    /// <summary>
    /// Handles deposits for a user's account. Ensures deposit figure is valid.
    /// </summary>
    /// <param name="account">the user's account.</param>
    private static void DepositOption(Account account)
    {
        Console.Clear();
        Logger.Info("Deposit");
        Logger.Info("===============================================");

        bool validDeposit = false;
        decimal depositResult = 0m;
        while (!validDeposit)
        {
            Logger.Input("Enter the amount you want to deposit");
            var deposit = ReadInput().Replace("£", "");
            bool converted = Decimal.TryParse(deposit, out depositResult);
            if (!converted)
            {
                Logger.Error("Invalid deposit number! Please enter a valid amount");
            }
            else
            {
                validDeposit = true;
            }
        }

        BankManager bankManager = new(account);
        bankManager.Deposit(depositResult);
    }

    /// <summary>
    /// Handles withdrawals for a user's account. Ensures withdrawal figure is valid.
    /// </summary>
    /// <param name="account">the user's account.</param>
    private static void WithdrawOption(Account account)
    {
        Console.Clear();
        Logger.Info("Withdraw");
        Logger.Info("===============================================");

        Logger.Input("Enter the amount you want to withdraw");
        var withdraw = ReadInput().Replace("£", "");
        bool converted = Decimal.TryParse(withdraw, out decimal withdrawResult);
        while (!converted)
        {
            Logger.Input("Invalid withdraw number! Please enter a valid amount");
            withdraw = ReadInput();
            converted = Decimal.TryParse(withdraw, out withdrawResult);
        }

        BankManager bankManager = new(account);
        bankManager.Withdraw(withdrawResult);
    }

    private static void DisplayMainOptions()
    {
        Logger.Info("Select an option to continue");
        Logger.Info("1. Create a new account (1)");
        Logger.Info("2. Manage your account (2)");
        Logger.Info("3. Exit (3)");
        Logger.Input("");
    }

    private static void DisplayBankOptions()
    {
        Logger.Info("Select an option to continue");
        Logger.Info("1. Deposit (1)");
        Logger.Info("2. Withdraw (2)");
        Logger.Info("3. View Balance (3)");
        Logger.Info("4. Back (4)");
        Logger.Input("");
    }

    private static string GetUsernameInput()
    {
        Logger.Input("Enter your username");
        var username = ReadInput();
        return username;
    }

    private static string GetEmailInput()
    {
        bool emailValid = false;
        string email = "";

        while (!emailValid)
        {
            Logger.Input("Enter your email");
            email = ReadInput();
            if (!EmailRegex().IsMatch(email))
            {
                Logger.Error("Invalid email! Please enter a valid email address.");
            }
            else
            {
                emailValid = true;
            }
        }

        return email;
    }

    private static string GetPhoneNumberInput()
    {
        bool phoneNumberValid = false;
        string phoneNumber = "";

        while (!phoneNumberValid)
        {
            Logger.Input("Enter your phone number");

            phoneNumber = ReadInput();
            if (!PhoneNumberRegex().IsMatch(phoneNumber))
            {
                Logger.Error("Invalid input! Please enter a valid UK phone number.");
            }
            else
            {
                phoneNumberValid = true;
            }
        }

        return phoneNumber;
    }

    private static string GetAddressInput()
    {
        Logger.Input("Enter your address");
        var address = ReadInput();
        return address;
    }


    /*
     * Auto-generated code
     * Regexes generated by ChatGPT, tested with common use cases
    */

    [GeneratedRegex(@"^(?:(?:\(?(?:0(?:0|11)\)?[\s-]?\(?|\+)44\)?[\s-]?(?:\(?0\)?[\s-]?)?)|(?:\(?0))(?:(?:\d{5}\)?[\s-]?\d{4,5})|(?:\d{4}\)?[\s-]?(?:\d{5}|\d{3}[\s-]?\d{3}))|(?:\d{3}\)?[\s-]?\d{3}[\s-]?\d{3,4})|(?:\d{2}\)?[\s-]?\d{4}[\s-]?\d{4}))(?:[\s-]?(?:x|ext\.?|\#)\d{3,4})?$")]
    private static partial Regex PhoneNumberRegex();

    [GeneratedRegex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")]
    private static partial Regex EmailRegex();
}
