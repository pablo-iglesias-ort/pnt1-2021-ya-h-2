using ReservaCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReservaCine.Data
{
    public class InicializacionDeDatos
    {
		public static void Inicializar(ReservaCineContext context)
		{
			context.Database.EnsureCreated();

			if (context.Cliente.Any())
			{
				// Si ya hay datos aqui, significa que ya los hemos creado previamente
				return;
			}

			var newCliente = new Cliente();
			newCliente.Id = Guid.NewGuid();
			newCliente.Nombre = "San";
			newCliente.Apellido = "Albín";
			newCliente.DNI = 1111111111;
			newCliente.Email = "san.a@hotmail.com";
			newCliente.Domicilio= "as 1245";
			newCliente.Telefono = 3234235245;
			newCliente.NombreUsuario = "assa";
			newCliente.Password = seguridad.Encriptar pass("1234");
			 
			
			newCliente.FechaAlta = DateTime.Now;
			context.Cliente.Add(newCliente);
			context.SaveChanges();

			var Cliente = context.Cliente.First();

			
			if (context.Pelicula.Any())
			{
				// Si ya hay datos aqui, significa que ya los hemos creado previamente
				return;
			}

			var nuevaPelicula = new Pelicula();
			nuevaPelicula.FechaLanzamiento = DateTime.Now.Date;
			nuevaPelicula.Titulo = "La viuda Negra";
			nuevaPelicula.Id = Guid.NewGuid();
			nuevaPelicula.Descripcion = "Una de las preferidas de las series de Marvel, viene a vengarse de su pasado";
			nuevaPelicula.Genero = new Genero();
			nuevaPelicula.Duracion = 180;
			context.Pelicula.Add(nuevaPelicula); 
			context.SaveChanges();

			var pelicula= context.Pelicula.First();
			

			if (context.Genero.Any())
            {
				return;
            }

			var nuevoGenero = new Genero();
			nuevoGenero.Nombre = "Acción";
			nuevoGenero.Id = Guid.NewGuid();
			nuevoGenero.PeliculaId = nuevaPelicula.Id;
			context.Genero.Add(nuevoGenero);
			context.SaveChanges();

			var genero = context.Genero.First();

			}

			
    }
}
