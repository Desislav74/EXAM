
using System.Collections;
using System.Collections.Generic;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void Create(string name, string repositoryType);

        IEnumerable<RepositoryViewModel> GetAll();
    }
}
