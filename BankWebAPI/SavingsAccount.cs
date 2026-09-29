using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAPI
{
    public class SavingsAccount : BankAccount
    {
        [Column("interestrate")]
        public double InterestRate { get; set; }

        public SavingsAccount(string accountHolder, double balance, double interestRate)
            : base(accountHolder, balance)
        {
            InterestRate = interestRate;
        }

        public void ApplyInterest()
        {
            double interestAmount = Balance * InterestRate;
            Deposit(interestAmount);
        }
    }
}
