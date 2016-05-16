using System;

namespace ConsoleApplication1
{
    class Program
    {
        /*
            Ambas fazem o mesmo papel (passar um parâmetro por referência), mas a diferença entre os dois tipos de parâmetros são bem sutis. 

            out = não precisamos inicializar a variável que vamos passar, mas o metodo é obrigado a atribuir um valor ao parâmetro.
            ref = somos obrigados a inicializar a variável antes de passar, mas o metodo não é obrigado a atribuir um valor ao parâmetro.
        */

        static void Main()
        {
            int x, y;

            y = 5;

            Out(out x);
            Ref(ref y); // if y was not assigned then you get the error: Use of unassigned local variable 'y'

            Console.Write(x + y); // 8
            Console.Read();
        }

        static void Out(out int a)
        {
            a = 3; // without this line you get the error: The out parameter 'a' must be assigned to before control leaves the current method
        }

        static void Ref(ref int a)
        {
        }
    }
}
