using Dominio.Entidad.Negocios.Entidad;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace appEstefanoSac.Controllers
{
    public class AccesoController : Controller
    {
        public readonly static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        // GET: Acceso
        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            var sp = "usp_ValidarUsuario";

            usuario.Clave = ConvertirSha256(usuario.Clave);

            using (SqlConnection cone = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand(sp, cone);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);


                cmd.CommandType = CommandType.StoredProcedure;

                cone.Open();

                cmd.ExecuteScalar();

                usuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }


            if (usuario.IdUsuario != 0)
            {
                Session["usua"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no Encontrado ";
                return View();
            }


        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Clave) || string.IsNullOrWhiteSpace(usuario.ComfirmarClave))
            {
                ViewData["Mensaje"] = "Por favor, complete todos los campos.";
                return View();
            }

            if (usuario.Clave != usuario.ComfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden.";
                return View();
            }

            usuario.Clave = ConvertirSha256(usuario.Clave);

            bool registrado;
            string mensaje;
            var sp = "usp_RegistrarUsuario";

            using (SqlConnection cone = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand(sp, cone);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cone.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["@Registrado"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@Mensaje"].Value);
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }




        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();



            try
            {


                using (SHA256 hash = SHA256Managed.Create())
                {
                    Encoding enc = Encoding.UTF8;
                    byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                    foreach (byte b in result)
                        Sb.Append(b.ToString("x2"));
                }

            }
            catch (Exception)
            {

                throw;
            }

            return Sb.ToString();
        }
    }
}