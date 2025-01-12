using Dominio.Entidad.Negocios.Abstraccion;
using Dominio.Entidad.Negocios.Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Infraestructura.Data.Negocios
{
    public class PaisDTO : IPaisRepositorio
    {
             
        private readonly static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        #region Login y registrar del sistema 

        #endregion





        #region Listado por Nombre y pais
        public IEnumerable<Paises> GetAll()
        {
            var listado = new List<Paises>();
            var sp = "usp_list_paises";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Paises nompa = new Paises()
                        {
                            idPais = Convert.ToInt32(dr["idPais"]),
                            Pais = Convert.ToString(dr["Pais"])
                        };
                        listado.Add(nompa);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listado;
        } 
        #endregion
    }
}
