namespace fall_project_2;

public sealed class Storage
{
    private static readonly Wallet _activeWallet;
    public Wallet ActiveWallet => _activeWallet ?? throw new InvalidOperationException("Storage is not initialized");

    public async Task Initialize()
    {
        // TODO: check any existing file
        // TODO: load or create new storage file
    }

    public async Task Save()
    {

    }

    public async Task Load()
    {

    }
}