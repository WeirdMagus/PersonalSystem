using PersonalSystem.DataAccess;
using PersonalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonalSystem.Repositories
{
    public class ApplicationRepository
    {
        private ApplicationDbContext _db;

        public ApplicationRepository()
        {
            _db = ApplicationDbContext.Create();
        }

        public Application Create(Application application)
        {
            if (application != null || String.IsNullOrWhiteSpace(application.CoverLetter) || application.Applicant != null || application.Vacancy != null)
                return application;
            return null;
        }
        public Application Get(int? id)
        {
            return _db.Applications.Single(a => a.Id == id);
        }

        public List<Application> GetAllFromVacancy(int? id)
        {
            return _db.Applications.Where(a => a.Vacancy.Id == id).ToList();
        }

        public bool Update(Application app)
        {
            _db.Entry(app).State = EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
        public void Delete(int? id)
        {
            Application app = Get(id);
            Delete(app);
        }
        public void Delete(Application app)
        {
            _db.Applications.Remove(app);
            _db.SaveChanges();
        }

    }
}