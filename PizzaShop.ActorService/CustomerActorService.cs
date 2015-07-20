using Microsoft.ServiceFabric.Actors;
using PizzaShop.ActorService.Interfaces;
using PizzaShop.DomainModel;

namespace PizzaShop.ActorService
{
    [ActorService(Name = "PizzaShop.CustomerService")]
    public class CustomerActorService : Actor<CustomerActorServiceState>, ICustomerActorService
    {
         
    }
}