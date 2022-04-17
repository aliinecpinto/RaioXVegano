namespace RaioXVegano.entities.BancoDeDados
{
    public class AtualizaProdutoRequest : IBaseBancoDeDadosRequest
    {
        public string ChaveUsuarioLogado { get; set; }
        public Produto Produto { get; set; }
    }
}
