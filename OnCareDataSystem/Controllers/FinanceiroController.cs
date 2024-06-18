﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnCareDataSystem.Models.DTOs;

namespace OnCareDataSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceiroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YourDbContext _context;

        public FinanceiroController(IMapper mapper, YourDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanceiroDTO>>> GetFinanceiros()
        {
            var financeiros = await _context.Financeiros.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FinanceiroDTO>>(financeiros));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinanceiroDTO>> GetFinanceiro(int id)
        {
            var financeiro = await _context.Financeiros.FindAsync(id);
            if (financeiro == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FinanceiroDTO>(financeiro));
        }

        [HttpPost]
        public async Task<ActionResult<FinanceiroDTO>> CreateFinanceiro(FinanceiroDTO financeiroDTO)
        {
            var financeiro = _mapper.Map<Financeiro>(financeiroDTO);
            _context.Financeiros.Add(financeiro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFinanceiro), new { id = financeiro.Id }, _mapper.Map<FinanceiroDTO>(financeiro));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinanceiro(int id, FinanceiroDTO financeiroDTO)
        {
            if (id != financeiroDTO.Id)
            {
                return BadRequest();
            }

            var financeiro = _mapper.Map<Financeiro>(financeiroDTO);
            _context.Entry(financeiro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinanceiroExists(id))
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
        public async Task<IActionResult> DeleteFinanceiro(int id)
        {
            var financeiro = await _context.Financeiros.FindAsync(id);
            if (financeiro == null)
            {
                return NotFound();
            }

            _context.Financeiros.Remove(financeiro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinanceiroExists(int id)
        {
            return _context.Financeiros.Any(e => e.Id == id);
        }
    }

}
