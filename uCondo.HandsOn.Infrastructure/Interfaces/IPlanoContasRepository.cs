using uCondo.HandsOn.Domain.Entities;

namespace uCondo.HandsOn.Infrastructure.Interfaces
{
    public interface IPlanoContasRepository
    {
        Task<IEnumerable<PlanoContaEntity>> Listar();
        Task Inserir(PlanoContaEntity planoContas);
    }
}
