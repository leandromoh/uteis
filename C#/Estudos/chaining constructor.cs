using System;
using System.Text;

namespace ConsoleApplication1
{
    class Time
    {
        private string h, m, s;

        public Time() : this("00") { }

        public Time(string hora) : this(hora, "00") { }

        public Time(string hora, string minuto) : this(hora, minuto, "00") { }

        public Time(string hora, string minuto, string segundo)
        {
            h = hora;
            m = minuto;
            s = segundo;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", h, m, s);
        }

        static void Main(string[] args)
        {
            /*
              Quando usamos construtores em cadeia, o próximo construtor é chamado
              antes do corpo do construtor atual ser executado.
             
              Portanto, se dentro de cada construtor nos imprimíssemos o número de parâmetros
              que ele recebe, quando executássemos new Time(), seria escrito na tela 3 2 1 0
            */

            Console.WriteLine("Hora 1: " + new Time());
            Console.WriteLine("Hora 2: " + new Time("02"));
            Console.WriteLine("Hora 3: " + new Time("02", "10"));
            Console.WriteLine("Hora 4: " + new Time("02", "10", "50"));

            Console.ReadLine();
        }
    }
}
