using System.Security.Cryptography.X509Certificates;

namespace DebuggingAndRefactoringTask1;

public class AccountService
{
    private readonly List<Account> accounts;

    public AccountService(List<Account> accounts)
    {
        this.accounts = accounts;
    }

    /// <summary>
    /// Add an account
    /// </summary>
    /// <param name="id">Id of account</param>
    /// <param name="name">Name of account holder</param>
    public void AddAccount(string id, string name)
    {
        if (accounts.Any(x => x.Id == id))
        {
            throw new InvalidOperationException($"Account with id '{id}' already exists.");
        }

        var account = new Account { Id = id, Name = name, Balance = 0 };
        accounts.Add(account);
    }

    /// <summary>
    /// Deposit money into an account
    /// </summary>
    /// <param name="id">Id of account</param>
    /// <param name="amount">Amount to deposit</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void DepositMoney(string id, decimal amount)
    {
        var account = FindAccount(id);

        account.Balance += amount;
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
        var account = FindAccount(id);

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

    /// <summary>
    /// Display account details
    /// </summary>
    /// <param name="id">Id of account</param>
    /// <exception cref="InvalidOperationException">Thrown if account doesn't exist</exception>
    public AccountDetails GetAccountDetails(string id)
    {
        var account = FindAccount(id);

        // Uses a different type for returning data so the original object can't be
        // accidentally modified and if Account was updated in the future, the extra
        // properties wouldn't be returned here
        // If more properties were needed to be returned, AutoMapper would make sense
        return new AccountDetails(
            account.Id,
            account.Name,
            account.Balance);
    }

    public Account FindAccount(string id)
    {
        return accounts.SingleOrDefault(x => x.Id == id)
            ?? throw new InvalidOperationException("Account not found.");
    }
}
