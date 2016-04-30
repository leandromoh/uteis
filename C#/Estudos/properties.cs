using System;

namespace ConsoleApplication1
{
    class Program
    {
        private int _current;

        //Instead of use methods
        public int GetValue() { _current++; return _current; }
        public void SetValue(int value) { _current = value; }

        //Use properties
        public int Current
        {
            get { _current++; return _current; }
            set { _current = value; }
        }

        //Instead of default properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        //Use automatic properties
        public string Name { get; set; }
/*        
    The compiler translates automatic property into a property with a private,
    anonymous backing field. This can save you some time when you type a lot of properties.
    When you need some additional code to execute, the code is easily changed to use a get and set method.
    
    The get and set accessor can have different access modifiers
*/
    }
}