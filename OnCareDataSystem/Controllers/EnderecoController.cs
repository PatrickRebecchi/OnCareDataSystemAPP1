using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnCareDataSystem.Data.Context;
using OnCareDataSystem.Models.DTOs;
using OnCareDataSystem.Models.Entities;

namespace OnCareDataSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public EnderecoController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDTO>>> GetEnderecos()
        {
            var enderecos = await _context.Enderecos.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EnderecoDTO>>(enderecos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoDTO>> GetEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EnderecoDTO>(endereco));
        }

        [HttpPost]
        public async Task<ActionResult<EnderecoDTO>> CreateEndereco(EnderecoDTO enderecoDTO)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEndereco), new { id = endereco.Id }, _mapper.Map<EnderecoDTO>(endereco));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEndereco(int id, EnderecoDTO enderecoDTO)
        {
            if (id != enderecoDTO.Id)
            {
                return BadRequest();
            }

            var endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
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
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.Id == id);
        }
    }

}
