
using fall_project_2.Enums;

namespace fall_project_2;

public class Wallet
{
    public Wallet()
    { }

    public Wallet(string name, Currency currency, Money amount)
    {
        Name = name;
        Currency = currency;
        Amount = amount;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public Currency Currency { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public List<Operation> Operations { get; set; }

    public Money Amount { get; set; }

    public void AddOperation(Operation operation)
    {
        if (operation.Value.Currency != this.Currency)
        {
            // TODO: throw custom exception indicating the wallet does not support the operation currency
            throw new Exception();
        }

        Operations.Add(operation);
    }

    public void CollectStatistic(DateTime from, DateTime to)
    {
        var totalIncomeOperations = this.Operations.OfType<Income>().Count();
        var incomeOperations = this.Operations
            .OfType<Income>()
            .Where(operation => operation.Date >= from && operation.Date <= to)
            .GroupBy(operation => operation.Type)
            .Select(operation => new
            {
                Type = operation.Key,
                Overview = operation.Count() / totalIncomeOperations,
                Total = operation.Count(),
                Operations = operation.ToArray()
            })
            .ToArray();

        var totalExpenseOperations = this.Operations.OfType<Expense>().Count();
        var expenseOperations = this.Operations
            .OfType<Expense>()
            .Where(operation => operation.Date >= from && operation.Date <= to)
            .GroupBy(operation => operation.Type)
            .Select(operation => new
            {
                Type = operation.Key,
                Overview = operation.Count() / totalIncomeOperations,
                Total = operation.Count(),
                Operations = operation.ToArray()
            })
            .ToArray();

        // TODO: build string to print operations aggregates
    }

}