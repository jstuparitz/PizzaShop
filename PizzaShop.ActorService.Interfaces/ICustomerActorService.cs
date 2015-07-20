using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using PizzaShop.Commands.Order;

namespace PizzaShop.ActorService.Interfaces
{
    public interface ICustomerActorService: IActor
    {
        Task<Guid> CreateOrder(CreateOrderCommand command);
        Task CancelOrder(CancelOrderCommand command);
        Task<string> CheckOrderStatus(CheckOrderStatusCommand command);
        Task CompleteOrder(CompleteOrderCommand command);
    }
}