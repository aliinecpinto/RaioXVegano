using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using System;

namespace RaioXVegano.so.Acao
{
    public abstract class BaseAcaoProdutoSO<Request, Response> : BaseAcaoSO<Request, Response>, IBaseAcaoProdutoSO<Request, Response>
        where Request : IBaseAcaoRequest
        where Response : IBaseAcaoResponse
    {
        public BaseAcaoProdutoSO(Type type, Uri baseEndpoint) : base(type)
        {
            _baseEndpoint = new Uri(baseEndpoint, "Produto/");
        }
    }
}
