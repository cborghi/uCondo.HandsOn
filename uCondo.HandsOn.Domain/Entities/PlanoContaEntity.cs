namespace uCondo.HandsOn.Domain.Entities
{
    public class PlanoContaEntity
    {
        public Guid ID_PLANO_CONTAS { get; set; }
        public string COD_PLANO_CONTAS { get; set; }
        public string COD_PAI_PLANO_CONTAS { get; set; }
        public string NOME_CONTAS { get; set; }
        public Guid ID_TIPO_CONTAS { get; set; }
        public string NOME_TIPO_CONTAS { get; set; }
        public bool ACEITA_LANCAMENTOS { get; set; }
        public bool ATIVO { get; set; }
    }
}
