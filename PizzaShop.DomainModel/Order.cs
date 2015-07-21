using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PizzaShop.DomainModel.Shared;
using IAggregate = PizzaShop.DomainModel.Shared.IAggregate;

namespace PizzaShop.DomainModel
{
    [DataContract]
    public class Order : IAggregate
    {
        public Order()
        {
        }

        public Order(Guid orderId, List<OrderDetail> orderDetails)
        {
            Id = orderId;
            OrderDate = DateTime.UtcNow;
            OrderStatus = OrderStatusType.InProgress;
            SetOrderDetails(orderDetails);
        }

        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public List<OrderDetail> OrderDetails { get; private set; }

        [DataMember]
        public DateTime OrderDate { get; private set; }

        [DataMember]
        public string OrderStatus { get; private set; }

        private void SetOrderDetails(List<OrderDetail> orderDetails)
        {
            AssertionConcern.AssertArgumentRange(orderDetails.Count, 1, 500, "Orders should have a minimum of one item.");
            OrderDetails = orderDetails;
        }

        public void CancelOrder(Guid id)
        {
            if (OrderStatus == OrderStatusType.InProgress || OrderStatus == OrderStatusType.Canceled)
                OrderStatus = OrderStatusType.Canceled;
            else
                throw new InvalidOperationException("This order is completed and can not be canceled.");
        }

        public void CompleteOrder(Guid id)
        {
            if (OrderStatus == OrderStatusType.InProgress || OrderStatus == OrderStatusType.Completed)
                OrderStatus = OrderStatusType.Completed;
            else
                throw new InvalidOperationException("This order has been canceled and can not be completed.");
        }
    }
}