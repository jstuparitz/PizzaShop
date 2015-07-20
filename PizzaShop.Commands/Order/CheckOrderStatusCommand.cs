using System;
using System.Runtime.Serialization;

namespace PizzaShop.Commands.Order
{
    [DataContract]
    public class CheckOrderStatusCommand : ICommand
    {
        [DataMember]
        public Guid OrderId { get; set; }
    }
}