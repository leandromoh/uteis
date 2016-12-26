using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public static class Program
    {
        public static void Main()
        {

            var code = @"
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return ""{ Name = "" + this.Name + "", Age = "" + this.Age + "" }"";
    }
}
";
            // RETRIEVING COMPILER INTERFACE
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            // PREPARING COMPILER PARAMETERS
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateInMemory = true;
            compilerParameters.TreatWarningsAsErrors = false;
            compilerParameters.WarningLevel = 4;
            compilerParameters.ReferencedAssemblies.Add("System.dll");

            // COMPILING CODE
            CompilerResults results = codeProvider.CompileAssemblyFromSource(compilerParameters, code);

            if (results.Errors.Count > 0)
            {
                StringBuilder sbExceptions = new StringBuilder();

                foreach (CompilerError CompErr in results.Errors)
                {
                    sbExceptions.AppendLine("Line number " + CompErr.Line + ", Error Number: " + CompErr.ErrorNumber + ", ‘" + CompErr.ErrorText + ";" + Environment.NewLine + Environment.NewLine);
                }

                Console.WriteLine("Exception raised while compiling your code: nn" + sbExceptions.ToString());
            }
            else
            {
                // GETTING COMPILED ASSEMBLY
                Assembly assembly = results.CompiledAssembly;

                // RETRIEVING CLASS FROM COMPILED ASSEMBLY.
                Type personType = assembly.GetType("Person");

                // CREATING INSTANCE OF CLASS
                object obj = Activator.CreateInstance(personType);


                // ASSIGNING VALUES TO THE ENTITY CLASS OBJECT.
                personType.GetProperty("Name").SetValue(obj, "Sanjay", null);
                personType.GetProperty("Age").SetValue(obj, 30, null);

                Console.WriteLine(obj);
            }

            Console.Read();
        }
    }
}
