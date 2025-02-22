using System;
using System.Collections.Generic;

namespace EcommerceTH.data;

public partial class Category
{
    public int Idcate { get; set; }

    public string NameCate { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
