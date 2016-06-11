using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ExpressionEvaluator.Eval("(2 + 2) * 5"));
            Console.Read();
        }
    }

    public class ExpressionEvaluator
    {
        public static double Eval(string expression)
        {
            CompilerResults results = new CSharpCodeProvider().CompileAssemblyFromSource(new CompilerParameters(), new string[] {
                string.Format(@"
                    namespace MyAssembly
                    {{
                        public class Evaluator
                        {{
                            public double Eval()
                            {{
                                return {0};
                            }}
                        }}
                    }}
                ", expression)
                });

            Assembly assembly = results.CompiledAssembly;
            dynamic evaluator = Activator.CreateInstance(assembly.GetType("MyAssembly.Evaluator"));
            return evaluator.Eval();
        }
    }
}
