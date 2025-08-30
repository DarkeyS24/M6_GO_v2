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
    public class AtendimentosController : Controller
    {
        private readonly Sessao6 _context;

        public AtendimentosController(Sessao6 context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Atendimentos
        public async Task<IActionResult> Index()
        {
            var sessao6 = _context.Atendimentos
                .Include(a => a.Cuidador)
                .ThenInclude(c => c.IdNavigation)
                .Include(a => a.Idoso)
                .ThenInclude(c => c.IdNavigation)
                .Include(a => a.Procedimento)
                .Include(a => a.MedicamentoAplicados)
                .Include(a => a.Avaliaco)
                .Include(a => a.Status)
                .ToList();
            return Ok(sessao6);
        }

        [HttpGet("{id:int}")]
        // GET: Atendimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Cuidador)
                .ThenInclude(c => c.IdNavigation)
                .Include(a => a.Idoso)
                .ThenInclude(c => c.IdNavigation)
                .Include(a => a.Procedimento)
                .Include(a => a.MedicamentoAplicados)
                .Include(a => a.Avaliaco)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atendimento == null)
            {
                return NotFound();
            }

            return Ok(atendimento);
        }

        public class AtendimentoDTO 
        {
            public int IdosoId { get; set; }

            public int? CuidadorId { get; set; }

            public DateTime DataHora { get; set; }

            public int ProcedimentoId { get; set; }

            public string? Observacoes { get; set; }

            public int StatusId { get; set; }
        }

        // POST: Atendimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(AtendimentoDTO atendimento)
        {
            if (atendimento != null)
            {
                Atendimento atendimento1 = new Atendimento()
                {
                    IdosoId = atendimento.IdosoId,
                    CuidadorId = atendimento.CuidadorId,
                    DataHora = atendimento.DataHora,
                    ProcedimentoId = atendimento.ProcedimentoId,
                    Observacoes = atendimento.Observacoes,
                    StatusId = atendimento.StatusId
                };

                _context.Add(atendimento1);
                await _context.SaveChangesAsync();
                return Ok(atendimento1);
            }
            return BadRequest();
        }

        // POST: Atendimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, Atendimento atendimento)
        {
            if (id != atendimento.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(atendimento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtendimentoExists(atendimento.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            } 
            return Ok(atendimento);
        }
        private bool AtendimentoExists(int id)
        {
            return _context.Atendimentos.Any(e => e.Id == id);
        }
    }
}
