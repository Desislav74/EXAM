
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Git.Data;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public class RepositoriesService:IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string name,string repositoryType)
        {
            var ownerId = this.db.Users.Select(x => x.Id).FirstOrDefault();
            var repository=new Repository
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
                Commits = new List<Commit>(),
                IsPublic = repositoryType=="Public"?true:false,
                OwnerId = ownerId
              

            };
            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }


        

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            var repositories = this.db.Repositories
                .Select(x => new RepositoryViewModel
                {
                    
                    Id = x.Id,
                    Name = x.Name,
                    CommitsCount = x.Commits.Count,
                    CreatedOn = x.CreatedOn,
                    Owner = x.Owner.Username,
                }).ToList();
            return repositories;
        }
    }
}
