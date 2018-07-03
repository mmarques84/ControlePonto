using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePontoAM.Models.repositorio
{
    public interface IRepositorio<T> where T : class
    {
        IList<T> GetAll();
        T GetById(Guid id);
        IList<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        T Edit(T entity);
        void Save();
    }
}
