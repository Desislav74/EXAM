
using System;
using System.Collections.Generic;
using Git.Data;

namespace Git.ViewModels.Repositories
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => CreatedOn.ToString("s");

        //public Commit Commit { get; set; }
        public int CommitsCount {get; set; }



    }
}
