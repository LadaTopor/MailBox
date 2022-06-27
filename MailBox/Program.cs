using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mail gosha = new Mail("Garry.Popens@mail.ru", "goroskop15");
            Mail tolya = new Mail("Varjka.Kota@yandex.ru", "voronenok");
            
            tolya.Authorization("voronenok");
            //Message messtoTolya = new Message("Как дела?", "Давно не виделись, дружище, как ты? Расскажи о себе, как поживаешь", "Varjka.Kota@yandex.ru");
            tolya.CheckMessages();
        }
    }
}
