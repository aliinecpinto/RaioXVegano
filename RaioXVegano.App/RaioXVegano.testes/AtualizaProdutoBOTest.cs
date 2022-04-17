using NUnit.Framework;
using RaioXVegano.entities.App;
using RaioXVegano.entities.Enum;
using RaioXVegano.entities.MapMensagens;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.testes
{
    public class AtualizaProdutoBOTest : BaseBOTest
    {
        private IAtualizaProdutoBO _bo;
        private AtualizaProdutoAppRequest _request;
        private MapCampoTelaMsgProduto _mapMensagens;

        public AtualizaProdutoBOTest() : base(typeof(AtualizaProdutoBOTest))
        {
            _bo = Container.GetInstance<IAtualizaProdutoBO>();
            _mapMensagens = MapCampoTelaMsgProduto.Instancia;
        }

        private void PreencheRequestComSucessoProdutoVeganoParaAtualizacao()
        {
            _request = new AtualizaProdutoAppRequest()
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
        public void ErroProdutoNull()
        {
            _request.Produto = null;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroChaveUsuarioLogadoEmBranco()
        {
            _request.ChaveUsuarioLogado = string.Empty;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroCodigoBarrasEmBranco()
        {
            _request.Produto.CodigoDeBarras = string.Empty;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroNomeEmBranco()
        {
            _request.Produto.Nome = string.Empty;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.NOME_OBRIGATORIO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroTipoProdutoEmBranco()
        {
            _request.Produto.IsVegano = null;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.TIPO_PRODUTO_OBRIGATORIO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroMotivoEmBranco()
        {
            _request.Produto.IsVegano = false;
            _request.Produto.Motivo = string.Empty;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.MOTIVO_OBRIGATORIO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroBase64ImagemProdutoEmBranco()
        {
            _request.Produto.Base64ImagemProduto = string.Empty;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarAtualizarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroInesperadoAoTentarAtualizarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroAoAtualizarProdutoQueEstaSendoEditado()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            _request.ChaveUsuarioLogado = "ChaveUsuario2";
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.PRODUTO_SENDO_EDITADO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoAoAtualizarProdutoQueEstaSendoEditadoParaMesmoUsuario()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_SENDO_EDITADO;
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
        }

        [Test]
        public void SucessoAoAtualizarProdutoVegano()
        {
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
        }

        [Test]
        public void SucessoAoAtualizarProdutoNaoVegano()
        {
            _request.Produto = PreencheProdutoNaoVegano();
            AtualizaProdutoAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
        }
    }
}
