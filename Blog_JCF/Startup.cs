using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog_JCF.Startup))]
namespace Blog_JCF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
