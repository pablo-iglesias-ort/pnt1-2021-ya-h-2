using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaCine.Data;
using ReservaCine.Models;

namespace ReservaCine.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly ReservaCineContext _context;
        static List<Pelicula> peliculas = new List<Pelicula>()
        {
            new Pelicula()
            {
                Id = Guid.NewGuid(),
                Titulo = "La Viuda Negra",
                Descripcion = "Bienvenido al infierno, también conocido como Belle Reve, la prisión con la tasa de mortalidad más alta en los EE. UU. Donde se guardan los peores supervillanos y donde harán cualquier cosa para escapar, incluso unirse al grupo de trabajo súper secreto y súper sombrío Task Force X. ¿Cuál es la tarea de vida o muerte del día? Reunir a una colección de estafadores, incluidos Bloodsport, Peacemaker, Captain Boomerang, Ratcatcher 2, Savant, King Shark, Blackguard, Javelin y la psicópata favorita de todos, Harley Quinn. Luego, llenarlos de armas y déjalos caer (literalmente) en la remota isla de Corto Maltese, infundida por enemigos. Caminando a través de una jungla repleta de adversarios militantes y fuerzas guerrilleras a cada paso, el Escuadrón está en una misión de búsqueda y destrucción con solo el Coronel Rick Flag en el suelo para hacerlos comportarse ... cada movimiento. Y como siempre, un movimiento en falso y están muertos (ya sea a manos de sus oponentes, un compañero de equipo o la propia Waller). Si alguien está haciendo apuestas, las chances están en su contra.",
                Duracion = 132,
                FechaLanzamiento = new DateTime(2021,03,28),
                Genero = new Genero("Acción"),
                
            },

            new Pelicula()
            {
                Id = Guid.NewGuid(),
                Titulo = "El Escuadrón Suicida",
                Descripcion = "Una peligrosa conspiración, relacionada con su pasado, persigue a Natasha Romanoff, también conocida como Viuda Negra. La agente tendrá que lidiar con las consecuencias de haber sido espía, así como con las relaciones rotas, para sobrevivir.",
                Duracion = 140,
                FechaLanzamiento = new DateTime(2021,07,15),
                Genero = new Genero("Acción"),

            }
        };

        public static List<Pelicula> Peliculas { get => peliculas; set => peliculas = value; }

        public PeliculaController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Pelicula
        public async Task<IActionResult> Index()
        {
            return View(Peliculas);
        }

        // GET: Pelicula/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = BuscarPelicula(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Pelicula/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pelicula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaLanzamiento,Titulo,Descripcion,Duracion")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                pelicula.Id = Guid.NewGuid();
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Pelicula/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = BuscarPelicula(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        // POST: Pelicula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FechaLanzamiento,Titulo,Descripcion,Duracion")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var peliculaEncontrada = BuscarPelicula(id);
                    peliculaEncontrada.FechaLanzamiento = pelicula.FechaLanzamiento;
                    peliculaEncontrada.Titulo = pelicula.Titulo;
                    peliculaEncontrada.Descripcion = pelicula.Descripcion;
                    peliculaEncontrada.Duracion = pelicula.Duracion;

                }
                catch (Exception)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Pelicula/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pelicula = await _context.Pelicula.FindAsync(id);
            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(Guid id)
        {
            return _context.Pelicula.Any(e => e.Id == id);
        }

        private Pelicula BuscarPelicula(Guid? id)
        {
            if (id == null)
                return null;
            var pelicula = Peliculas.FirstOrDefault(p => p.Id == id);
            return pelicula;
    }
    }
}


       
