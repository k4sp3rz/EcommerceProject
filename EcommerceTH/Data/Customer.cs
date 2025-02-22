using System;
using System.Collections.Generic;

namespace EcommerceTH.data;

public partial class Customer
{
    public int Idcus { get; set; }

    public string NameCus { get; set; } = null!;

    public string PhoneCus { get; set; } = null!;

    public string EmailCus { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public int Point { get; set; }

    public string Viplevel { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<OrderPro> OrderPros { get; set; } = new List<OrderPro>();
}
