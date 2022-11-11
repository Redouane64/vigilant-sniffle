using fall_project_2.Enums;

namespace fall_project_2;

public class Income : Operation
{
    public IncomeType Type;

    public Income(Money value, DateTime date) : base(value, date)
    {
    }
}