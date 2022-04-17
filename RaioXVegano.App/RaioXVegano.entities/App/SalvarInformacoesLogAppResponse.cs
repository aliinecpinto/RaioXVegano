using System.Collections.Generic;

namespace RaioXVegano.entities.App
{
    public class SalvarInformacoesLogAppResponse : IBaseResponseApp
    {
        public IDictionary<string, string> Mensagens { get; set; }
    }
}
