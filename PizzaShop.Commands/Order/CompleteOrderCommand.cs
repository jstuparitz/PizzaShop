using System;
using System.Runtime.Serialization;

namespace PizzaShop.Commands.Order
{
    [DataContract]
    public class CompleteOrderCommand : ICommand
    {
        [DataMember]
        public Guid OrderId { get; set; }
    }
}