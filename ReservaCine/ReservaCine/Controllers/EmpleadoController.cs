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
    public class EmpleadoController : Controller
    {
        private readonly ReservaCineContext _context;
        static List<Empleado> empleados = new List<Empleado>()
        {
            new Empleado()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Patricio",
                 Apellido = "Castellano",
                 Direccion = "Avenida Siempreviva 742",
                 dni = 1234,
                 Email = "patocastell@hotmail.com",
                 Legajo = 12343224,
                 Telefono = 1222323564,
                 FechaAlta = new DateTime(1998/11/2)
                
            },
            new Empleado()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Lionel",
                 Apellido = "Messi",
                 Direccion = "Paris, Francia",
                 dni = 33016244,
                 Email = "messikpo@yahoo.com",
                 Legajo = 123437724,
                 Telefono = 0303456,
                 FechaAlta = new DateTime(1987/6/24)

            }
        };

        public EmpleadoController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            return View(empleados);
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = BuscarEmpleado(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,dni,Telefono,Direccion,FechaAlta,Email,Legajo")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.Id = Guid.NewGuid();
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = BuscarEmpleado(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Apellido,dni,Telefono,Direccion,FechaAlta,Email,Legajo")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(Guid id)
        {
            return _context.Empleado.Any(e => e.Id == id);
        }
        private Empleado BuscarEmpleado(Guid? id)
        {
            return empleados.FirstOrDefault(e => e.Id == id);
        }
    }
}
