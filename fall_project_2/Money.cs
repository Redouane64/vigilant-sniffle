using fall_project_2.Enums;

namespace fall_project_2;

public sealed class Money
{

    private readonly ulong _integer;
    public ulong Integer => _integer;

    private readonly ushort _fraction;
    public ushort Fraction => _fraction;

    private readonly char _sign;
    public Char Sign => _sign;

    public Currency Currency { get; }

    public override string ToString()
    {
        return $"{_integer}.{_fraction}";
    }

    public Money(string money, Currency currency)
    {
        Currency = currency;
        String[] newValue = money.Split('.');
        this._integer = Convert.ToUInt64(newValue[0]);
        this._fraction = Convert.ToUInt16(newValue[1]);
        this._sign = money.StartsWith('-') ? '-' : '+';

    }
}