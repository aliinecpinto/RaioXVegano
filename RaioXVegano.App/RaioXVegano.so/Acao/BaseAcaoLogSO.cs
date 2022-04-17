using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using System;

namespace RaioXVegano.so.Acao
{
    public abstract class BaseAcaoLogSO<Request, Response> : BaseAcaoSO<Request, Response>, IBaseAcaoLogSO<Request, Response>
        where Request : IBaseAcaoRequest
        where Response : IBaseAcaoResponse
    {

        public BaseAcaoLogSO(Type type, Uri baseEndpoint) : base(type) 
        {
            _baseEndpoint = new Uri(baseEndpoint, "Log/");
        }
    }
}
