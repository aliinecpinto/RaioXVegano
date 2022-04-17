using NUnit.Framework;
using RaioXVegano.entities.App;
using RaioXVegano.entities.Enum;
using RaioXVegano.entities.MapMensagens;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;

namespace RaioXVegano.testes
{
    public class CadastraProdutoBOTest : BaseBOTest
    {
        private ICadastraProdutoBO _bo;
        private CadastraProdutoAppRequest _request;
        private MapCampoTelaMsgProduto _mapMensagens;

        public CadastraProdutoBOTest() : base(typeof(CadastraProdutoBOTest))
        {
            _bo = Container.GetInstance<ICadastraProdutoBO>();
            _mapMensagens = MapCampoTelaMsgProduto.Instancia;
        }

        private void PreencheRequestComSucessoProdutoVeganoNovo()
        {
            _request = new CadastraProdutoAppRequest()
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
        public void ErroProdutoNull()
        {
            _request.Produto = null;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroCodigoBarrasEmBranco()
        {
            _request.Produto.CodigoDeBarras = string.Empty;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroChaveUsuarioLogadoEmBranco()
        {
            _request.ChaveUsuarioLogado = string.Empty;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroNomeEmBranco()
        {
            _request.Produto.Nome = string.Empty;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.NOME_OBRIGATORIO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroTipoProdutoEmBranco()
        {
            _request.Produto.IsVegano = null;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.TIPO_PRODUTO_OBRIGATORIO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroMotivoEmBranco()
        {
            _request.Produto.IsVegano = false;
            _request.Produto.Motivo = string.Empty;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.MOTIVO_OBRIGATORIO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroBase64ImagemProdutoEmBranco()
        {
            _request.Produto.Base64ImagemProduto = string.Empty;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroGenericoAoTentarCadastrarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_INVALIDO;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void ErroInesperadoAoTentarCadastrarProduto()
        {
            _request.Produto.CodigoDeBarras = Consts.TESTE_CODIGO_BARRAS_PRODUTO_ERRO_NAO_ESPERADO;
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            bool sucesso = VerificaSeCodigoFoiRetornado(response.Mensagens, _mapMensagens.MapCampoProduto[(int)CodigoRetorno.ERRO_GENERICO]);
            Assert.IsTrue(sucesso);
        }

        [Test]
        public void SucessoAoCadastrarProdutoVegano()
        {
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
        }

        [Test]
        public void SucessoAoCadastrarProdutoNaoVegano()
        {
            _request.Produto = PreencheProdutoNaoVegano();
            CadastraProdutoAppResponse response = _bo.Executar(_request);

            Assert.IsNull(response.Mensagens);
        }
    }
}
