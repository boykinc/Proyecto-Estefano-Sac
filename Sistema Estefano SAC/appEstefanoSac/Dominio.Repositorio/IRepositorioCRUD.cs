namespace Dominio.Repositorio
{
    public interface IRepositorioCRUD<T>    where T : class
    {
        string Create(T entity);

        string Update(T entity);

        string Delete(int id);

         T Find(int id);

    }
}
