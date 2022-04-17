namespace RaioXVegano.entities.App
{
    public class CadastraProdutoAppRequest : IBaseRequestApp
    {
        public string ChaveUsuarioLogado { get; set; }
        public Produto Produto { get; set; }
    }
}
