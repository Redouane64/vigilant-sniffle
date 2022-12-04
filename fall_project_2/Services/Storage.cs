using System.Security.Cryptography;
using System.Text;

using fall_project_2.Data;

namespace fall_project_2.Services;

public class Storage : IDisposable
{
    private static ApplicationDataContext _context = new DatabaseContextFactory().CreateDbContext(null);

    private static User s_activeUser = null;
    public static User ActiveUser
    {
        get => s_activeUser ?? throw new InvalidOperationException("Active user is not set");
    }

    private static Wallet s_activeWallet = null;

    public static Wallet ActiveWallet
    {
        get => s_activeWallet ?? throw new InvalidOperationException("Active wallet is not set");
    }

    public Task<User> RegisterUser(string email, string password)
    {

        // TODO: 1. Hash password
        var hasher = MD5.Create();
        var passwordHash = Convert.ToBase64String((hasher.ComputeHash(Encoding.ASCII.GetBytes(password))));
        hasher.Dispose();

        // TODO: 2. build user object
        // TODO: 3. save user object to database

        return null;
    }

    public Task<User> Login(string email, string password)
    {
        // TODO: 1. Hash password
        var hasher = MD5.Create();
        var passwordHash = Convert.ToBase64String((hasher.ComputeHash(Encoding.ASCII.GetBytes(password))));
        hasher.Dispose();

        // TODO: 2. fetch user by email

        // TODO: 3. verify fetched user passwordHash field to match with passwordHash

        return null;
    }

    public Task<Wallet[]> GetUserWallets(string userEmail) { return null; }

    public Task SetActiveWallet(Wallet wallet) { return null; }

    public Task CreateWallet()
    {
        // TODO: 1. create wallet object
        // TODO: 2. save wallet object to database
        // TODO: 3. set as active wallet
        return null;
    }

    public Task DeleteWallet()
    {
        // TODO: 1. delete active wallet

        return null;
    }

    public Task AddOperation()
    {
        // ActiveWallet.Operations.Add();
        return null;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}