using Android.Content;
using Android.Support.V4.Content;
using Android.Text;
using RaioXVegano.App.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(RaioXVegano.App.Droid.Helpers.EmailService))]
namespace RaioXVegano.App.Droid.Helpers
{
    public class EmailService : IEmailService
    {
        public void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody, string attachmentPath, string attachmentName)
        {
            Intent email = new Intent(Android.Content.Intent.ActionSend);

            if (emailAddresses != null && emailAddresses.Any())
            {
                email.PutExtra(Android.Content.Intent.ExtraEmail, emailAddresses.ToArray());
            }

            if (ccs != null && ccs.Any())
            {
                email.PutExtra(Android.Content.Intent.ExtraCc, ccs.ToArray());
            }

            email.PutExtra(Android.Content.Intent.ExtraSubject, subject);

            email.PutExtra(Android.Content.Intent.ExtraText, Html.FromHtml(body));

            email.PutExtra(Android.Content.Intent.ExtraHtmlText, htmlBody);

            if (!string.IsNullOrEmpty(attachmentPath) && !string.IsNullOrEmpty(attachmentName))
            {
                string attachment = Path.Combine(attachmentPath, attachmentName);
                Java.IO.File file = new Java.IO.File(attachment);
                Android.Net.Uri uri = FileProvider.GetUriForFile(MainActivity.Instance.BaseContext, MainActivity.Instance.BaseContext.ApplicationContext.PackageName + ".fileprovider", file);
                email.PutExtra(Android.Content.Intent.ExtraStream, uri); 
            }
            
            email.SetType("message/rfc822");

            MainActivity.Instance.StartActivity(email);
        }
    }
}