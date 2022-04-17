using NHibernate;
using RaioXVegano.entities;
using RaioXVegano.entities.BancoDeDados;
using RaioXVegano.entities.Enum;
using RaioXVegano.exception;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.BancoDeDados
{
    public class AtualizaProdutoSO : BaseBancoDeDadosSO<AtualizaProdutoRequest, AtualizaProdutoResponse>, IAtualizaProdutoSO
    {
        public AtualizaProdutoSO(ISession sessao) : base(typeof(AtualizaProdutoSO), sessao) { }

        /// <summary>
        /// Método responsável por:
        ///     - Buscar o produto do banco de dados através do ID.
        ///     - Verificar se tem alguém editando o produto.
        ///         - Se sim, retornar ProdutoSendoEditadoException.
        ///         - Se não, chamar o método de atualização do banco de dados passando a 
        ///           entidade Produto. Se a atualização for realizada com sucesso retornar 
        ///           campo CodigoRetorno com 0 (código de sucesso), se não retornar com 1 (erro genérico).
        /// </summary>
        /// <param name="request">
        /// Objeto AtualizaProdutoRequest contendo o Produto para ser editado.
        /// </param>
        /// <returns></returns>
        protected override AtualizaProdutoResponse ChamaServico(AtualizaProdutoRequest request)
        {
            AtualizaProdutoResponse response = new AtualizaProdutoResponse();

            Produto produtoAtualizado = BancoDeDadosUtil.ListarPorId<Produto>(request.Produto.Id, _sessao, _log);

            if (!string.IsNullOrEmpty(produtoAtualizado.UsuarioEditando) && !produtoAtualizado.UsuarioEditando.Equals(request.ChaveUsuarioLogado))
            {
                throw new ProdutoSendoEditadoException();
            }

            try
            {
                BancoDeDadosUtil.Atualizar<Produto>(request.Produto, _sessao, _log);

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
            AtualizaProdutoRequest apRequest = request as AtualizaProdutoRequest;
            if (apRequest.Produto != null)
            {
                string temp = apRequest.Produto.Base64ImagemProduto;
                apRequest.Produto.Base64ImagemProduto = string.IsNullOrWhiteSpace(temp) ? "BASE64-> VAZIO" : "BASE64-> PREENCHIDO";
                _log.Info($" dados {apRequest.GetType().Name} : { AplicacaoUtil.GetDadosLog(apRequest) } ");
                apRequest.Produto.Base64ImagemProduto = temp;
            }
            else
            {
                base.GerarLogAcaoRequest(request);
            }
        }
    }
}
