using System;
using System.Collections.Generic;

namespace RaioXVegano.exception
{
    public class ValidacaoFormException : Exception
    {
        public IDictionary<string, string> Mensagens { get; set; }

        public ValidacaoFormException(IDictionary<string, string> mensagens) : base(mensagens.ToString()) 
        {
            Mensagens = mensagens;
        }
    }
}
