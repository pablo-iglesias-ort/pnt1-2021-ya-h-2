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
    public class FuncionController : Controller
    {
        private readonly ReservaCineContext _context;

        public FuncionController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Funcion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcion.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Funcion/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }
        
        
        // GET: Funcion/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = nameof(Rol.Administrador))] 
        // POST: Funcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Hora,Descripcion,CantButacasDisponibles,Confirmar,PeliculaId,SalaId,Horario")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                funcion.Id = Guid.NewGuid();
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcion);
        }

        // GET: Funcion/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            return View(funcion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // POST: Funcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Fecha,Hora,Descripcion,CantButacasDisponibles,Confirmar,PeliculaId,SalaId,Horario")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.Id))
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
            return View(funcion);
        }

        // GET: Funcion/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // POST: Funcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var funcion = await _context.Funcion.FindAsync(id);
            _context.Funcion.Remove(funcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(Guid id)
        {
            return _context.Funcion.Any(e => e.Id == id);
        }
    }
}
