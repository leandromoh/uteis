using System;
using System.Text;

namespace ConsoleApplication1
{
	//https://giovanibettiol.wordpress.com/2009/10/06/algoritmo-para-calcular-o-periodo-do-horario-de-verao/
    public static class BrazilianSpecialEvents
    {
        public static DateTime DomingoDePascoa(int ano)
        {
            Int32 a = ano % 19;
            Int32 b = ano / 100;
            Int32 c = ano % 100;
            Int32 d = b / 4;
            Int32 e = b % 4;
            Int32 f = (b + 8) / 25;
            Int32 g = (b - f + 1) / 3;
            Int32 h = (19 * a + b - d - g + 15) % 30;
            Int32 i = c / 4;
            Int32 k = c % 4;
            Int32 L = (32 + 2 * e + 2 * i - h - k) % 7;
            Int32 m = (a + 11 * h + 22 * L) / 451;
            Int32 mes = (h + L - 7 * m + 114) / 31;
            Int32 dia = ((h + L - 7 * m + 114) % 31) + 1;
            return new DateTime(ano, mes, dia);
        }

        public static DateTime DomingoDeCarnaval(int ano)
        {
            return DomingoDePascoa(ano).AddDays(-49);
        }

        public static DateTime InicioHorarioVerao(Int32 ano)
        {
            // terceiro domingo de outubro
            DateTime primeiroDeOutubro = new DateTime(ano, 10, 1);
            DateTime primeiroDomingoDeOutubro = primeiroDeOutubro.AddDays((7 - (Int32)primeiroDeOutubro.DayOfWeek) % 7);
            DateTime terceiroDomingoDeOutubro = primeiroDomingoDeOutubro.AddDays(14);
            return terceiroDomingoDeOutubro;
        }

        public static DateTime TerminoHorarioVerao(Int32 ano)
        {
            DateTime primeiroDeFevereiro = new DateTime(ano + 1, 2, 1);
            DateTime primeiroDomingoDeFevereiro = primeiroDeFevereiro.AddDays((7 - (Int32)primeiroDeFevereiro.DayOfWeek) % 7);
            DateTime terceiroDomingoDeFevereiro = primeiroDomingoDeFevereiro.AddDays(14);

            if (terceiroDomingoDeFevereiro != DomingoDeCarnaval(ano))
            {
                return terceiroDomingoDeFevereiro;
            }
            else
            {
                return terceiroDomingoDeFevereiro.AddDays(7);
            }
        }
    }
}