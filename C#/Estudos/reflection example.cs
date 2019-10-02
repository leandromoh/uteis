using System;
using System.Reflection;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var pessoa = new Pessoa();

            while(true)
            {
                Console.WriteLine("digite a propriedade que voce deseja alterar");
                string prop = Console.ReadLine();

                Console.WriteLine("digite o valor que voce deseja atribuir");
                string value = Console.ReadLine();
                
                Type type = pessoa.GetType();

                FieldInfo field = type.GetField(prop);
                var newValue = Convert.ChangeType(value, field.FieldType);

                field.SetValue(pessoa, newValue);

                Console.WriteLine($"nome = {pessoa.nome}, idade = {pessoa.idade}");

                Console.WriteLine("======================");
            }
        }
    }

    public class Pessoa
    {
        public string nome;
        public int idade;
    }
}
