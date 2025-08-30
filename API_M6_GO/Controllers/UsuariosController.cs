using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_M6_GO.Context;
using API_M6_GO.Models;

namespace API_M6_GO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly Sessao6 _context;

        public UsuariosController(Sessao6 context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        public class UserAuth 
        {
            public string Email { get; set; } = null!;

            public string Senha { get; set; } = null!;
        }


        [HttpPost]
        public async Task<IActionResult> IsUser(UserAuth user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Email == user.Email && m.Senha == user.Senha);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
    }
}
