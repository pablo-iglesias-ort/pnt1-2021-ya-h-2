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
    public class TipoSalaController : Controller
    {
        private readonly ReservaCineContext _context;
        static List<TipoSala> tipoSalas = new List<TipoSala>()
        {
            new TipoSala
            {
              Id = Guid.NewGuid(),
              Nombre = "Sala Dorada",
              Precio = 2000

            },
            new TipoSala
            {
                Id = Guid.NewGuid(),
              Nombre = "Sala de Plata",
              Precio = 1000
            }

        };

        public TipoSalaController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: TipoSala
        public async Task<IActionResult> Index()
        {
            return View(tipoSalas);
        }

        // GET: TipoSala/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSala = BuscarTipoSala(id);
            if (tipoSala == null)
            {
                return NotFound();
            }

            return View(tipoSala);
        }

        // GET: TipoSala/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoSala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] TipoSala tipoSala)
        {
            if (ModelState.IsValid)
            {
                tipoSala.Id = Guid.NewGuid();
                tipoSalas.Add(tipoSala);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoSala);
        }

        // GET: TipoSala/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSala = BuscarTipoSala(id);
            if (tipoSala == null)
            {
                return NotFound();
            }
            return View(tipoSala);
        }

        // POST: TipoSala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Precio")] TipoSala tipoSala)
        {
            if (id != tipoSala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tipoSalaEncontrada = BuscarTipoSala(id);
                    tipoSalaEncontrada.Precio = tipoSala.Precio;
                    tipoSalaEncontrada.Nombre = tipoSala.Nombre;
                    

                }
                catch (Exception)
                {
                    if (!TipoSalaExists(tipoSala.Id))
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
            return View(tipoSala);
        }

        // GET: TipoSala/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSala = BuscarTipoSala(id);
                
            if (tipoSala == null)
            {
                return NotFound();
            }

            return View(tipoSala);
        }

        // POST: TipoSala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoSala = BuscarTipoSala(id);
            tipoSalas.Remove(tipoSala);
            return RedirectToAction(nameof(Index));
        }

        private bool TipoSalaExists(Guid id)
        {
            return _context.TipoSala.Any(e => e.Id == id);
        }
        private TipoSala BuscarTipoSala(Guid? id)
        {
            if (id == null)
                return null;

            var tipoSala = tipoSalas.FirstOrDefault(p => p.Id == id);
            return tipoSala;
        }
    }
}
