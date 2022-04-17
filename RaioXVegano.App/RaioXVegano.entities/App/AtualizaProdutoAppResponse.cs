using System.Collections.Generic;

namespace RaioXVegano.entities.App
{
    public class AtualizaProdutoAppResponse : IBaseResponseApp
    {
        public IDictionary<string, string> Mensagens { get; set; }
    }
}
