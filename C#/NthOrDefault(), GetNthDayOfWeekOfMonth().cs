using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste2.ServiceReference1;

namespace teste2
{
    class Program
    {
        static void Main(string[] args)
        {
            var diaDasMaes = DateTimeExtensions.GetNthDayOfWeekOfMonth(2, DayOfWeek.Sunday, 5, 2017);

            Console.ReadKey();
        }
    }

    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> GetDatesInMonth(int month, int year)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                  .Select(day => new DateTime(year, month, day));
        }

        public static DateTime GetNthDayOfWeekOfMonth(int n, DayOfWeek dayOfWeek, int month, int year)
        {
            return GetDatesInMonth(month, year)
                  .NthOrDefault(n, date => date.DayOfWeek == dayOfWeek);
        }
    }

    public static class LinqExtensions
    {
        public static T NthOrDefault<T>(this IEnumerable<T> source, int n, Func<T, bool> predicate)
        {
            int found = 0;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    if (++found == n)
                    {
                        return item;
                    }
                }
            }

            return default(T);
        }
    }
}
