using Dominio.Entidad.Negocios.Entidad;
using Dominio.Repositorio;
using System.Collections.Generic;

namespace Dominio.Entidad.Negocios.Abstraccion
{
    public interface IEmpleadoRepositorio : IRepositorioGET<Empleado> , IRepositorioCRUD<Empleado>
    {
        IEnumerable<Empleado> GetAllNomPais(string nom ,int pai  );

    }
}
