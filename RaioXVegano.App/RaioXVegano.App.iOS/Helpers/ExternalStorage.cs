using RaioXVegano.App.Helpers;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(RaioXVegano.App.iOS.Helpers.ExternalStorage))]
namespace RaioXVegano.App.iOS.Helpers
{
    public class ExternalStorage : IExternalStorage
    {
        public string GetExternalStorage()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "RaioXVegano";
        }
    }
}