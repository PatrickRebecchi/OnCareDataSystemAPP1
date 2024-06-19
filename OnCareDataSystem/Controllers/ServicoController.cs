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
    public class ServicoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ServicoController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoDTO>>> GetServicos()
        {
            var servicos = await _context.Servicos.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServicoDTO>>(servicos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoDTO>> GetServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServicoDTO>(servico));
        }

        [HttpPost]
        public async Task<ActionResult<ServicoDTO>> CreateServico(ServicoDTO servicoDTO)
        {
            var servico = _mapper.Map<Servico>(servicoDTO);
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServico), new { id = servico.Id }, _mapper.Map<ServicoDTO>(servico));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServico(int id, ServicoDTO servicoDTO)
        {
            if (id != servicoDTO.Id)
            {
                return BadRequest();
            }

            var servicoExistente = await _context.Servicos.FindAsync(id);
            if (servicoExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(servicoDTO, servicoExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoExists(id))
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
        public async Task<IActionResult> DeleteServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicoExists(int id)
        {
            return _context.Servicos.Any(e => e.Id == id);
        }
    }
}
