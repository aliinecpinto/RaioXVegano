using Android.OS;
using RaioXVegano.App.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(RaioXVegano.App.Droid.Helpers.ExternalStorage))]
namespace RaioXVegano.App.Droid.Helpers
{
    public class ExternalStorage : IExternalStorage
    {
        public string GetExternalStorage()
        {
            return Environment.ExternalStorageDirectory.AbsolutePath + "/RaioXVegano";
        }
    }
}