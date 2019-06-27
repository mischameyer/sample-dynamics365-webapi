using System;

namespace service
{
    public class Service1 : IDisposable
    {
        private readonly Service2 _child;
        public Service1(Service2 child)
        {
            Console.WriteLine("Constructor Service1");
            _child = child;            
        }

        public object GetData() {
            return null;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose Service1");
            _child.Dispose();
        }
    }

    public class Service2 : IDisposable
    {
        public Service2()
        {
            Console.WriteLine("Constructor Service2");
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose Service2");
        }
    }
}
