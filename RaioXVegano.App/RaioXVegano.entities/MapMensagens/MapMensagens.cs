using RaioXVegano.entities.Enum;
using RaioXVegano.entities.Properties;
using System.Collections.Generic;

namespace RaioXVegano.entities.MapMensagens
{
    public class MapMensagens
    {
        private static MapMensagens _instancia;

        public static MapMensagens Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new MapMensagens();
                }

                return _instancia;
            }
        }

        private IDictionary<int, string> _mapMensagensApp;

        public IDictionary<int, string> MapMensagensApp
        {
            get
            {
                if (_mapMensagensApp == null)
                {
                    PreencheMapMensagens();
                }

                return _mapMensagensApp;
            }
        }

        private void PreencheMapMensagens()
        {
            _mapMensagensApp = new Dictionary<int, string>()
            {
                { (int)CodigoRetorno.ERRO_GENERICO, string.Format(Resources.erroGenerico, Resources.emailRaioXVegano)},
                { (int)CodigoRetorno.NOME_OBRIGATORIO, Resources.campoObrigatorio},
                { (int)CodigoRetorno.MOTIVO_OBRIGATORIO, Resources.campoObrigatorio},
                { (int)CodigoRetorno.PRODUTO_SENDO_EDITADO, Resources.mensagemProdutoSendoEditado},
                { (int)CodigoRetorno.ERRO_CADASTRO_PRODUTO, string.Format(Resources.erroCadastrarProduto, Resources.emailRaioXVegano)},
                { (int)CodigoRetorno.ERRO_ATUALIZAR_PRODUTO, string.Format(Resources.erroAtualizarProduto, Resources.emailRaioXVegano)},
                { (int)CodigoRetorno.ERRO_CONSULTAR_PRODUTO, string.Format(Resources.erroConsultaProduto, Resources.emailRaioXVegano)},
                { (int)CodigoRetorno.ERRO_SALVAR_LOG, string.Format(Resources.erroSalvarLog, Resources.emailRaioXVegano)}
            };
        }
    }
}
