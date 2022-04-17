using RaioXVegano.entities.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.Acao
{
    public class SalvarInformacoesLogSO : BaseAcaoLogSO<SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>, ISalvarInformacoesLogSO
    {
        public SalvarInformacoesLogSO(Uri baseEndpoint) : base(typeof(SalvarInformacoesLogSO), baseEndpoint) { }

        protected override void AjustaEndpoint()
        {
            _baseEndpoint = new Uri(_baseEndpoint, "SalvarInformacoesLog");
        }

        protected override SalvarInformacoesLogResponse ChamaServico(SalvarInformacoesLogRequest request)
        {
            return ApiClientServiceUtil<SalvarInformacoesLogRequest, SalvarInformacoesLogResponse>.Post(_baseEndpoint, request);
        }
    }
}
