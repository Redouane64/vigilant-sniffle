using System.Text.RegularExpressions;

using fall_project_2;
using fall_project_2.Enums;

using Storage = fall_project_2.Services.Storage;

internal class Program
{
    static bool terminateProgram = false;
    static bool isAuthenticated = false;
    static bool hasErrored = false;

    private static Storage storage = new Storage();

    private static async Task Main(string[] args)
    {

        do
        {
            // red program loop
            Console.WriteLine("Welcome to your personal finance console app");
            Console.WriteLine("Choose an option");
            Console.WriteLine("[1] register");
            Console.WriteLine("[2] login");
            Console.WriteLine("[3] exit");
            Console.WriteLine();
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":

                    // green program loop
                    do
                    {

                        try
                        {
                            var input = Register();
                            _ = await storage.RegisterUser(input.name, input.email, input.password);

                            hasErrored = false;
                            isAuthenticated = true;
                            await ShowMainMenu();
                        }
                        catch (Exception exception)
                        {
                            Console.Error.WriteLine($"Unable to register: {exception.Message}");
                            Console.WriteLine();

                            hasErrored = true;
                        }


                        if (!hasErrored && !isAuthenticated)
                        {
                            break;
                        }

                    } while (true);

                    break;

                case "2":

                    // green program loop
                    do
                    {
                        try
                        {
                            var input = Login();
                            _ = await storage.Login(input.email, input.password);

                            isAuthenticated = true;
                            hasErrored = false;
                            await ShowMainMenu();

                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"Login failed: {exception.Message}");
                            Console.WriteLine();

                            hasErrored = true;
                        }

                        if (!hasErrored && !isAuthenticated)
                        {
                            break;
                        }

                    } while (true);

                    break;
                case "3":
                    Console.WriteLine("GoodBye!");
                    terminateProgram = true;
                    break;
            }
        } while (!terminateProgram);
    }

    static (string name, string currency, string amount) CreateWallet()
    {
        Console.Write("Enter wallet name: ");
        var name = Console.ReadLine();
        Console.Write("Please choose currency: ");
        var currency = Console.ReadLine();
        Console.Write("Please enter your amount: ");
        var amount = Console.ReadLine();

        return (name, currency, amount);
    }

    static (string email, string password) Login()
    {
        Console.Write("Please enter your email: ");
        var email = Console.ReadLine();
        if (!Regex.IsMatch(email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"))
        {
            throw new Exception("Invalid email address");
        }

        Console.Write("Please enter your password: ");
        var password = ReadPassword();
        if (password.Length > 4)
        {
            throw new Exception("Password must be at least 4 characters length");
        }

        return (email, password);
    }

    static (string name, string email, string password) Register()
    {
        Console.Write("Please enter your name: ");
        var name = Console.ReadLine();
        if (!Regex.IsMatch(name, "[a-zA-Z]"))
        {
            throw new Exception("Invalid name");
        }

        Console.Write("Please enter your email: ");
        var email = Console.ReadLine();
        if (!Regex.IsMatch(email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"))
        {
            throw new Exception("Invalid email address");
        }

        Console.Write("Please enter your password: ");
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

        return new string(value.ToArray());
    }

    static string AskForPassword(string email)
    {
        Console.Write($"Enter ({email}) password: ");
        return ReadPassword();
    }

    static (string amount, string date, string category) AddOperation()
    {
        Console.Write("Please enter your amount: ");
        var amount = Console.ReadLine();
        Console.Write("Please enter your date: ");
        var date = Console.ReadLine();
        Console.Write("Choose your category: ");
        var category = Console.ReadLine();

        return (amount, date, category);
    }

    static string ChooseWallet()
    {
        Console.Write("Please enter your wallet name: ");
        var name = Console.ReadLine();

        return name;
    }

    static async Task ShowMainMenu()
    {
        Console.WriteLine("Choose an option");
        Console.WriteLine("[1] Create Wallet");
        Console.WriteLine("[2] Choose Wallet");
        Console.WriteLine("[4] Logout");
        Console.WriteLine();
        Console.Write("Select an option: ");
        // orange loop
        do
        {

            switch (Console.ReadLine())
            {
                // grey loop
                case "1":
                    await ShowCreateWallet();

                    break;
                case "2":
                    Console.WriteLine("Choose Wallet Menu Selected");
                    // TODO: 1. ask user to choose wallet
                    // TODO: 2. set selected wallet as Active Wallet
                    ShowWalletMainMenu();
                    break;
                case "3":
                    Console.WriteLine("Delete Wallet Menu Selected");

                    break;

                case "4":
                    isAuthenticated = false;

                    Console.WriteLine("Logged out successfully!");
                    break;
            }

        } while (isAuthenticated);
    }

    static async Task ShowCreateWallet()
    {
        hasErrored = false;
        do
        {
            try
            {
                var input = CreateWallet();

                // TODO: fix this
                if (!Enum.TryParse(input.currency, true, out Currency currency))
                {
                    throw new ArgumentException("Invalid currency selected");
                }

                await storage.CreateWallet(input.name, currency, new Money(input.amount, currency));
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"Unable to create wallet: {exception.Message}");
                Console.WriteLine();

                hasErrored = true;
            }
        } while (hasErrored);
    }

    static void ShowChooseWallet()
    {
        // TODO: 1. Fetch and show user wallet list by name
        // TODO: 2. Read user selection and set it as active wallet
        //          call Storage.SetActiveWallet
    }

    static void ShowDeleteWallet()
    {
        // TODO: 1. call Storage.DeleteWallet()
        // TODO: 2. Prompt user to select new active wallet
    }

    static void LogOut()
    {

    }

    static void ShowWalletMainMenu()
    {
        // We have an active wallet selection

        Console.WriteLine("Choose an option");
        Console.WriteLine("[1] Add Income Operation");
        Console.WriteLine("[2] Add Expense Operation");
        Console.WriteLine("[3] Delete Wallet");
        Console.WriteLine("[4] Go Back");
        Console.WriteLine("[5] Logout");
        Console.WriteLine();
        Console.Write("Select an option: ");
        // orange loop
        do
        {

            switch (Console.ReadLine())
            {
                // grey loop
                case "1":
                    Console.WriteLine("Add Operation");
                    // TODO: 1. ask user for operation type
                    // TODO: 2. build operation object
                    // TODO: 3. call Storage.AddOperation

                    break;
                case "3":
                    Console.WriteLine("Delete Wallet");
                    // TODO: 1. call Storage.DeleteWallet
                    // TODO: 2. return to previous menu
                    return;

                case "4":
                    Console.WriteLine("Go Back");
                    return;

                case "5":
                    isAuthenticated = false;

                    Console.WriteLine("Logged out successfully!");
                    break;
            }

        } while (isAuthenticated);
    }

    static Operation CreateWalletOperation()
    {
        // TODO: 1. ask user for category
        // TODO: 2. ask user for operation money amount
        // TODO: 3. build operation object
        return null;
    }
}