using NUnit.Framework;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.testes
{
    public class CadastraProdutoBOTest : BaseBOTest
    {
        private ICadastraProdutoBO _bo;
        private CadastraProdutoRequest _request;

        public CadastraProdutoBOTest() : base(typeof(CadastraProdutoBOTest))
        {
            _bo = Container.GetInstance<ICadastraProdutoBO>();
        }

        private void PreencheRequestComSucessoProdutoVeganoNovo()
        {
            _request = new CadastraProdutoRequest()
            {
                ChaveUsuarioLogado = "ChaveUsuario1",
                Produto = PreencheProdutoVeganoNovo()
            };
        }

        [SetUp]
        public void Setup()
        {
            PreencheRequestComSucessoProdutoVeganoNovo();
        }

        [Test]
        public void ErroRequestNull()
        {
            _request = null;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroChaveUsuarioLogadoNull()
        {
            _request.ChaveUsuarioLogado = null;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroProdutoNull()
        {
            _request.Produto = null;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroCodigoBarrasEmBranco()
        {
            _request.Produto.CodigoDeBarras = string.Empty;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroNomeEmBranco()
        {
            _request.Produto.Nome = string.Empty;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.NOME_OBRIGATORIO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroMotivoEmBranco()
        {
            _request.Produto.IsVegano = false;
            _request.Produto.Motivo = string.Empty;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.MOTIVO_OBRIGATORIO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarCadastrarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_CADASTRO_PRODUTO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroInesperadoAoTentarCadastrarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO;
            CadastraProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoAoCadastrarProdutoVegano()
        {
            CadastraProdutoResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }

        [Test]
        public void SucessoAoCadastrarProdutoNaoVegano()
        {
            _request.Produto = PreencheProdutoNaoVeganoParaAtualizacao();
            CadastraProdutoResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }
    }
}