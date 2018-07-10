using ControlePontoAM.Models.repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ControlePontoAM.Repositorio
{
    public class cadastrohoraRepositorio : IRepositorio<cadastrohora>
    {
        private Contexto db = new Contexto();
        public cadastrohora Add(cadastrohora entity)
        {
            throw new NotImplementedException();
        }

        public cadastrohora Delete(cadastrohora entity)
        {
            throw new NotImplementedException();
        }

        public cadastrohora Edit(cadastrohora entity)
        {
            throw new NotImplementedException();
        }

        public IList<cadastrohora> FindBy(Expression<Func<cadastrohora, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IList<cadastrohora> GetAll()
        {
            throw new NotImplementedException();
        }

        public cadastrohora GetById(Guid id)
        {
            var result = from c in db.cadastrohora
                                   where c.mes == "07"
                                   select c;
            if (result.Count() > 0)
            {
                return null;
            }
            else
                return null;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}