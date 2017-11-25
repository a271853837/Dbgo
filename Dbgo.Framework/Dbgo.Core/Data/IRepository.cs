using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Data
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object Id);

        void Insert(T entity);

        void Update(T entity);

        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}
