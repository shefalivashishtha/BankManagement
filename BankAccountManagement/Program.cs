
using BankAccountManagement.Business.Repositories;
using BankAccountManagement.Data.DataAccessor;
using BankAccountManagement.Data.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUserAccessor, UserAccessor>();
builder.Services.AddTransient<IAccountAccessor, AccountAccessor>();
builder.Services.AddTransient<ILoanApplicationAccessor, LoanApplicationAccessor>();
builder.Services.AddTransient<ITestGeneratorAccessor, TestGeneratorAccessor>();
builder.Services.AddTransient<ITransactionAccessor, TransactionAccessor>();
builder.Services.AddTransient<IFileWriter, FileWriter>();
builder.Services.AddTransient<IFileReader, FileReader>();

builder.Services.AddTransient<ILoanService, LoanService>();
builder.Services.AddTransient<IUserAccountManagementService, UserAccountManagementService>();
builder.Services.AddTransient<ITestGeneratorService, TestGeneratorService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseCors("AllowLocalhost");

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

