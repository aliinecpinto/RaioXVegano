using System.Collections.Generic;

namespace RaioXVegano.entities.Acao
{
    public interface IBaseAcaoResponse
    {
        bool IsExecucaoSucesso { get; set; }
        IList<int> ListaErros { get; set; }
    }
}
