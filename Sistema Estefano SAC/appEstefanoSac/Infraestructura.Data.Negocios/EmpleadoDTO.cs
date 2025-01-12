using Dominio.Entidad.Negocios.Abstraccion;
using Dominio.Entidad.Negocios.Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infraestructura.Data.Negocios
{
    public class EmpleadoDTO : IEmpleadoRepositorio
    {
        private readonly static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public string Create(Empleado emple)
        {
            string mensaje = "";
            var sp = "usp_create_empleado";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@nomEmple", emple.Nombre);
                    cmd.Parameters.AddWithValue("@apeEmple", emple.Apellidos);
                    cmd.Parameters.AddWithValue("@dirEmple", emple.Direccion);
                    cmd.Parameters.AddWithValue("@fecIngreso", emple.FechaIngreso);
                    cmd.Parameters.AddWithValue("@remuneracion", emple.Remuneracion);
                    cmd.Parameters.AddWithValue("@horasTra", emple.HorasTrabajadas);
                    cmd.Parameters.AddWithValue("@correo", emple.Correo);
                    cmd.Parameters.AddWithValue("@idPais", emple.idPais);

                    var resultado = cmd.ExecuteNonQuery();
                    mensaje = $"Se registro  {resultado} Empleado.";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return mensaje;
        }

        public string Delete(int id)
        {
            string mensaje = "";
            var sp = "usp_delete_empleados";

            using (SqlConnection cone = new SqlConnection(cadena))
            {
                cone.Open();
                SqlCommand cmd = new SqlCommand(sp, cone)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idEmple", id);


                var resultado = cmd.ExecuteNonQuery();
                mensaje = $"Se ha Elimino {resultado} Empleado(as).";
            }

            try
            {

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;

            }
            return mensaje;
        }

        public Empleado Find(int id)
        {
            var listado = GetAll();

            return listado.FirstOrDefault(x => x.idEmpleado == id);
        }

        public IEnumerable<Empleado> GetAll()
        {
            var listado = new List<Empleado>();

            var sp = "usp_list_empleados";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Empleado emple = new Empleado()
                        {
                            idEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                            Nombre = Convert.ToString(dr["Nombre"]),
                            Apellidos = Convert.ToString(dr["Apellidos"]),
                            Direccion = Convert.ToString(dr["Direccion"]),
                            FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),
                            Remuneracion = Convert.ToDecimal(dr["Remuneracion"]),
                            HorasTrabajadas = Convert.ToInt32(dr["HorasTrabajadas"]),
                            Correo = Convert.ToString(dr["Correo"]),
                            idPais = Convert.ToInt32(dr["idPais"]),
                            Pais = Convert.ToString(dr["Pais"])
                        };
                        listado.Add(emple);
                    }
                    dr.Close();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return listado;
        }

        public IEnumerable<Empleado> GetAllNomPais(string nom, int pai)
        {
            //Inicializamos variables
            List<Empleado> empleado = new List<Empleado>();

            //Iniciamos proceso
            using (SqlConnection cone = new SqlConnection(cadena))
            {

                cone.Open();
                SqlCommand cmd = new SqlCommand("usp_list_nombrepais_empleados @nomEmple , @idPais ", cone);
                cmd.Parameters.AddWithValue("@nomEmple", nom);
                cmd.Parameters.AddWithValue("@idPais", pai);

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int idEmpleado  = rd.GetInt32(0);
                    string nombre = rd.GetString(1);
                    string apellidos = rd.GetString(2);
                    string direccion = rd.GetString(3);
                    DateTime? fechaIngreso = rd.GetDateTime(4);
                    decimal? remuneracion = rd.GetDecimal(5);
                    int? horasTrabajadas = rd.GetInt32(6);
                    string correo = rd.GetString(7);
                    int idPais = rd.GetInt32(8);
                    string pais = rd.GetString(9);

                    Empleado emple = new Empleado( idEmpleado,  nombre, apellidos, direccion, fechaIngreso,  remuneracion,  horasTrabajadas,correo,idPais,pais);
                    empleado.Add(emple);


                }
                rd.Close();
            }
            return empleado;

        }

        public string Update(Empleado empleado)
        {
            string mensaje = "";
            var sp = "usp_update_empleado";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();
                    SqlCommand cmd = new SqlCommand(sp, cone)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@idEmple", empleado.idEmpleado);
                    cmd.Parameters.AddWithValue("@nomEmple", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@apeEmple", empleado.Apellidos);
                    cmd.Parameters.AddWithValue("@dirEmple", empleado.Direccion);
                    cmd.Parameters.AddWithValue("@fecIngreso", empleado.FechaIngreso);
                    cmd.Parameters.AddWithValue("@remuneracion", empleado.Remuneracion);
                    cmd.Parameters.AddWithValue("@horasTra", empleado.HorasTrabajadas);
                    cmd.Parameters.AddWithValue("@correo", empleado.Correo);
                    cmd.Parameters.AddWithValue("@idPais", empleado.idPais);

                    var resultado = cmd.ExecuteNonQuery();
                    mensaje = $"Se Actualizo {resultado} Empleado.";
                }
            }
            catch (Exception ex)
            {

                mensaje = ex.Message;
            }

            return mensaje;
        }
    }
}
