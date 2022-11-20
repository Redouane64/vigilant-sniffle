namespace fall_project_2;

public abstract class Operation
{
    public int Id { get; set; }

    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }

    public DateTime Date { get; }

    public Money Value { get; }

    public Operation(Money value, DateTime date)
    {
        Value = value;
        Date = date;
    }
}