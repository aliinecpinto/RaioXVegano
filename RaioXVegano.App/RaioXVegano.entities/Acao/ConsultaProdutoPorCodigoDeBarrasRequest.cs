namespace RaioXVegano.entities.Acao
{
    public class ConsultaProdutoPorCodigoDeBarrasRequest : IBaseAcaoRequest
    {
        public string ChaveUsuarioLogado { get; set; }
        public string CodigoDeBarras { get; set; }
    }
}
