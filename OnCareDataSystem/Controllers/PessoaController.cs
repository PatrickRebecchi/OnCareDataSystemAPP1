using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnCareDataSystem.Data.Context;
using OnCareDataSystem.Models;
using OnCareDataSystem.Models.DTOs;

namespace OnCareDataSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public PessoaController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaDTO>>> GetPessoas()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PessoaDTO>>(pessoas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaDTO>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PessoaDTO>(pessoa));
        }

        [HttpPost]
        public async Task<ActionResult<PessoaDTO>> CreatePessoa(PessoaDTO pessoaDTO)
        {
            var pessoa = _mapper.Map<Pessoa>(pessoaDTO);
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, _mapper.Map<PessoaDTO>(pessoa));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePessoa(int id, PessoaDTO pessoaDTO)
        {
            if (id != pessoaDTO.Id)
            {
                return BadRequest();
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaDTO);
            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }

}
