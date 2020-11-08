
using Git.Services;
using Git.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController:Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }
        public HttpResponse Create()
        {

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name,string repositoryType)

        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            this.repositoriesService.Create(name, repositoryType);
            return this.Redirect("/Repositories/All");
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repositories = this.repositoriesService.GetAll();
            return this.View(repositories);
        }
    }
}
