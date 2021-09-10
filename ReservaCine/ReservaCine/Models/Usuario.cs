using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReservaCine.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ingrese Nombre")]
        public String Nombre { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ingrese Apellido")]
        public String Apellido { get; set; }

        [Required]
        [Range(1000000, 999999999,ErrorMessage ="Ingrese DNI válido")]
        public long DNI { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Formato de Email inválido")]
        [StringLength(50, MinimumLength = 3,ErrorMessage = "El email es requerido")]
        public String Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Domicilio inválido")]
        public String Domicilio { get; set; }

        [Required(ErrorMessage = "Teléfono incorrecto")]
        [Range(10000000, 999999999)]

        public long Telefono { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        // [DisplayName("Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 4,ErrorMessage = "El usuario es inválido")]
        [DisplayName("Nombre de Usuario")]
        public String NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres")]
        [StringLength(15, MinimumLength = 8)]
        [DisplayName("Contraseña")]
        [ScaffoldColumn(false)] //para ocultar en columna
        public String Password { get; set; }

        public Usuario()
        {

        }
        




    }
}
         