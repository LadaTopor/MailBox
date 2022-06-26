using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBox
{
    internal class Message
    {
        string mail = Mail.authorization;

        public string Header { get; set; }
        public string MessageText { get; set; }
        public string To_mail { get; set; }
        public DateTime Date { get; set; }
        public string From_mail
        {
            get
            {
                return mail;
            }
            set 
            {
                if (value == null) throw new Exception("Вы не авторизоаны.");
            }
        }
        public Message(string header, string message, string to_mail)
        {
            From_mail = mail;
            To_mail = to_mail;
            Header = header;
            MessageText = message;
            Date = DateTime.Now;
        }
    }
}
