using System.Collections.Generic;

namespace RaioXVegano.entities.App
{
    public class ConsultaProdutoPorCodigoDeBarrasAppResponse : IBaseResponseApp
    {
        public IDictionary<string, string> Mensagens { get; set; }
        public bool IsProdutoEncontrado { get; set; }
        public bool IsProdutoSendoEditado { get; set; }
        public Produto Produto { get; set; }
    }
}
