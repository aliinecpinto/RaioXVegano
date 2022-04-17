using RaioXVegano.entities;

namespace RaioXVegano.ibo
{
    public interface IBaseAcaoBO<Request, Response>
        where Request : IBaseAcaoRequest
        where Response : IBaseAcaoResponse
    {
        Response Executar(Request request);
    }
}
