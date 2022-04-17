using System.Collections.Generic;

namespace RaioXVegano.entities.Acao
{
    public class SalvarInformacoesLogResponse : IBaseAcaoResponse
    {
        public bool IsExecucaoSucesso { get; set; }
        public IList<int> ListaErros { get; set; }
    }
}
