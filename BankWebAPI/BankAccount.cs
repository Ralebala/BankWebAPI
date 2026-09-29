using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAPI
{
    [Table("bankaccounts")]
    public abstract class BankAccount
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("accountholder")]
        public string AccountHolder { get; set; }

        [Column("email_address")] // Tells EF Core what the lowercase column name should be in PostgreSQL!
        public string EmailAddress { get; set; } = "customer@bank.com"; // We set a default email string value

        [Column("balance")]
        public double Balance { get; protected set; }

        public BankAccount(string accountHolder, double balance)
        {
            AccountHolder = accountHolder;
            Balance = balance >= 0 ? balance : 0;
        }

        public void Deposit(double amount)
        {
            if (amount > 0) Balance += amount;
        }

        public void Withdrawal(double amount)
        {
            if (amount > 0 && Balance >= amount) Balance -= amount;
        }
    }
}
