
using System;
using System.Buffers;
using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length < 5)
            {
                return this.Error("Description should be at least 5 characters long.");
            }

            var userId = this.GetUserId();
            this.commitsService.CreateCommit(description, id, userId);
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repoName = this.commitsService.GetNameById(id);

            var viewModel = new CreateCommitViewModel
            {
                Id = id,
                Name = repoName
            };

            return this.View(viewModel);
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.commitsService.GetAll();
            return this.View(viewModel);
        }

        public HttpResponse Delete(string id)
        {
            this.commitsService.Delete(id);
            return this.Redirect("/Commits/All");
        }
    }
}
