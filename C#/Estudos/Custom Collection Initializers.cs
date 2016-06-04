using System;
using System.Collections.Generic;
using System.Collections;

//font: http://stackoverflow.com/questions/2495791/custom-collection-initializers

namespace ConsoleApplication1
{
    // simple struct which represents a point in three-dimensional space
    public struct Point3D
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    // implementation of a collection of points, which respects
    // the compiler convention for collection initializers and
    // therefore both implements IEnumerable<T> and provides
    // a public Add method
    public class Points : IEnumerable<Point3D>
    {
        private readonly List<Point3D> _points;

        public Points()
        {
            _points = new List<Point3D>();
        }

        public void Add(double x, double y, double z)
        {
            _points.Add(new Point3D(x, y, z));
        }

        public void Add(Point3D p)
        {
            _points.Add(p);
        }

        public IEnumerator<Point3D> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /*
        C# Language Specification - 7.5.10.3 Collection Initializers
     
        The collection object to which a collection initializer is applied must be of a type that implements 
        System.Collections.IEnumerable or a compile-time error occurs. For each specified element in order, 
        the collection initializer invokes an Add method on the target object with the expression list of the 
        element initializer as argument list, applying normal overload resolution for each invocation. 
        Thus, the collection object must contain an applicable Add method for each element initializer.
    */

    class Program
    {
        static void Main()
        {
            // instantiate the Points class and fill it with values like this:
            var cube = new Points
                        {
                            { -1, -1, -1 }, // Points.Add(double x, double y, double z)
                            { -1, -1,  1 },
                            { -1,  1, -1 },
                            { -1,  1,  1 },
                            {  1, -1, -1 },
                            {  1, -1,  1 },
                            {  1,  1, -1 },
                            new Point3D(  1,  1,  1 ) // Points.Add(Point3D p)
                        };
            
            foreach (Point3D p in cube)
                Console.WriteLine(string.Format("{0} {1} {2}", p.X, p.Y, p.Z));

            Console.Read();
        }
    }
}
