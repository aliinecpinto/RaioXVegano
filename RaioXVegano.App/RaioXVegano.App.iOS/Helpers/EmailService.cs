using Foundation;
using MessageUI;
using RaioXVegano.App.Helpers;
using System.Collections.Generic;
using System.IO;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(RaioXVegano.App.iOS.Helpers.EmailService))]
namespace RaioXVegano.App.iOS.Helpers
{
    public class EmailService : NSObject, IEmailService, IMFMailComposeViewControllerDelegate
    {
        public void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody, string attachmentPath, string attachmentName)
        {
            MFMailComposeViewController vc = new MFMailComposeViewController();
            vc.MailComposeDelegate = this;

            if (emailAddresses?.Count > 0)
            {
                vc.SetToRecipients(emailAddresses.ToArray());
            }

            if (ccs?.Count > 0)
            {
                vc.SetCcRecipients(ccs.ToArray());
            }

            vc.SetSubject(subject);
            vc.SetMessageBody(htmlBody, true);

            if (!string.IsNullOrEmpty(attachmentPath) && !string.IsNullOrEmpty(attachmentName))
            {
                string attachment = Path.Combine(attachmentPath, attachmentName);
                NSData data = NSData.FromFile(attachment);
                vc.AddAttachmentData(data, "application/csv", attachmentName); 
            }

            vc.Finished += (sender, e) =>
            {
                vc.DismissModalViewController(true);
            };

            UIApplication.SharedApplication.Windows[0].
                RootViewController.PresentViewController(vc, true, null);
        }
    }
}