using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DataLayer.Entities.Wallet;

public class WalletType
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int WalletTypeId { get; set; }
    public string? WalletTypeTitle { get; set; }

    #region Relations

    public List<Wallet>? Wallets { get; set; }

    #endregion
}