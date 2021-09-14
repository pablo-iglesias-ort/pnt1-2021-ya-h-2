using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaCine.Models;
using Microsoft.AspNetCore.Authorization;
using ReservaCine.Data;

namespace ReservaCine.Controllers
{

    public class FuncionesController : Controller
    {
        private readonly ReservaCineContext _context;

        public FuncionesController(ReservaCineContext context)
        {
            _context = context;
        }

        [HttpGet]
        // agregar rol
        public IActionResult Index()
        {
            var funciones = _context
                .Funcion
                .Include(x => x.Pelicula)
                .Include(x => x.Sala)
                .ToList();

            return View(funciones);
        }


        [HttpGet]
        // agregar rol
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var funcion = _context.Funcion
                .Include(x => x.Reservas).ThenInclude(x => x.Cliente)
                .Include(x => x.Pelicula)
                .Include(x => x.Sala);
               

            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }


        [HttpGet]
        // agregar rol
        public IActionResult Create()
        {
            ViewBag.SelectSalas = new SelectList(_context.Sala, "Id", "Nombre");
            ViewBag.SelectPeliculas = new SelectList(_context.Pelicula, "Id", "Nombre");

            return View();
        }


        [HttpPost]
        // agregar rol
        [ValidateAntiForgeryToken]
        public IActionResult Create(Funcion funcion)
        {

            ValidarFecha(funcion);
            ValidarHorario(funcion);

            if (ModelState.IsValid)
            {
                var sala = _context.Sala
                             .Where(x => x.Id == funcion.Id)
                             .FirstOrDefault();

                funcion.CantButacasDisponibles = sala.CapacidadButacas;

                _context.Add(funcion);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.SelectSalas = new SelectList(_context.Sala, "Id", "Nombre");
            ViewBag.SelectPeliculas = new SelectList(_context.Pelicula, "Id", "Nombre");

            return View(funcion);
        }


        [HttpGet]
        // agregar rol
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = _context.Funcion.Find(id);

            if (funcion == null)
            {
                return NotFound();
            }

            ViewBag.SelectSalas = new SelectList(_context.Sala, "Id", "Nombre");
            ViewBag.SelectPeliculas = new SelectList(_context.Pelicula, "Id", "Nombre");

            return View(funcion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // agregar rol
        public IActionResult Edit(Guid id, Funcion funcion)
        {

            if (id != funcion.Id)
            {
                return NotFound();
            }

            ValidarFecha(funcion);
            ValidarHorario(funcion);
           

            if (ModelState.IsValid)
            {
                try
                {
             
                    var funcionDb = _context.Funcion.Find(id);

 
                    funcionDb.SalaId = funcion.SalaId;
                    funcionDb.PeliculaId = funcion.PeliculaId;
                    funcionDb.Fecha = funcion.Fecha;
                    funcionDb.Horario = funcion.Horario;

                    _context.Update(funcionDb);
                    _context.SaveChanges();
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

            ViewBag.SelectSalas = new SelectList(_context.TipoSala, "Id", "Nombre");
            ViewBag.SelectPeliculas = new SelectList(_context.Pelicula, "Id", "Nombre");

            return View(funcion);
        }

        private bool FuncionExists(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        // agregar rol
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = _context
                .Funcion
                .Include(x => x.Pelicula)
                .Include(x => x.Sala);

            if (funcion == null)
            {
                return NotFound();
            }


            return View(funcion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // agregar rol
        public IActionResult DeleteConfirmed(int id)
        {
            var funcion = _context.Funcion.Find(id);

            _context.Funcion.Remove(funcion);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        // agregar rol
        public IActionResult SeleccionarFiltro()
        {
            ViewBag.SelectPeliculas = new SelectList(_context.Pelicula, "Id", "Nombre");

            return View();
        }


        [AllowAnonymous]
        public IActionResult FiltrarPorPelicula(Pelicula pelicula) //cuando entra por cartelera
        {
            var funciones = _context
                 .Funcion
                 .Include(x => x.Pelicula)
                 .Include(x => x.Sala).ThenInclude(x => x.tipoSala)
                 .Where(x => x.Id == pelicula.Id && x.Fecha >= DateTime.Now)
                 .ToList();

            if (!funciones.Any())
            {
                return RedirectToAction(nameof(NoHayFunciones));
            }

            return View(funciones);
        }


       // agregar rol
        public IActionResult FiltrarPorPeliculaId(Guid PeliculaId) 
        {
            var funciones = _context
                .Funcion
                .Include(x => x.Pelicula)
                .Include(x => x.Sala).ThenInclude(x => x.tipoSala)
                .Where(x => x.Pelicula.Id == PeliculaId && x.Fecha >= DateTime.Now)
                .ToList();

            if (!funciones.Any())
            {
                return RedirectToAction(nameof(NoHayFunciones));
            }

            return View(funciones);
        }


        //agregar rol
        public IActionResult FiltrarPorFecha(DateTime Fecha) // Cuando entra por el filtro día/pelicula
        {
            var funciones =
                _context
                .Funcion
                .Include(x => x.Pelicula)
                .Include(x => x.Sala).ThenInclude(x => x.tipoSala)
                .Where(x => x.Fecha == Fecha)
                .ToList();

            if (!funciones.Any())
            {
                return RedirectToAction(nameof(NoHayFunciones));
            }

            return View(funciones);
        }


        [HttpGet]
        public IActionResult NoHayFunciones()
        {
            return View();
        }



        ////---- Métodos privados para validaciones ----////

        private bool FuncionExists(int id)
        {
            return _context.Funcion.Any();
        }


        private void ValidarFecha(Funcion funcion)
        {
            if (funcion.Fecha < DateTime.Now)
            {
                ModelState.AddModelError(nameof(funcion.Fecha), "La fecha no puede ser anterior a la fecha actual");
            }

            if (funcion.Fecha.Year > DateTime.Now.Year + 1)
            {
                ModelState.AddModelError(nameof(funcion.Fecha), "La fecha debe ser dentro del año actual");
            }
        }


        private void ValidarHorario(Funcion funcion)
        {
            if (funcion.Hora.Hour > 1 && funcion.Hora.Hour < 9)
            {
                ModelState.AddModelError(nameof(funcion.Horario), "El horario debe estar comprendido entre las 9:00 y la 01:59 (A.M.)");
            }

            ValidarSalaLibre(funcion);  // Si validar horario esta OK, llama a Validar sala libre

        }


        // Valida que la sala se encuentre libre cuando quiero crear o editar una funcion.
        private void ValidarSalaLibre(Funcion f)
        {

            if (_context.Funcion.Any(
                x => x.Fecha == f.Fecha &&
                x.SalaId == f.SalaId &&
               (x.Hora.Hour >= f.Hora.Hour - 3 && x.Hora.Hour <= f.Hora.Hour + 3) &&
                x.Id != f.Id))// verifico que el Id para que la funcion no se encuentre a si misma en caso de edicion
            {
                ModelState.AddModelError(nameof(f.Horario), "La sala está ocupada en ese horario");
            }
        }

        // Valida que la cantidad de butacas total de la sala nueva no sea menor a la cantidad ya reservada para la función.
        private void ValidarCapacidadNuevaSalaSegunReservas(int funcionId, Funcion funcion)
        {
            var funcionDb = _context.Funcion.Find(funcionId);
            var salaAnterior = _context.Sala.Find(funcionDb.SalaId);
            var salaNueva = _context.Sala.Find(funcion.SalaId);

            var cantidadButacasReservadas = salaAnterior.CapacidadButacas - funcionDb.CantButacasDisponibles;
            funcionDb.CantButacasDisponibles = salaNueva.CapacidadButacas - cantidadButacasReservadas;

            if (funcionDb.CantButacasDisponibles < 0)
            {
                ModelState.AddModelError(nameof(funcion.SalaId), "No se puede cambiar la sala, dado que no es posible disminuir la cantidad de butacas por debajo de la cantidad ya reservada");
            }

        }


    }

}