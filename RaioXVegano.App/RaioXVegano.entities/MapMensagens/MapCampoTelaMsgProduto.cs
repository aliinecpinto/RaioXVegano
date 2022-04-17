using RaioXVegano.entities.Enum;
using RaioXVegano.Util;
using System.Collections.Generic;

namespace RaioXVegano.entities.MapMensagens
{
    public class MapCampoTelaMsgProduto
    {
        private static MapCampoTelaMsgProduto _instancia;

        public static MapCampoTelaMsgProduto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new MapCampoTelaMsgProduto();
                }

                return _instancia;
            }
        }

        private IDictionary<int, string> _mapCampoProduto;

        public IDictionary<int, string> MapCampoProduto
        {
            get
            {
                if (_mapCampoProduto == null)
                {
                    PreencheMapCampoProduto();
                }

                return _mapCampoProduto;
            }
        }

        private void PreencheMapCampoProduto()
        {
            _mapCampoProduto = new Dictionary<int, string>()
            {
                { (int)CodigoRetorno.ERRO_GENERICO, Consts.ERRO_GENERICO },
                { (int)CodigoRetorno.NOME_OBRIGATORIO, Consts.NOME_PRODUTO },
                { (int)CodigoRetorno.TIPO_PRODUTO_OBRIGATORIO, Consts.TIPO_PRODUTO },
                { (int)CodigoRetorno.MOTIVO_OBRIGATORIO, Consts.MOTIVO },
                { (int)CodigoRetorno.PRODUTO_SENDO_EDITADO, Consts.ERRO_PRODUTO_SENDO_EDITADO }
            };
        }
    }
}
