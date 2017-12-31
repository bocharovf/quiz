﻿using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess.Repository
{
    public class QuizTemplateRepository : GenericRepository<QuizTemplate, int>, IQuizTemplateRepository
    {
        internal QuizTemplateRepository(ApplicationDatabaseContext context) : base(context)
        {

        }
    }
}
