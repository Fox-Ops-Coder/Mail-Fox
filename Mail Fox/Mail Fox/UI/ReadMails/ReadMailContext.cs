using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailFox.UI.ReadMails
{
    internal class ReadMailContext
    {
        private readonly string from;
        public string From => from;

        private readonly string to;
        public string To => to;

        private readonly string mailTheme;
        public string MailTheme => mailTheme;

        private readonly string mailText;
        public string MailText => mailText;

        private readonly IEnumerable<MimeEntity> attachments;

        public ReadMailContext(MimeMessage message)
        {
            from = message.From.ToString();
            to = message.To.ToString();
            mailTheme = message.Subject;
            mailText = message.GetTextBody(MimeKit.Text.TextFormat.Text);
            attachments = message.Attachments;
        }
    }
}
