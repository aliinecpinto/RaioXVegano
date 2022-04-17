using System;
using System.IO;
using Xamarin.Forms;

namespace RaioXVegano.Util
{
    public static class ImagemUtil
    {
        public static ImageSource ConverteBase64ToImageSource(string base64, out byte[] byteArray)
        {
            byteArray = Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(byteArray);
            return ImageSource.FromStream(() => stream);
        }
    }
}
