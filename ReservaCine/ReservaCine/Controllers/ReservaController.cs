using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public ReservaController(ReservaCineContext context)
        {
            _context = context;
        }

        [Authorize(Roles = nameof(Rol.Administrador) + "," + nameof(Rol.Cliente))]
        // GET: Reserva
        public async Task<IActionResult> Index()//reservas historicas
        {
            var IdDeCliente = Guid.Parse(User.FindFirst("IdDeUsuario").Value);

           return View(await _context.Reserva.Where(r => r.ClienteId == IdDeCliente && r.Activa).OrderByDescending(x => x.FechaAlta).ToListAsync());
        }

        public async Task<IActionResult> ReservasHistoricas()
        {
            var IdDeCliente = Guid.Parse(User.FindFirst("IdDeUsuario").Value);

            return View(await _context.Reserva.Where(r => r.ClienteId == IdDeCliente && !r.Activa).OrderByDescending(x => x.FechaAlta).ToListAsync());
        }
       
        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Funcion)
                .ThenInclude(r => r.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            completarFuncion();
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
                var clienteId = Guid.Parse(User.FindFirst("IdDeUsuario").Value);
                reserva.ClienteId = clienteId;

                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        // GET: Reserva/Edit/5

        //[Authorize(Roles = nameof(Rol.Administrador) + "," + nameof(Rol.Cliente))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
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
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
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

            var reserva = await _context.Reserva
                 .Include(r => r.Funcion)
                 .ThenInclude(r => r.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
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
           
           
            var reserva = await _context.Reserva
                                                .Include(r=>r.Funcion)
                                                .ThenInclude(r => r.Pelicula)
                                                .FirstOrDefaultAsync(f => f.Id == id);

        
            reserva.Funcion.CantButacasDisponibles = reserva.Funcion.CantButacasDisponibles + reserva.CantidadButacas;
           
            _context.Reserva.Remove(reserva);
            _context.Update(reserva.Funcion);

            await _context.SaveChangesAsync();
          
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeleccionarPelicula()
        {
            var clienteId = Guid.Parse(User.FindFirst("IdDeUsuario").Value);
            if (ClienteTieneReservaActiva(clienteId, out Guid? reservaId))
            {
                return RedirectToAction(nameof(Details), new {id = reservaId});
            }

            // peliculas con funciones confirmadas y butacas disponibles
            var peliculasDisponibles = await _context.Funcion
                                                        .Include(f => f.Pelicula)
                                                         .ThenInclude(f => f.Genero)
                                                        .Where(f => f.CantButacasDisponibles > 0 && f.Confirmar)
                                                       .Select(f => f.Pelicula)
                                                      .Distinct()
                                                       .ToListAsync();

            return View(peliculasDisponibles);            
        }

        public async Task<IActionResult> SeleccionarFuncion(Guid peliculaId, int butacas)
        {
            if(butacas <= 0)
            {
                
                return RedirectToAction(nameof(SeleccionarPelicula));
            }
            // peliculas con funciones confirmadas y butacas disponibles
            var funcionesDisponibles = await _context.Funcion
                                                       .Where(f => f.CantButacasDisponibles >= butacas && f.Confirmar && f.PeliculaId == peliculaId)
                                                       .Include(f=> f.Sala)
                                                       .ThenInclude(f=> f.TipoSala)
                                                       .Include(f=> f.Pelicula)
                                                       .ToListAsync();
            ViewBag.Butacas = butacas;
            return View(funcionesDisponibles);
        }

        public async Task<IActionResult> ConfirmarReserva(Guid FuncionId, int butacas)
        {
            //TODO -- COMO MANDAR UN MSJ DE ERROR EN CASO DE QUERER RESERVAR SIN HABER SELECCIONADO LAS BUTACAS
           
            var peliculaReservada = await _context.Funcion
                                                        .Include(f => f.Sala)
                                                       .ThenInclude(f => f.TipoSala)
                                                       .Include(f => f.Pelicula)
                                                       .FirstOrDefaultAsync(f => f.Id == FuncionId);
            ViewBag.Butacas = butacas;

            return View(peliculaReservada);
        }

        [HttpPost]
        public async Task<IActionResult> ReservaConfirmada(Guid FuncionId, int butacas)
        {
            var reserva = new Reserva();
            reserva.Id = Guid.NewGuid();
            reserva.FuncionId = FuncionId;
            reserva.ClienteId = Guid.Parse(User.FindFirst("IdDeUsuario").Value);
            reserva.Activa = true;
            reserva.CantidadButacas = butacas;
            reserva.FechaAlta = DateTime.Now;

            _context.Reserva.Add(reserva);

            var funcionReservada = await _context.Funcion
                                                .FirstOrDefaultAsync(f => f.Id == FuncionId);
            funcionReservada.CantButacasDisponibles = funcionReservada.CantButacasDisponibles - butacas;

            _context.Update(funcionReservada);

            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = reserva.Id });
            }
           
        
        private bool ClienteTieneReservaActiva(Guid clienteId, out Guid? reservaId)
        {
            var reserva = _context.Reserva.FirstOrDefault(r => r.Activa && r.ClienteId == clienteId);
            reservaId = reserva?.Id;
            return reserva != null;
        }

        private bool ReservaExists(Guid id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }

        private async void completarFuncion()
        {
            ViewBag.FuncionId = await _context.Funcion.ToListAsync();
        }
    }
}
