
using fall_project_2.Enums;

namespace fall_project_2;

public class User
{
    public string Name { get; }

    public string Email { get; }

    public List<Wallet> Wallets { get; }

    public void AddWallet(Wallet wallet)
    {
        // TODO: check if user does not own wallet with the provided wallet currency 
        // TODO: add wallet to wallets list
    }

    public void AddWallet(Currency currency)
    {
        // TODO: check if user does not own wallet with the provided currency 
        // TODO: create wallet instance
        // TODO: add wallet to wallets list
    }
}