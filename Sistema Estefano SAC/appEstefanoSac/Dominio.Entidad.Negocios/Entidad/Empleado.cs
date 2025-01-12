using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidad.Negocios.Entidad
{
    public class Empleado
    {

        [Display(Name = "idEmpleado")]
        public int idEmpleado { get; set; }


        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string Nombre { get; set;}


        [Display(Name = "Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string Apellidos { get; set;}

        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string Direccion { get; set;}


        [Display(Name = "Fecha Ingreso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? FechaIngreso { get; set; }

        [Display(Name = "Fecha Ingreso")]
        public string FechaIngresoTexto { get => FechaIngreso.Value.ToString("dd/MM/yyyy"); }

        [Display(Name = "Remuneracion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "La  Remuneración no puede ser 0")]
        public decimal? Remuneracion { get; set;}


        [Display(Name = "Horas Trabajadas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Las Horas Trabajadas no puede ser 0")]
        public int? HorasTrabajadas { get; set;}


        [Display(Name ="Correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Correo requerido")]
        [RegularExpression("[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,5}", ErrorMessage = "Formato incorrecto")]
        public string Correo {  get; set;}

        [Display(Name = "id Pais")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public int idPais { get; set;}

        [Display(Name ="Pais")]
        public string Pais { get;set;}

        public Empleado(string nombre, string apellidos, string direccion, DateTime? fechaIngreso, decimal? remuneracion, int? horasTrabajadas, string correo, int idPais, string pais)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Direccion = direccion;
            FechaIngreso = fechaIngreso;
            Remuneracion = remuneracion;
            HorasTrabajadas = horasTrabajadas;
            Correo = correo;
            this.idPais = idPais;
            Pais = pais;
        }

        public Empleado(int idEmpleado, string nombre, string apellidos, string direccion, DateTime? fechaIngreso, decimal? remuneracion, int? horasTrabajadas, string correo, int idPais, string pais)
        {
            this.idEmpleado = idEmpleado;
            Nombre = nombre;
            Apellidos = apellidos;
            Direccion = direccion;
            FechaIngreso = fechaIngreso;
            Remuneracion = remuneracion;
            HorasTrabajadas = horasTrabajadas;
            Correo = correo;
            this.idPais = idPais;
            Pais = pais;
        }

        public Empleado()
        {
        }
    }
}
