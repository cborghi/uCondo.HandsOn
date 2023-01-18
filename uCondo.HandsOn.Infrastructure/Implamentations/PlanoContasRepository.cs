using Dapper;
using uCondo.HandsOn.Domain.Entities;
using uCondo.HandsOn.Infrastructure.Interfaces;

namespace uCondo.HandsOn.Infrastructure.Implamentations
{
    public class PlanoContasRepository : RepositoryBase<PlanoContaEntity>, IPlanoContasRepository
    {
        public PlanoContasRepository(RepositoryConfiguration config) : base(config) { }

        public async Task<IEnumerable<PlanoContaEntity>> Listar()
        {
            return await CallStoredProcedureAsync("SP_PLANO_CONTAS", null);
        }

        public async Task Inserir(PlanoContaEntity planoContas)
        {
            var param = new DynamicParameters(new
            {
                COD_PLANO_CONTAS = planoContas.COD_PLANO_CONTAS,
                COD_PAI_PLANO_CONTAS = planoContas.COD_PAI_PLANO_CONTAS,
                NOME_CONTAS = planoContas.NOME_CONTAS,
                ID_TIPO_CONTAS = planoContas.ID_TIPO_CONTAS,
                ACEITA_LANCAMENTOS = planoContas.ACEITA_LANCAMENTOS,
                ATIVO = planoContas.ATIVO
            });
            await CallStoredProcedureAsync("SPI_PLANO_CONTAS", param);
        }
    }
}
