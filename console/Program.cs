using System;
using Microsoft.Extensions.DependencyInjection;
using service;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var services = new ServiceCollection();
            services.AddTransient<Service2>();
            services.AddTransient<Service1>();

            var provider = services.BuildServiceProvider();

            using (var service1 = provider.GetService<Service1>())
            {
                
                // so something with the class
            }
        }


    }
}
