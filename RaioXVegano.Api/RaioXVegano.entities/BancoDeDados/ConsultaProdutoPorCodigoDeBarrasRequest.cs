namespace RaioXVegano.entities.BancoDeDados
{
    public class ConsultaProdutoPorCodigoDeBarrasRequest : IBaseBancoDeDadosRequest
    {
        public string CodigoDeBarras { get; set; }
        public string ChaveUsuarioLogado { get; set; }
    }
}
