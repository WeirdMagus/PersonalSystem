using PersonalSystem.DataAccess;
using PersonalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace PersonalSystem.Repositories
{
	public class CompanyRepository
	{
		private ApplicationDbContext _db;

		public CompanyRepository()
		{
			_db = ApplicationDbContext.Create();
		}

		#region Create
		public async Task<Company> CreateCompany(Company company)
		{
			if (!IsValidCompany(company))
				return null;
			Company result = _db.Companies.Add(company);
			await _db.SaveChangesAsync();
			return result;
		}

		private bool IsValidCompany(Company company)
		{
			if (company == null || company.Name == null || company.Name.Length < 3 || _db.Companies.Any(c => c.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase)))
				return false;
			return true;
		}
		#endregion
		#region Get
		public async Task<List<Company>> GetAllCompanies()
		{
			return await _db.Companies.ToListAsync();
		}

		public Company GetCompanyByName(string companyName)
		{
			return _db.Companies.FirstOrDefault(c => c.Name.Equals(companyName, StringComparison.OrdinalIgnoreCase));
		}
		#endregion
		#region Modify
		#endregion
	}
}