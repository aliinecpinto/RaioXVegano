using NHibernate;
using RaioXVegano.entities;
using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.exception;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System.Linq;

namespace RaioXVegano.so.BancoDeDados
{
    public class ConsultaProdutoPorCodigoDeBarrasSO : BaseBancoDeDadosSO<ConsultaProdutoPorCodigoDeBarrasRequest, ConsultaProdutoPorCodigoDeBarrasResponse>, IConsultaProdutoPorCodigoDeBarrasSO
    {

        public ConsultaProdutoPorCodigoDeBarrasSO(ISession sessao) : base(typeof(ConsultaProdutoPorCodigoDeBarrasSO), sessao) { }

        /// <summary>
        /// Método responsável por:
        ///     - Buscar o produto do banco de dados através do código de barras.
        ///     - Verificar se tem alguém editando o produto.
        ///         - Se sim, retornar ProdutoSendoEditadoException.
        ///         - Se não, retornar o produto preenchido.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override ConsultaProdutoPorCodigoDeBarrasResponse ChamaServico(ConsultaProdutoPorCodigoDeBarrasRequest request)
        {
            ConsultaProdutoPorCodigoDeBarrasResponse response = new ConsultaProdutoPorCodigoDeBarrasResponse();

            Produto produtoRetornado = _sessao.Query<Produto>().FirstOrDefault(p => request.CodigoDeBarras.Equals(p.CodigoDeBarras));

            if (!string.IsNullOrEmpty(produtoRetornado?.UsuarioEditando) && !produtoRetornado.UsuarioEditando.Equals(request.ChaveUsuarioLogado))
            {
                throw new ProdutoSendoEditadoException();
            }

            response.Produto = produtoRetornado;

            return response;
        }

        /// <summary>
        /// Método responsável por trocar o base 64 do arquivo de log 
        /// por "BASE64-> VAZIO" (se estiver em branco) ou "BASE64-> PREENCHIDO" (se estiver preenchido) 
        /// para não sobrecarregar o log.
        /// </summary>
        /// <param name="request">Objeto genérico IBaseBancoDeDadosRequest para buscar o base64.</param>
        protected override void GerarLogAcaoResponse(IBaseBancoDeDadosResponse response)
        {
            ConsultaProdutoPorCodigoDeBarrasResponse cppcdbResponse = response as ConsultaProdutoPorCodigoDeBarrasResponse;
            if (cppcdbResponse.Produto != null)
            {
                string temp = cppcdbResponse.Produto.Base64ImagemProduto;
                cppcdbResponse.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cppcdbResponse.GetType().Name} : { AplicacaoUtil.GetDadosLog(cppcdbResponse) } ");
                cppcdbResponse.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoResponse(response);
            }
        }
    }
}
