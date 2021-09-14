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
    public class GeneroController : Controller
    {
        private readonly ReservaCineContext _context;
        static List<Genero> generos = new List<Genero>()
        {
            new Genero ()
            {
                Id = Guid.NewGuid(),
                Nombre = "Acción",
                Peliculas = new List<Pelicula>()
                {
                }
            },
            new Genero
            {
                Id = Guid.NewGuid(),
                Nombre = "Comedia",
                Peliculas = new List<Pelicula>()
                {
                }
            }
        };
        public GeneroController(ReservaCineContext context)
        {
            _context = context;
        }
        public void CargarGeneros()
        {
            foreach (Genero c in generos)
            {
                _context.Genero.Add(c);
            }
        }
        // GET: Genero
        public async Task<IActionResult> Index()
        {
            return View(generos);
        }

        // GET: Genero/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Pelicula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Genero/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Genero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Peliculas")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                genero.Id = Guid.NewGuid();
                _context.Add(genero);
                foreach (Pelicula p in genero.Peliculas)
                {
                    p.Genero = genero;
                    _context.Update(p);
                }
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Genero/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CargarGeneros();
            var genero = _context.Genero.Local.ToList().Find(c => c.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Genero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Peliculas")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genero);
                    foreach (Pelicula p in genero.Peliculas)
                    {
                        p.Genero = genero;
                        _context.Update(p);
                    }
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.Id))
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
            return View(genero);
        }



        private bool GeneroExists(Guid id)
        {
            return _context.Genero.Any(e => e.Id == id);
        }
    }
}