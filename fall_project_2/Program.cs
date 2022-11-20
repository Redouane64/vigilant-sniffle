Console.Clear();
Console.WriteLine("Welcome to your personal finance console app");
Console.WriteLine("Choose an option");
Console.WriteLine("[1] register");
Console.WriteLine("[2] login");
Console.WriteLine("[3] exit");
Console.Write($"{Environment.NewLine}Select an option: ");

switch (Console.ReadLine())
{
    case "1":
        Console.WriteLine("This is the register option");
        break;
    case "2":
        Console.WriteLine("This is the login option");
        break;
    case "3":
        Console.WriteLine("GoodBye!");
        break;
}

static (String currency, String amount) CreateWallet()
{
    var currency = Console.ReadLine();
    var amount = Console.ReadLine();

    return (currency, amount);
}

static (String email, String password) Login()
{
    var email = Console.ReadLine();
    var password = ReadPassword();
    return (email, password);
}

static (String name, String email, String password) Register()
{
    var name = Console.ReadLine();
    var email = Console.ReadLine();
    var password = ReadPassword();

    return (name, email, password);
}

// Helper to read masked password
static string ReadPassword()
{
    var value = new List<char>();
    while (true)
    {
        var key = Console.ReadKey(true);
        Console.Write('*');

        if (key.Key == ConsoleKey.Enter)
        {
            break;
        }

        value.Add(key.KeyChar);
    }

    return new String(value.ToArray());
}