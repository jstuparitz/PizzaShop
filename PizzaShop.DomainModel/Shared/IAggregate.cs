using System;
using System.Runtime.Serialization;

namespace PizzaShop.DomainModel.Shared
{
    public interface IAggregate
    {
        [DataMember]
        Guid Id { get; }
    }
}