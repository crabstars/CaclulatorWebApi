var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/prime/{number:int}", (int number) =>
    Results.Ok(new PrimeCheckResponse(number, PrimeChecker.IsPrime(number))))
    .WithName("CheckPrimeNumber");

app.Run();

record PrimeCheckResponse(int Number, bool IsPrime);

static class PrimeChecker
{
    public static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        if (number == 2)
        {
            return true;
        }

        if (number % 2 == 0)
        {
            return false;
        }

        var limit = (int)Math.Sqrt(number);

        for (var divisor = 3; divisor <= limit; divisor += 2)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }
}

public partial class Program;
