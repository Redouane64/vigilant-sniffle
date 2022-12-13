using System.Security.Cryptography;
using System.Text;

using fall_project_2.Data;
using fall_project_2.Enums;

using Microsoft.EntityFrameworkCore;

namespace fall_project_2.Services;

public class Storage : IDisposable
{
    private ApplicationDataContext _context = new DatabaseContextFactory().CreateDbContext(null);

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

    public async Task<User> RegisterUser(string name, string email, string password)
    {

        var hasher = MD5.Create();
        var passwordHash = Convert.ToBase64String((hasher.ComputeHash(Encoding.ASCII.GetBytes(password))));
        hasher.Dispose();

        var user = new User()
        {
            Email = email,
            Name = name,
            PasswordHash = passwordHash,
        };

        this._context.Users.Add(user);
        await _context.SaveChangesAsync();

        SetActiveUser(user);

        return user;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = await this._context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new Exception($"User <{email}> does not exist.");
        }

        var hasher = MD5.Create();
        var passwordHash = Convert.ToBase64String((hasher.ComputeHash(Encoding.ASCII.GetBytes(password))));
        hasher.Dispose();

        if (!user.PasswordHash.Equals(passwordHash, StringComparison.Ordinal))
        {
            throw new Exception("Invalid login credentials");
        }

        SetActiveUser(user);

        return user;
    }

    public async Task<Wallet[]> GetUserWallets()
    {
        // Load dependent wallet entities if not loaded already.
        await this._context.Entry(ActiveUser).Collection(u => u.Wallets).LoadAsync();
        return ActiveUser.Wallets.ToArray();
    }

    public async Task SetActiveWallet(Wallet wallet)
    {
        var wallets = await this.GetUserWallets();
        s_activeWallet = wallets.FirstOrDefault(w => w.Id == wallet.Id);
    }

    public /* async Task */ void SetActiveUser(User user)
    {
        s_activeUser = user;
    }

    public async Task CreateWallet(string name, Currency currency, Money amount)
    {
        var wallet = new Wallet(name, currency, amount);
        wallet.User = ActiveUser;

        this._context.Wallets.Add(wallet);
        await this._context.SaveChangesAsync();

        await this.SetActiveWallet(wallet);
    }

    public async Task DeleteWallet()
    {
        if (ActiveWallet is null)
        {
            return;
        }

        this._context.Remove(ActiveWallet);
        await this._context.SaveChangesAsync();

        s_activeWallet = null;
    }

    public async Task AddOperation(Operation operation)
    {
        if (ActiveWallet is null)
        {
            return;
        }

        ActiveWallet.Operations.Add(operation);
        // Tell EF that a new item to operation collection is added so that the SaveChanges call
        // perform the necessary update.
        this._context.Entry(ActiveWallet).Collection(w => w.Operations).IsModified = true;
        await this._context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}