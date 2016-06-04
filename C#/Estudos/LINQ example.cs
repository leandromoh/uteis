using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Account
    {
        public string Status { get; set; }
        public decimal Balance { get; set; }

        public Account(string status, decimal balance)
        {
            Status = status;
            Balance = balance;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            List<Account> myAccounts = new List<Account> { 
                new Account("active", 10),
                new Account("active", 15),
                new Account("active", 25),
                new Account("inactive", 100),
                new Account("inactive", 200),
                new Account("inactive", 300),
            };

            #region [conventional way]

            decimal total1 = 0;

            foreach (Account account in myAccounts)
            {
                if (account.Status == "active")
                {
                    total1 += account.Balance;
                }
            }

            #endregion



            #region [LINQ method syntax way]

            decimal total2 = myAccounts.Where(a => a.Status == "active").Select(a => a.Balance).Sum();

            #endregion



            #region [LINQ query syntax way]

            decimal total3 = (from
                                a in myAccounts
                              where
                                 a.Status == "active"
                              select
                                 a.Balance).Sum();

            #endregion


         // LINQ query syntax is always translated to method syntax
         // based on the rules on 7.16 of the C# 4 specification

            Console.WriteLine(total1 == total2 && total2 == total3);

            Console.Read();
        }
    }
}
