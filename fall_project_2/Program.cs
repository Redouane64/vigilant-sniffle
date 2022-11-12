Console.Clear();
Console.WriteLine("Welcome to your personal finance console app");
Console.WriteLine("Choose an option");
Console.WriteLine("[1] register");
Console.WriteLine("[2] login");
Console.WriteLine("[3] exit");
Console.Write("\r\nSelect an option: ");

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

static (String, String) CreateWallet()
{
    var currency = Console.ReadLine();
    var amount = Console.ReadLine();

    return (currency, amount);
}

static (String, String) Login()
{
    var email = Console.ReadLine();
    var password = Console.ReadLine();
    return (email, password);
}

static (String, String, String) Register()
{
    var name = Console.ReadLine();
    var email = Console.ReadLine();
    var password = Console.ReadLine();

    return (name, email, password);
}