namespace RaioXVegano.entities.App
{
    public class ConsultaProdutoPorCodigoDeBarrasAppRequest : IBaseRequestApp
    {
        public string ChaveUsuarioLogado { get; set; }
        public string CodigoDeBarras { get; set; }
    }
}
