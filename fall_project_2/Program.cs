using System.Text.RegularExpressions;

internal class Program
{
    static bool terminateProgram = false;
    static bool isAuthenticated = false;
    static bool hasErrored = false;

    private static void Main(string[] args)
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
                            // TODO: 1. create user object
                            // TODO: 2. save user object to database
                            // TODO: 3. move to logout/create/delete/wallet

                            isAuthenticated = true;
                            ShowMainMenu();
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
                            // TODO: 1. verify user credentials
                            // TODO: 2. move to logout/create/delete/wallet

                            isAuthenticated = true;
                            hasErrored = false;
                            ShowMainMenu();

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

        static (string currency, string amount) CreateWallet()
        {
            Console.Write("Please choose currency: ");
            var currency = Console.ReadLine();
            Console.Write("Please enter your amount: ");
            var amount = Console.ReadLine();

            return (currency, amount);
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

        static void ShowMainMenu()
        {
            Console.WriteLine("Choose an option");
            Console.WriteLine("[1] Create Wallet");
            Console.WriteLine("[2] Choose Wallet");
            Console.WriteLine("[3] Delete Wallet");
            Console.WriteLine("[4] Logout");
            Console.WriteLine();
            Console.Write("Select an option: ");
            // orange loop
            do
            {

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Create Wallet Menu Selected");

                        break;
                    case "2":
                        Console.WriteLine("Choose Wallet Menu Selected");

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
    }
}