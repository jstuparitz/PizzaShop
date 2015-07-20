using Microsoft.ServiceFabric.Actors;
using PizzaShop.ActorService.Interfaces;

namespace PizzaShop.ActorService
{
    public class CustomerActorService : Actor<CustomerActorServiceState>, ICustomerActorService
    {
         
    }
}