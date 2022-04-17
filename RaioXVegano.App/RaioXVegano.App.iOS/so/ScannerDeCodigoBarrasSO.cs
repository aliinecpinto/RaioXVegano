using RaioXVegano.iso.Acao;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(RaioXVegano.App.iOS.so.ScannerDeCodigoBarrasSO))]
namespace RaioXVegano.App.iOS.so
{
    public class ScannerDeCodigoBarrasSO : IScannerDeCodigoBarrasSO
    {
        public async Task<string> ScanAsync()
        {
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Aproxime a câmera do código de barra",
                BottomText = "Toque na tela para focar"
            };
            var scanResults = await scanner.Scan();
            //Fix by Ale
            return (scanResults != null) ? scanResults.Text : string.Empty;
        }
    }
}