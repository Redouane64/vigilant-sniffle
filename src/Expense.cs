using fall_project_2.Enums;

namespace fall_project_2;

public class Expense : Operation
{
    public ExpenseType Type { get; set; }


    public Expense(Money value, DateTime date) : base(value, date)
    {
    }
}