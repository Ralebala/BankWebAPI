using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankWebAPI.Controllers
{
    // These tags tell the computer that this class handles internet routing requests 
    // accessible at the web address path: http://localhost:PORT/api/accounts
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        // 1. GET ALL ACCOUNTS: This is our first live web endpoint!
        [HttpGet]
        public ActionResult<List<BankAccount>> GetAllAccounts()
        {
            // Open an automated EF Core database gateway session
            using (var context = new BankDbContext())
            {
                // Fetch all accounts from your PostgreSQL rows and turn them into a list
                var bankDatabase = context.BankAccounts.ToList();

                // Throw the records over the internet wall to the frontend user!
                return Ok(bankDatabase);
            }
        }

        // 2. PROCESS DEPOSIT OVER THE WEB: http://localhost:PORT/api/accounts/deposit
        [HttpPost("deposit")]
        public ActionResult ProcessDeposit(int id, double amount)
        {
            using (var context = new BankDbContext())
            {
                // Search your PostgreSQL database table rows to find the account matching the incoming ID
                var account = context.BankAccounts.Find(id);

                if (account == null)
                {
                    // If the ID doesn't exist, send a standard 404 Error message over the web
                    return NotFound($"[ERROR] Bank account profile with ID {id} was not found.");
                }

                // Run your strict C# validation and calculation kitchen rules!
                account.Deposit(amount);

                // Tell EF Core to automatically write the background SQL 'UPDATE' statement and sync it to PostgreSQL!
                context.SaveChanges();

                // Send a successful 200 OK message back to the frontend with the client's updated status
                return Ok($"[SUCCESS] Successfully deposited R{amount:N0} to {account.AccountHolder}. New Balance: R{account.Balance:N0}");
            }
        }
        // 3. PROCESS WITHDRAWAL OVER THE WEB: http://localhost:PORT/api/accounts/withdrawal
        [HttpPost("withdrawal")]
        public ActionResult ProcessWithdrawal(int id, double amount)
        {
            using (var context = new BankDbContext())
            {
                // 1. Fetch the targeted customer account profile row straight from PostgreSQL
                var account = context.BankAccounts.Find(id);

                if (account == null)
                {
                    return NotFound($"[ERROR] Bank account profile with ID {id} was not found.");
                }

                // 2. Run your strict business gatekeeper kitchen logic!
                if (account.Balance < amount)
                {
                    // If they don't have enough money, return a standard 400 Bad Request error code over the web
                    return BadRequest($"[DENIED] Insufficient funds for {account.AccountHolder}. Current Balance: R{account.Balance:N0}");
                }

                // Deduct the money safely from their balance property
                account.Withdrawal(amount);

                // 3. Sync the updated numbers permanently to your database server disk records
                context.SaveChanges();

                return Ok($"[SUCCESS] Successfully withdrew R{amount:N0} from {account.AccountHolder}. Remaining Balance: R{account.Balance:N0}");
            }
        }
    }
}
