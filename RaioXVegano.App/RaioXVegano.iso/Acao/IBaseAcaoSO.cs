namespace RaioXVegano.iso.Acao
{
    public interface IBaseAcaoSO<Request, Response>
    {
        Response Executa(Request request);
    }
}
