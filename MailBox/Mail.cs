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
            using (FileStream path = new FileStream("mails.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(path)) {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains(Mail_address)) return; 
                    }
                    using (StreamWriter fs = new StreamWriter(path))
                    {
                        fs.WriteLine($"{Mail_address} {Password}");
                        return;
                    }
                }
            }
        }
        
    
        public void Authorization(string password)
        {
            StreamReader fw = new StreamReader("mails.txt");
            string line;
            while ((line = fw.ReadLine()) != null)
            {
                string[] s = line.Split(' ');                
                if (s[0] == Mail_address && s[1] == password)
                {
                    authorization = mail;
                    Console.WriteLine("Вы успешно авторизовались!");
                    return;
                }
            }
            throw new Exception("Неверный почтовый ящик или пароль.");
        }

        public void LogOut()
        {
            authorization = null;
            Console.WriteLine("Вы вышли из почты!");
        }

        public void SendMessage(Message message)
        {
            if (authorization == null) throw new Exception("Вы не авторизованы");

            string addressee = message.To_mail;
            foreach (string s in File.ReadAllLines("mails.txt"))
            {
                if (s.Contains(addressee))
                {
                    File.WriteAllText($"{addressee.Substring(0, addressee.IndexOf('@'))}.json", JsonConvert.SerializeObject(message));
                    Console.WriteLine("Сообщение успешно отправлено!");
                    return;
                }
            }
            Console.WriteLine("Получатель не найден!");
            return;
        }

        public void CheckMessages()
        {
            if (authorization == null) throw new Exception("Вы не авторизованы");

            var message = File.Exists($"{mail}.json") ? JsonConvert.DeserializeObject<Message>(File.ReadAllText($"{mail}.json")) : null;
            if (message == null)
            {
                Console.WriteLine("Нет новых сообщений!");
                return;
            }
            Console.WriteLine($"Тема: {message.Header} \nОт: {message.From_mail}\nКому: {message.To_mail}\nОтправлено {message.Date}\nСообщение: {message.MessageText}");
        }
    }
}
