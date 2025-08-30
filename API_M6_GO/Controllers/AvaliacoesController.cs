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
    public class AvaliacoesController : Controller
    {
        private readonly Sessao6 _context;

        public AvaliacoesController(Sessao6 context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Avaliacoes
        public async Task<IActionResult> Index()
        {
            var sessao6 = _context.Avaliacoes.ToList();
            return Ok(sessao6);
        }

        [HttpGet("{id:int}")]
        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliaco = await _context.Avaliacoes
                .Include(a => a.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliaco == null)
            {
                return NotFound();
            }

            return Ok(avaliaco);
        }

        public class AvaliacaoDTO 
        {
            public int? Avaliacao { get; set; }
        }

        // POST: Avaliacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(AvaliacaoDTO avaliaco)
        {
            if (avaliaco != null)
            {
                Avaliaco avaliaco1 = new Avaliaco() 
                {
                    Avaliacao = avaliaco.Avaliacao 
                };

                _context.Add(avaliaco1);
                await _context.SaveChangesAsync();
                return Ok(avaliaco1);
            }
            return BadRequest();
        }
    }
}
