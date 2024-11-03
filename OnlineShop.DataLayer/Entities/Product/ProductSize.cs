using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DataLayer.Entities.Product
{
    public class ProductSize
    {
        public int PzId { get; set; }
        public int? ProductId { get; set; }
        public int? SizeId { get; set; }

        [DisplayName("تعداد")]
        public int? Quantity { get; set; }

        #region Relations

        public Product? Product { get; set; }
        public Size? Size { get; set; }

        #endregion
    }
}
