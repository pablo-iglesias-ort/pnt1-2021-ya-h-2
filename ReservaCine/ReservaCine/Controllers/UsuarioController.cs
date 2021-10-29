using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaCine.Data;
using ReservaCine.Models;

namespace ReservaCine.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {
        private readonly ReservaCineContext _context;

        public UsuarioController(ReservaCineContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public IActionResult Ingresar(string returnUrl)
        {
            TempData["UrlIngreso"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string NombreUsuario, string Password)
        {
           // Guardamos la URL a la que debemos redirigir al usuario
            var urlIngreso = TempData["UrlIngreso"] as string;

            // Verificamos que ambos esten informados
            if (!string.IsNullOrEmpty(NombreUsuario) && !string.IsNullOrEmpty(Password))
            {

                // Verificamos que exista el usuario
                var user = await _context.Usuario.FirstOrDefaultAsync(u => u.NombreUsuario == NombreUsuario);
                if (user != null)
                {

                    // Verificamos que coincida la contraseña
                    var contraseña = Encoding.ASCII.GetBytes(Password);
                    if (contraseña.SequenceEqual(user.Password))
                    {
                        // Creamos los Claims (credencial de acceso con informacion del usuario)-- cookies
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        // Agregamos a la credencial el nombre de usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, NombreUsuario));
                        // Agregamos a la credencial el nombre del estudiante/administrador
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));
                        // Agregamos a la credencial el Rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Rol.ToString()));
                        // Agregamos el Id de Usuario
                        identidad.AddClaim(new Claim("IdDeUsuario", user.Id.ToString()));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);

                        // Ejecutamos el Login
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrEmpty(urlIngreso))
                        {
                            return Redirect(urlIngreso);
                        }
                        else
                        {
                            // Redirigimos a la pagina principal
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            ViewBag.ErrorEnLogin = "Verifique el usuario y contraseña";
            TempData["UrlIngreso"] = urlIngreso; // Volvemos a enviarla en el TempData para no perderla
            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AccesoDenegado()
        {
            return View();
        }


        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse(Usuario usuario, string pass, int? legajo, bool altaEmpleado = false)
        {
            if (ModelState.IsValid)
            {

                if (altaEmpleado)
                {
                    var nuevoEmpleado = new Empleado();
                    nuevoEmpleado.NombreUsuario = usuario.Nombre + usuario.DNI.ToString();
                    //nuevoEmpleado.Password = seguridad.EncriptarPass(usuario.DNI.ToString());
                    _context.Empleado.Add(nuevoEmpleado);
                }
                else
                {
                    var nuevoCliente = new Cliente();

                    
                    _context.Cliente.Add(nuevoCliente);
                }
                                

                usuario.Id = Guid.NewGuid();
                usuario.Password = Encoding.ASCII.GetBytes(pass);
                if (legajo == null)
                {
                    
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Ingresar));
            }
            return View(usuario);
        }

        
    }
}

