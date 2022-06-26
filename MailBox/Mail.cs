using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MailBox
{
    internal class Mail
    {
        public static string authorization;
        private string mail;
        private string password;
        private string domain;

        public string Mail_address
        {
            get
            {
                return mail + domain;
            }
            set 
            {
                if (value == null) { throw new Exception("Вы не ввели имя почтового ящика!"); }

                int dog_count = 0;
                foreach (char c in value)
                {
                    if (c == '@') dog_count++;
                }
                if (dog_count != 1) { throw new Exception("Не правильное имя почтового ящика!"); }
                if (value.Substring(0, value.IndexOf("@")).Length < 2) { throw new Exception("Слишком маленькое имя ящика!"); };
                
                mail = value.Substring(0, value.IndexOf("@"));
                domain = value.Substring(value.IndexOf("@"));
            }
        }
        public string Password
        {
            private get
            {
                return password;
            }
            set
            {
                string[] errors = { "@", "&", "#", "№", "$", "%", ":", ";", "*", "(", ")", "-" };

                if (value.Length < 7) { throw new Exception("Пароль должен состоять как минимум из 8 символов!"); }
                foreach (string s in errors)
                {
                    if (value.Contains(s)) { throw new Exception($"{s} - недопустимый символ в пароле!"); }
                }
                password = value;
            }
        }

        public Mail(string mail, string password)
        {
            Mail_address = mail;
            Password = password;
            SaveMail();
        }
        
        private void SaveMail()
        {
            Console.WriteLine("cocopay");
            StreamWriter fs = new StreamWriter("mails.txt");
            fs.WriteLine($"{Mail_address} | {Password}");
        }

        public void Authorization(string mail, string password)
        {
            StreamReader fw = new StreamReader("mails.txt");
            string line;
            while ((line = fw.ReadLine()) != null)
            {
                if (line.Split('|')[0] == mail && line.Split('|')[1] == password)
                {
                    authorization = mail;
                    Console.WriteLine("Вы успешно авторизовались!");
                    return;
                }
            }
            Console.WriteLine("Неверный почтовый ящик или пароль.");
            return;
        }

        public void LogOut()
        {
            authorization = null;
        }

/*        public void SendMessage(Message message)
        {
            if (authorization == null) throw new Exception("Вы не авторизованы");
            using (TextWriter fs = File().OpenWrite($"{mail}.json"))
            {
                JsonSerializer.Serialize(fs, message);
                Console.WriteLine("Data has been saved to file");
            }
        }*/

/*        public void CheckMessages()
        {
            if (authorization == null) throw new Exception("Вы не авторизованы");
            Десилиряция из JSON файла сообщений.
        }*/
    }
}
