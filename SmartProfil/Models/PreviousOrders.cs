using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartProfil.Models
{
    public class PreviousOrders
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string Color { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
