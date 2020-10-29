
using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using SUS.HTTP;

namespace Git.Services
{
    public class CommitsService:ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void CreateCommit(string description, string id, string userId)
        {
            
            var creator = this.db.Users
                .Where(x => x.Id == userId).Select(x=>x.Id).FirstOrDefault();

            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                Description = description,
                CreatorId = creator,
                RepositoryId = id,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
            
        }
        public IEnumerable<CommitsViewModel> GetAll()
        {
            var commits = this.db.Commits.Select(x => new CommitsViewModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                Repository = x.Repository.Name
            }).ToArray();

            return commits;
        }



        public string GetNameById(string id)
        {
            var name = this.db.Repositories.Where(x => x.Id == id)
                .Select(y => y.Name)
                .FirstOrDefault();

            return name;
        }

        public void Delete(string id)
        {
            var commit = this.db.Commits.Find(id);
            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }
    }
}
