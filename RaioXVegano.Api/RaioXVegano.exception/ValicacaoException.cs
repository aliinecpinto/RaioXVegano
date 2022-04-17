using System;
using System.Collections.Generic;

namespace RaioXVegano.exception
{
    public class ValicacaoException : Exception
    {
        public IList<int> ListaErros { get; set; }

        public ValicacaoException(int codigoErro)
        {
            ListaErros = new List<int>() { { codigoErro } };
        }

        public ValicacaoException(IList<int> listaErros)
        {
            ListaErros = listaErros;
        }
    }
}
