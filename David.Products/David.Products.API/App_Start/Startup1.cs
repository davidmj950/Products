using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(David.Products.API.Startup1))]

namespace David.Products.API
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            Debug.Assert(app != null);
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
