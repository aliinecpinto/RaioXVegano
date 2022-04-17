namespace RaioXVegano.entities.BancoDeDados
{
    public class CadastraProdutoRequest : IBaseBancoDeDadosRequest
    {
        public string ChaveUsuarioLogado { get; set; }
        public Produto Produto { get; set; }
    }
}
