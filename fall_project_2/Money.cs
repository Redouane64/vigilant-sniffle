using System.Text.Json;
using System.Text.Json.Serialization;

using fall_project_2.Enums;

namespace fall_project_2;

public sealed class Money
{

    private readonly ulong _integer;

    [JsonIgnore]
    public ulong Integer => _integer;

    private readonly ushort _fraction;

    [JsonIgnore]
    public ushort Fraction => _fraction;

    private readonly char _sign;
    public Char Sign => _sign;

    [JsonPropertyName("value")]
    private readonly string _raw;

    [JsonPropertyName("currency")]
    public Currency Currency { get; }

    public override string ToString()
    {
        return $"{_integer}.{_fraction}";
    }

    public Money(string money, Currency currency)
    {
        _raw = money;
        Currency = currency;
        String[] newValue = money.Split('.');
        this._integer = Convert.ToUInt64(newValue[0]);
        this._fraction = Convert.ToUInt16(newValue[1]);
        this._sign = money.StartsWith('-') ? '-' : '+';

    }
}