//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GymManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderedProduct
    {
        public OrderedProduct()
        {
            this.ProductSolds = new HashSet<ProductSold>();
        }
    
        public int B_ID { get; set; }
        public string BillNo { get; set; }
        public int CustomerID { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Cash { get; set; }
        public decimal Change { get; set; }
        public System.DateTime BillDate { get; set; }
    
        public virtual ICollection<ProductSold> ProductSolds { get; set; }
    }
}
