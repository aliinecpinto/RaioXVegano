namespace RaioXVegano.entities.App
{
    public class SalvarInformacoesLogAppRequest : IBaseRequestApp
    {
        public string ChaveUsuarioLogado { get; set; }
        public string Parametro1 { get; set; }
        public string Parametro2 { get; set; }
    }
}
