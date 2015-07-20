using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.DomainModel
{
    [DataContract]
    public class OrderDetail
    {
        public OrderDetail(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        [DataMember]
        public Product Product { get; private set; }

        [DataMember]
        public int Quantity { get; private set; }
    }
}
