using System;
using System.ComponentModel.DataAnnotations;

namespace OnCareDataSystem.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        // Dados Pessoais
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Sobrenome { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        [StringLength(14)]
        public string CPF { get; set; }

        [StringLength(100)]
        public string NomeMae { get; set; }

        // Dados de Contato
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string Telefone1 { get; set; }

        [Phone]
        [StringLength(15)]
        public string Telefone2 { get; set; }

        // Dados de Trabalho
        [Required]
        [StringLength(100)]
        public string Profissao { get; set; }

        [StringLength(20)]
        public string NumRegistro { get; set; }

        [StringLength(50)]
        public string Especialidade { get; set; }

        // Datas
        [DataType(DataType.Date)]
        [Required]
        public DateTime Nascimento { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpRG { get; set; }

        [DataType(DataType.Date)]
        public DateTime? VenRegistro { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCriado { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime DataAlterado { get; set; } = DateTime.Now;
    }
}
