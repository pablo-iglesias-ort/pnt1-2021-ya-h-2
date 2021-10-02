using ReservaCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReservaCine.Data
{
    public class InicializacionDeDatos
    {
		public static void Inicializar(ReservaCineContext context)
		{
			context.Database.EnsureCreated();

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
			nuevaPelicula.Duracion = 180;
			context.Pelicula.Add(nuevaPelicula); 
			context.SaveChanges();

			var pelicula= context.Pelicula.First();
			

			}
    }
}
