using System.Collections.Generic;

namespace Dominio.Repositorio
{
    public interface IRepositorioGET<T> where T : class
    {
        IEnumerable<T> GetAll();

    }
}
