using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using PizzaShop.ActorService.Interfaces;
using PizzaShop.Commands.Order;
using PizzaShop.DomainModel;
using OrderDetail = PizzaShop.DomainModel.OrderDetail;
using Product = PizzaShop.DomainModel.Product;

namespace PizzaShop.ActorService
{
    [ActorService(Name = "PizzaShop.OrderService")]
    public class OrderActorService : Actor<CustomerActorServiceState>, ICustomerActorService
    {
        public Task<Guid> CreateOrder(CreateOrderCommand command)
        {
            var orderDetails = new List<OrderDetail>();
            foreach (var orderItem in command.OrderDetails)
            {
                orderDetails.Add(new OrderDetail(new Product(orderItem.Product.Id,
                    orderItem.Product.Name, orderItem.Product.Price), orderItem.Quantity));
            }
            //State = new Order(command.OrderId, orderDetails);
            SaveStateAsync();
            return Task.FromResult(command.OrderId);
        }

        public Task CancelOrder(CancelOrderCommand command)
        {
           // State.CancelOrder(command.OrderId);
            return Task.FromResult(true);
        }

        public Task<string> CheckOrderStatus(CheckOrderStatusCommand command)
        {
            throw new NotImplementedException();
        }

        public Task CompleteOrder(CompleteOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}