namespace DebuggingAndRefactoringTask1;

public class AccountService
{
    private readonly List<Account> accounts = [];

    /// <summary>
    /// Add an account
    /// </summary>
    /// <param name="id">Id of account</param>
    /// <param name="name">Name of account holder</param>
    public void AddAccount(string id, string name)
    {
        // TOOO: Check if account with same ID already exists
        var account = new Account { Id = id, Name = name, Balance = 0 };
        accounts.Add(account);
    }

    public decimal DepositMoney(string id, decimal amount)
    {
        foreach (var account in accounts)
        {
            if (account.Id == id)
            {
                account.Balance += amount;

                return account.Balance;
            }
        }

        throw new InvalidOperationException("Account not found.");
    }

    /// <summary>
    /// Withdraws money from the account with the specified ID.
    /// </summary>
    /// <param name="id">Account id</param>
    /// <param name="amount">Amount to withdraw</param>
    /// <returns>true if withdrawal successful, false if not</returns>
    /// <exception cref="InvalidOperationException">Thrown if account doesn't exist</exception>
    public bool WithdrawMoney(string id, decimal amount)
    {
        foreach (var account in accounts)
        {
            if (account.Id == id)
            {
                if (account.Balance >= amount)
                {
                    account.Balance -= amount;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        throw new InvalidOperationException("Account not found.");
    }

    public void DisplayAccountDetails()
    {
        Console.WriteLine("Enter Account ID:");
        string? id = Console.ReadLine();

        foreach (var account in accounts)
        {
            if (account.Id == id)
            {
                Console.WriteLine($"Account ID: {account.Id}");
                Console.WriteLine($"Account Holder: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance}");
                return;
            }
        }

        Console.WriteLine("Account not found.");
    }
}
