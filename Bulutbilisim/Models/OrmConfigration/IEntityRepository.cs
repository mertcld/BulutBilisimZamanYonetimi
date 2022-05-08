using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models.OrmConfigration
{
    public interface IEntityRepository<T> : IDisposable where T : class, new()
    {
        T GetByID(object id);

        T QueryFirstOrDefault(string query, object parameters, CommandType commandType);

        List<T> GetAll();

        List<T> Query(string query, object parameters, CommandType commandType);

        int Execute(string query, object parameters, CommandType commandType);
        int Add(T entity);
        int Delete(T entity);
        int Update(T entity);

    }
}
