using NHibernate;
using RaioXVegano.entities;
using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.BancoDeDados
{
    public class CadastraProdutoSO : BaseBancoDeDadosSO<CadastraProdutoRequest, CadastraProdutoResponse>, ICadastraProdutoSO
    {
        public CadastraProdutoSO(ISession sessao) : base(typeof(CadastraProdutoSO), sessao) { }

        /// <summary>
        /// Método responsável por método de inserir do banco de dados passando a entidade Produto. 
        /// </summary>
        /// <param name="request">
        /// Objeto CadastraProdutoRequest contendo o Produto para ser inserido.
        /// </param>
        /// <returns>
        /// Se a inserção for realizada com sucesso retornar campo CodigoRetorno com 0 (código de sucesso), 
        /// se não retornar com 1 (erro genérico).
        /// </returns>
        protected override CadastraProdutoResponse ChamaServico(CadastraProdutoRequest request)
        {
            CadastraProdutoResponse response = new CadastraProdutoResponse();

            try
            {
                BancoDeDadosUtil.Inserir<Produto>(request.Produto, _sessao, _log);
                response.CodigoRetorno = CodigoRetorno.EXECUCAO_OK;
            }
            catch (Exception)
            {
                response.CodigoRetorno = CodigoRetorno.ERRO_GENERICO;
            }

            return response;
        }

        /// <summary>
        /// Método responsável por trocar o base 64 do arquivo de log 
        /// por "BASE64-> VAZIO" (se estiver em branco) ou "BASE64-> PREENCHIDO" (se estiver preenchido) 
        /// para não sobrecarregar o log.
        /// </summary>
        /// <param name="request">Objeto genérico IBaseBancoDeDadosRequest para buscar o base64.</param>
        protected override void GerarLogAcaoRequest(IBaseBancoDeDadosRequest request)
        {
            CadastraProdutoRequest cpRequest = request as CadastraProdutoRequest;
            if (cpRequest.Produto != null)
            {
                string temp = cpRequest.Produto.Base64ImagemProduto;
                cpRequest.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {cpRequest.GetType().Name} : { AplicacaoUtil.GetDadosLog(cpRequest) } ");
                cpRequest.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoRequest(request);
            }
        }
    }
}
