using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static List<Conexao> conexoes = new List<Conexao>();

        static SortedSet<Caminho> estadosParaExplorar = new SortedSet<Caminho>(new Caminho());
        static List<Caminho> estadosJaVisitados = new List<Caminho>();
        static List<Caminho> estadosMeta = new List<Caminho>();

        static void Main(string[] args)
        {
            criarConexoes();
            estadosParaExplorar.Add(new Caminho("A", 0));

            while (estadosParaExplorar.Count != 0)
            {
                var conexao = estadosParaExplorar.First();
                estadosParaExplorar.Remove(conexao);

                estadosJaVisitados.Add(conexao);

                if (conexao.atual == "K")
                {
                    estadosMeta.Add(conexao);
                    continue;
                }

                var conexoesPossiveis = conexoes.Where(c => conexao.atual == c.atual).OrderBy(c => c.custo);

                foreach (var c in conexoesPossiveis)
                {
                    var conexaoPossivel = new Caminho(c.destino, conexao.custo + c.custo, conexao);

                    if (!existe(conexaoPossivel))
                        estadosParaExplorar.Add(conexaoPossivel);
                }
            }

            Console.WriteLine("Quantidade de caminhos possiveis: {0}", estadosMeta.Count);

            foreach (var caminho in estadosMeta)
            {
                Console.Write("\n");
                mostraCaminho(caminho);
                Console.WriteLine("\n custo: {0}", caminho.custo);
            }

            Console.ReadKey();
        }

        static void mostraCaminho(Caminho caminho)
        {
            if (caminho.pai != null)
                mostraCaminho(caminho.pai);

            Console.Write(caminho.pai == null ? " {0}" : " --> {0}", caminho.atual);
        }

        static bool existe(Caminho caminho)
        {
            if (estadosParaExplorar.Any(c => c.atual == caminho.atual && c.pai.atual == caminho.pai.atual))
                return true;

            if (estadosJaVisitados.Any(c => c.atual == caminho.atual && c.pai.atual == caminho.pai.atual))
                return true;

            return false;
        }

        static void criarConexoes()
        {
            conexoes.Add(new Conexao("A", "B", 7));
            conexoes.Add(new Conexao("A", "C", 9));
            conexoes.Add(new Conexao("A", "D", 3));
            conexoes.Add(new Conexao("B", "I", 4));
            conexoes.Add(new Conexao("B", "F", 3));
            conexoes.Add(new Conexao("C", "J", 5));
            conexoes.Add(new Conexao("D", "E", 1));
            conexoes.Add(new Conexao("F", "G", 2));
            conexoes.Add(new Conexao("G", "H", 3));
            conexoes.Add(new Conexao("I", "K", 5));
            conexoes.Add(new Conexao("J", "L", 6));
            conexoes.Add(new Conexao("L", "K", 4));
        }
    }

    public class Caminho : IComparer<Caminho>
    {
        public string atual;
        public int custo;
        public Caminho pai;

        public Caminho()
        {
        }

        public Caminho(string a, int c, Caminho p = null)
        {
            atual = a;
            custo = c;
            pai = p;
        }

        int IComparer<Caminho>.Compare(Caminho x, Caminho y)
        {
            return x.custo - y.custo;
        }
    }

    public class Conexao
    {
        public string atual;
        public string destino;
        public int custo;

        public Conexao(string a, string d, int c)
        {
            atual = a;
            destino = d;
            custo = c;
        }
    }
}
