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
                 Domicilio = "Avenida Siempreviva 742",
                 DNI = 1234,
                 Email = "patocastell@hotmail.com",
                 Legajo = 12343224,
                 Telefono = 1222323564,
                 FechaAlta = new DateTime(1998,11,2),
                 NombreUsuario = "Pato98",
                 Password = "12324254"
                

            },
            new Empleado()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Lionel",
                 Apellido = "Messi",
                 Domicilio = "Paris, Francia",
                 DNI = 33016244,
                 Email = "messikpo@yahoo.com",
                 Legajo = 123437724,
                 Telefono = 0303456,
                 FechaAlta = new DateTime(1987,6,24),
                 NombreUsuario = "MessiCrack",
                 Password = "12324s254"

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
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,DNI,Email,Domicilio,Telefono,FechaAlta,NombreUsuario, Password")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.Id = Guid.NewGuid();
                _context.Add(empleado);
                empleados.Add(empleado);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Legajo,Id,Nombre,Apellido,DNI,Email,Domicilio,Telefono,FechaAlta,NombreUsuario, Password")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var EmpleadoEncontrado = BuscarEmpleado(id);
                    EmpleadoEncontrado.Nombre = empleado.Nombre;
                    EmpleadoEncontrado.Apellido = empleado.Apellido;
                    EmpleadoEncontrado.DNI = empleado.DNI;
                    EmpleadoEncontrado.Email = empleado.Email;
                    EmpleadoEncontrado.Domicilio = empleado.Domicilio;
                    EmpleadoEncontrado.Telefono = empleado.Telefono;
                    EmpleadoEncontrado.NombreUsuario = empleado.NombreUsuario;
                    EmpleadoEncontrado.Password = empleado.Password;

                }
                catch (Exception)
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

            var empleado = BuscarEmpleado(id);
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
            var empleado = BuscarEmpleado(id);
            empleados.Remove(empleado);
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(Guid id)
        {
            return _context.Empleado.Any(e => e.Id == id);
        }
        private Empleado BuscarEmpleado(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            return empleados.FirstOrDefault(e => e.Id == id);
        }

    }
}
