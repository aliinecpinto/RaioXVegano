using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaioXVegano.bo.Acao
{
    public class SalvarInformacoesLogBO : BaseAcaoBO<SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>, ISalvarInformacoesLogBO
    {

        private readonly ISalvarInformacoesLogSO _so;

        public SalvarInformacoesLogBO(ISalvarInformacoesLogSO so) : base(typeof(SalvarInformacoesLogBO)) 
        {
            _so = so;
        }

        /// <summary>
        /// Método responsável por validar os campos para salvar no banco informações do log da seguinte forma:
        ///     - Campos obrigatórios:
        ///         - Data: retorna código 14
        ///         - Parametro1: retorna código 15
        ///         - Parametro2: retorna código 16
        /// </summary>
        /// <param name="request">Objeto do tipo SalvarInformacoesLogAppRequest contendo os atributos para validação</param>
        protected override void Valida(SalvarInformacoesLogRequest request)
        {
            IList<int> listaErros = new List<int>();

            if (request.Data.Equals(DateTime.MinValue))
            {
                _log.Error($"Código Erro {CodigoRetorno.DATA_LOG_OBRIGATORIO}");
                listaErros.AddIfDoesntExists((int)CodigoRetorno.ERRO_GENERICO);
            }

            if (string.IsNullOrEmpty(request.Parametro1))
            {
                _log.Error($"Código Erro {CodigoRetorno.PARAMETRO_1_OBRIGATORIO}");
                listaErros.AddIfDoesntExists((int)CodigoRetorno.ERRO_GENERICO);
            }

            if (string.IsNullOrEmpty(request.Parametro2))
            {
                _log.Error($"Código Erro {CodigoRetorno.PARAMETRO_2_OBRIGATORIO}");
                listaErros.AddIfDoesntExists((int)CodigoRetorno.ERRO_GENERICO);
            }

            if (listaErros.Any())
            {
                throw new ValicacaoException(listaErros);
            }
        }

        /// <summary>
        /// Método responsável por:
        ///     1) Chamar o método AjustaRequestBancoDeDados para preparar o request para o serviço.
        ///     2) Chamar o SO de salvar log.
        /// </summary>
        /// <param name="request">
        /// Objeto do tipo SalvarInformacoesLogAppRequest contendo objeto de log para salvar.
        /// </param>
        /// <returns>
        /// Verificar se o response retornado do SO está com o CodigoRetorno == 1 (erro genérico):
        ///     - Se estiver retorna o campo IsExecucaoSucesso = false e a ListaErros com 
        ///       código 17 (Erro genérico de salvar log).
        ///     - Se não estiver retorna o campo IsExecucaoSucesso = true.
        /// </returns>
        protected override SalvarInformacoesLogResponse ChamaServico(SalvarInformacoesLogRequest request)
        {
            SalvarInformacoesLogResponse response = new SalvarInformacoesLogResponse() { IsExecucaoSucesso = true };

            entities.BancoDeDados.SalvarInformacoesLogRequest bdRequest = AjustaRequestBancoDeDados(request);

            entities.BancoDeDados.SalvarInformacoesLogResponse bdResponse = _so.Executar(bdRequest);

            if (CodigoRetorno.ERRO_GENERICO == bdResponse.CodigoRetorno)
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_SALVAR_LOG } };
            }

            return response;

        }

        /// <summary>
        /// Método responsável por montar o SalvarInformacoesLogAppRequest do banco de dados atráves do 
        /// SalvarInformacoesLogAppRequest que chegou do aplicativo.
        /// </summary>
        /// <param name="request">Objeto do tipo SalvarInformacoesLogAppRequest da Acao com informações que chegaram do aplicativo</param>
        /// <returns>SalvarInformacoesLogAppRequest do banco de dados com as informações convertidas do SalvarInformacoesLogAppRequest da Acao.</returns>
        private entities.BancoDeDados.SalvarInformacoesLogRequest AjustaRequestBancoDeDados(SalvarInformacoesLogRequest request)
        {
            return new entities.BancoDeDados.SalvarInformacoesLogRequest() 
            {
                Log = new entities.Log() 
                { 
                    ChaveUsuarioLogado = request.ChaveUsuarioLogado, 
                    Data = request.Data, 
                    Parametro1 = request.Parametro1, 
                    Parametro2 = request.Parametro2 
                }
            };
        }
    }
}
