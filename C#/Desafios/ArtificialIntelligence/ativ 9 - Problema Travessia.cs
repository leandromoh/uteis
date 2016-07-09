using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemaDaTravessia
{
    class Program
    {
        static Stack<Travessia> estadosParaExplorar = new Stack<Travessia>();
        static List<Travessia> estadosJaVisitados = new List<Travessia>();

        static void Main(string[] args)
        {
            estadosParaExplorar.Push(new Travessia(false, false, false, false));

            while (estadosParaExplorar.Count != 0)
            {
                var travessia = estadosParaExplorar.Pop();

                if (travessia.estadoMeta())
                {
                    imprimeResposta(travessia);
                    return;
                }

                estadosJaVisitados.Add(travessia);

                if (travessia.podeIr())
                    cria(travessia.vai());

                if (travessia.podeLevarLobo())
                    cria(travessia.levaLobo());

                if (travessia.podeLevarOvelha())
                    cria(travessia.levaOvelha());

                if (travessia.podeLevarRepolho())
                    cria(travessia.levaRepolho());

                if (travessia.podeVoltar())
                    cria(travessia.volta());

                if (travessia.podeTrazerLobo())
                    cria(travessia.trazLobo());

                if (travessia.podeTrazerOvelha())
                    cria(travessia.trazOvelha());

                if (travessia.podeTrazerRepolho())
                    cria(travessia.trazRepolho());
            }

            Console.WriteLine("Não há solução");
            Console.ReadKey();
        }

        static void imprimeResposta(Travessia travessia)
        {
            Console.WriteLine("{0} passos para o melhor caminho\n\n{1}\n", qtdPassosNecessarios(travessia), "F,L,O,R".PadLeft(25, ' '));
            mostraCaminho(travessia);
            Console.ReadKey();
        }

        static void cria(Travessia travessia)
        {
            if (!existe(travessia))
                estadosParaExplorar.Push(travessia);
        }

        static bool existe(Travessia travessia)
        {
            if (estadosParaExplorar.Any(c => c.fazendeiro == travessia.fazendeiro && c.lobo == travessia.lobo && c.ovelha == travessia.ovelha && c.repolho == travessia.repolho))
                return true;

            if (estadosJaVisitados.Any(c => c.fazendeiro == travessia.fazendeiro && c.lobo == travessia.lobo && c.ovelha == travessia.ovelha && c.repolho == travessia.repolho))
                return true;

            return false;
        }

        static void mostraCaminho(Travessia travessia)
        {
            if (travessia.pai != null)
                mostraCaminho(travessia.pai);

            Console.WriteLine("{0} -> {1},{2},{3},{4}", travessia.acaoQueGerou.PadRight(14, ' '), asd(travessia.fazendeiro), asd(travessia.lobo), asd(travessia.ovelha), asd(travessia.repolho));
        }

        static int qtdPassosNecessarios(Travessia travessia)
        {
            return (travessia != null) ? 1 + qtdPassosNecessarios(travessia.pai) : 0;
        }

        static string asd(bool x)
        {
            return x ? "D" : "E";
        }
    }

    class Travessia
    {
        public bool fazendeiro { get; private set; }
        public bool lobo { get; private set; }
        public bool ovelha { get; private set; }
        public bool repolho { get; private set; }

        public Travessia pai { get; private set; }
        public string acaoQueGerou { get; private set; }

        public Travessia(bool f, bool l, bool o, bool r)
            : this(f, l, o, r, null, "estado inicial")
        {
        }

        public Travessia(bool f, bool l, bool o, bool r, Travessia Pai, string a)
        {
            fazendeiro = f;
            lobo = l;
            ovelha = o;
            repolho = r;

            pai = Pai;
            acaoQueGerou = a;
        }

        public bool estadoMeta()
        {
            return fazendeiro && lobo && ovelha && repolho;
        }

        public Travessia vai()
        {
            return new Travessia(true, lobo, ovelha, repolho, this, "atravessa");
        }

        public Travessia levaLobo()
        {
            return new Travessia(true, true, ovelha, repolho, this, "leva o lobo");
        }

        public Travessia levaOvelha()
        {
            return new Travessia(true, lobo, true, repolho, this, "leva ovelha");
        }

        public Travessia levaRepolho()
        {
            return new Travessia(true, lobo, ovelha, true, this, "leva repolho");
        }

        public Travessia volta()
        {
            return new Travessia(false, lobo, ovelha, repolho, this, "volta");
        }

        public Travessia trazLobo()
        {
            return new Travessia(false, false, ovelha, repolho, this, "traz lobo");
        }

        public Travessia trazOvelha()
        {
            return new Travessia(false, lobo, false, repolho, this, "traz ovelha");
        }

        public Travessia trazRepolho()
        {
            return new Travessia(false, lobo, ovelha, false, this, "traz repolho");
        }

        public bool podeIr()
        {
            return !fazendeiro && lobo != ovelha && ovelha != repolho;
        }

        public bool podeLevarLobo()
        {
            return !fazendeiro && !lobo && ovelha != repolho;
        }

        public bool podeLevarOvelha()
        {
            return !fazendeiro && !ovelha;
        }

        public bool podeLevarRepolho()
        {
            return !fazendeiro && !repolho && lobo != ovelha;
        }

        public bool podeVoltar()
        {
            return fazendeiro && lobo != ovelha && ovelha != repolho;
        }

        public bool podeTrazerLobo()
        {
            return fazendeiro && lobo && ovelha != repolho;
        }

        public bool podeTrazerOvelha()
        {
            return fazendeiro && ovelha;
        }

        public bool podeTrazerRepolho()
        {
            return fazendeiro && repolho && lobo != ovelha;
        }
    }
}