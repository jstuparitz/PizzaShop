using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using PizzaShop.ActorService.Interfaces;
using PizzaShop.Commands.Order;
using PizzaShop.DomainModel;
using OrderDetail = PizzaShop.DomainModel.OrderDetail;
using Product = PizzaShop.DomainModel.Product;

namespace PizzaShop.ActorService
{
    /// <summary>
    ///     Order actor type. There will be many instances of this actor type distributed on various nodes
    ///     across a cluster. Each actor has a unique actor id.
    ///     This actor is essentially a command handler.
    /// </summary>
    //[ActorService(Name = "PizzaShop.ActorService")]
    //public class OrderActor : Actor<Order>, IOrderActor
    //{
    //    public Task<Guid> CreateOrder(CreateOrderCommand command)
    //    {
    //        var orderDetails = new List<OrderDetail>();
    //        foreach (var orderItem in command.OrderDetails)
    //        {
    //            orderDetails.Add(new OrderDetail(new Product(orderItem.Product.Id,
    //                orderItem.Product.Name, orderItem.Product.Price), orderItem.Quantity));
    //        }
    //        State = new Order(command.OrderId, orderDetails);
    //        SaveStateAsync();
    //        return Task.FromResult(State.Id);
    //    }

    //    public Task CancelOrder(CancelOrderCommand command)
    //    {
    //        State.CancelOrder(command.OrderId);
    //        return Task.FromResult(true);
    //    }

    //    public Task<string> CheckOrderStatus(CheckOrderStatusCommand command)
    //    {
    //        return Task.FromResult(State.OrderStatus.DisplayName);
    //    }

    //    public Task CompleteOrder(CompleteOrderCommand command)
    //    {
    //        State.CompleteOrder(command.OrderId);
    //        return Task.FromResult(true);
    //    }
    //}
}
