using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApplication1
{
    public static class MethodInfoExtension
    {
        public static bool IsExtension(this MethodInfo mi)
        {
            return mi.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), true);
        }

        public static string GetSignature(this MethodInfo mi)
        {
            var sb = new StringBuilder();

            sb.Append(mi.IsPublic ? "public " : null);
            sb.Append(mi.IsPrivate ? "private " : null);
            sb.Append(mi.IsFamily ? "protected " : null);
            sb.Append(mi.IsAssembly ? "internal " : null);
            sb.Append(mi.IsFamilyOrAssembly ? "protected internal " : null);
            sb.Append(mi.IsAbstract ? "abstract " : null);
            sb.Append(mi.IsStatic ? "static " : null);
            sb.Append(mi.IsVirtual ? "virtual " : null);

            var param = mi.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name).ToArray();

            if (mi.IsExtension())
                param[0] = "this " + param[0];

            return string.Format("{0}{1} {2}({3})", sb, mi.ReturnType.Name, mi.Name, String.Join(", ", param));
        }
    }

    class Dummy
    {
        private void m_private() { }
        public void m_public() { }
        protected void m_protected() { }
        internal void m_internal() { }
        protected internal void m_protected_internal() { }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var methods = typeof(Dummy).GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var m in methods)
            {
                Console.WriteLine(m.GetSignature());
            }

            Console.Read();
        }
    }
}
