namespace DebuggingAndRefactoringTask1;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Add Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. Display Account Details");
            Console.WriteLine("5. Exit");

            string? choice = Console.ReadLine();

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
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
