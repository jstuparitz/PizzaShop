using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PizzaShop.DomainModel.Shared
{
    public interface IRepository<TAggregate>
    {
        Task<bool> Insert(TAggregate entity);

        Task<bool> Insert(IEnumerable<TAggregate> entities);

        Task<bool> Update(TAggregate entity);

        Task<bool> Delete(TAggregate entity);

        Task<IList<TAggregate>> SearchFor(Expression<Func<TAggregate, bool>> predicate);

        Task<IList<TAggregate>> GetAll();

        Task<TAggregate> GetById(Guid id);
    }
}