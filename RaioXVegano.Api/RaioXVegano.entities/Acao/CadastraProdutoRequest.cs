namespace RaioXVegano.entities.Acao
{
    public class CadastraProdutoRequest : IBaseAcaoRequest
    {
        public string ChaveUsuarioLogado { get; set; }
        public Produto Produto { get; set; }
    }
}
