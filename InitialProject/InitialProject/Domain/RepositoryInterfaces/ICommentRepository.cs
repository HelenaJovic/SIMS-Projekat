﻿using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public List<Comment> GetByUser(User user);

    }
}
