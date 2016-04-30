using System;
using System.Text;
using System.Threading.Tasks;

/*
Why Use Event instead of pure Delegate

An Event declaration adds a layer of abstraction and protection on the delegate instance. This protection prevents clients of the delegate from resetting the delegate and its invocation list and only allows adding or removing targets from the invocation list.
this protection layer also prevents "clients" (code outside the defining class/struct) from invoking the delegate, and from obtaining in any way the delegate object "behind" the event

Delegates x Events

In addition to the syntactic and operational properties, there's also a semantical difference.
Delegates are, conceptually, function templates; that is, they express a contract a function must adhere to in order to be considered of the "type" of the delegate.
Events represent ... well, events. They are intended to alert someone when something happens and yes, they adhere to a delegate definition but they're not the same thing.
*/

namespace ConsoleApplication1
{
    public class Publisher
    {
        private EventHandler _multicastDelegate = delegate { };
        public event EventHandler ExplicitEvent
        {
            add
            {
                lock (_multicastDelegate)
                {
                    _multicastDelegate += value;
                }
            }
            remove
            {
                lock (_multicastDelegate)
                {
                    _multicastDelegate -= value;
                }
            }
        }

        public void Rise()
        {
            _multicastDelegate(3, new EventArgs());
        }
    }

    class Program
    {
        public static void Main()
        {
            var p = new Publisher();
            p.ExplicitEvent += (sender, e) => { Console.WriteLine(sender); };
            p.ExplicitEvent += (sender, e) => { Console.WriteLine(sender); };
            p.Rise();
            Console.Read();
        }
    }
}