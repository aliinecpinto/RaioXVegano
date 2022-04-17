using RaioXVegano.entities.Acao;
using RaioXVegano.ibo.Acao;
using System.Web.Http;

namespace RaioXVegano.Api.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly IAtualizaProdutoBO _atualizaProdutoBO;
        private readonly ICadastraProdutoBO _cadastraProdutoBO;
        private readonly IConsultaProdutoPorCodigoDeBarrasBO _consultaProdutoPorCodigoDeBarrasBO;

        public ProdutoController(IAtualizaProdutoBO atualizaProdutoBO, ICadastraProdutoBO cadastraProdutoBO, IConsultaProdutoPorCodigoDeBarrasBO consultaProdutoPorCodigoDeBarrasBO) 
        {
            _atualizaProdutoBO = atualizaProdutoBO;
            _cadastraProdutoBO = cadastraProdutoBO;
            _consultaProdutoPorCodigoDeBarrasBO = consultaProdutoPorCodigoDeBarrasBO;
        }

        /// <summary>
        /// Método responsável por consultar produto.
        /// </summary>
        /// <param name="request">Objeto ConsultaProdutoPorCodigoDeBarrasRequest contendo os campos de chave e código de barras.</param>
        /// <returns>
        /// Objeto ConsultaProdutoPorCodigoDeBarrasResponse contendo o retorno da atualização:
        ///     - Campo ListaErros preenchido caso de erro.
        ///     - Campo IsExecucaoSucesso true (sucesso) ou false (qualquer erro).
        ///     - Objeto de Produto preenchido ou vazio.
        /// </returns>
        [HttpPost]
        [ActionName("ConsultaProdutoPorCodigoDeBarras")]
        public ConsultaProdutoPorCodigoDeBarrasResponse ConsultaProdutoPorCodigoDeBarras([FromBody] ConsultaProdutoPorCodigoDeBarrasRequest request)
        {
            return _consultaProdutoPorCodigoDeBarrasBO.Executar(request);
        }

        /// <summary>
        /// Método responsável por cadastrar produto.
        /// </summary>
        /// <param name="request">Objeto CadastraProdutoRequest contendo os campos de produto para atualização.</param>
        /// <returns>
        /// Objeto CadastraProdutoResponse contendo o retorno da atualização:
        ///     - Campo ListaErros preenchido caso de erro.
        ///     - Campo IsExecucaoSucesso true (sucesso) ou false (qualquer erro).
        /// </returns>
        [HttpPost]
        [ActionName("CadastraProduto")]
        public CadastraProdutoResponse CadastraProduto([FromBody]CadastraProdutoRequest request)
        {
            return _cadastraProdutoBO.Executar(request);
        }

        /// <summary>
        /// Método responsável por atualizar o produto.
        /// </summary>
        /// <param name="request">Objeto AtualizaProdutoRequest contendo os campos de produto para atualização.</param>
        /// <returns>
        /// Objeto AtualizaProdutoResponse contendo o retorno da atualização:
        ///     - Campo ListaErros preenchido caso de erro.
        ///     - Campo IsExecucaoSucesso true (sucesso) ou false (qualquer erro).
        /// </returns>
        [HttpPost]
        [ActionName("AtualizaProduto")]
        public AtualizaProdutoResponse AtualizaProduto([FromBody] AtualizaProdutoRequest request)
        {
            return _atualizaProdutoBO.Executar(request);
        }
    }
}
