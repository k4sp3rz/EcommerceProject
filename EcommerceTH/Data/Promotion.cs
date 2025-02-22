using System;
using System.Collections.Generic;

namespace EcommerceTH.data;

public partial class Promotion
{
    public int Idpromote { get; set; }

    public string PromoteCode { get; set; } = null!;

    public double SalePercent { get; set; }

    public int Quantity { get; set; }

    public int IdorderPro { get; set; }

    public virtual OrderPro IdorderProNavigation { get; set; } = null!;
}
