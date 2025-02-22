using System;
using System.Collections.Generic;

namespace EcommerceTH.data;

public partial class OrderDetail
{
    public int Idorder { get; set; }

    public int Quantity { get; set; }

    public int UnitPrice { get; set; }

    public int Idpro { get; set; }

    public int IdorderPro { get; set; }

    public virtual OrderPro IdorderProNavigation { get; set; } = null!;

    public virtual Product IdproNavigation { get; set; } = null!;
}
