

using RaioXVegano.entities.Acao;
using RaioXVegano.ibo.Acao;
using System.Web.Http;

namespace RaioXVegano.Api.Controllers
{
    public class LogController : ApiController
    {

        private readonly ISalvarInformacoesLogBO _salvarInformacoesLogAppBO;

        public LogController(ISalvarInformacoesLogBO salvarInformacoesLogAppBO) 
        {
            _salvarInformacoesLogAppBO = salvarInformacoesLogAppBO;
        }

        /// <summary>
        /// Método responsável por salvar log.
        /// </summary>
        /// <param name="request">
        /// Objeto SalvarInformacoesLogAppRequest contendo 
        /// os campos de informação do log para salvar.
        /// </param>
        /// <returns>
        /// Objeto SalvarInformacoesLogAppResponse contendo o retorno da atualização:
        ///     - Campo ListaErros preenchido caso de erro.
        ///     - Campo IsExecucaoSucesso true (sucesso) ou false (qualquer erro).
        /// </returns>
        [HttpPost]
        [ActionName("SalvarInformacoesLog")]
        public SalvarInformacoesLogResponse SalvarInformacoesLog(SalvarInformacoesLogRequest request)
        {
            return _salvarInformacoesLogAppBO.Executar(request);
        }
    }
}