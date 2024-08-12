namespace DebuggingAndRefactoringTask1;

public class ConsoleBankApp
{
    private readonly AccountService accountService;

    public ConsoleBankApp()
    {
        // Note: For a real app, use DI and inject a singleton instance of AccountService.
        // AccountService would then have a database injected into it.
        var accounts = new List<Account>();
        accountService = new AccountService(accounts);
    }

    public void Run()
    {
        string choice = string.Empty;
        while (choice != "5")
        {
            Write("1. Add Account");
            Write("2. Deposit Money");
            Write("3. Withdraw Money");
            Write("4. Display Account Details");
            Write("5. Exit");

            choice = ReadString();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddAccount();
                        break;
                    case "2":
                        DepositMoney();
                        break;
                    case "3":
                        WithdrawMoney();
                        break;
                    case "4":
                        DisplayAccountDetails();
                        break;
                    case "5":
                        break;
                    default:
                        Write("Invalid choice.");
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                Write(e.Message);
            }
        }
    }

    private void AddAccount()
    {
        Write("Enter Account ID:");
        string id = ReadString();

        Write("Enter Account Holder Name:");
        string name = ReadString();

        accountService.AddAccount(id, name);

        Write("Account added successfully.");
    }

    private void DepositMoney()
    {
        Write("Enter Account ID:");
        string id = ReadString();

        Write("Enter Amount to Deposit:");
        decimal amount = ReadDecimal();

        accountService.DepositMoney(id, amount);

        Write("Amount deposited successfully.");
    }

    private void WithdrawMoney()
    {
        Write("Enter Account ID:");
        string id = ReadString();

        Write("Enter Amount to Withdraw:");
        decimal amount = ReadDecimal();

        bool success = accountService.WithdrawMoney(id, amount);

        if (success)
        {
            Write("Amount withdrawn successfully.");
        }
        else
        {
            Write("Insufficient balance.");
        }
    }

    private void DisplayAccountDetails()
    {
        Write("Enter Account ID:");
        string id = ReadString();

        AccountDetails accountDetails = accountService.GetAccountDetails(id);

        Write($"Account ID: {accountDetails.Id}");
        Write($"Account Holder Name: {accountDetails.Name}");
        Write($"Account Balance: {accountDetails.Balance}");
    }

    private static string ReadString()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    private static decimal ReadDecimal()
    {
        var str = ReadString();

        return decimal.TryParse(str, out decimal val) ? val : throw new InvalidOperationException("Invalid number entered.");
    }

    private static void Write(string message)
    {
        Console.WriteLine(message);
    }
}