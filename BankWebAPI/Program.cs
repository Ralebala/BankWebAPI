using BankWebAPI;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. REGISTER CORE WEB API SERVICES
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 2. CONFIGURE THE SECURITY PASS WITH CORS ENTRIES
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDashboard", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 3. CONFIGURE WEB REQUEST PIPELINES (THE EXACT CRITICAL ORDER)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// FIRST: Turn on static file asset streaming permissions
app.UseStaticFiles();

// SECOND: Activate your security pass directly BEFORE endpoints load up!
app.UseCors("AllowFrontendDashboard");

app.MapControllers();

// 4. FORCE-POPULATE REAL DATA ROWS TO POSTGRESQL
using (var scope = app.Services.CreateScope())
{
    using (var context = new BankDbContext())
    {
        try
        {
            // 1. Wipe out any empty conflicting table placeholders inside PostgreSQL
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.bankaccounts RESTART IDENTITY CASCADE;");

            // 2. Directly add our 3 real corporate banking client profiles
            context.SavingsAccounts.Add(new SavingsAccount("Thabo Mokoena", 2500, 0.02));
            context.SavingsAccounts.Add(new SavingsAccount("Alice Smith", 5000, 0.05));
            context.SavingsAccounts.Add(new SavingsAccount("John Doe", 100, 0.01));

            // 3. Save them permanently onto the server disk rows
            context.SaveChanges();
            Console.WriteLine("[SYSTEM COMPLETE] 3 Real Corporate Banking Profiles Loaded Successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SEEDING OVERRIDE LOG]: {ex.Message}");
        }
    }
}


app.Run(); // The absolute final execution line block
