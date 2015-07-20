using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using PizzaShop.ActorService.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;

namespace PizzaShop.ActorService
{
    [DataContract]
    public class PizzaShopActorServiceState
    {
        [DataMember]
        public int Count;

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "PizzaShopActorServiceState[Count = {0}]", Count);
        }
    }
}