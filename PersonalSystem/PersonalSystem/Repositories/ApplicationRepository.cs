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

        private Application Create(Application application)
        {
            if (application != null || String.IsNullOrWhiteSpace(application.CoverLetter) || application.Applicant != null || application.Vacancy != null)
                return application;
            return null;
        }
        private Application Get(int? id)
        {
            return _db.Applications.Single(a => a.Id == id);
        }
        private List<Application> GetAllAppsForVacancy(int? id) // Hämtar alla ansökningar för specifik annons
        {
            return _db.Applications.Where(a => a.Vacancy.Id == id).ToList();
        }
        private bool Update(Application app)
        {
            _db.Entry(app).State = EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
        private void Delete(int? id)
        {
            Application app = Get(id);
            Delete(app);
        }
        private void Delete(Application app)
        {
            _db.Applications.Remove(app);
            _db.SaveChanges();
        }

        public void CreateApp(Application app)
        {
            if (app.Vacancy != null && app.CoverLetter != null && app.CoverLetter != null && app.Applicant != null)
            {
                this.Create(app);
            }
        }
        public Application GetApp(int? id)
        {
            if(id != null)
            {
               return this.Get(id);
            }
            return null;
        }
        public List<Application> GetAppsForVacancy(int? id)
        {
            if(id != null)
            {
                return this.GetAllAppsForVacancy(id);
            }
            return null;
        }
        public void DeleteApp(Application app)
        {
            if(app != null)
            {
                this.Delete(app);
            }
        }
        public void UpdateApp(Application app)
        {
            if(app != null)
            {
                this.Update(app);
            }
        }
        public void DeleteApp(int? id)
        {
            if(id != null)
            {
                this.Delete(id);
            }
        }
    }
}