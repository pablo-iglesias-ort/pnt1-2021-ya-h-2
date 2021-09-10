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
    public class ReservaController : Controller
    {
        private readonly ReservaCineContext _context;

        static List<Reserva> reservas = new List<Reserva>()
        {
            new Reserva()
            {
                Id = Guid.NewGuid(),
                FechaAlta = new DateTime(2021,09,16),
                CantidadButacas = 3
            },

            new Reserva()
            {
                Id = Guid.NewGuid(),
                FechaAlta = new DateTime(2021,10,14),
                CantidadButacas = 6
            }
        };
        public ReservaController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            return View(reservas);
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = BuscarReserva(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaAlta,CantidadButacas")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                reserva.Id = Guid.NewGuid();
                _context.Add(reserva);
                reservas.Add(reserva);

                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = BuscarReserva(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FechaAlta,CantidadButacas")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ReservaEncontrada = BuscarReserva(id);
                    ReservaEncontrada.FechaAlta = reserva.FechaAlta;
                    ReservaEncontrada.CantidadButacas = reserva.CantidadButacas;
                }
                catch (Exception)
                {
                    if (!ReservaExists(reserva.Id))
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
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = BuscarReserva(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reserva = BuscarReserva(id);
            reservas.Remove(reserva);

            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(Guid id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }
        private Reserva BuscarReserva(Guid? id)
        {
            if (id == null)
                return null;
            var reserva = reservas.FirstOrDefault(r => r.Id == id);

            return reserva;
        }

    }
}
