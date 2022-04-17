using RaioXVegano.di;
using RaioXVegano.entities;
using RaioXVegano.Util;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace RaioXVegano.testes
{
    public abstract class BaseBOTest
    {
        protected Container Container { get; }

        public BaseBOTest(Type type)
        {
            Container = DependencyInjection.Configure();

            AplicacaoUtil.Ambiente = Consts.AMBIENTE_DEV;
        }

        protected Produto PreencheProdutoVeganoNovo()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_CADATRO_SUCESSO,
                Id = 1,
                Ingredientes = "Tudo com amor e sem crueldade",
                IsVegano = true,
                Nome = "Produto com Amor",
                Base64ImagemProduto = "Base64"
            };
        }
        
        protected Produto PreencheProdutoVeganoParaAtualizacao()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_VEGANO_ALTERACAO_SUCESSO,
                Id = 1,
                Ingredientes = "Tudo com amor e sem crueldade",
                IsVegano = true,
                Nome = "Produto com Amor",
                Base64ImagemProduto = "Base64"
            };
        }

        protected Produto PreencheProdutoNaoVeganoParaAtualizacao()
        {
            return new Produto()
            {
                CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_NAO_VEGANO_ALTERACAO_SUCESSO,
                Id = 2,
                Ingredientes = "Com crueldade =/",
                IsVegano = false,
                Nome = "Produto com Animais =/",
                Motivo = "Tem leite e ovos"
            };
        }

        protected static bool VerificaSeCodigoFoiRetornado(IList<int> listaErros, int codigoRetorno)
        {
            return listaErros.Contains(codigoRetorno);
        }
    }
}
