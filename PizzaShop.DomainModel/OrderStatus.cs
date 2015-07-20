using System.Runtime.Serialization;

namespace PizzaShop.DomainModel
{
    [DataContract]
    public class OrderStatusType
    {
        public const string InProgress = "InProgress";
        public const string Canceled = "Canceled";
        public const string Completed = "Completed";
    }
}