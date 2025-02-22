using System;
using System.Collections.Generic;

namespace EcommerceTH.data;

public partial class OrderPro
{
    public int IdorderPro { get; set; }

    public DateOnly OrderDate { get; set; }

    public string OrderAddress { get; set; } = null!;

    public int Idcus { get; set; }

    public virtual Customer IdcusNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
