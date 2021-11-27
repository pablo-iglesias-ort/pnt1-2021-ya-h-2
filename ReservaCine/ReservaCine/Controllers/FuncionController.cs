﻿using System;
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
            var reservaCineContext = _context.Funcion
                                       .Where(f => f.Confirmar)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Sala)
                                    .ThenInclude(f => f.TipoSala);
            return View(await reservaCineContext.ToListAsync());
        }

        // GET: Funcion/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Funcion/Create
        public IActionResult Create()
        {
            completarPeliculas();
            completarSalas();
            return View();
        }

        // POST: Funcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create(Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                funcion.Id = Guid.NewGuid();
                funcion.Hora = new DateTime(1, 1, 1, funcion.Hora.Hour, funcion.Hora.Minute, funcion.Hora.Second);



                var butacas = await _context.Sala
                                         .Where(s => s.Id == funcion.SalaId)
                                        //.Select(s => s.CapacidadButacas)

                                        .FirstOrDefaultAsync(s => s.Id == funcion.SalaId);

                funcion.CantButacasDisponibles = butacas.CapacidadButacas;


                _context.Add(funcion);
                await _context.SaveChangesAsync();

               
                return RedirectToAction(nameof(Index));
            }


            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Sala, "Id", "Numero", funcion.SalaId);
          
            return View(funcion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
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
            buscarSala(funcion.SalaId);
            buscarPelicula(funcion.PeliculaId);
            completarPeliculas();
            completarSalas();

            var salas = await _context.Sala
                                        .Include(s => s.TipoSala)
                                        .Select(s => new SelectListItem(string.Concat(s.Numero, " - ", s.TipoSala.Nombre, " - ",s.CapacidadButacas),s.Id.ToString()))
                                        .ToListAsync();

            ViewBag.Salas = salas;
            

            return View(funcion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // POST: Funcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Funcion funcion)
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
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Sala, "Id", "Numero", funcion.SalaId);
            return View(funcion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Funcion/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
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

        private async void completarPeliculas()
        {
            ViewBag.PeliculaId = await _context.Pelicula.ToListAsync();
        }

        //para buscar pelicula y llamar al metodo en el details,delete
        private async void buscarPelicula(Guid id)
        {
            ViewBag.PeliculaTitulo = await _context.Pelicula
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //completar lista de salas a seleccionar
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
        //private async void mostrarSalas()
        //{
        //    var tipoSalas = await _context.TipoSala
        //                                .Select(t => new SelectListItem(t.Nombre, t.Id.ToString()))
        //                                .ToListAsync();

        //    ViewBag.TipoSalas = tipoSalas;
        //}

    }

    
}
