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

            using (var service1 = provider.GetService<GetContacts>())
            {
                var result = service1.GetData();
            }
        }


    }
}
