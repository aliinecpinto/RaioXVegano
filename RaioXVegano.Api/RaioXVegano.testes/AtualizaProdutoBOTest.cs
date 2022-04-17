using NUnit.Framework;
using RaioXVegano.entities.Acao;
using RaioXVegano.entities.Enum;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.testes
{
    public class AtualizaProdutoBOTest : BaseBOTest
    {
        private IAtualizaProdutoBO _bo;
        private AtualizaProdutoRequest _request;

        public AtualizaProdutoBOTest() : base(typeof(AtualizaProdutoBOTest))
        {
            _bo = Container.GetInstance<IAtualizaProdutoBO>();
        }

        private void PreencheRequestComSucessoProdutoVeganoParaAtualizacao() 
        {
            _request = new AtualizaProdutoRequest()
            {
                ChaveUsuarioLogado = "ChaveUsuario1",
                Produto = PreencheProdutoVeganoParaAtualizacao()
            };
        }

        [SetUp]
        public void Setup()
        {
            PreencheRequestComSucessoProdutoVeganoParaAtualizacao();
        }

        [Test]
        public void ErroRequestNull()
        {
            _request = null;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroChaveUsuarioLogadoNull()
        {
            _request.ChaveUsuarioLogado = null;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroProdutoNull()
        {
            _request.Produto = null;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroIdZero()
        {
            _request.Produto.Id = 0;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroCodigoBarrasEmBranco()
        {
            _request.Produto.CodigoDeBarras = string.Empty;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroNomeEmBranco()
        {
            _request.Produto.Nome = string.Empty;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.NOME_OBRIGATORIO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroMotivoEmBranco()
        {
            _request.Produto.IsVegano = false;
            _request.Produto.Motivo = string.Empty;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.MOTIVO_OBRIGATORIO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarAtualizarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_ATUALIZAR_PRODUTO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroAoAtualizarProdutoQueEstaSendoEditado()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            _request.ChaveUsuarioLogado = "ChaveUsuario2";
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.PRODUTO_SENDO_EDITADO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroInesperadoAoTentarAtualizarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.ListaErros, (int)CodigoRetorno.ERRO_GENERICO);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoAoAtualizarProdutoQueEstaSendoEditadoParaMesmoUsuario()
        {
            _request.Produto.UsuarioEditando = "ChaveUsuario1";
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            AtualizaProdutoResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }

        [Test]
        public void SucessoAoAtualizarProdutoVegano()
        {
            AtualizaProdutoResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }

        [Test]
        public void SucessoAoAtualizarProdutoNaoVegano()
        {
            _request.Produto = PreencheProdutoNaoVeganoParaAtualizacao();
            AtualizaProdutoResponse response = _bo.Executar(_request);

            Assert.IsTrue(response.IsExecucaoSucesso);
        }
    }
}
