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
    public class UsuarioController : Controller
    {
        private readonly ReservaCineContext _context;
        static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Patricio",
                 Apellido = "Castellano",
                 Domicilio = "Avenida Siempreviva 742",
                 DNI = 1234,
                 Email = "patocastell@hotmail.com",
                 Telefono = 1222323564,
                 FechaAlta = DateTime.Now,
                 NombreUsuario = "pato2021",

            },
            new Usuario()
            {
                 Id = Guid.NewGuid(),
                 Nombre = "Lionel",
                 Apellido = "Messi",
                 Domicilio = "Paris, Francia",
                 DNI = 33016244,
                 Email = "messikpo@yahoo.com",
                 Telefono = 0303456,
                 FechaAlta = DateTime.Now,
                 NombreUsuario="lio2021",


            }
        };

        public UsuarioController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = BuscarUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,DNI,Email,Domicilio,Telefono,NombreUsuario,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                _context.Usuario.Add(usuario);
                usuarios.Add(usuario);
               
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = BuscarUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Apellido,DNI,Email,Domicilio,Telefono,NombreUsuario,Password")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var UsuarioEncontrada = BuscarUsuario(id);
                    UsuarioEncontrada.Nombre = usuario.Nombre;
                    UsuarioEncontrada.Apellido = usuario.Apellido; 
                    UsuarioEncontrada.DNI = usuario.DNI;
                    UsuarioEncontrada.Email = usuario.Email;
                    UsuarioEncontrada.Domicilio = usuario.Domicilio;
                    UsuarioEncontrada.Telefono = usuario.Telefono;
                    UsuarioEncontrada.NombreUsuario = usuario.NombreUsuario;
                    UsuarioEncontrada.Password = usuario.Password;


                }
                catch (Exception)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = BuscarUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuario = BuscarUsuario(id);
            _context.Usuario.Remove(usuario);
            usuarios.Remove(usuario);
            return RedirectToAction("Index");
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        private Usuario BuscarUsuario(Guid? id)  
        {
            if (id == null)
                return null;
            var usuario= usuarios.FirstOrDefault(u => u.Id == id);

            return usuario;
        }
    }
}
