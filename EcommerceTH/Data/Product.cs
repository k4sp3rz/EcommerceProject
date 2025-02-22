using System;
using System.Collections.Generic;

namespace EcommerceTH.data;

public partial class Product
{
    public int Idpro { get; set; }

    public string NamePro { get; set; } = null!;

    public string DesPro { get; set; } = null!;

    public int Price { get; set; }

    public string ImagePro { get; set; } = null!;

    public int Idcate { get; set; }

    public virtual Category IdcateNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
