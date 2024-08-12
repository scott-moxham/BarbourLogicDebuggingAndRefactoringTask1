namespace DebuggingAndRefactoringTask1.Tests;

public class AccountServiceTests
{
    private readonly List<Account> accounts;
    private readonly AccountService accountService;

    public AccountServiceTests()
    {
        accounts = [];
        accountService = new AccountService(accounts);
    }

    [Fact]
    public void AddAccount_ShouldAddNewAccount()
    {
        // Arrange
        const string id = "123";
        const string name = "John Doe";

        // Act
        accountService.AddAccount(id, name);

        // Assert
        accounts.Should().HaveCount(1);

        var account = accounts[0];
        account.Id.Should().Be(id);
        account.Name.Should().Be(name);
        account.Balance.Should().Be(0);
    }

    [Fact]
    public void AddAccount_ShouldAddMultipleNewAccounts()
    {
        // Arrange
        const string id1 = "123";
        const string name1 = "John Doe";

        const string id2 = "456";
        const string name2 = "Bob Smith";

        // Act
        accountService.AddAccount(id1, name1);
        accountService.AddAccount(id2, name2);

        // Assert
        accounts.Should().HaveCount(2);

        var account1 = accounts.Single(x => x.Id == id1);
        account1.Id.Should().Be(id1);
        account1.Name.Should().Be(name1);
        account1.Balance.Should().Be(0);

        var account2 = accounts.Single(x => x.Id == id2);
        account2.Id.Should().Be(id2);
        account2.Name.Should().Be(name2);
        account2.Balance.Should().Be(0);
    }

    [Fact]
    public void AddAccount_ShouldThrowException_WhenAccountWithSameIdExists()
    {
        // Arrange
        const string id = "123";
        const string name1 = "John Doe";
        const string name2 = "Bob Smith";

        accounts.Add(new Account { Id = id, Name = name1, Balance = 54 });

        // Act
        var fn = () => accountService.AddAccount(id, name2);

        // Assert
        fn.Should().Throw<InvalidOperationException>().WithMessage($"Account with id '{id}' already exists.");
    }

    [Theory]
    [InlineData(0, 100, 100)]
    [InlineData(50, 100, 150)]
    [InlineData(50.67, 100.58, 151.25)]
    public void DepositMoney_ShouldIncreaseAccountBalance(decimal initialBalance, decimal deposit, decimal expectedBalance)
    {
        // Arrange
        const string id1 = "123";
        const string name1 = "John Doe";

        const string id2 = "456";
        const string name2 = "Bob Smith";
        const decimal initialBalance2 = 40;

        const string id3 = "789";
        const string name3 = "Jane Doe";
        const decimal initialBalance3 = 60;

        accounts.Add(new Account { Id = id2, Name = name2, Balance = initialBalance2 });
        accounts.Add(new Account { Id = id1, Name = name1, Balance = initialBalance });
        accounts.Add(new Account { Id = id3, Name = name3, Balance = initialBalance3 });

        // Act
        accountService.DepositMoney(id1, deposit);

        // Assert
        var account1 = accounts.Single(x => x.Id == id1);
        account1.Balance.Should().Be(expectedBalance);

        var account2 = accounts.Single(x => x.Id == id2);
        account2.Balance.Should().Be(initialBalance2);

        var account3 = accounts.Single(x => x.Id == id3);
        account3.Balance.Should().Be(initialBalance3);
    }

    [Fact]
    public void DepositMoney_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        var id = "123";
        var amount = 100;

        accounts.Add(new Account { Id = "456", Name = "Bob Smith", Balance = 54 });

        // Act
        var fn = () => accountService.DepositMoney(id, amount);

        // Assert
        fn.Should().Throw<InvalidOperationException>().WithMessage("Account not found.");
    }

    [Theory]
    [InlineData(25, 0, 25)]
    [InlineData(100, 25, 75)]
    [InlineData(100.21, 25.32, 74.89)]
    [InlineData(100, 100, 0)]
    [InlineData(0, 0, 0)]
    public void WithdrawMoney_ShouldDecreaseAccountBalance(decimal initialBalance, decimal withdrawal, decimal expectedBalance)
    {
        // Arrange
        const string id1 = "123";
        const string name1 = "John Doe";

        const string id2 = "456";
        const string name2 = "Bob Smith";
        const decimal initialBalance2 = 40;

        const string id3 = "789";
        const string name3 = "Jane Doe";
        const decimal initialBalance3 = 60;

        accounts.Add(new Account { Id = id2, Name = name2, Balance = initialBalance2 });
        accounts.Add(new Account { Id = id1, Name = name1, Balance = initialBalance });
        accounts.Add(new Account { Id = id3, Name = name3, Balance = initialBalance3 });

        // Act
        var result = accountService.WithdrawMoney(id1, withdrawal);

        // Assert
        result.Should().BeTrue();

        var account1 = accounts.Single(x => x.Id == id1);
        account1.Balance.Should().Be(expectedBalance);

        var account2 = accounts.Single(x => x.Id == id2);
        account2.Balance.Should().Be(initialBalance2);

        var account3 = accounts.Single(x => x.Id == id3);
        account3.Balance.Should().Be(initialBalance3);
    }

    [Theory]
    [InlineData(0, 25)]
    [InlineData(10, 50)]
    [InlineData(10.37, 10.38)]
    public void WithdrawMoney_ShouldReturnFalse_WhenInsufficientBalance(decimal initialBalance, decimal withdrawal)
    {
        // Arrange
        const string id1 = "123";
        const string name1 = "John Doe";

        const string id2 = "456";
        const string name2 = "Bob Smith";
        const decimal initialBalance2 = 40;

        const string id3 = "789";
        const string name3 = "Jane Doe";
        const decimal initialBalance3 = 60;

        accounts.Add(new Account { Id = id2, Name = name2, Balance = initialBalance2 });
        accounts.Add(new Account { Id = id1, Name = name1, Balance = initialBalance });
        accounts.Add(new Account { Id = id3, Name = name3, Balance = initialBalance3 });

        // Act
        var result = accountService.WithdrawMoney(id1, withdrawal);

        // Assert
        result.Should().BeFalse();

        var account1 = accounts.Single(x => x.Id == id1);
        account1.Balance.Should().Be(initialBalance);

        var account2 = accounts.Single(x => x.Id == id2);
        account2.Balance.Should().Be(initialBalance2);

        var account3 = accounts.Single(x => x.Id == id3);
        account3.Balance.Should().Be(initialBalance3);
    }

    [Fact]
    public void WithdrawMoney_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        const string id = "123";
        const decimal amount = 100;

        accounts.Add(new Account { Id = "456", Name = "Bob Smith", Balance = 54 });

        // Act
        var fn = () => accountService.WithdrawMoney(id, amount);

        // Assert
        fn.Should().Throw<InvalidOperationException>().WithMessage("Account not found.");
    }

    [Fact]
    public void DisplayAccountDetails_ShouldReturnAccountDetails()
    {
        // Arrange
        const string id = "123";
        const string name = "John Doe";
        const decimal balance = 100;

        var account = new Account { Id = id, Name = name, Balance = balance };
        accounts.Add(account);

        // Act
        var accountDetails = accountService.GetAccountDetails(id);

        // Assert
        accountDetails.Id.Should().Be(id);
        accountDetails.Name.Should().Be(name);
        accountDetails.Balance.Should().Be(balance);
    }

    [Fact]
    public void GetAccountDetails_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        const string id = "123";

        accounts.Add(new Account { Id = "456", Name = "Bob Smith", Balance = 54 });

        // Act
        var fn = () => accountService.GetAccountDetails(id);

        // Act & Assert
        fn.Should().Throw<InvalidOperationException>().WithMessage("Account not found.");
    }
}
