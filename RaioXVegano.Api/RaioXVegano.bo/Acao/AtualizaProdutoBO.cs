using RaioXVegano.entities;
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
    public class AtualizaProdutoBO : BaseAcaoBO<AtualizaProdutoRequest, AtualizaProdutoResponse>, IAtualizaProdutoBO
    {
        private IAtualizaProdutoSO _so;

        public AtualizaProdutoBO(IAtualizaProdutoSO so) : base(typeof(AtualizaProdutoBO))
        {
            _so = so;
        }

        /// <summary>
        /// Método responsável por validar o produto para atualização da seguinte forma:
        ///     - Se a classe de produto do request estiver null, retornar código 1 (erro genérico)
        ///     - Campos obrigatórios:
        ///         - Id retorna código 1 (erro genérico pois esse campo é interno. Não é exibido em tela)
        ///         - CodigoDeBarras retorna código 6.
        ///         - Nome retorna código 7.
        ///         - Motivo retorna código 8 (Apenas se o produto não for vegano).
        /// </summary>
        /// <param name="request">Objeto do tipo AtualizaProdutoRequest contendo objeto de produto para validação</param>
        protected override void Valida(AtualizaProdutoRequest request)
        {
            if (request.Produto == null)
            {
                _log.Error($"Código Erro {CodigoRetorno.PRODUTO_OBRIGATORIO}");
                throw new ValicacaoException((int)CodigoRetorno.ERRO_GENERICO);
            }
            else
            {
                IList<int> listaErros = new List<int>();

                if (request.Produto.Id == 0)
                {
                    _log.Error($"Código Erro {CodigoRetorno.ID_PRODUTO_OBRIGATORIO}");
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.ERRO_GENERICO);
                }

                if (string.IsNullOrEmpty(request.Produto.CodigoDeBarras))
                {
                    _log.Error($"Código Erro {CodigoRetorno.CODIGO_BARRAS_OBRIGATORIO}");
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.ERRO_GENERICO);
                }

                if (string.IsNullOrEmpty(request.Produto.Nome))
                {
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.NOME_OBRIGATORIO);
                }

                if (!request.Produto.IsVegano && string.IsNullOrEmpty(request.Produto.Motivo))
                {
                    listaErros.AddIfDoesntExists((int)CodigoRetorno.MOTIVO_OBRIGATORIO);
                }

                if (listaErros.Any())
                {
                    throw new ValicacaoException(listaErros);
                }
            }
        }

        /// <summary>
        /// Método responsável por:
        ///     1) Chamar o método AjustaProdutoParaAtualizacao para preparar o request para o serviço.
        ///     2) Chamar o SO de atualização do produto.
        /// </summary>
        /// <param name="request">
        /// Objeto do tipo AtualizaProdutoRequest contendo objeto de produto para atualização.
        /// </param>
        /// <returns>
        /// Se o SO retornar ProdutoSendoEditadoException retornar o campo IsExecucaoSucesso = false e a ListaErros com código 9 (Produto sendo editado).
        /// Verificar se o response retornado do SO está com o CodigoRetorno == 1 (erro genérico):
        ///     - Se estiver retorna o campo IsExecucaoSucesso = false e a ListaErros com 
        ///       código 12 (Erro genérico de atualização de produto).
        ///     - Se não estiver retorna o campo IsExecucaoSucesso = true.
        /// </returns>
        protected override AtualizaProdutoResponse ChamaServico(AtualizaProdutoRequest request)
        {
            AtualizaProdutoResponse response = new AtualizaProdutoResponse() { IsExecucaoSucesso = true };

            try
            {
                entities.BancoDeDados.AtualizaProdutoRequest bdRequest = AjustaProdutoParaAtualizacao(request);

                entities.BancoDeDados.AtualizaProdutoResponse bdResponse = _so.Executar(bdRequest);

                if (CodigoRetorno.ERRO_GENERICO == bdResponse.CodigoRetorno)
                {
                    response.IsExecucaoSucesso = false;
                    response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_ATUALIZAR_PRODUTO } };
                }
            }
            catch (ProdutoSendoEditadoException)
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.PRODUTO_SENDO_EDITADO } };
            }

            return response;
        }

        /// <summary>
        /// Método responsável por montar o AtualizaProdutoRequest do banco de dados através do 
        /// AtualizaProdutoRequest que chegou do aplicativo.
        /// </summary>
        /// <param name="request">
        /// Objeto do tipo AtualizaProdutoRequest da Acao com as informações que 
        /// chegaram do aplicativo
        /// </param>
        /// <returns>
        /// AtualizaProdutoRequest do banco de dados com as informações convertidas do 
        /// AtualizaProdutoRequest da Acao.
        /// </returns>
        private static entities.BancoDeDados.AtualizaProdutoRequest AjustaProdutoParaAtualizacao(AtualizaProdutoRequest request)
        {
            request.Produto.DataAtualizacao = !string.IsNullOrEmpty(request.Produto.UsuarioEditando) ? DateTime.Now : (DateTime?)null;

            entities.BancoDeDados.AtualizaProdutoRequest bdRequest = new entities.BancoDeDados.AtualizaProdutoRequest()
            {
                ChaveUsuarioLogado = request.ChaveUsuarioLogado,
                Produto = request.Produto
            };

            return bdRequest;
        }

        /// <summary>
        /// Método responsável por trocar o base 64 do arquivo de log 
        /// por "BASE64-> VAZIO" (se estiver em branco) ou "BASE64-> PREENCHIDO" (se estiver preenchido) 
        /// para não sobrecarregar o log.
        /// </summary>
        /// <param name="request">Objeto genérico IBaseAcaoRequest para buscar o base64.</param>
        protected override void GerarLogAcaoRequest(IBaseAcaoRequest request)
        {
            AtualizaProdutoRequest apRequest = request as AtualizaProdutoRequest;
            if (apRequest?.Produto != null)
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
