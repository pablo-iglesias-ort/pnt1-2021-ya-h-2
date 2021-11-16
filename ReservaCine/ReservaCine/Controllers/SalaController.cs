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
    public class SalaController : Controller
    {
        private readonly ReservaCineContext _context;

        public SalaController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Sala
        public async Task<IActionResult> Index()
        {
            var salas = await _context.Sala.ToListAsync();

            foreach (var s in salas)
            {
                var tipoSala = await _context.TipoSala
                 .FirstOrDefaultAsync(t => t.Id == s.TipoSalaId);

                s.TipoSala.Nombre = tipoSala.Nombre;
                s.TipoSala.Id = s.TipoSalaId;
            }

            return View(salas);
        }

        


        // GET: Sala/Create
        public IActionResult Create()
        {
            completarTipoSalas();
            return View();
        }

       
        // POST: Sala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create( Sala sala)
        {
            if (ModelState.IsValid)
            {
                sala.Id = Guid.NewGuid();
                _context.Add(sala);
                await _context.SaveChangesAsync();
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

            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            mostrarTipoSalas();
            return View(sala);
        }

        private async void mostrarTipoSalas()
        {
            var tipoSalas = await _context.TipoSala
                                        .Select(t => new SelectListItem(t.Nombre, t.Id.ToString()))
                                        .ToListAsync();

            ViewBag.TipoSalas = tipoSalas;
        }

        // POST: Sala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var salaNueva = _context.Sala.FirstOrDefault(s => s.Id == id);
                   salaNueva.TipoSalaId = sala.TipoSalaId;
                    _context.Update(salaNueva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
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
            mostrarTipoSalas();
            return View(sala);
        }

        

        

        private bool SalaExists(Guid id)
        {
            return _context.Sala.Any(e => e.Id == id);
        }

        private async void completarSalas()
        {
            ViewBag.SalaId = await _context.Sala.ToListAsync();
        }

        //para buscar  y llamar al metodo en el details,delete
        private async void buscarSala(Guid id)
        {
            ViewBag.SalaNumero = await _context.Sala
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        private async void completarTipoSalas()
        {
            ViewBag.TipoSalaId = await _context.TipoSala.ToListAsync();
        }

        private async void buscarTipoSala(Guid id)
        {
            ViewBag.TipoSalaNombre = await _context.TipoSala
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
