using RaioXVegano.iso.Acao;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(RaioXVegano.App.Droid.so.ScannerDeCodigoBarrasSO))]
namespace RaioXVegano.App.Droid.so
{
    public class ScannerDeCodigoBarrasSO : IScannerDeCodigoBarrasSO
    {
        public async Task<string> ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions()
            {
                //UseFrontCameraIfAvailable = true
            };
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Aproxime a câmera do código de barra",
                BottomText = "Toque na tela para focar"
            };

            var scanResults = await scanner.Scan(optionsCustom);

            return (scanResults != null) ? scanResults.Text : string.Empty;
        }
    }
}