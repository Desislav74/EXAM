
using System.Collections.Generic;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public interface ICommitsService
    {
        IEnumerable<CommitsViewModel> GetAll();

        string GetNameById(string id);

        void CreateCommit(string description, string id, string userId);

        void Delete(string id);

    }
}
