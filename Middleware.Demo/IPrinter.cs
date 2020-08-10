using System;

namespace Middleware.Demo
{
    public interface IPrinter
    {
        public void Print();
    }

    public class Printer : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("Printing!");
        }
    }
}