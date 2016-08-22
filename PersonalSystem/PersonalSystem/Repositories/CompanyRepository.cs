﻿using PersonalSystem.DataAccess;
using PersonalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
 
namespace PersonalSystem.Repositories
{
    public class CompanyDbRepository
    {
        private ApplicationDbContext _db;
 
        public CompanyDbRepository()
        {
             
            _db = ApplicationDbContext.Create();
        }
         
        #region Companies
        #region Create
        public Company Create(Company company)
        {
            if (!IsValidCompany(company))
                return null;
            Company result = _db.Companies.Add(company);
            _db.SaveChangesAsync();
            return result;
        }
 
        public bool IsValidCompany(Company company)
        {
            if (company == null || company.Name == null || company.Name.Length < 3 || _db.Companies.Any(c => c.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase)))
                return false;
            return true;
        }
        #endregion
        #region Get
        public List<Company> GetAllCompanies()
        {
            return _db.Companies.ToList();
        }
 
        public Company GetCompanyByName(string companyName)
        {
            return _db.Companies.FirstOrDefault(c => c.Name.Equals(companyName, StringComparison.OrdinalIgnoreCase));
        }
        #endregion
        #region Update
        public void Update(Company companyToUpdate)
        {
            Company company = GetCompanyByName(companyToUpdate.Name);
            _db.Entry(company).State = EntityState.Modified;
            _db.SaveChanges();
        }
        #endregion
        #region Delete
        public void Delete(Company companyToRemove)
        {
            Company company = GetCompanyByName(companyToRemove.Name);
            _db.Entry(company).State = EntityState.Deleted;
            _db.SaveChanges();
        }
        #endregion
        #endregion
        #region Departments
        #region Create
        public Department Create(Department department)
        {
            if (!IsValidDepartment(department))
                return null;
            Department result = _db.Departments.Add(department);
            _db.SaveChanges();
            return result;
        }
 
        public bool IsValidDepartment(Department department)
        {
            if (department == null || department.Name == null || department.Name.Length < 3 || _db.Departments.Any(d => d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase) && d.Company.Name.Equals(department.Company.Name, StringComparison.OrdinalIgnoreCase)))
                return false;
            return true;
        }
        #endregion
        #region Get
        public List<Department> GetAllDepartmentsInCompany(Company company)
        {
            return _db.Departments.Where(d => d.Company.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
 
        public Department GetDepartmentByNameInCompany(string departmentName, Company company)
        {
            return _db.Departments.FirstOrDefault(c => c.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase) && c.Company.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase));
        }
        #endregion
        #region Update
        public void Update(Department departmentToUpdate)
        {
            Department department = GetDepartmentByNameInCompany(departmentToUpdate.Name, departmentToUpdate.Company);
            _db.Entry(department).State = EntityState.Modified;
            _db.SaveChanges();
        }
        #endregion
        #region Delete
        public void Delete(Department departmentToRemove)
        {
            Department department = GetDepartmentByNameInCompany(departmentToRemove.Name, departmentToRemove.Company);
            _db.Entry(department).State = EntityState.Deleted;
            _db.SaveChanges();
        }
        #endregion
        #endregion
        #region Groups
        #region Create
        public Department Create(Group group, Department department)
        {
            if (!IsValidGroup(group, department))
                return null;
            Department result = _db.Departments.Add(department);
            _db.SaveChangesAsync();
            return result;
        }
 
        public bool IsValidGroup(Group group, Department department)
        {
            if (group == null || group.Name == null || group.Name.Length < 3 || _db.Departments.First(d => d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)).Groups.Any(g => g.Name.Equals(group.Name)))
                return false;
            return true;
        }
        #endregion
        #region Get
        public List<Group> GetAllGroupsInDepartment(Department department)
        {
            return _db.Groups.Where(g => g.Department.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
 
        public Group GetGroupByNameInDepartment(string groupName, Department department)
        {
            return _db.Groups.FirstOrDefault(c => c.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase) && c.Department.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase));
        }
        #endregion
        #region Update
        public void Update(Group groupToUpdate)
        {
            Group group = GetGroupByNameInDepartment(groupToUpdate.Name, groupToUpdate.Department);
            _db.Entry(group).State = EntityState.Modified;
            _db.SaveChanges();
        }
        #endregion
        #region Delete
        public void Delete(Group groupToRemove)
        {
            Group group = GetGroupByNameInDepartment(groupToRemove.Name, groupToRemove.Department);
            _db.Entry(group).State = EntityState.Deleted;
            _db.SaveChanges();
        }
        #endregion
        #endregion
    }
    public class Test<T> where T : BaseEntity
    {
        ApplicationDbContext _db;
        private IDbSet<T> entities;
        public Test()
        {
            _db = ApplicationDbContext.Create();
        }
 
        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }
 
        public IEnumerable<T> GetByName(string name)
        {
            return this.Entities.Where(e => e.ToString().Equals(name, StringComparison.OrdinalIgnoreCase));
        }
 
        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!this.IsValid(entity))
                return entity;
            T result = this.Entities.Add(entity);
            this._db.SaveChanges();
            return result;
        }
 
        public bool IsValid(T check)
        {
            var type = check.GetType();
            if (type.Equals(typeof(Company)))
            {
                Company company = check as Company;
                return this.IsValidCompany(company);
            }
            else if (type.Equals(typeof(Department)))
            {
                Department department = check as Department;
                return this.IsValidDepartment(department);
            }
            else if (type.Equals(typeof(Group)))
            {
                Group group = check as Group;
                return this.IsValidGroup(group);
            }
            return true;
        }
        public bool IsValidCompany(Company company)
        {
            if (String.IsNullOrWhiteSpace(company.Name) || company.Name.Length <= 3 || _db.Companies.Any(c => c.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase)) || company.Admin == null || company.Departments.Count() <= 0)
                return false;
            foreach (var d in company.Departments)
                if (!this.IsValidDepartment(d))
                    return false;
            return true;
        }
        public bool IsValidDepartment(Department department)
        {
            if (String.IsNullOrWhiteSpace(department.Name) || department.Name.Length <= 3 && department.Groups.Count() <= 0)
                return false;
            foreach(var g in department.Groups)
                if (!this.IsValidGroup(g))
                    return false;
            return true;
        }
        public bool IsValidGroup(Group group)
        {
            if (String.IsNullOrWhiteSpace(group.Name) || group.Name.Length <= 3)
            {
                return false;
            }
            return true;
        }
        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = _db.Set<T>();
                }
                return entities;
            }
        }
    }
    public class CompanyRepository
    {
        CompanyDbRepository _DbRepo;
        public CompanyRepository()
        {
            _DbRepo = new CompanyDbRepository();
        }
        public Company CreateCompany(Company company)
        {
            return _DbRepo.Create(company);
        }
        public List<Company> GetAllCompanies()
        {
            return _DbRepo.GetAllCompanies();
        }
        public void UpdateCompany(Company company)
        {
            _DbRepo.Update(company);
        }
        public void DeleteCompany(Company company)
        {
            _DbRepo.Delete(company);
        }
        public void CreateDepartment()
        {
            _DbRepo.Create(new Department { Name = "TheDepartment", Company = _DbRepo.GetCompanyByName("654321") });
        }
    }
}