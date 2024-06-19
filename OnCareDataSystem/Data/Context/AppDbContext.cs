using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using OnCareDataSystem.Models;
using OnCareDataSystem.Models.DTOs;
using OnCareDataSystem.Models.Entities;

namespace OnCareDataSystem.Data.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Imagem> Imagens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Financeiro> Financeiros { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Servico> Servicos { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("adc o acesso ao banco");
        }

    }
}
