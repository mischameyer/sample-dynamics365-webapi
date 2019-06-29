using System;
using Microsoft.Extensions.DependencyInjection;
using service;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<GetContacts>();

            var provider = services.BuildServiceProvider();

            using (var service = provider.GetService<GetContacts>())
            {
                var result = service.GetData();
            }

            Console.ReadKey();
        }


    }
}
