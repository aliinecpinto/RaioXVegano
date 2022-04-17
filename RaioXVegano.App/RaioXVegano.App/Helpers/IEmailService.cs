using System.Collections.Generic;

namespace RaioXVegano.App.Helpers
{
    public interface IEmailService
    {
        void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody, string attachmentPath, string attachmentName);
    }
}
