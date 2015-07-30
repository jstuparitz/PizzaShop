using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaShop.DomainModel;
using PizzaShop.DomainModel.Shared;

namespace PizzaShop.DocumentDBRepository.IntegrationTests
{
    [TestFixture]
    public class DocumentDBRepositoryTests
    {
        [Test]
        public void Insert_Creates_A_New_Document()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var product = new Product(Guid.NewGuid(), "some product name for integration test", (decimal)5.54);
            var details = new List<OrderDetail>();
            details.Add(new OrderDetail(product, 3));
            details.Add(new OrderDetail(product, 5));
            details.Add(new OrderDetail(product, 8));
            var order = new Order(orderId, details);

            //Act
            IRepository<Order> repository = new DocumentDbRepository<Order>();
            repository.CollectionId = "Orders";
            repository.Insert(order);

            //Assert
            //Assert.IsTrue(isSuccess);
        }


    }
}
