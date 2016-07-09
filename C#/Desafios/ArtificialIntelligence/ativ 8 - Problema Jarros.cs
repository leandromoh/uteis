using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemaDosJarros
{
    class Program
    {
        static Queue<Jarros> estadosParaExplorar = new Queue<Jarros>();
        static List<Jarros> estadosJaVisitados = new List<Jarros>();

        static void Main(string[] args)
        {
            estadosParaExplorar.Enqueue(new Jarros(0, 0));

            while (estadosParaExplorar.Count != 0)
            {
                var jarro = estadosParaExplorar.Dequeue();

                if (jarro.estadoMeta())
                {
                    imprimeResposta(jarro);
                    return;
                }

                estadosJaVisitados.Add(jarro);

                if (jarro.podeEncher1())
                    cria(jarro.encher1());

                if (jarro.podeEncher2())
                    cria(jarro.encher2());

                if (jarro.podeEsvaziar1())
                    cria(jarro.esvazia1());

                if (jarro.podeEsvaziar2())
                    cria(jarro.esvazia2());

                if (jarro.podeDespejar1em2Esvaziando1())
                    cria(jarro.despeja1em2Esvaziando1());

                if (jarro.podeDespejar1em2NaoEsvaziando1())
                    cria(jarro.despeja1em2NaoEsvaziando1());

                if (jarro.podeDespejar2em1Esvaziando2())
                    cria(jarro.despeja2em1Esvaziando2());

                if (jarro.podeDespejar2em1NaoEsvaziando2())
                    cria(jarro.despeja2em1NaoEsvaziando2());
            }

            Console.WriteLine("Não há solução");
            Console.ReadKey();
        }

        static void imprimeResposta(Jarros jarro)
        {
			Console.WriteLine("{0} passos para o melhor caminho\n", qtdPassosNecessarios(jarro));
			mostraCaminho(jarro);
			Console.ReadKey();
        }
		
        static void cria(Jarros jarro)
        {
            if (!existe(jarro))
                estadosParaExplorar.Enqueue(jarro);
        }

        static bool existe(Jarros jarro)
        {
            if (estadosParaExplorar.Any(c => c.x == jarro.x && c.y == jarro.y))
                return true;

            if (estadosJaVisitados.Any(c => c.x == jarro.x && c.y == jarro.y))
                return true;

            return false;
        }

        static void mostraCaminho(Jarros jarro)
        {
            if (jarro.pai != null)
                mostraCaminho(jarro.pai);

            Console.WriteLine("x = {0}, y = {1}", jarro.x, jarro.y);
        }

        static int qtdPassosNecessarios(Jarros jarro)
        {
            return (jarro != null) ? 1 + qtdPassosNecessarios(jarro.pai) : 0;
        }
    }
	
	class Jarros
	{
		public int x { get; private set; }
		public int y { get; private set; }

		public static int minX = 0;
		public static int maxX = 3;

		public static int minY = 0;
		public static int maxY = 4;

		public Jarros pai { get; private set; }

		public Jarros(int X, int Y)
			:this(X, Y, null)
		{
		}

		public Jarros(int X, int Y, Jarros Pai)
		{
			x = X;
			y = Y;
			pai = Pai;
		}

        public bool estadoMeta()
        {
            return y == 2;
        }

        public Jarros encher1()
		{
			return new Jarros(maxX, y, this);
		}

		public Jarros encher2()
		{
			return new Jarros(x, maxY, this);
		}

		public Jarros esvazia1()
		{
			return new Jarros(0, y, this);
		}

		public Jarros esvazia2()
		{
			return new Jarros(x, 0, this);
		}

		public Jarros despeja1em2Esvaziando1()
		{
			return new Jarros(0, x + y, this);
		}

		public Jarros despeja1em2NaoEsvaziando1()
		{
			return new Jarros(x + y - maxY, maxY, this);
		}

		public Jarros despeja2em1Esvaziando2()
		{
			return new Jarros(x + y, 0, this);
		}

		public Jarros despeja2em1NaoEsvaziando2()
		{
			return new Jarros(maxX, x + y - maxX, this);
		}

        public bool podeEncher1()
        {
            return x < maxX;
        }

        public bool podeEncher2()
        {
            return y < maxY;
        }

        public bool podeEsvaziar1()
        {
            return x > minX;
        }

        public bool podeEsvaziar2()
        {
            return y > minY;
        }

        public bool podeDespejar1em2Esvaziando1()
        {
            return x > minX && y < maxY && (x + y) <= maxY;
        }

        public bool podeDespejar1em2NaoEsvaziando1()
        {
            return x > minX && y < maxY && (x + y) > maxY;
        }

        public bool podeDespejar2em1Esvaziando2()
        {
            return x < maxX && y > minY && (x + y) <= maxX;
        }

        public bool podeDespejar2em1NaoEsvaziando2()
        {
            return x < maxX && y > minY && (x + y) > maxX;
        }
    }
}
