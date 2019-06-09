using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GitHubSearchProject.Startup))]
namespace GitHubSearchProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
