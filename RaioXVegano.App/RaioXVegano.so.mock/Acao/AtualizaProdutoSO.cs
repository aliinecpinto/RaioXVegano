using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;

namespace RaioXVegano.so.mock.Acao
{
    public class AtualizaProdutoSO : BaseAcaoSO<AtualizaProdutoRequest, AtualizaProdutoResponse>, IAtualizaProdutoSO
    {
        public override AtualizaProdutoResponse Executa(AtualizaProdutoRequest request)
        {
            AtualizaProdutoResponse response = new AtualizaProdutoResponse() { IsExecucaoSucesso = true };

            if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO.Equals(request.Produto.CodigoDeBarras) && !"ChaveUsuario1".Equals(request.ChaveUsuarioLogado))
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.PRODUTO_SENDO_EDITADO } };
            }
            else if (Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO.Equals(request.Produto.CodigoDeBarras))
            {
                throw new Exception();
            }
            else if (!Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO.Equals(request.Produto.CodigoDeBarras) && 
                !Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO.Equals(request.Produto.CodigoDeBarras) &&
                !Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO.Equals(request.Produto.CodigoDeBarras))
            {
                response.IsExecucaoSucesso = false;
                response.ListaErros = new List<int>() { { (int)CodigoRetorno.ERRO_GENERICO } };
            }

            return response;
        }
    }
}
