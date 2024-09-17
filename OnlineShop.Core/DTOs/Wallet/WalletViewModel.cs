using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace OnlineShop.Core.DTOs.Wallet
{
    public class ChargeWalletViewModel
    {
        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class WalletViewModel
    {
        public DateTime CreateDate { get; set; }
        public int Amount { get; set; }
        public int TypeId { set; get; }
        public string? Description { get; set; }
    }
}
