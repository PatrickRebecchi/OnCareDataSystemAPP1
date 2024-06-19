using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnCareDataSystem.Data.Context;
using OnCareDataSystem.Models;
using OnCareDataSystem.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnCareDataSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public RelatorioController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioDTO>>> GetRelatorios()
        {
            var relatorios = await _context.Relatorios.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RelatorioDTO>>(relatorios));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelatorioDTO>> GetRelatorio(int id)
        {
            var relatorio = await _context.Relatorios.FindAsync(id);
            if (relatorio == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RelatorioDTO>(relatorio));
        }

        [HttpPost]
        public async Task<ActionResult<RelatorioDTO>> CreateRelatorio(RelatorioDTO relatorioDTO)
        {
            var relatorio = _mapper.Map<Relatorio>(relatorioDTO);
            _context.Relatorios.Add(relatorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRelatorio), new { id = relatorio.Id }, _mapper.Map<RelatorioDTO>(relatorio));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRelatorio(int id, RelatorioDTO relatorioDTO)
        {
            if (id != relatorioDTO.Id)
            {
                return BadRequest();
            }

            var relatorioExistente = await _context.Relatorios.FindAsync(id);
            if (relatorioExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(relatorioDTO, relatorioExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelatorioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelatorio(int id)
        {
            var relatorio = await _context.Relatorios.FindAsync(id);
            if (relatorio == null)
            {
                return NotFound();
            }

            _context.Relatorios.Remove(relatorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelatorioExists(int id)
        {
            return _context.Relatorios.Any(e => e.Id == id);
        }
    }
}
