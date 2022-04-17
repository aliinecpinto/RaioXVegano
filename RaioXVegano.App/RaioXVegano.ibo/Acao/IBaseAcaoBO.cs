using RaioXVegano.entities.App;

namespace RaioXVegano.ibo.Acao
{
    public interface IBaseAcaoBO<RequestApp, ResponseApp, RequestAcao, ResponseAcao>
        where RequestApp : IBaseRequestApp
        where ResponseApp : IBaseResponseApp
    {
        ResponseApp Executar(RequestApp requestApp);
    }
}
