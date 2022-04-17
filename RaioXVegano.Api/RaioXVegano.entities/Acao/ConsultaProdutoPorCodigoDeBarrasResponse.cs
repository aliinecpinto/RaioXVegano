using System.Collections.Generic;

namespace RaioXVegano.entities.Acao
{
    public class ConsultaProdutoPorCodigoDeBarrasResponse : IBaseAcaoResponse
    {
        public bool IsExecucaoSucesso { get; set; }
        public IList<int> ListaErros { get; set; }
        public Produto Produto { get; set; }
    }
}
