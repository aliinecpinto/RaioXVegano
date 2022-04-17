using System;

namespace RaioXVegano.entities.Acao
{
    public class SalvarInformacoesLogRequest : IBaseAcaoRequest
    {
        public string ChaveUsuarioLogado { get; set; }
        public DateTime Data { get; set; }
        public string Parametro1 { get; set; }
        public string Parametro2 { get; set; }
    }
}
