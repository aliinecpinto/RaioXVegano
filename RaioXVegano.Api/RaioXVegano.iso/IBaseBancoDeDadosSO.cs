using RaioXVegano.entities;

namespace RaioXVegano.iso
{
    public interface IBaseBancoDeDadosSO<Request, Response>
        where Request : IBaseBancoDeDadosRequest
        where Response : IBaseBancoDeDadosResponse
    {
        Response Executar(Request request);
    }
}
