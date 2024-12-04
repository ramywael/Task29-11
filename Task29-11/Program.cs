using System.Diagnostics.Metrics;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task29_11
{

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public static Account operator +(Account acc1, Account acc2)
        {

            Account account = new Account();
            account.Balance = acc2.Balance + acc1.Balance;

            return account;
        }


        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public override string ToString() {

            return $"Name is {Name} and the balance is {Balance}";
        }

    }
    public class SavingAccount : Account
    {
        public SavingAccount( string name="Ramy", double balance=5000, double interestRate=0.25) : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public double InterestRate { get; set; }

        public override bool Deposit(double amount)
        {
            return base.Deposit(amount+InterestRate);
        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount);
        }

        public override string ToString()
        {

            return $"{base.ToString()} and the rate is {InterestRate}";
        }

    }


    public class CheckingAccount : Account
    {
        const double fee = 1.50;
        public CheckingAccount(string name="Ramy", double balance=5000): base(name, balance)
        {

        }
        public override bool Deposit(double amount)
        {
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            Balance -= fee;
            return base.Withdraw(amount);
        }

        public override string ToString()
        {

            return $"{base.ToString()} and the rate is the fee is {fee}";
        }

    }

    public class TrustAccount : SavingAccount
    {
        const double bonus = 50;
        int Counter = 1;
        public TrustAccount( string name="Ramy", double balance=6000, double interestRate = 0.25) : base( name, balance, interestRate)
        {

        }


        public override bool Withdraw(double amount)
        {
            if ((Counter != 3) && (amount) < (0.20 * Balance))
            {
                Counter++;
                return base.Withdraw(amount);
            }
            if (Counter == 3)
            {
                Console.WriteLine("Sorry You Hit The Limit of Withdrawyearly");
                return false;
            }
            if ((Balance - amount) > (0.20 * Balance))
            {
                Console.WriteLine("Sorry You should Withdraw only 20% of the balance");
                return false;
            }
            return false;


        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                Balance += bonus;
            }

            return base.Deposit(amount);
        }

        public override string ToString()
        {

            return $"{base.ToString()}";
        }

    }
    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            List<Account> accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);


            // Savings
            List<Account> savAccounts = new List<Account>();
            savAccounts.Add(new SavingAccount());
            savAccounts.Add(new SavingAccount("Superman"));
            savAccounts.Add(new SavingAccount("Batman", 2000));
            savAccounts.Add(new SavingAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            List<Account> checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            List<Account> trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
        }
    }
}
