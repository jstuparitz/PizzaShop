using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using PizzaShop.Commands.Order;

namespace PizzaShop.ActorService.Interfaces
{
    /// <summary>
    ///     Actors interact with rest of the system, including other actors, by passing asynchronous messages using
    ///     a request-response pattern. These interactions are defined in an interface as asynchronous methods.
    ///     Arguments and the result type of the Task it returns must be serializable.
    /// </summary>
    public interface IOrderActorService : IActor
    {
        Task<Guid> CreateOrder(CreateOrderCommand command);
        Task CancelOrder(CancelOrderCommand command);
        Task<string> CheckOrderStatus(CheckOrderStatusCommand command);
        Task CompleteOrder(CompleteOrderCommand command);
        Task<string> SayHello();
    }
}