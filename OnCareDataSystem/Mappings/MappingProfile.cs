using AutoMapper;
using OnCareDataSystem.Models;
using OnCareDataSystem.Models.DTOs;
using OnCareDataSystem.Models.Entities;

namespace OnCareDataSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Financeiro, FinanceiroDTO>().ReverseMap();
            CreateMap<Pessoa, PessoaDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Servico, ServicoDTO>().ReverseMap();
            CreateMap<Relatorio, RelatorioDTO>().ReverseMap();

        }
    }
}
