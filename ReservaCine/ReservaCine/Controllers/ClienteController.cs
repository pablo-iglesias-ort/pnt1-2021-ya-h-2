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
    public class ClienteController : Controller
    {
        private readonly ReservaCineContext _context;

        static List<Cliente> clientes = new List<Cliente>()
        {
            new Cliente()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Diego Armando",
                 Apellido = "Maradona",
                 Domicilio = "Segurola y Habana",
                 DNI = 10234086,
                 Email = "diegoarmando86@gmail.com",
                 Telefono = 1119861990,
                 FechaAlta = new DateTime(1960,10,30),
                 NombreUsuario = "Diegol86",
                 Password = "12324254"
            },
            new Cliente()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Lionel",
                 Apellido = "Messi",
                 Domicilio = "Paris, Francia",
                 DNI = 33016244,
                 Email = "messikpo@yahoo.com",
                 Telefono = 0303456,
                 FechaAlta = new DateTime(1987,6,24),
                 NombreUsuario = "MessiCrack",
                 Password = "12324s254"
            }
        };

        public ClienteController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = BuscarCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,DNI,Email,Domicilio,Telefono,FechaAlta,NombreUsuario, Password")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                _context.Add(cliente);
                clientes.Add(cliente);

                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = BuscarCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Apellido,DNI,Email,Domicilio,Telefono,FechaAlta,NombreUsuario,Password")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ClienteEncontrado = BuscarCliente(id);
                    ClienteEncontrado.Nombre = cliente.Nombre;
                    ClienteEncontrado.Apellido = cliente.Apellido;
                    ClienteEncontrado.DNI = cliente.DNI;
                    ClienteEncontrado.Email = cliente.Email;
                    ClienteEncontrado.Domicilio = cliente.Domicilio;
                    ClienteEncontrado.Telefono = cliente.Telefono;
                    ClienteEncontrado.NombreUsuario = cliente.NombreUsuario;
                    ClienteEncontrado.Password = cliente.Password;
                }
                catch (Exception)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = BuscarCliente(id);
                
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = BuscarCliente(id);
            _context.Cliente.Remove(cliente);
            clientes.Remove(cliente);
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
        private Cliente BuscarCliente(Guid? id)
        {
            if (id == null)
                return null;
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            return cliente;
        }
    }
}
