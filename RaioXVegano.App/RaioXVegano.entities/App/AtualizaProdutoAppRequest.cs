namespace RaioXVegano.entities.App
{
    public class AtualizaProdutoAppRequest : IBaseRequestApp
    {
        public string ChaveUsuarioLogado { get; set; }
        public Produto Produto { get; set; }
    }
}
