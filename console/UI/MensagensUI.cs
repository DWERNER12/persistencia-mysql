using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace console.UI
{
    public class MensagensUI
    {
        public static void Mensagem(string mensagem, int timer = 5000)
        {
            Console.Clear();
            Console.WriteLine(mensagem);
            Thread.Sleep(timer);
            Console.Clear();
        }
    }
}