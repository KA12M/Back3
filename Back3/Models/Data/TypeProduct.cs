// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Back3.Models.Data
{
    public partial class TypeProduct
    {
        public TypeProduct()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}