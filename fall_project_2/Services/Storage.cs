using fall_project_2.Data;

namespace fall_project_2.Services;

public class Storage
{
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

    private readonly ApplicationDataContext _context;

    public Storage(ApplicationDataContext context)
    {
        _context = context;
    }

    public Task<User> CreateUser(string email, string password) { return null; }

    public Task<User> Login(string email, string password) { return null; }
}