namespace RaioXVegano.entities.Acao
{
    public class AtualizaProdutoRequest : IBaseAcaoRequest
    {
        public string ChaveUsuarioLogado { get; set; }
        public Produto Produto { get; set; }
    }
}
