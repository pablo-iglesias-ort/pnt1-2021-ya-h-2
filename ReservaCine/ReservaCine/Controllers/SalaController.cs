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
    public class SalaController : Controller
    {
        private readonly ReservaCineContext _context;
        static List<Sala> salas = new List<Sala>()
        {
            new Sala
            {
              Id = Guid.NewGuid(),
              CapacidadButacas = 50,
              Numero = 45


            },
            new Sala
            {
                 Id = Guid.NewGuid(),
              CapacidadButacas = 52,
              Numero = 43
            }

        };

        

        public SalaController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Sala
        public async Task<IActionResult> Index()
        {
            return View(salas);
        }

        // GET: Sala/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = BuscarSala(id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Sala/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,CapacidadButacas")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                sala.Id = Guid.NewGuid();
                salas.Add(sala);
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Sala/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = BuscarSala(id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Sala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Numero,CapacidadButacas")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var salaEncontrada = BuscarSala(id);
                    salaEncontrada.Numero = sala.Numero;
                    salaEncontrada.CapacidadButacas = sala.CapacidadButacas;
                    

                }
                catch (Exception)
                {
                    if (!SalaExists(sala.Id))
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
            return View(sala);
        }

        // GET: Sala/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = BuscarSala(id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Sala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sala = BuscarSala(id);
            salas.Remove(sala);
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(Guid id)
        {
            return _context.Sala.Any(e => e.Id == id);
        }
        private Sala BuscarSala(Guid? id)
        {
            if (id == null)
                return null;

            var Sala = salas.FirstOrDefault(p => p.Id == id);
            return Sala;
        }
    }
}
