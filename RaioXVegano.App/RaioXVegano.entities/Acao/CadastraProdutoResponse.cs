using System.Collections.Generic;

namespace RaioXVegano.entities.Acao
{
    public class CadastraProdutoResponse : IBaseAcaoResponse
    {
        public bool IsExecucaoSucesso { get; set; }
        public IList<int> ListaErros { get; set; }
    }
}
