using fall_project_2.Enums;

namespace fall_project_2.Interfaces;

public interface IWalletService
{
    Wallet CreateWallet(User user, string walletName, Currency currency);

    ICollection<Wallet> GetUserWallets(User user);

    Wallet GetWalletById(User user, string id);
}