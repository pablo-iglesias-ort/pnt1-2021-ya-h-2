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

        [Required (ErrorMessage = "Ingrese un nombre válido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener un mínimo de 3 letras.")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un apellido válido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido debe tener un mínimo de 3 letras")]
        public String Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese un DNI válido")]
        [Range(1000000, 999999999,ErrorMessage ="Ingrese DNI válido")]
        public long DNI { get; set; }

        [Required(ErrorMessage = "Ingrese un Email válido")]
        [EmailAddress(ErrorMessage = "Formato de Email inválido")]
        [StringLength(50, MinimumLength = 3,ErrorMessage = "El email es inválido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Ingrese un domicilio válido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Domicilio inválido.")]
        public String Domicilio { get; set; }

        [Required(ErrorMessage = "Ingrese un número de Teléfono")]
        [Range(10000000, 999999999 , ErrorMessage = "Teléfono incorrecto")]
        [DisplayName("Teléfono")]
        public long Telefono { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre de usuario válido")]
        [StringLength(10, MinimumLength = 4,ErrorMessage = "El usuario es inválido. Debe contener mínimo 4 caracteres.")]
        [DisplayName("Nombre de Usuario")]
        public String NombreUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres")]
        [DisplayName("Contraseña")]
        [ScaffoldColumn(false)] //para ocultar en columna
        public String Password { get; set; }

        public Usuario()
        {

        }
        




    }
}
         