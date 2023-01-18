using uCondo.HandsOn.Domain.Entities;
using uCondo.HandsOn.Service.TemplateService;

namespace uCondo.HandsOn.Service.Interfaces
{
    public interface IPlanoContasService
    {
        Task<List<PlanoContaEntity>> Listar();
        Task<Dictionary<string, string>> BuscarFilha(string codPai);
        Task Inserir(PlanoContaEntity planoContas);
    }
}
