using Dominio.Entidad.Negocios.Entidad;
using Infraestructura.Data.Negocios;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;



using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace appEstefanoSac.Controllers
{
    public class EmpleadoController : Controller
    {
        EmpleadoDTO _emple = new EmpleadoDTO();
        PaisDTO _pais = new PaisDTO();

        public readonly static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        #region Listado por nombre y pais
        public ActionResult ConsultaEmpleados(int numberPage = 0, string nom = "", int pai = 0)
        {
            var listado = _pais.GetAll();
            var empleados = _emple.GetAllNomPais(nom, pai);

            // Normalizar el filtro 'nom' a minúsculas para la comparación insensible a mayúsculas y minúsculas
            nom = nom.ToLower();

            var empleadosFiltrados = empleados.Where(e => e.Nombre.ToLower().Contains(nom)).ToList();
            var cantidad = empleadosFiltrados.Count();
            int rowsPerPage = 2;

            // Calcular número de páginas
            int numberPages = (cantidad + rowsPerPage - 1) / rowsPerPage; // Redondear hacia arriba

            // Aplicar la paginación
            var empleadosPaginados = empleadosFiltrados.Skip(rowsPerPage * numberPage).Take(rowsPerPage).ToList();

            // Configurar ViewBag para la vista
            ViewBag.numberPage = numberPage;
            ViewBag.numberPages = numberPages;
            ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais", pai);
            ViewBag.nom = nom; // Pasar el valor de nom normalizado para mantener el filtro en la vista
            ViewBag.mensaje = $"Cantidad de Empleados: {cantidad}";

            return View(empleadosPaginados);
        }
        public ActionResult Index()
        {
            return View();
        }
        #endregion


        #region Crear un Empleado
        public ActionResult Agregar()
        {
            var listado = _pais.GetAll();
            ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais");
            return View(new Empleado());
        }

        [HttpPost]
        public ActionResult Agregar(Empleado nuevoEmple)
        {
            var listado = _pais.GetAll();

            if (!ModelState.IsValid)
            {
                ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais");
                return View(nuevoEmple);
            }

            ViewBag.mensaje = _emple.Create(nuevoEmple);
            ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais",nuevoEmple.idPais);
            return View(nuevoEmple);
        }

        #endregion

        #region Actualizar Empleado
        public ActionResult Editar(int id)
        {
            Empleado emple = _emple.Find(id);
            var listado = _pais.GetAll();
            ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais");
            return View(emple);
        }

        [HttpPost]
        public ActionResult Editar(Empleado empleado)
        {

            var listado = _pais.GetAll();

            if (!ModelState.IsValid)
            {

                ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais");
                return View(empleado);
            }

            ViewBag.mensaje = _emple.Update(empleado);
            ViewBag.selectPaises = new SelectList(listado, "idPais", "Pais",empleado.idPais);
            return View(empleado);
        }
        #endregion

        #region Detalle del Empleado
        public ActionResult Details(int? id)
        {

            if (id == null) return RedirectToAction("ConsultaEmpleados");


            Empleado empleado = _emple.Find(id.Value);
            return View(empleado);

        }
        #endregion

        #region Eliminar Empleado
        public ActionResult Delete(int? id, bool confirmar)
        {
            if (id == null || !confirmar)
                return RedirectToAction("ConsultaEmpleados");

            ViewBag.mensajeEliminar = _emple.Delete(id.Value);
            TempData["MensajeEliminacion"] = $"El Empleado con el Código {id} , ha sido eliminado";
            return RedirectToAction("ConsultaEmpleados");
        }
        #endregion


        //#region Reporte de pdf ,word , exel 
        //public DataTable obtenerReporteEmpleado()
        //{
            

        //    SqlDataAdapter da = new SqlDataAdapter("usp_list_empleados", cadena);

        //    da.SelectCommand.CommandType = CommandType.StoredProcedure;



        //    DataTable dt = new DataTable();

        //    da.Fill(dt);



        //    return dt;

        //}

        //public ActionResult ReporteEmpleado()

        //{

        //    ReportViewer rpv = new ReportViewer();

        //    rpv.ProcessingMode = ProcessingMode.Local;

        //    rpv.SizeToReportContent = true;



        //    rpv.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptEmpleados.rdlc";

        //    rpv.LocalReport.DataSources.Add(new ReportDataSource("dsReportes", obtenerReporteEmpleado()));



        //    ViewBag.ReportViewer = rpv;



        //    return View();

        //}

        ////public FileResult DownloadPDFReport()

        ////{

        ////    string FileNameReportPDF = "clientes" + "-" + DateTime.Now + ".pdf";

        ////    LocalReport localReport = new LocalReport();

        ////    localReport.DataSources.Clear();

        ////    localReport.DataSources.Add(new ReportDataSource("dsReportes", obtenerReporteCliente()));

        ////    localReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";



        ////    byte[] bytes = localReport.Render("PDF");

        ////    return File(bytes, "application/pdf", FileNameReportPDF);



        ////}



        ////public FileResult DownloadXLSReport()

        ////{

        ////    string FileNameReportXLS = "clientes" + "-" + DateTime.Now + ".xls";

        ////    LocalReport localReport = new LocalReport();

        ////    localReport.DataSources.Clear();

        ////    localReport.DataSources.Add(new ReportDataSource("dsReportes", obtenerReporteCliente()));

        ////    localReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";



        ////    byte[] bytes = localReport.Render("Excel");

        ////    return File(bytes, "application/vnd.ms-excel", FileNameReportXLS);



        ////}



        ////public FileResult DownloadWORDReport()

        ////{

        ////    string FileNameReportWORD = "clientes" + "-" + DateTime.Now + ".doc";

        ////    LocalReport localReport = new LocalReport();

        ////    localReport.DataSources.Clear();

        ////    localReport.DataSources.Add(new ReportDataSource("dsReportes", obtenerReporteCliente()));

        ////    localReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";



        ////    byte[] bytes = localReport.Render("WORD");

        ////    return File(bytes, "application/msword", FileNameReportWORD);



        ////}

        //#endregion
    }
}