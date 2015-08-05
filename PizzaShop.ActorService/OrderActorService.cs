using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using PizzaShop.ActorService.Interfaces;
using PizzaShop.Commands.Order;
using PizzaShop.DocumentDBRepository;
using PizzaShop.DomainModel;
using PizzaShop.DomainModel.Shared;
using OrderDetail = PizzaShop.DomainModel.OrderDetail;
using Product = PizzaShop.DomainModel.Product;


namespace PizzaShop.ActorService
{
    [ActorService(Name = "PizzaShop.OrderService")]
    public class OrderActorService : Actor<Order>, IOrderActorService
    {
        public async Task<Guid> CreateOrder(CreateOrderCommand command)
        {
            var orderDetails = new List<OrderDetail>();
            foreach (var orderItem in command.OrderDetails)
            {
                orderDetails.Add(new OrderDetail(new Product(orderItem.Product.Id,
                    orderItem.Product.Name, orderItem.Product.Price), orderItem.Quantity));
            }
            State = new Order(command.OrderId, orderDetails);
            await SaveStateAsync();

            ActorEventSource.Current.ActorMessage(this, "Order has been created.");

            //try
            //{
            //    IRepository<Order> repository = new DocumentDbRepository<Order>();
            //    repository.CollectionId = "Orders";
            //    repository.Insert(State);
            //    ActorEventSource.Current.ActorMessage(this, "Order has been saved to DocumentDB.");
            //}
            //catch (Exception ex)
            //{
            //    ActorEventSource.Current.ActorMessage(this, ex.ToString());
            //}

            return command.OrderId;
        }

        public Task CancelOrder(CancelOrderCommand command)
        {
            State.CancelOrder(command.OrderId);
            return Task.FromResult(true);
        }

        public Task<string> CheckOrderStatus(CheckOrderStatusCommand command)
        {
            return Task.FromResult(State.OrderStatus);
        }

        public Task CompleteOrder(CompleteOrderCommand command)
        {
            State.CompleteOrder(command.OrderId);
            return Task.FromResult(true);
        }

        public Task<string> SayHello()
        {
            var message = "Hello: " + DateTime.Now.ToLongDateString();
            ActorEventSource.Current.ActorMessage(this, message);
            return Task.FromResult(message);
        }
    }
}