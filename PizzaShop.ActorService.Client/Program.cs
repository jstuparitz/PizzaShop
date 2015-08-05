using PizzaShop.ActorService.Interfaces;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PizzaShop.Commands.Order;

namespace PizzaShop.ActorService.Client
{
    public class Program
    {
        private const string ApplicationName = "fabric:/PizzaShop";

        public static void Main(string[] args)
        {

            //var proxy = ActorProxy.Create<IPizzaShopActorService>(ActorId.NewId(), ApplicationName);

            //int count = 10;
            //Console.WriteLine("Setting Count to in Actor {0}: {1}", proxy.GetActorId(), count);
            //proxy.SetCountAsync(count).Wait();

            //Console.WriteLine("Count from Actor {1}: {0}", proxy.GetActorId(), proxy.GetCountAsync().Result);

            //gives you the ability to connect to the actor

            //var orderId = new Guid("e7dda76d-ee1d-4aae-afcd-c0d5b70c04d4");
            //var proxyOrder = ActorProxy.Create<IOrderActorService>(new ActorId(orderId), ApplicationName, "PizzaShop.OrderService");
            //var checkOrderStatusCommand = new CheckOrderStatusCommand();
            //checkOrderStatusCommand.OrderId = orderId;
            //var status = proxyOrder.CheckOrderStatus(checkOrderStatusCommand).Result;



            for (int i = 1; i <= 100000; i++)
            {
                int maxInterval = 2000;
                for (int x = 0; x < maxInterval; x++)
                {
                    CreateOrder();
                }
                Console.WriteLine((i* maxInterval).ToString() + " pizza orders have been processed at " + DateTime.Now.ToLongTimeString());
            }
        }

        private static Guid CreateOrder()
        {
            var orderId = Guid.NewGuid();
            var proxyOrder = ActorProxy.Create<IOrderActorService>(new ActorId(orderId), ApplicationName, "PizzaShop.OrderService");

            proxyOrder.CreateOrder(CreateOrderCommand(orderId));

            //Thread.Sleep(1000);

            //var checkOrderStatusCommand = new CheckOrderStatusCommand();
            //checkOrderStatusCommand.OrderId = orderId;
            //var status = proxyOrder.CheckOrderStatus(checkOrderStatusCommand).Result;
            return orderId;
        }

        private static CreateOrderCommand CreateOrderCommand(Guid orderId)
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "some product name", Price = (decimal)5.54 };
            var details = new List<OrderDetail>();
            details.Add(new OrderDetail(product, 3));
            details.Add(new OrderDetail(product, 5));
            details.Add(new OrderDetail(product, 8));
            var command = new CreateOrderCommand(orderId, details);
            return command;
        }
    }
}
