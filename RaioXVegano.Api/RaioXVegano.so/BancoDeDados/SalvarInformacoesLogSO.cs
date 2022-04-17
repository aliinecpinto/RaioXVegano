using NHibernate;
using RaioXVegano.entities;
using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.BancoDeDados;
using System;

namespace RaioXVegano.so.BancoDeDados
{
    public class SalvarInformacoesLogSO : BaseBancoDeDadosSO<SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>, ISalvarInformacoesLogSO
    {

        public SalvarInformacoesLogSO(ISession sessao) : base(typeof(SalvarInformacoesLogSO), sessao) { }

        /// <summary>
        /// Método responsável por método de inserir do banco de dados passando a entidade Log. 
        /// </summary>
        /// <param name="request">
        /// Objeto SalvarInformacoesLogAppRequest contendo o Log para ser inserido.
        /// </param>
        /// <returns>
        /// Se a inserção for realizada com sucesso retornar campo CodigoRetorno com 0 (código de sucesso), 
        /// se não retornar com 1 (erro genérico).
        /// </returns>
        protected override SalvarInformacoesLogResponse ChamaServico(SalvarInformacoesLogRequest request)
        {
            SalvarInformacoesLogResponse response = new SalvarInformacoesLogResponse();

            try
            {
                BancoDeDadosUtil.Inserir<Log>(request.Log, _sessao, _log);
                response.CodigoRetorno = CodigoRetorno.EXECUCAO_OK;
            }
            catch (Exception)
            {
                response.CodigoRetorno = CodigoRetorno.ERRO_GENERICO;
            }

            return response;
        }
    }
}
